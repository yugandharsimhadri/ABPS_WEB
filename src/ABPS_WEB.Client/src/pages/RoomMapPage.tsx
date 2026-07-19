import { useEffect, useMemo, useState } from 'react';
import { Link } from 'react-router-dom';
import { reportsApi } from '../api/endpoints';
import { ApiError } from '../api/http';
import type { RoomReportVM } from '../api/types';
import { Banner, Button, Card, PageHeader, RoomCategoryBadge } from '../components/ui';

type RoomState = 'available' | 'occupied' | 'expiring' | 'other';

/* tbl_RoomStatus descriptions vary by deployment ("Available", "Occupied", "Booked",
 * "Blocked", "Under Maintenance"…), so classify by keyword rather than exact match; anything
 * unrecognized lands in "other" (grey) instead of being mislabeled available. */
function classify(room: RoomReportVM, expiringBookingIds: Set<number>): RoomState {
  const s = (room.status ?? '').toLowerCase();
  if (s.includes('avail') || s.includes('vacant')) return 'available';
  if (s.includes('occup') || s.includes('book')) {
    return room.bookingId != null && expiringBookingIds.has(room.bookingId) ? 'expiring' : 'occupied';
  }
  return 'other';
}

const STATE_LABEL: Record<RoomState, string> = {
  available: 'Available',
  occupied: 'Occupied',
  expiring: 'Near checkout',
  other: 'Blocked / other',
};

