import { useEffect, useState } from 'react';
import { adminApi } from '../../api/endpoints';
import { ApiError } from '../../api/http';
import type { MemberVM } from '../../api/types';
import { Banner, Button, Card, Field, PageHeader, Select, Table, TextInput } from '../../components/ui';

function today() {
  return new Date().toISOString().slice(0, 10);
}
function plusYear(dateStr: string) {
  const d = new Date(dateStr);
  d.setFullYear(d.getFullYear() + 1);
  return d.toISOString().slice(0, 10);
}

const emptyForm = () => ({
  memberName: '', memberTname: '', phone: '', email: '', address: '',
  memberType: '', isActive: 'Y', effectiveFrom: today(), effectiveTo: plusYear(today()),
});

export function MembersPage() {
  const [rows, setRows] = useState<MemberVM[]>([]);
  const [editingId, setEditingId] = useState(0);
  const [form, setForm] = useState(emptyForm());
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  const [busy, setBusy] = useState(false);

  const load = () => adminApi.getAllMembers().then(setRows);

  useEffect(() => {
    load();
  }, []);

  const startNew = () => {
    setEditingId(0);
    setForm(emptyForm());
    setError(null);
  };

  const startEdit = (row: MemberVM) => {
    setEditingId(row.memberId);
    setForm({
      memberName: row.memberName ?? '', memberTname: row.memberTname ?? '', phone: row.phone ?? '',
      email: row.email ?? '', address: row.address ?? '', memberType: row.memberType ?? '',
      isActive: row.isActive ?? 'Y', effectiveFrom: (row.effectiveFrom ?? today()).slice(0, 10),
      effectiveTo: (row.effectiveTo ?? plusYear(today())).slice(0, 10),
    });
    setError(null);
  };

  const save = async () => {
    setError(null);
    setSuccess(null);
    if (form.memberName.trim().length < 3) return setError('Member name must be at least 3 characters.');
    if (!form.memberType) return setError('Please select a member type.');
    if (!form.phone.trim()) return setError('Invalid phone number format.');
    if (!form.email.trim()) return setError('Invalid email format.');
    if (form.address.trim().length < 3) return setError('Address must be at least 3 characters.');
    if (new Date(form.effectiveFrom) >= new Date(form.effectiveTo)) return setError('Effective From must be earlier than Effective To.');

    const payload = { ...form, effectiveFrom: new Date(form.effectiveFrom).toISOString(), effectiveTo: new Date(form.effectiveTo).toISOString() };

    setBusy(true);
    try {
      const res = editingId === 0
        ? await adminApi.saveMember(payload)
        : await adminApi.modifyMember(editingId, { ...payload, memberId: editingId });
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
      <PageHeader title="Executive Members" subtitle="Board/executive member roster." />
      {error && <Banner tone="error">{error}</Banner>}
      {success && <Banner tone="success">{success}</Banner>}

      <div style={{ display: 'grid', gridTemplateColumns: '1.3fr 1fr', gap: 20, alignItems: 'start' }}>
        <Card>
          <Table<MemberVM>
            rows={rows}
            rowKey={(r) => r.memberId}
            columns={[
              { header: 'Name', render: (r) => r.memberName ?? '—' },
              { header: 'Type', render: (r) => r.memberType ?? '—' },
              { header: 'Phone', render: (r) => r.phone ?? '—' },
              { header: 'Active', render: (r) => (r.isActive === 'Y' ? 'Yes' : 'No') },
              { header: '', render: (r) => <Button size="sm" onClick={() => startEdit(r)}>Edit</Button> },
            ]}
          />
        </Card>

        <Card>
          <h3 style={{ marginBottom: 14 }}>{editingId === 0 ? 'New Member' : `Editing #${editingId}`}</h3>
          <Field label="Name">
            <TextInput value={form.memberName} onChange={(e) => setForm({ ...form, memberName: e.target.value })} />
          </Field>
          <Field label="Telugu Name">
            <TextInput value={form.memberTname} onChange={(e) => setForm({ ...form, memberTname: e.target.value })} />
          </Field>
          <Field label="Phone">
            <TextInput value={form.phone} onChange={(e) => setForm({ ...form, phone: e.target.value })} />
          </Field>
          <Field label="Email">
            <TextInput value={form.email} onChange={(e) => setForm({ ...form, email: e.target.value })} />
          </Field>
          <Field label="Address">
            <TextInput value={form.address} onChange={(e) => setForm({ ...form, address: e.target.value })} />
          </Field>
          <Field label="Member Type">
            <Select value={form.memberType} onChange={(e) => setForm({ ...form, memberType: e.target.value })}>
              <option value="">Select…</option>
              <option value="General">General</option>
              <option value="Executive">Executive</option>
            </Select>
          </Field>
          <Field label="Effective From">
            <input type="date" value={form.effectiveFrom} onChange={(e) => setForm({ ...form, effectiveFrom: e.target.value })} />
          </Field>
          <Field label="Effective To">
            <input type="date" value={form.effectiveTo} onChange={(e) => setForm({ ...form, effectiveTo: e.target.value })} />
          </Field>
          <label className="checkbox-row" style={{ marginBottom: 14 }}>
            <input type="checkbox" checked={form.isActive === 'Y'} onChange={(e) => setForm({ ...form, isActive: e.target.checked ? 'Y' : 'N' })} />
            Active
          </label>
          <div style={{ display: 'flex', gap: 8 }}>
            <Button variant="primary" busy={busy} onClick={save}>Save</Button>
            <Button variant="secondary" onClick={startNew}>New</Button>
          </div>
        </Card>
      </div>
    </div>
  );
}
