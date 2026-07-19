import { useEffect, useState } from 'react';
import { roomsApi } from '../../api/endpoints';
import { ApiError } from '../../api/http';
import type { BookingRoomLine, BookingSummary, RoomVM } from '../../api/types';
import { useLookups } from '../../lookups/LookupsContext';
import { Banner, Button, Card, Field, PageHeader, Select, TextInput } from '../../components/ui';

export type BookingActionType = 'vacate' | 'extend' | 'shift' | 'cancel' | 'partial-checkout' | 'add-room';

const TITLES: Record<BookingActionType, { title: string; subtitle: string }> = {
  vacate: { title: 'Vacate Room', subtitle: 'Settle final amount and release the room(s).' },
  extend: { title: 'Extend Room', subtitle: 'Extend a stay and collect additional advance.' },
  shift: { title: 'Shift Room', subtitle: 'Move a guest from one room to another.' },
  cancel: { title: 'Cancel Room', subtitle: 'Cancel one or more rooms on a booking.' },
  'partial-checkout': { title: 'Partial Checkout', subtitle: 'Check out a subset of rooms on a multi-room booking.' },
  'add-room': { title: 'Add Room', subtitle: 'Add an additional room to an existing booking.' },
};

function nowLocalDateTime() {
  const d = new Date();
  d.setSeconds(0, 0);
  const tzOffset = d.getTimezoneOffset() * 60000;
  return new Date(d.getTime() - tzOffset).toISOString().slice(0, 16);
}