export function RoomMapPage() {
  const [rooms, setRooms] = useState<RoomReportVM[]>([]);
  const [expiringIds, setExpiringIds] = useState<Set<number>>(new Set());
  const [selected, setSelected] = useState<RoomReportVM | null>(null);
  const [filter, setFilter] = useState('');
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    let cancelled = false;
    Promise.all([reportsApi.getRoomReport(0, 0, 0, -1), reportsApi.getExpiringBookingReport()])
      .then(([roomRows, expiring]) => {
        if (cancelled) return;
        setRooms(roomRows);
        setExpiringIds(new Set(expiring.map((e) => e.bookingId)));
      })
      .catch((err) => !cancelled && setError(err instanceof ApiError ? err.message : 'Failed to load the room map'))
      .finally(() => !cancelled && setLoading(false));
    return () => {
      cancelled = true;
    };
  }, []);

  const floors = useMemo(() => {
    const term = filter.trim().toLowerCase();
    const visible = term
      ? rooms.filter((r) => r.roomNo.toLowerCase().includes(term) || r.category.toLowerCase().includes(term))
      : rooms;
    const byFloor = new Map<string, RoomReportVM[]>();
    for (const room of visible) {
      const key = room.floor || 'Unassigned';
      const list = byFloor.get(key);
      if (list) list.push(room);
      else byFloor.set(key, [room]);
    }
    for (const list of byFloor.values()) {
      list.sort((a, b) => a.roomNo.localeCompare(b.roomNo, undefined, { numeric: true }));
    }
    return [...byFloor.entries()];
  }, [rooms, filter]);

  const totals = useMemo(() => {
    const t: Record<RoomState, number> = { available: 0, occupied: 0, expiring: 0, other: 0 };
    for (const room of rooms) t[classify(room, expiringIds)] += 1;
    return t;
  }, [rooms, expiringIds]);

  const selectedState = selected ? classify(selected, expiringIds) : null;

  return (
    <div>
      <PageHeader
        title="Room Map"
        subtitle="Live floor-wise view of every room — click a room for details and actions."
        actions={
          <input
            type="search"
            placeholder="Filter by room no or category…"
            value={filter}
            onChange={(e) => setFilter(e.target.value)}
            style={{ width: 230 }}
            aria-label="Filter rooms"
          />
        }
      />

      {error && <Banner tone="error">{error}</Banner>}

      <div className="room-map-legend" role="list" aria-label="Room status legend">
        {(Object.keys(STATE_LABEL) as RoomState[]).map((state) => (
          <span key={state} className="room-map-legend-item" role="listitem">
            <span className={`room-map-dot ${state}`} />
            {STATE_LABEL[state]}
            <strong>{loading ? '…' : totals[state]}</strong>
          </span>
        ))}
      </div>

      <div className="room-map-layout">
        <div className="room-map-floors">
          {loading && (
            <Card>
              <div className="skeleton-grid">
                {Array.from({ length: 24 }, (_, i) => (
                  <div key={i} className="skeleton room-map-skeleton-tile" />
                ))}
              </div>
            </Card>
          )}
          {!loading &&
            floors.map(([floorName, floorRooms]) => {
              const availableCount = floorRooms.filter((r) => classify(r, expiringIds) === 'available').length;
              return (
                <Card key={floorName} style={{ marginBottom: 16 }}>
                  <div className="room-map-floor-head">
                    <h3>{floorName}</h3>
                    <span className="room-map-floor-count">
                      {availableCount} of {floorRooms.length} available
                    </span>
                  </div>
                  <div className="room-map-grid">
                    {floorRooms.map((room) => {
                      const state = classify(room, expiringIds);
                      return (
                        <button
                          key={room.roomId}
                          type="button"
                          className={`room-map-tile ${state} ${selected?.roomId === room.roomId ? 'selected' : ''}`}
                          onClick={() => setSelected(room)}
                          title={`${room.roomNo} · ${room.category} · ${STATE_LABEL[state]}${room.customerName ? ` · ${room.customerName}` : ''}`}
                        >
                          <span className="room-no">{room.roomNo}</span>
                          <span className="room-cat">{room.category}</span>
                        </button>
                      );
                    })}
                  </div>
                </Card>
              );
            })}
          {!loading && !error && floors.length === 0 && (
            <Card>
              <div className="empty-state">No rooms match “{filter}”.</div>
            </Card>
          )}
        </div>

        <div className="room-map-detail">
          <Card>
            {!selected && (
              <div className="empty-state" style={{ padding: '30px 10px' }}>
                Select a room on the map to see its details.
              </div>
            )}
            {selected && selectedState && (
              <div>
                <div className="room-map-detail-head">
                  <div>
                    <div className="room-map-detail-no">{selected.roomNo}</div>
                    <RoomCategoryBadge category={selected.category} inline />
                  </div>
                  <span className={`room-map-state-chip ${selectedState}`}>{STATE_LABEL[selectedState]}</span>
                </div>
                <dl className="room-map-facts">
                  <div><dt>Floor</dt><dd>{selected.floor}</dd></div>
                  <div><dt>Beds</dt><dd>{selected.beds}</dd></div>
                  <div><dt>Allowed</dt><dd>{selected.allowed} persons</dd></div>
                  <div><dt>Price / day</dt><dd>₹{selected.price}</dd></div>
                  {selected.customerName && <div><dt>Guest</dt><dd>{selected.customerName}</dd></div>}
                  {selected.phoneNo && <div><dt>Phone</dt><dd>{selected.phoneNo}</dd></div>}
                  {selected.bookingId != null && <div><dt>Booking</dt><dd>#{selected.bookingId}</dd></div>}
                </dl>
                <div style={{ display: 'flex', flexDirection: 'column', gap: 8, marginTop: 16 }}>
                  {selectedState === 'available' && (
                    <Link to="/rooms/book">
                      <Button variant="primary" style={{ width: '100%' }}>Book a room</Button>
                    </Link>
                  )}
                  {(selectedState === 'occupied' || selectedState === 'expiring') && (
                    <>
                      <Link to="/rooms/vacate">
                        <Button variant="primary" style={{ width: '100%' }}>Vacate</Button>
                      </Link>
                      <Link to="/rooms/extend">
                        <Button variant="secondary" style={{ width: '100%' }}>Extend stay</Button>
                      </Link>
                    </>
                  )}
                </div>
              </div>
            )}
          </Card>
        </div>
      </div>
    </div>
  );
}
