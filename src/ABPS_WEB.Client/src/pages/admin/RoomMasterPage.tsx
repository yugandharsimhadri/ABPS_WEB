import { useEffect, useState } from 'react';
import { reportsApi, roomsApi } from '../../api/endpoints';
import { ApiError } from '../../api/http';
import type { RoomReportVM } from '../../api/types';
import { useLookups } from '../../lookups/LookupsContext';
import { Banner, Button, Card, Field, PageHeader, RoomCategoryBadge, Select, Table, TextInput } from '../../components/ui';

const emptyForm = () => ({ roomNo: '', roomTypeId: 0, floorId: 0, roomStatusId: 0, bookingId: 0, price: 0, beds: 1, allowed: 1, advanceAmount: 0 });

export function RoomMasterPage() {
  const { lookups } = useLookups();
  const [rows, setRows] = useState<RoomReportVM[]>([]);
  const [editingId, setEditingId] = useState(0);
  const [form, setForm] = useState(emptyForm());
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  const [busy, setBusy] = useState(false);

  const load = () => reportsApi.getRoomReport(0, 0, 0, -1).then(setRows);

  useEffect(() => {
    load();
  }, []);

  const startNew = () => {
    setEditingId(0);
    setForm(emptyForm());
    setError(null);
  };

  const startEdit = (row: RoomReportVM) => {
    setEditingId(row.roomId);
    setForm({ roomNo: row.roomNo, roomTypeId: 0, floorId: 0, roomStatusId: 0, bookingId: 0, price: row.price, beds: row.beds, allowed: row.allowed, advanceAmount: 0 });
    setError(null);
  };

  const save = async () => {
    setError(null);
    setSuccess(null);
    if (!form.roomNo.trim()) return setError('Room number is required.');
    if (!form.roomTypeId) return setError('Select a room type.');
    if (!form.floorId) return setError('Select a floor.');
    if (!form.roomStatusId) return setError('Select a status.');

    setBusy(true);
    try {
      const res = editingId === 0
        ? await roomsApi.saveRoomDetails(form)
        : await roomsApi.modifyRoomDetails(editingId, { ...form, roomId: editingId });
      setSuccess(res.message);
      await load();
      startNew();
    } catch (err) {
      setError(err instanceof ApiError ? err.message : 'Save failed.');
    } finally {
      setBusy(false);
    }
  };

  return (
    <div>
      <PageHeader title="Room Master" subtitle="Physical room inventory — type, floor, capacity, and pricing." />
      {error && <Banner tone="error">{error}</Banner>}
      {success && <Banner tone="success">{success}</Banner>}

      <div style={{ display: 'grid', gridTemplateColumns: '1.3fr 1fr', gap: 20, alignItems: 'start' }}>
        <Card>
          <Table<RoomReportVM>
            rows={rows}
            rowKey={(r) => r.roomId}
            columns={[
              { header: 'Room No', render: (r) => r.roomNo },
              { header: 'Category', render: (r) => <RoomCategoryBadge category={r.category} inline /> },
              { header: 'Floor', render: (r) => r.floor },
              { header: 'Beds', render: (r) => r.beds },
              { header: 'Price', render: (r) => `₹${r.price}` },
              { header: 'Status', render: (r) => r.status },
              { header: '', render: (r) => <Button size="sm" onClick={() => startEdit(r)}>Edit</Button> },
            ]}
          />
        </Card>

        <Card>
          <h3 style={{ marginBottom: 4 }}>{editingId === 0 ? 'New Room' : `Editing Room #${editingId}`}</h3>
          {editingId !== 0 && (
            <p className="hint" style={{ marginBottom: 12 }}>
              Re-select type/floor/status below — the list view doesn't expose their numeric IDs, only descriptions.
            </p>
          )}
          <Field label="Room No">
            <TextInput value={form.roomNo} onChange={(e) => setForm({ ...form, roomNo: e.target.value })} />
          </Field>
          <Field label="Room Type">
            <Select value={form.roomTypeId} onChange={(e) => setForm({ ...form, roomTypeId: Number(e.target.value) })}>
              <option value={0}>Select…</option>
              {(lookups?.roomTypes ?? []).map((rt) => (
                <option key={rt.rtId} value={rt.rtId}>{rt.rtDesc} ({rt.roomCategory})</option>
              ))}
            </Select>
          </Field>
          <Field label="Floor">
            <Select value={form.floorId} onChange={(e) => setForm({ ...form, floorId: Number(e.target.value) })}>
              <option value={0}>Select…</option>
              {(lookups?.floors ?? []).map((f) => (
                <option key={f.floorId} value={f.floorId}>{f.floorDescription}</option>
              ))}
            </Select>
          </Field>
          <Field label="Status">
            <Select value={form.roomStatusId} onChange={(e) => setForm({ ...form, roomStatusId: Number(e.target.value) })}>
              <option value={0}>Select…</option>
              {(lookups?.roomStatuses ?? []).map((s) => (
                <option key={s.id} value={s.id}>{s.name}</option>
              ))}
            </Select>
          </Field>
          <Field label="Price / Day">
            <input type="number" value={form.price} onChange={(e) => setForm({ ...form, price: Number(e.target.value) })} />
          </Field>
          <Field label="Advance Amount">
            <input type="number" value={form.advanceAmount} onChange={(e) => setForm({ ...form, advanceAmount: Number(e.target.value) })} />
          </Field>
          <Field label="Beds">
            <input type="number" min={1} value={form.beds} onChange={(e) => setForm({ ...form, beds: Number(e.target.value) })} />
          </Field>
          <Field label="Allowed (persons)">
            <input type="number" min={1} value={form.allowed} onChange={(e) => setForm({ ...form, allowed: Number(e.target.value) })} />
          </Field>
          <div style={{ display: 'flex', gap: 8 }}>
            <Button variant="primary" busy={busy} onClick={save}>Save</Button>
            <Button variant="secondary" onClick={startNew}>New</Button>
          </div>
        </Card>
      </div>
    </div>
  );
}
