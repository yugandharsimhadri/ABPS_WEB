import { useEffect, useState } from 'react';
import { adminApi } from '../../api/endpoints';
import { ApiError } from '../../api/http';
import type { DonorTypeVM } from '../../api/types';
import { Banner, Button, Card, Field, PageHeader, Table, TextInput } from '../../components/ui';

const EMPTY = { donartype: '', code: '', amount: 0, days: 0 };

export function DonorTypesPage() {
  const [rows, setRows] = useState<DonorTypeVM[]>([]);
  const [editingId, setEditingId] = useState(0);
  const [form, setForm] = useState(EMPTY);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  const [busy, setBusy] = useState(false);

  const load = () => adminApi.getDonorTypes().then(setRows);

  useEffect(() => {
    load();
  }, []);

  const startNew = () => {
    setEditingId(0);
    setForm(EMPTY);
    setError(null);
  };

  const startEdit = (row: DonorTypeVM) => {
    setEditingId(row.dtid);
    setForm({ donartype: row.donartype ?? '', code: row.code ?? '', amount: row.amount ?? 0, days: row.days ?? 0 });
    setError(null);
  };

  const save = async () => {
    setError(null);
    setSuccess(null);
    if (form.donartype.trim().length < 3) return setError('Donation type name must be at least 3 characters.');
    if (!form.code.trim()) return setError('Enter a code.');

    setBusy(true);
    try {
      const res = editingId === 0
        ? await adminApi.saveDonorType(form)
        : await adminApi.modifyDonorType(editingId, { ...form, dtid: editingId });
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
      <PageHeader title="Donation Types" subtitle="Master list of donation categories, amounts, and day allowances." />
      {error && <Banner tone="error">{error}</Banner>}
      {success && <Banner tone="success">{success}</Banner>}

      <div style={{ display: 'grid', gridTemplateColumns: '1.3fr 1fr', gap: 20, alignItems: 'start' }}>
        <Card>
          <Table<DonorTypeVM>
            rows={rows}
            rowKey={(r) => r.dtid}
            columns={[
              { header: 'Type', render: (r) => r.donartype ?? '—' },
              { header: 'Code', render: (r) => r.code ?? '—' },
              { header: 'Amount', render: (r) => `₹${r.amount ?? 0}` },
              { header: 'Days', render: (r) => r.days ?? 0 },
              { header: '', render: (r) => <Button size="sm" onClick={() => startEdit(r)}>Edit</Button> },
            ]}
          />
        </Card>

        <Card>
          <h3 style={{ marginBottom: 14 }}>{editingId === 0 ? 'New Donation Type' : `Editing #${editingId}`}</h3>
          <Field label="Name">
            <TextInput value={form.donartype} onChange={(e) => setForm({ ...form, donartype: e.target.value })} />
          </Field>
          <Field label="Code">
            <TextInput value={form.code} onChange={(e) => setForm({ ...form, code: e.target.value })} />
          </Field>
          <Field label="Amount">
            <input type="number" value={form.amount} onChange={(e) => setForm({ ...form, amount: Number(e.target.value) })} />
          </Field>
          <Field label="Days">
            <input type="number" value={form.days} onChange={(e) => setForm({ ...form, days: Number(e.target.value) })} />
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
