import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { reportsApi, roomsApi } from '../api/endpoints';
import type { ExpiringBookingReportVM, RoomCounts } from '../api/types';
import { Banner, Button, Card, StatTile, Table } from '../components/ui';
import { BedIcon, ClockIcon, DoorOutIcon, KeyIcon, UsersIcon } from '../components/icons';
import { ApiError } from '../api/http';
import heroBg from '../assets/srisailam-hero.jpg';

function formatDateTime(value: string | null) {
  if (!value) return '—';
  const d = new Date(value);
  return Number.isNaN(d.getTime()) ? '—' : d.toLocaleString();
}

/** SVG donut showing occupied share of total rooms, with the percentage in the middle.
 * Pure stroke-dasharray on a circle — no chart library. */
function OccupancyDonut({ occupied, total }: { occupied: number; total: number }) {
  const pct = total > 0 ? occupied / total : 0;
  const r = 52;
  const circumference = 2 * Math.PI * r;
  return (
    <div className="donut-wrap">
      <svg viewBox="0 0 140 140" width="150" height="150" role="img" aria-label={`${Math.round(pct * 100)}% occupied`}>
        <circle cx="70" cy="70" r={r} fill="none" stroke="var(--surface-alt)" strokeWidth="16" />
        <circle
          cx="70"
          cy="70"
          r={r}
          fill="none"
          stroke="var(--accent)"
          strokeWidth="16"
          strokeLinecap="round"
          strokeDasharray={`${circumference * pct} ${circumference}`}
          transform="rotate(-90 70 70)"
          className="donut-arc"
        />
        <text x="70" y="66" textAnchor="middle" className="donut-pct">{Math.round(pct * 100)}%</text>
        <text x="70" y="86" textAnchor="middle" className="donut-sub">occupied</text>
      </svg>
      <div className="donut-legend">
        <span><span className="room-map-dot occupied" /> Occupied <strong>{occupied}</strong></span>
        <span><span className="room-map-dot available" /> Available <strong>{total - occupied}</strong></span>
      </div>
    </div>
  );
}

export function DashboardPage() {
  const [roomCounts, setRoomCounts] = useState<RoomCounts | null>(null);
  const [dormCounts, setDormCounts] = useState<RoomCounts | null>(null);
  const [expiring, setExpiring] = useState<ExpiringBookingReportVM[]>([]);
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    let cancelled = false;
    setLoading(true);
    Promise.all([roomsApi.getCounts(), roomsApi.getDormLockerCounts(), reportsApi.getExpiringBookingReport()])
      .then(([rooms, dorm, exp]) => {
        if (cancelled) return;
        setRoomCounts(rooms);
        setDormCounts(dorm);
        setExpiring(exp);
      })
      .catch((err) => !cancelled && setError(err instanceof ApiError ? err.message : 'Failed to load dashboard data'))
      .finally(() => !cancelled && setLoading(false));
    return () => {
      cancelled = true;
    };
  }, []);

  return (
    <div>
      <div className="hero-band">
        <div className="hero-scene-layer photo" style={{ ['--hero-photo' as string]: `url(${heroBg})` }} />
        <div className="hero-scene-layer classic" />
        <div className="hero-text">
          <h1 className="title">Dashboard</h1>
          <p className="subtitle">Live room availability and bookings approaching checkout</p>
        </div>
        <div className="hero-actions">
          <Link to="/rooms/book">
            <Button variant="primary">+ Book Room</Button>
          </Link>
        </div>
      </div>

      {error && <Banner tone="error">{error}</Banner>}

      <div className="stat-row">
        <StatTile icon={<BedIcon />} value={loading ? '…' : roomCounts?.available ?? 0} label="Rooms Available" />
        <StatTile icon={<UsersIcon />} value={loading ? '…' : roomCounts?.occupied ?? 0} label="Rooms Occupied" />
        <StatTile icon={<KeyIcon />} value={loading ? '…' : dormCounts?.available ?? 0} label="Dorm/Locker Available" />
        <StatTile icon={<DoorOutIcon />} value={loading ? '…' : dormCounts?.occupied ?? 0} label="Dorm/Locker Occupied" />
      </div>

      <div className="dash-split">
        <Card>
          <h3 style={{ marginBottom: 6 }}>Room Occupancy</h3>
          <p className="hint" style={{ marginBottom: 10 }}>Across all physical rooms right now.</p>
          <OccupancyDonut
            occupied={roomCounts?.occupied ?? 0}
            total={(roomCounts?.occupied ?? 0) + (roomCounts?.available ?? 0)}
          />
          <Link to="/rooms/map" className="dash-map-link">
            View the full room map →
          </Link>
        </Card>
        <Card>
          <h3 style={{ marginBottom: 6 }}>
            <span style={{ display: 'inline-flex', alignItems: 'center', gap: 8 }}>
              <ClockIcon /> Bookings Near Checkout
            </span>
          </h3>
        <Table<ExpiringBookingReportVM>
          rows={expiring}
          rowKey={(r) => r.bookingId}
          emptyMessage={loading ? 'Loading…' : 'No bookings approaching checkout.'}
          columns={[
            { header: 'Booking', render: (r) => `#${r.bookingId}` },
            { header: 'Guest', render: (r) => r.name ?? '—' },
            { header: 'Phone', render: (r) => r.phoneNo ?? '—' },
            { header: 'Rooms', render: (r) => r.noOfRooms ?? '—' },
            { header: 'Check-in', render: (r) => formatDateTime(r.checkinDateTime) },
            { header: 'Expected Checkout', render: (r) => formatDateTime(r.expectedCheckoutDate) },
            { header: 'Days', render: (r) => r.noOfDays ?? '—' },
          ]}
        />
        </Card>
      </div>
    </div>
  );
}
