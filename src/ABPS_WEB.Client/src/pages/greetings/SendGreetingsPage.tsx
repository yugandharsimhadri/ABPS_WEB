import { useEffect, useState } from 'react';
import { greetingsApi } from '../../api/endpoints';
import { ApiError } from '../../api/http';
import type { GreetingRecipient } from '../../api/types';
import { useLookups } from '../../lookups/LookupsContext';
import { Banner, Button, Card, Field, PageHeader, Select, StatTile } from '../../components/ui';

interface Row extends GreetingRecipient {
  selected: boolean;
}

function today() {
  return new Date().toISOString().slice(0, 10);
}

export function SendGreetingsPage() {
  const { lookups } = useLookups();
  const [teluguMode, setTeluguMode] = useState(false);
  const [date, setDate] = useState(today());
  const [tMasamId, setTMasamId] = useState(0);
  const [tPakshamId, setTPakshamId] = useState(0);
  const [tThiduluId, setTThiduluId] = useState(0);

  const [rows, setRows] = useState<Row[]>([]);
  const [sentToday, setSentToday] = useState(0);
  const [error, setError] = useState<string | null>(null);
  const [result, setResult] = useState<string | null>(null);
  const [busy, setBusy] = useState(false);
  const [searching, setSearching] = useState(false);

  const refreshSentToday = () => greetingsApi.getSentTodayCount().then(setSentToday);

  useEffect(() => {
    refreshSentToday();
  }, []);

  const find = async () => {
    setError(null);
    setResult(null);
    setSearching(true);
    try {
      const recipients = teluguMode
        ? await greetingsApi.findByTeluguCalendar(tMasamId, tPakshamId, tThiduluId)
        : await greetingsApi.findByEnglishDate(new Date(date).toISOString());
      setRows(recipients.map((r) => ({ ...r, selected: true })));
    } catch (err) {
      setError(err instanceof ApiError ? err.message : 'Search failed.');
    } finally {
      setSearching(false);
    }
  };

  const toggle = (index: number) => {
    setRows((prev) => prev.map((r, i) => (i === index ? { ...r, selected: !r.selected } : r)));
  };

  const send = async () => {
    setError(null);
    setResult(null);
    const selected = rows.filter((r) => r.selected);
    if (selected.length === 0) return setError('Select at least one recipient.');

    setBusy(true);
    try {
      const res = await greetingsApi.send({
        smsSendType: teluguMode ? 'T' : 'D',
        smsDate: new Date(date).toISOString(),
        tMasamId, tPakshamId, tThiduluId,
        recipients: selected.map(({ selected: _s, ...rest }) => rest),
      });
      setResult(`Sent ${res.sent} of ${res.attempted} selected.`);
      setRows([]);
      refreshSentToday();
    } catch (err) {
      setError(err instanceof ApiError ? err.message : 'Send failed.');
    } finally {
      setBusy(false);
    }
  };

  return (
    <div>
      <PageHeader title="Send Greetings" subtitle="Find donors with a matching occasion and send a WhatsApp greeting." />
      {error && <Banner tone="error">{error}</Banner>}
      {result && <Banner tone="success">{result}</Banner>}

      <div className="stat-row" style={{ gridTemplateColumns: '200px' }}>
        <StatTile value={sentToday} label="Sent Today" />
      </div>

      <Card style={{ marginBottom: 20 }}>
        <div style={{ display: 'flex', gap: 16, marginBottom: 14 }}>
          <label className="checkbox-row">
            <input type="radio" checked={!teluguMode} onChange={() => setTeluguMode(false)} /> English Date
          </label>
          <label className="checkbox-row">
            <input type="radio" checked={teluguMode} onChange={() => setTeluguMode(true)} /> Telugu Calendar
          </label>
        </div>

        {!teluguMode ? (
          <Field label="Date">
            <input type="date" value={date} onChange={(e) => setDate(e.target.value)} />
          </Field>
        ) : (
          <div className="form-grid">
            <Field label="Masam">
              <Select value={tMasamId} onChange={(e) => setTMasamId(Number(e.target.value))}>
                <option value={0}>Select…</option>
                {(lookups?.teluguMasams ?? []).map((m) => (
                  <option key={m.id} value={m.id}>{m.name}</option>
                ))}
              </Select>
            </Field>
            <Field label="Paksham">
              <Select value={tPakshamId} onChange={(e) => setTPakshamId(Number(e.target.value))}>
                <option value={0}>Select…</option>
                {(lookups?.teluguPakshams ?? []).map((m) => (
                  <option key={m.id} value={m.id}>{m.name}</option>
                ))}
              </Select>
            </Field>
            <Field label="Thidi">
              <Select value={tThiduluId} onChange={(e) => setTThiduluId(Number(e.target.value))}>
                <option value={0}>Select…</option>
                {(lookups?.teluguThidis ?? []).map((m) => (
                  <option key={m.id} value={m.id}>{m.name}</option>
                ))}
              </Select>
            </Field>
          </div>
        )}
        <Button busy={searching} onClick={find} style={{ marginTop: 8 }}>Find Recipients</Button>
      </Card>

      <Card>
        <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 12 }}>
          <h3>{rows.length} donor(s) found</h3>
          <Button variant="primary" busy={busy} onClick={send} disabled={rows.length === 0}>
            Send to Selected
          </Button>
        </div>
        {rows.length === 0 ? (
          <p style={{ color: 'var(--text-muted)' }}>Run a search to find matching donors.</p>
        ) : (
          <table className="grid">
            <thead>
              <tr>
                <th></th>
                <th>Name</th>
                <th>Phone</th>
                <th>Bond No</th>
                <th>In the name of</th>
                <th>Occasion</th>
              </tr>
            </thead>
            <tbody>
              {rows.map((r, i) => (
                <tr key={i}>
                  <td><input type="checkbox" checked={r.selected} onChange={() => toggle(i)} /></td>
                  <td>{r.name ?? '—'}</td>
                  <td>{r.phoneNo ?? '—'}</td>
                  <td>{r.bondNo ?? '—'}</td>
                  <td>{r.intheNameof ?? '—'}</td>
                  <td>{r.ocassionName ?? '—'}</td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </Card>
    </div>
  );
}
