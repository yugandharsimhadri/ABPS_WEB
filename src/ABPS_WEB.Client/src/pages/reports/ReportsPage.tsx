import { useState } from 'react';
import { reportsApi } from '../../api/endpoints';
import { ApiError } from '../../api/http';
import { useLookups } from '../../lookups/LookupsContext';
import { Banner, Button, Card, Field, PageHeader, Select, Tabs } from '../../components/ui';

type ReportId = 'donations' | 'occupancy' | 'expiring' | 'transactions' | 'audit';

const REPORTS: { id: ReportId; label: string; needsRange: boolean; needsCategory?: boolean }[] = [
  { id: 'donations', label: 'Donation Report', needsRange: true },
  { id: 'transactions', label: 'Transaction Report', needsRange: true, needsCategory: true },
  { id: 'audit', label: 'Audit Transaction Report', needsRange: true },
  { id: 'occupancy', label: 'Occupancy Report', needsRange: false },
  { id: 'expiring', label: 'Expiring Bookings', needsRange: false },
];

function toIso(dateStr: string) {
  return new Date(dateStr).toISOString();
}

function formatCell(value: unknown): string {
  if (value === null || value === undefined) return '—';
  if (typeof value === 'boolean') return value ? 'Yes' : 'No';
  if (typeof value === 'string' && /^\d{4}-\d{2}-\d{2}T/.test(value)) {
    const d = new Date(value);
    return Number.isNaN(d.getTime()) ? value : d.toLocaleString();
  }
  return String(value);
}

function toTitleCase(key: string) {
  return key.replace(/([A-Z])/g, ' $1').replace(/^./, (c) => c.toUpperCase()).trim();
}

export function ReportsPage() {
  const { lookups } = useLookups();
  const [active, setActive] = useState<ReportId>('donations');
  const [fromDate, setFromDate] = useState(new Date(new Date().setMonth(new Date().getMonth() - 1)).toISOString().slice(0, 10));
  const [toDate, setToDate] = useState(new Date().toISOString().slice(0, 10));
  const [categoryId, setCategoryId] = useState(0);
  const [typeId, setTypeId] = useState(0);

  const [rows, setRows] = useState<Record<string, unknown>[]>([]);
  const [busy, setBusy] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [ran, setRan] = useState(false);

  const meta = REPORTS.find((r) => r.id === active)!;

  const run = async () => {
    setError(null);
    setBusy(true);
    setRan(true);
    try {
      let data: Record<string, unknown>[] = [];
      switch (active) {
        case 'donations':
          data = (await reportsApi.getDonationReport(toIso(fromDate), toIso(toDate))) as Record<string, unknown>[];
          break;
        case 'transactions':
          data = (await reportsApi.getTransactionReport(toIso(fromDate), toIso(toDate), categoryId, typeId)) as Record<string, unknown>[];
          break;
        case 'audit':
          data = (await reportsApi.getAuditTransactionReport(toIso(fromDate), toIso(toDate))) as Record<string, unknown>[];
          break;
        case 'occupancy':
          data = (await reportsApi.getOccupancyReport()) as Record<string, unknown>[];
          break;
        case 'expiring':
          data = (await reportsApi.getExpiringBookingReport()) as unknown as Record<string, unknown>[];
          break;
      }
      setRows(data);
    } catch (err) {
      setError(err instanceof ApiError ? err.message : 'Report failed to load.');
    } finally {
      setBusy(false);
    }
  };

  const columns = rows.length > 0 ? Object.keys(rows[0]) : [];

  return (
    <div>
      <PageHeader title="Reports" subtitle="Run any report and review results in a sortable grid." />
      <Tabs tabs={REPORTS.map((r) => ({ id: r.id, label: r.label }))} active={active} onChange={(id) => { setActive(id as ReportId); setRan(false); setRows([]); }} />

      {error && <Banner tone="error">{error}</Banner>}

      <Card style={{ marginBottom: 20 }}>
        <div style={{ display: 'flex', gap: 16, alignItems: 'flex-end', flexWrap: 'wrap' }}>
          {meta.needsRange && (
            <>
              <Field label="From">
                <input type="date" value={fromDate} onChange={(e) => setFromDate(e.target.value)} />
              </Field>
              <Field label="To">
                <input type="date" value={toDate} onChange={(e) => setToDate(e.target.value)} />
              </Field>
            </>
          )}
          {meta.needsCategory && (
            <>
              <Field label="Category">
                <Select value={categoryId} onChange={(e) => setCategoryId(Number(e.target.value))}>
                  <option value={0}>All</option>
                  {(lookups?.transactionCategories ?? []).map((c) => (
                    <option key={c.id} value={c.id}>{c.name}</option>
                  ))}
                </Select>
              </Field>
              <Field label="Type">
                <Select value={typeId} onChange={(e) => setTypeId(Number(e.target.value))}>
                  <option value={0}>All</option>
                  {(lookups?.transactionTypes ?? []).map((c) => (
                    <option key={c.id} value={c.id}>{c.name}</option>
                  ))}
                </Select>
              </Field>
            </>
          )}
          <Button variant="primary" busy={busy} onClick={run} style={{ marginBottom: 14 }}>
            Run Report
          </Button>
        </div>
      </Card>

      <Card>
        {!ran ? (
          <p style={{ color: 'var(--text-muted)' }}>Choose filters and run the report.</p>
        ) : rows.length === 0 ? (
          <p style={{ color: 'var(--text-muted)' }}>No records found.</p>
        ) : (
          <div style={{ overflowX: 'auto' }}>
            <table className="grid">
              <thead>
                <tr>
                  {columns.map((c) => (
                    <th key={c}>{toTitleCase(c)}</th>
                  ))}
                </tr>
              </thead>
              <tbody>
                {rows.map((row, i) => (
                  <tr key={i}>
                    {columns.map((c) => (
                      <td key={c}>{formatCell(row[c])}</td>
                    ))}
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </Card>
    </div>
  );
}