export function BookingActionPage({ action }: { action: BookingActionType }) {
  const { lookups } = useLookups();
  const { title, subtitle } = TITLES[action];

  const [searchTerm, setSearchTerm] = useState('');
  const [bookingId, setBookingId] = useState<number | null>(null);
  const [summary, setSummary] = useState<BookingSummary | null>(null);
  const [bookingRooms, setBookingRooms] = useState<BookingRoomLine[]>([]);
  const [availableRooms, setAvailableRooms] = useState<RoomVM[]>([]);
  const [selectedRoomIds, setSelectedRoomIds] = useState<number[]>([]);

  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  const [busy, setBusy] = useState(false);
  const [searching, setSearching] = useState(false);

  const [checkoutDateTime, setCheckoutDateTime] = useState(nowLocalDateTime());
  const [paymentTypeId, setPaymentTypeId] = useState(0);
  const [paymentSubTypeId, setPaymentSubTypeId] = useState(0);
  const [transactionNo, setTransactionNo] = useState('');

  // Vacate-specific
  const [adjustedAmount, setAdjustedAmount] = useState(0);
  const [payAmount, setPayAmount] = useState(0);
  const [noOfDays, setNoOfDays] = useState(1);
  const [roomDonation, setRoomDonation] = useState(0);
  const [otherDonation, setOtherDonation] = useState(0);

  // Extend-specific
  const [extendDays, setExtendDays] = useState(1);
  const [updateTotalAmount, setUpdateTotalAmount] = useState(0);
  const [addAdvance, setAddAdvance] = useState(0);
  const [newCheckout, setNewCheckout] = useState(nowLocalDateTime());

  // Shift-specific
  const [fromRoomId, setFromRoomId] = useState(0);
  const [toRoomId, setToRoomId] = useState(0);

  // Cancel-specific
  const [fullCancellation, setFullCancellation] = useState(false);
  const [refund, setRefund] = useState(0);

  const refundAmount = Math.max((summary?.advance ?? 0) - adjustedAmount, 0);
  const payableAmount = Math.max(adjustedAmount - (summary?.advance ?? 0), 0);

  useEffect(() => {
    if (action === 'shift' || action === 'add-room') {
      roomsApi.getAvailable().then(setAvailableRooms);
    }
  }, [action]);

  const reset = () => {
    setBookingId(null);
    setSummary(null);
    setBookingRooms([]);
    setSelectedRoomIds([]);
    setSearchTerm('');
  };

  const search = async () => {
    setError(null);
    setSuccess(null);
    if (!searchTerm.trim()) return;
    setSearching(true);
    try {
      let id = await roomsApi.getBookingIdByRoomNo(searchTerm.trim());
      if (id === 0) {
        const parsed = Number(searchTerm.trim());
        if (Number.isInteger(parsed) && (await roomsApi.isBookingActive(parsed))) id = parsed;
      }
      if (id === 0 || !(await roomsApi.isBookingActive(id))) {
        setError('No active booking found for that room/booking ID.');
        return;
      }
      const [details, rooms] = await Promise.all([roomsApi.getBookingDetails(id), roomsApi.getBookingRooms(id)]);
      setBookingId(id);
      setSummary(details);
      setBookingRooms(rooms);
      setSelectedRoomIds(rooms.map((r) => r.roomId));
      setAdjustedAmount(details?.totalAmount ?? 0);
      setUpdateTotalAmount(details?.totalAmount ?? 0);
      setFromRoomId(rooms[0]?.roomId ?? 0);
    } catch (err) {
      setError(err instanceof ApiError ? err.message : 'Search failed.');
    } finally {
      setSearching(false);
    }
  };

  const toggleRoom = (roomId: number) => {
    setSelectedRoomIds((prev) => (prev.includes(roomId) ? prev.filter((id) => id !== roomId) : [...prev, roomId]));
  };

  const subTypes = (lookups?.paymentSubTypes ?? []).filter((p) => p.paymentTypeId === paymentTypeId);

  const submit = async () => {
    if (!bookingId) return;
    setError(null);
    setSuccess(null);
    setBusy(true);
    try {
      let message = '';
      const checkoutIso = new Date(checkoutDateTime).toISOString();

      if (action === 'vacate') {
        const res = await roomsApi.vacate(bookingId, {
          bookingId, checkoutDateTime: checkoutIso, adjustedAmount, refundAmount,
          paymentType: (lookups?.paymentTypes ?? []).find((p) => p.id === paymentTypeId)?.name ?? 'Cash',
          paymentSubTypeId, transactionNo: transactionNo.trim() || null, payAmount: payableAmount,
          noOfDays, roomDonation, otherDonation,
        });
        message = res.message;
      } else if (action === 'extend') {
        const res = await roomsApi.extend(bookingId, {
          bookingId, noOfDays, extendDays, totalExtendDays: noOfDays + extendDays, updateTotalAmount,
          totalAdvance: (summary?.advance ?? 0) + addAdvance, addAdvance,
          newCheckout: new Date(newCheckout).toISOString(), paymentTypeId, paymentSubTypeId,
          transactionNo: transactionNo.trim() || null, oldCheckout: summary?.checkinDateTime ?? new Date().toISOString(),
        });
        message = res.message;
      } else if (action === 'shift') {
        const res = await roomsApi.shift(bookingId, {
          fromRoomId, toRoomId, addAdvance, totalAdvance: (summary?.advance ?? 0) + addAdvance,
          totalAmount: updateTotalAmount, bookingId, paymentTypeId, paymentSubTypeId,
          transactionNo: transactionNo.trim() || null, checkout: checkoutIso,
        });
        message = res.message;
      } else if (action === 'cancel') {
        if (fullCancellation) {
          const res = await roomsApi.cancelComplete(bookingId, {
            bookingId, selectedRoomIds, refund, payment: payAmount, checkout: checkoutIso,
            paymentTypeId, paymentSubTypeId, transactionNo: transactionNo.trim() || null,
          });
          message = res.message;
        } else {
          const res = await roomsApi.cancel(bookingId, { bookingId, selectedRoomIds, checkout: checkoutIso });
          message = res.message;
        }
      } else if (action === 'partial-checkout') {
        const res = await roomsApi.partialCheckout(bookingId, { bookingId, selectedRoomIds, checkout: checkoutIso });
        message = res.message;
      } else if (action === 'add-room') {
        const res = await roomsApi.addRoom(bookingId, {
          bookingId, updateTotalAmount, totalAdvance: (summary?.advance ?? 0) + addAdvance, addAdvance,
          bookingDate: new Date().toISOString(), paymentTypeId, paymentSubTypeId,
          transactionNo: transactionNo.trim() || null, selectedRoomIds,
        });
        message = res.message;
      }

      setSuccess(message || 'Success');
      reset();
    } catch (err) {
      setError(err instanceof ApiError ? err.message : 'Operation failed.');
    } finally {
      setBusy(false);
    }
  };

  return (
    <div>
      <PageHeader title={title} subtitle={subtitle} />
      {error && <Banner tone="error">{error}</Banner>}
      {success && <Banner tone="success">{success}</Banner>}

      <Card style={{ marginBottom: 20 }}>
        <div style={{ display: 'flex', gap: 8, alignItems: 'flex-end' }}>
          <Field label="Room No. or Booking ID">
            <TextInput
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              onKeyDown={(e) => e.key === 'Enter' && search()}
              disabled={!!bookingId}
            />
          </Field>
          {!bookingId ? (
            <Button busy={searching} onClick={search} style={{ marginBottom: 14 }}>
              Search
            </Button>
          ) : (
            <Button variant="secondary" onClick={reset} style={{ marginBottom: 14 }}>
              Change
            </Button>
          )}
        </div>
        {summary && (
          <p style={{ color: 'var(--text-muted)', fontSize: 13 }}>
            <strong style={{ color: 'var(--text)' }}>{summary.name}</strong> ({summary.phoneNo}) — Booking #{summary.bookingId} ·
            Advance ₹{summary.advance ?? 0} · Total ₹{summary.totalAmount ?? 0}
          </p>
        )}
      </Card>

      {bookingId && summary && (
        <Card>
          {(action === 'cancel' || action === 'partial-checkout' || action === 'shift') && bookingRooms.length > 0 && (
            <Field label={action === 'shift' ? 'From Room' : 'Rooms'}>
              {action === 'shift' ? (
                <Select value={fromRoomId} onChange={(e) => setFromRoomId(Number(e.target.value))}>
                  {bookingRooms.map((r) => (
                    <option key={r.roomId} value={r.roomId}>
                      {r.roomNo} (₹{r.roomAmount}/day)
                    </option>
                  ))}
                </Select>
              ) : (
                <div style={{ display: 'flex', flexWrap: 'wrap', gap: 10 }}>
                  {bookingRooms.map((r) => (
                    <label key={r.roomId} className="checkbox-row">
                      <input type="checkbox" checked={selectedRoomIds.includes(r.roomId)} onChange={() => toggleRoom(r.roomId)} />
                      {r.roomNo}
                    </label>
                  ))}
                </div>
              )}
            </Field>
          )}

          {action === 'shift' && (
            <Field label="To Room">
              <Select value={toRoomId} onChange={(e) => setToRoomId(Number(e.target.value))}>
                <option value={0}>Select…</option>
                {availableRooms.map((r) => (
                  <option key={r.roomId} value={r.roomId}>
                    {r.roomNo} (₹{r.rtPrice ?? 0}/day)
                  </option>
                ))}
              </Select>
            </Field>
          )}

          {action === 'add-room' && (
            <Field label="Rooms to Add">
              <div style={{ display: 'flex', flexWrap: 'wrap', gap: 10 }}>
                {availableRooms.map((r) => (
                  <label key={r.roomId} className="checkbox-row">
                    <input type="checkbox" checked={selectedRoomIds.includes(r.roomId)} onChange={() => toggleRoom(r.roomId)} />
                    {r.roomNo} (₹{r.rtPrice ?? 0}/day)
                  </label>
                ))}
              </div>
            </Field>
          )}

          {(action === 'vacate' || action === 'shift' || action === 'cancel' || action === 'partial-checkout') && (
            <Field label={action === 'vacate' ? 'Checkout Date/Time' : 'Date/Time'}>
              <input type="datetime-local" value={checkoutDateTime} onChange={(e) => setCheckoutDateTime(e.target.value)} />
            </Field>
          )}

          {action === 'vacate' && (
            <>
              <Field label="No. of Days">
                <input type="number" min={1} value={noOfDays} onChange={(e) => setNoOfDays(Number(e.target.value))} />
              </Field>
              <Field label="Adjusted Total Amount">
                <input type="number" value={adjustedAmount} onChange={(e) => setAdjustedAmount(Number(e.target.value))} />
              </Field>
              <p style={{ fontSize: 12.5, color: 'var(--text-muted)', margin: '-6px 0 12px' }}>
                {refundAmount > 0 ? `Refund due: ₹${refundAmount.toFixed(2)}` : payableAmount > 0 ? `Additional payable: ₹${payableAmount.toFixed(2)}` : 'Settled — no balance.'}
              </p>
              <Field label="Room Donation">
                <input type="number" value={roomDonation} onChange={(e) => setRoomDonation(Number(e.target.value))} />
              </Field>
              <Field label="Other Donation">
                <input type="number" value={otherDonation} onChange={(e) => setOtherDonation(Number(e.target.value))} />
              </Field>
            </>
          )}

          {action === 'extend' && (
            <>
              <Field label="Current No. of Days">
                <input type="number" min={0} value={noOfDays} onChange={(e) => setNoOfDays(Number(e.target.value))} />
              </Field>
              <Field label="Extend By (days)">
                <input type="number" min={1} value={extendDays} onChange={(e) => setExtendDays(Number(e.target.value))} />
              </Field>
              <Field label="New Checkout Date/Time">
                <input type="datetime-local" value={newCheckout} onChange={(e) => setNewCheckout(e.target.value)} />
              </Field>
              <Field label="Updated Total Amount">
                <input type="number" value={updateTotalAmount} onChange={(e) => setUpdateTotalAmount(Number(e.target.value))} />
              </Field>
              <Field label="Additional Advance">
                <input type="number" value={addAdvance} onChange={(e) => setAddAdvance(Number(e.target.value))} />
              </Field>
            </>
          )}

          {action === 'shift' && (
            <>
              <Field label="Updated Total Amount">
                <input type="number" value={updateTotalAmount} onChange={(e) => setUpdateTotalAmount(Number(e.target.value))} />
              </Field>
              <Field label="Additional Advance">
                <input type="number" value={addAdvance} onChange={(e) => setAddAdvance(Number(e.target.value))} />
              </Field>
            </>
          )}

          {action === 'add-room' && (
            <>
              <Field label="Updated Total Amount">
                <input type="number" value={updateTotalAmount} onChange={(e) => setUpdateTotalAmount(Number(e.target.value))} />
              </Field>
              <Field label="Additional Advance">
                <input type="number" value={addAdvance} onChange={(e) => setAddAdvance(Number(e.target.value))} />
              </Field>
            </>
          )}

          {action === 'cancel' && (
            <>
              <label className="checkbox-row" style={{ marginBottom: 14 }}>
                <input type="checkbox" checked={fullCancellation} onChange={(e) => setFullCancellation(e.target.checked)} />
                Full cancellation with settlement (refund/payment)
              </label>
              {fullCancellation && (
                <>
                  <Field label="Refund Amount">
                    <input type="number" value={refund} onChange={(e) => setRefund(Number(e.target.value))} />
                  </Field>
                  <Field label="Additional Payment">
                    <input type="number" value={payAmount} onChange={(e) => setPayAmount(Number(e.target.value))} />
                  </Field>
                </>
              )}
            </>
          )}

          {(action === 'vacate' || action === 'extend' || action === 'shift' || action === 'add-room' || (action === 'cancel' && fullCancellation)) && (
            <>
              <Field label="Payment Type">
                <Select value={paymentTypeId} onChange={(e) => { setPaymentTypeId(Number(e.target.value)); setPaymentSubTypeId(0); }}>
                  <option value={0}>Select…</option>
                  {(lookups?.paymentTypes ?? []).map((p) => (
                    <option key={p.id} value={p.id}>{p.name}</option>
                  ))}
                </Select>
              </Field>
              {subTypes.length > 0 && (
                <Field label="Payment Sub Type">
                  <Select value={paymentSubTypeId} onChange={(e) => setPaymentSubTypeId(Number(e.target.value))}>
                    <option value={0}>Select…</option>
                    {subTypes.map((p) => (
                      <option key={p.paymentSubTypeId} value={p.paymentSubTypeId}>{p.paymentSubType}</option>
                    ))}
                  </Select>
                </Field>
              )}
              <Field label="Transaction No">
                <TextInput value={transactionNo} onChange={(e) => setTransactionNo(e.target.value)} />
              </Field>
            </>
          )}

          <Button variant="primary" busy={busy} onClick={submit} style={{ width: '100%', marginTop: 6 }}>
            Confirm {title}
          </Button>
        </Card>
      )}
    </div>
  );
}
