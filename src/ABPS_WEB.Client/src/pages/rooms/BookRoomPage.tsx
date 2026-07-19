import { useEffect, useMemo, useState } from 'react';
import { roomsApi } from '../../api/endpoints';
import { ApiError } from '../../api/http';
import type { BookingCategory, RoomVM } from '../../api/types';
import { useLookups } from '../../lookups/LookupsContext';
import { Banner, Button, Card, Field, PageHeader, RoomCategoryBadge, Select, TextInput } from '../../components/ui';
import { ReceiptOverlay, type ReceiptData } from '../../components/Receipt';

function nowLocalDateTime() {
  const d = new Date();
  d.setSeconds(0, 0);
  const tzOffset = d.getTimezoneOffset() * 60000;
  return new Date(d.getTime() - tzOffset).toISOString().slice(0, 16);
}

export function BookRoomPage() {
  const { lookups } = useLookups();

  const [categories, setCategories] = useState<BookingCategory[]>([]);
  const [catId, setCatId] = useState<number>(0);
  const [rooms, setRooms] = useState<RoomVM[]>([]);
  const [selectedRoomIds, setSelectedRoomIds] = useState<number[]>([]);
  const [multiMode, setMultiMode] = useState(false);

  const [name, setName] = useState('');
  const [aadhaar, setAadhaar] = useState('');
  const [phone, setPhone] = useState('');
  const [address, setAddress] = useState('');
  const [noOfPeople, setNoOfPeople] = useState(1);

  const [bondNo, setBondNo] = useState('');
  const [donarId, setDonarId] = useState(0);
  const [donarInfo, setDonarInfo] = useState<string | null>(null);

  const [paymentTypeId, setPaymentTypeId] = useState(0);
  const [paymentSubTypeId, setPaymentSubTypeId] = useState(0);
  const [advance, setAdvance] = useState(0);
  const [totalAmount, setTotalAmount] = useState(0);
  const [transactionNo, setTransactionNo] = useState('');
  const [checkinDate] = useState(nowLocalDateTime());

  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  const [busy, setBusy] = useState(false);
  const [loadingRooms, setLoadingRooms] = useState(true);
  const [receipt, setReceipt] = useState<ReceiptData | null>(null);
  const [showReceipt, setShowReceipt] = useState(false);

  const selectedCategory = categories.find((c) => c.catId === catId);
  const isDonarCategory = (selectedCategory?.category ?? '').toLowerCase().includes('donar');

  useEffect(() => {
    roomsApi.getCategories().then((cats) => {
      setCategories(cats);
      if (cats.length > 0) setCatId(cats[0].catId);
    });
    roomsApi
      .getAvailable()
      .then(setRooms)
      .catch((err) => setError(err instanceof ApiError ? err.message : 'Failed to load rooms'))
      .finally(() => setLoadingRooms(false));
  }, []);

  const selectedRooms = useMemo(() => rooms.filter((r) => selectedRoomIds.includes(r.roomId)), [rooms, selectedRoomIds]);
  const roomsByCategory = useMemo(() => {
    const groups = new Map<string, RoomVM[]>();
    for (const r of rooms) {
      const list = groups.get(r.roomCategory) ?? [];
      list.push(r);
      groups.set(r.roomCategory, list);
    }
    return groups;
  }, [rooms]);

  useEffect(() => {
    const sumPrice = selectedRooms.reduce((acc, r) => acc + (r.rtPrice ?? 0), 0);
    const sumAdvance = selectedRooms.reduce((acc, r) => acc + (r.roomAdvance ?? 0), 0);
    setTotalAmount(sumPrice);
    setAdvance(sumAdvance);
  }, [selectedRooms]);

  const toggleRoom = (room: RoomVM) => {
    setSelectedRoomIds((prev) => {
      if (prev.includes(room.roomId)) return prev.filter((id) => id !== room.roomId);
      if (!multiMode) return [room.roomId];
      return [...prev, room.roomId];
    });
  };

  const findByAadhaar = async () => {
    if (!aadhaar.trim()) return;
    try {
      const result = await roomsApi.getCustomerByAadhar(aadhaar.trim());
      setName(result.name ?? '');
      setPhone(result.phoneNo ?? '');
      setAddress(result.address ?? '');
      setSuccess('Found a previous guest record — fields pre-filled.');
      setError(null);
    } catch (err) {
      setError(err instanceof ApiError && err.status === 404 ? 'No previous booking found for this Aadhaar number.' : 'Aadhaar lookup failed.');
    }
  };

  const findByBondNo = async () => {
    if (!bondNo.trim()) return;
    try {
      const donor = await roomsApi.getDonarByBondNo(bondNo.trim());
      setDonarId(donor.donarId);
      setName(donor.name ?? '');
      setPhone(donor.phoneNo ?? '');
      setAddress(donor.address ?? '');
      setDonarInfo(
        `${donor.donationType ?? 'Donor'} — ${donor.roomDays} day(s) remaining this year (${donor.usedDays} used).`,
      );
      setError(null);
    } catch (err) {
      setDonarInfo(null);
      setError(err instanceof ApiError && err.status === 404 ? 'Bond number not found.' : 'Bond lookup failed.');
    }
  };

  const subTypes = (lookups?.paymentSubTypes ?? []).filter((p) => p.paymentTypeId === paymentTypeId);

  const handleSubmit = async () => {
    setError(null);
    setSuccess(null);
    if (selectedRooms.length === 0) return setError('Select at least one room.');
    if (!name.trim() || !phone.trim() || !aadhaar.trim()) return setError('Guest name, phone, and Aadhaar are required.');
    if (isDonarCategory && !bondNo.trim()) return setError('Bond number is required for Donar bookings.');

    setBusy(true);
    try {
      const result = await roomsApi.book({
        name: name.trim(),
        address: address.trim() || null,
        aadhaarNo: aadhaar.trim(),
        phoneNo: phone.trim(),
        noOfPeople,
        catID: catId,
        bookCategory: selectedCategory?.category ?? '',
        details: null,
        dtid: 0,
        donarID: donarId,
        bondNo: isDonarCategory ? bondNo.trim() : null,
        noOfRooms: selectedRooms.length,
        advance,
        totalAmount,
        groupBooking: selectedRooms.length > 1 ? 'Y' : 'N',
        advanceBooking: 'N',
        noOfDays: 1,
        totalAllowed: selectedRooms.reduce((acc, r) => acc + r.allowed, 0),
        checkinDate: new Date(checkinDate).toISOString(),
        paymentTypeID: paymentTypeId,
        paymentSubTypeID: paymentSubTypeId,
        trasactionNo: transactionNo.trim() || null,
        rooms: selectedRooms,
      });
      setSuccess(`${result.message} — Booking #${result.bookingId}`);
      setReceipt({
        title: 'Room Booking Receipt',
        receiptNo: `Booking #${result.bookingId}`,
        date: new Date(),
        lines: [
          { label: 'Guest Name', value: name.trim() },
          { label: 'Phone', value: phone.trim() },
          { label: 'Rooms', value: selectedRooms.map((r) => r.roomNo).join(', ') },
          { label: 'No. of Persons', value: String(noOfPeople) },
          { label: 'Check-in', value: new Date(checkinDate).toLocaleString('en-IN', { dateStyle: 'medium', timeStyle: 'short' }) },
          { label: 'Total Amount', value: `₹${totalAmount.toLocaleString('en-IN')}` },
        ],
        amount: advance,
        amountLabel: 'Advance Received',
      });
      setSelectedRoomIds([]);
      setName('');
      setAadhaar('');
      setPhone('');
      setAddress('');
      setBondNo('');
      setDonarId(0);
      setDonarInfo(null);
      setTransactionNo('');
      const refreshed = await roomsApi.getAvailable();
      setRooms(refreshed);
    } catch (err) {
      setError(err instanceof ApiError ? err.message : 'Booking failed.');
    } finally {
      setBusy(false);
    }
  };

  return (
    <div>
      <PageHeader title="Book Room" subtitle="Select rooms, capture guest details, and confirm advance payment." />

      {error && <Banner tone="error">{error}</Banner>}
      {success && (
        <Banner tone="success">
          {success}
          {receipt && (
            <Button size="sm" variant="secondary" style={{ marginLeft: 12 }} onClick={() => setShowReceipt(true)}>
              Print Receipt
            </Button>
          )}
        </Banner>
      )}
      {showReceipt && receipt && <ReceiptOverlay data={receipt} onClose={() => setShowReceipt(false)} />}

      <div style={{ display: 'grid', gridTemplateColumns: '1.4fr 1fr', gap: 20, alignItems: 'start' }}>
        <Card>
          <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 12 }}>
            <div style={{ display: 'flex', gap: 10, alignItems: 'center' }}>
              <label htmlFor="book-room-category" style={{ fontSize: 12.5, fontWeight: 600, color: 'var(--text-muted)' }}>Category</label>
              <Select id="book-room-category" value={catId} onChange={(e) => setCatId(Number(e.target.value))}>
                {categories.map((c) => (
                  <option key={c.catId} value={c.catId}>
                    {c.category}
                  </option>
                ))}
              </Select>
            </div>
            <label className="checkbox-row">
              <input type="checkbox" checked={multiMode} onChange={(e) => setMultiMode(e.target.checked)} />
              Select multiple rooms
            </label>
          </div>

          {loadingRooms ? (
            <p style={{ color: 'var(--text-muted)' }}>Loading available rooms…</p>
          ) : rooms.length === 0 ? (
            <p style={{ color: 'var(--text-muted)' }}>No rooms currently available.</p>
          ) : (
            [...roomsByCategory.entries()].map(([category, list]) => (
              <div key={category} style={{ marginBottom: 16 }}>
                <h4 style={{ fontSize: 12.5, color: 'var(--text-muted)', marginBottom: 8, textTransform: 'uppercase' }}>{category}</h4>
                <div className="tile-grid">
                  {list.map((room) => (
                    <button
                      type="button"
                      key={room.roomId}
                      className={`room-tile ${selectedRoomIds.includes(room.roomId) ? 'selected' : ''}`}
                      onClick={() => toggleRoom(room)}
                    >
                      <RoomCategoryBadge category={room.roomCategory} />
                      <div className="room-no">{room.roomNo}</div>
                      <div className="room-meta">
                        {room.floorDescription ?? 'Floor —'} · {room.beds} bed(s) · up to {room.allowed}
                      </div>
                      <div className="room-price">₹{room.rtPrice ?? 0}/day</div>
                    </button>
                  ))}
                </div>
              </div>
            ))
          )}
        </Card>

        <Card>
          <h3 style={{ marginBottom: 14 }}>Guest &amp; Payment</h3>

          {isDonarCategory && (
            <Field label="Bond No" hint="Required for Donar-category bookings">
              <div style={{ display: 'flex', gap: 8 }}>
                <TextInput value={bondNo} onChange={(e) => setBondNo(e.target.value)} />
                <Button type="button" size="sm" onClick={findByBondNo}>
                  Find
                </Button>
              </div>
              {donarInfo && <span className="hint">{donarInfo}</span>}
            </Field>
          )}

          <Field label="Aadhaar No">
            <div style={{ display: 'flex', gap: 8 }}>
              <TextInput value={aadhaar} onChange={(e) => setAadhaar(e.target.value)} />
              <Button type="button" size="sm" onClick={findByAadhaar}>
                Find
              </Button>
            </div>
          </Field>
          <Field label="Guest Name">
            <TextInput value={name} onChange={(e) => setName(e.target.value)} />
          </Field>
          <Field label="Phone No">
            <TextInput value={phone} onChange={(e) => setPhone(e.target.value)} />
          </Field>
          <Field label="Address">
            <TextInput value={address} onChange={(e) => setAddress(e.target.value)} />
          </Field>
          <Field label="No. of People">
            <input type="number" min={1} value={noOfPeople} onChange={(e) => setNoOfPeople(Number(e.target.value))} />
          </Field>
          <Field label="Check-in">
            <TextInput value={new Date(checkinDate).toLocaleString()} disabled />
          </Field>

          <div className="divider" />

          <Field label="Payment Type">
            <Select
              value={paymentTypeId}
              onChange={(e) => {
                setPaymentTypeId(Number(e.target.value));
                setPaymentSubTypeId(0);
              }}
            >
              <option value={0}>Select…</option>
              {(lookups?.paymentTypes ?? []).map((p) => (
                <option key={p.id} value={p.id}>
                  {p.name}
                </option>
              ))}
            </Select>
          </Field>
          {subTypes.length > 0 && (
            <Field label="Payment Sub Type">
              <Select value={paymentSubTypeId} onChange={(e) => setPaymentSubTypeId(Number(e.target.value))}>
                <option value={0}>Select…</option>
                {subTypes.map((p) => (
                  <option key={p.paymentSubTypeId} value={p.paymentSubTypeId}>
                    {p.paymentSubType}
                  </option>
                ))}
              </Select>
            </Field>
          )}
          <Field label="Transaction No" hint="Reference number for online/card payments">
            <TextInput value={transactionNo} onChange={(e) => setTransactionNo(e.target.value)} />
          </Field>
          <Field label="Total Amount">
            <input type="number" value={totalAmount} onChange={(e) => setTotalAmount(Number(e.target.value))} />
          </Field>
          <Field label="Advance">
            <input type="number" value={advance} onChange={(e) => setAdvance(Number(e.target.value))} />
          </Field>

          <Button variant="primary" busy={busy} onClick={handleSubmit} style={{ width: '100%', marginTop: 6 }}>
            Confirm Booking ({selectedRooms.length} room{selectedRooms.length === 1 ? '' : 's'})
          </Button>
        </Card>
      </div>
    </div>
  );
}
