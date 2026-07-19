import { useEffect, useState } from 'react';
import { adminApi } from '../../api/endpoints';
import { ApiError } from '../../api/http';
import type { UserVM } from '../../api/types';
import { useLookups } from '../../lookups/LookupsContext';
import { Banner, Button, Card, Field, PageHeader, Select, Table, TextInput } from '../../components/ui';

const emptyForm = () => ({ userName: '', password: '', roleId: 0, isActive: 'Y', email: '', phoneNo: '' });

export function UsersPage() {
  const { lookups } = useLookups();
  const [rows, setRows] = useState<UserVM[]>([]);
  const [editingId, setEditingId] = useState(0);
  const [form, setForm] = useState(emptyForm());
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  const [busy, setBusy] = useState(false);

  const load = () => adminApi.getUsers().then((users) => setRows(users.filter((u) => u.userId !== 0)));

  useEffect(() => {
    load();
  }, []);

  const startNew = () => {
    setEditingId(0);
    setForm(emptyForm());
    setError(null);
  };

  const startEdit = (row: UserVM) => {
    setEditingId(row.userId);
    setForm({ userName: row.userName ?? '', password: '', roleId: row.roleId, isActive: row.isActive ?? 'Y', email: row.email ?? '', phoneNo: row.mobileNumber ?? '' });
    setError(null);
  };

  const save = async () => {
    setError(null);
    setSuccess(null);
    if (!form.userName.trim()) return setError('User name is required.');
    if (!form.password) return setError('Password is required.');
    if (!form.roleId) return setError('Please select a role.');

    setBusy(true);
    try {
      if (editingId === 0) {
        const exists = await adminApi.userExists(form.userName.trim());
        if (exists) {
          setError('A user with this name already exists.');
          setBusy(false);
          return;
        }
        const res = await adminApi.addUser({ userName: form.userName.trim(), password: form.password, roleId: form.roleId, isActive: form.isActive, email: form.email.trim(), phoneNo: form.phoneNo.trim() });
        setSuccess(res.message);
      } else {
        const res = await adminApi.modifyUser(editingId, { userId: editingId, newPassword: form.password, newRoleId: form.roleId, isActive: form.isActive, email: form.email.trim(), phoneNo: form.phoneNo.trim() });
        setSuccess(res.message);
      }
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
      <PageHeader title="User Registration" subtitle="Application user accounts and roles." />
      {error && <Banner tone="error">{error}</Banner>}
      {success && <Banner tone="success">{success}</Banner>}

      <div style={{ display: 'grid', gridTemplateColumns: '1.3fr 1fr', gap: 20, alignItems: 'start' }}>
        <Card>
          <Table<UserVM>
            rows={rows}
            rowKey={(r) => r.userId}
            columns={[
              { header: 'User Name', render: (r) => r.userName ?? '—' },
              { header: 'Email', render: (r) => r.email ?? '—' },
              { header: 'Role', render: (r) => lookups?.userRoles.find((role) => role.id === r.roleId)?.name ?? r.roleId },
              { header: 'Active', render: (r) => (r.isActive === 'Y' ? 'Yes' : 'No') },
              { header: '', render: (r) => <Button size="sm" onClick={() => startEdit(r)}>Edit</Button> },
            ]}
          />
        </Card>

        <Card>
          <h3 style={{ marginBottom: 14 }}>{editingId === 0 ? 'New User' : `Editing #${editingId}`}</h3>
          <Field label="User Name">
            <TextInput value={form.userName} onChange={(e) => setForm({ ...form, userName: e.target.value })} disabled={editingId !== 0} />
          </Field>
          <Field label="Password" hint={editingId !== 0 ? 'Must re-enter to update this user (no “keep existing” option).' : undefined}>
            <input type="password" value={form.password} onChange={(e) => setForm({ ...form, password: e.target.value })} />
          </Field>
          <Field label="Role">
            <Select value={form.roleId} onChange={(e) => setForm({ ...form, roleId: Number(e.target.value) })}>
              <option value={0}>Select…</option>
              {(lookups?.userRoles ?? []).map((r) => (
                <option key={r.id} value={r.id}>{r.name}</option>
              ))}
            </Select>
          </Field>
          <Field label="Email">
            <TextInput value={form.email} onChange={(e) => setForm({ ...form, email: e.target.value })} />
          </Field>
          <Field label="Phone No">
            <TextInput value={form.phoneNo} onChange={(e) => setForm({ ...form, phoneNo: e.target.value })} />
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
