import { useState } from 'react';
import { donationsApi } from '../../api/endpoints';
import { ApiError } from '../../api/http';
import type { DonorDto } from '../../api/types';
import { useLookups } from '../../lookups/LookupsContext';
import { Banner, Button, Card, Field, PageHeader, Select, TextInput } from '../../components/ui';
import { ReceiptOverlay, type ReceiptData } from '../../components/Receipt';

function today() {
  return new Date().toISOString().slice(0, 10);
}

export function DonationForm({ mode }: { mode: 'new' | 'historical' }) {
  const { lookups } = useLookups();

  const [isExisting, setIsExisting] = useState(false);
  const [searchTerm, setSearchTerm] = useState('');
  const [searchResults, setSearchResults] = useState<DonorDto[]>([]);
  const [existingDonor, setExistingDonor] = useState<DonorDto | null>(null);

  const [donationTypeId, setDonationTypeId] = useState(0);
  const [name, setName] = useState('');
  const [teluguName, setTeluguName] = useState('');
  const [phoneNo, setPhoneNo] = useState('');
  const [address, setAddress] = useState('');
  const [gothram, setGothram] = useState('');
  const [pan, setPan] = useState('');
  const [aadhaar, setAadhaar] = useState('');
  const [countryId, setCountryId] = useState(0);
  const [stateId, setStateId] = useState(0);
  const [districtId, setDistrictId] = useState(0);

  const [occasionId, setOccasionId] = useState(0);
  const [dateMode, setDateMode] = useState<'E' | 'T'>('E');
  const [annadanamDate, setAnnadanamDate] = useState(today());
  const [tMasamId, setTMasamId] = useState(0);
  const [tPakshamId, setTPakshamId] = useState(0);
  const [tThiduluId, setTThiduluId] = useState(0);
  const [intheNameOf, setIntheNameOf] = useState('');
  const [teluguIntheNameOf, setTeluguIntheNameOf] = useState('');
  const [others, setOthers] = useState('');

  const [amount, setAmount] = useState(0);
  const [paymentTypeId, setPaymentTypeId] = useState(0);
  const [paymentSubTypeId, setPaymentSubTypeId] = useState(0);
  const [transactionNo, setTransactionNo] = useState('');
  const [bondNo, setBondNo] = useState('');
  const [roomDays, setRoomDays] = useState(0);
  const [donationDate, setDonationDate] = useState(today());

  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  const [busy, setBusy] = useState(false);
  const [receipt, setReceipt] = useState<ReceiptData | null>(null);
  const [showReceipt, setShowReceipt] = useState(false);

  const subTypes = (lookups?.paymentSubTypes ?? []).filter((p) => p.paymentTypeId === paymentTypeId);
  const districts = (lookups?.districts ?? []).filter((d) => d.stateId === stateId);

  const searchDonor = async () => {
    if (!searchTerm.trim()) return;
    try {
      const results = await donationsApi.searchDonors(searchTerm.trim(), 'phone');
      setSearchResults(results);
      if (results.length === 0) setError('No matching donors found.');
      else setError(null);
    } catch (err) {
      setError(err instanceof ApiError ? err.message : 'Search failed.');
    }
  };

  const pickDonor = (donor: DonorDto) => {
    setExistingDonor(donor);
    setSearchResults([]);
    setSearchTerm(donor.name ?? '');
  };

  const resolveOccasionName = () => lookups?.occasions.find((o) => o.id === occasionId)?.name ?? (others || 'Donation');

  const handleSubmit = async () => {
    setError(null);
    setSuccess(null);

    if (isExisting && !existingDonor) return setError('Search and select an existing donor first.');
    if (!isExisting && !name.trim()) return setError('Donor name is required.');
    if (amount <= 0) return setError('Enter a donation amount.');

    const occasion = {
      occasionId,
      others: others.trim() || null,
      intheNameOf: intheNameOf.trim() || null,
      teluguIntheNameOf: teluguIntheNameOf.trim() || null,
      anandanamType: dateMode,
      annadanamDate: new Date(annadanamDate).toISOString(),
      tMasamId,
      tPakshamId,
      tThiduluId,
      dateEnglishTelugu: dateMode,
    };
    const donation = {
      amount,
      paymentTypeId,
      paymentSubTypeId,
      transactionNo: transactionNo.trim() || null,
      bondNo: bondNo.trim() || null,
      donationDate: new Date(donationDate).toISOString(),
      roomDays,
    };

    setBusy(true);
    try {
      let message: string;
      if (isExisting && existingDonor) {
        if (mode === 'new') {
          const res = await donationsApi.saveForExistingDonor({
            existingDonarId: existingDonor.donarId,
            donationTypeId: existingDonor.donarTypeId,
            occasion,
            donation,
            transactionCategory: 2,
            occasionName: resolveOccasionName(),
            transactionTypeId: 2,
          });
          message = res.message;
        } else {
          const res = await donationsApi.saveHistorical({
            donor: {
              donationTypeId: existingDonor.donarTypeId, name: existingDonor.name, teluguName: existingDonor.teluguName,
              phoneNo: existingDonor.phoneNo, address: existingDonor.address, gothram: existingDonor.gothram,
              pan: existingDonor.pan, aadhaar: existingDonor.aadhaar, districtId: existingDonor.districtId,
              stateId: existingDonor.stateId, countryId: existingDonor.countryId,
            },
            occasion,
            donation,
            occasionName: resolveOccasionName(),
          });
          message = res.message;
        }
      } else {
        const donor = {
          donationTypeId, name: name.trim(), teluguName: teluguName.trim() || null, phoneNo: phoneNo.trim() || null,
          address: address.trim() || null, gothram: gothram.trim() || null, pan: pan.trim() || null,
          aadhaar: aadhaar.trim() || null, districtId, stateId, countryId,
        };
        if (mode === 'new') {
          const res = await donationsApi.save({ donor, occasion, donation, transactionCategory: 2, occasionName: resolveOccasionName(), transactionTypeId: 2 });
          message = res.message;
        } else {
          const res = await donationsApi.saveHistorical({ donor, occasion, donation, occasionName: resolveOccasionName() });
          message = res.message;
        }
      }
      setSuccess(message);
      const donorName = isExisting && existingDonor ? existingDonor.name ?? '' : name.trim();
      const paymentType = lookups?.paymentTypes.find((p) => p.id === paymentTypeId)?.name ?? '—';
      setReceipt({
        title: 'Donation Receipt',
        receiptNo: bondNo.trim() ? `Bond No. ${bondNo.trim()}` : `Dated ${new Date(donationDate).toLocaleDateString('en-IN')}`,
        date: new Date(),
        lines: [
          { label: 'Donor Name', value: donorName },
          { label: 'Occasion', value: resolveOccasionName() },
          { label: 'Payment Mode', value: paymentType },
          { label: 'Donation Date', value: new Date(donationDate).toLocaleDateString('en-IN', { dateStyle: 'medium' }) },
        ],
        amount,
        amountLabel: 'Donation Received',
      });
      setExistingDonor(null);
      setSearchTerm('');
      setName('');
      setAmount(0);
      setBondNo('');
    } catch (err) {
      setError(err instanceof ApiError ? err.message : 'Save failed.');
    } finally {
      setBusy(false);
    }
  };

  return (
    <div>
      <PageHeader
        title={mode === 'new' ? 'New Donation' : 'Historical Donation'}
        subtitle={mode === 'new' ? 'Record a new donation entry.' : 'Back-date a donation record.'}
      />
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

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 20, alignItems: 'start' }}>
        <Card>
          <div style={{ display: 'flex', gap: 16, marginBottom: 14 }}>
            <label className="checkbox-row">
              <input type="radio" checked={!isExisting} onChange={() => setIsExisting(false)} /> New Donor
            </label>
            <label className="checkbox-row">
              <input type="radio" checked={isExisting} onChange={() => setIsExisting(true)} /> Existing Donor
            </label>
          </div>

          {isExisting ? (
            <>
              <Field label="Search by Phone No">
                <div style={{ display: 'flex', gap: 8 }}>
                  <TextInput value={searchTerm} onChange={(e) => setSearchTerm(e.target.value)} />
                  <Button size="sm" onClick={searchDonor} type="button">Search</Button>
                </div>
              </Field>
              {searchResults.length > 0 && (
                <div style={{ display: 'flex', flexDirection: 'column', gap: 6, marginBottom: 12 }}>
                  {searchResults.map((d) => (
                    <button key={d.donarId} type="button" className="room-tile" style={{ textAlign: 'left' }} onClick={() => pickDonor(d)}>
                      <div className="room-no">{d.name}</div>
                      <div className="room-meta">{d.phoneNo} · {d.address}</div>
                    </button>
                  ))}
                </div>
              )}
              {existingDonor && (
                <Banner tone="info">
                  Selected: {existingDonor.name} ({existingDonor.phoneNo})
                </Banner>
              )}
            </>
          ) : (
            <>
              <Field label="Donation Type">
                <Select value={donationTypeId} onChange={(e) => setDonationTypeId(Number(e.target.value))}>
                  <option value={0}>Select…</option>
                  {(lookups?.donationTypes ?? []).map((d) => (
                    <option key={d.id} value={d.id}>{d.name}</option>
                  ))}
                </Select>
              </Field>
              <Field label="Name">
                <TextInput value={name} onChange={(e) => setName(e.target.value)} />
              </Field>
              <Field label="Telugu Name">
                <TextInput value={teluguName} onChange={(e) => setTeluguName(e.target.value)} />
              </Field>
              <Field label="Phone No">
                <TextInput value={phoneNo} onChange={(e) => setPhoneNo(e.target.value)} />
              </Field>
              <Field label="Address">
                <TextInput value={address} onChange={(e) => setAddress(e.target.value)} />
              </Field>
              <Field label="Gothram">
                <TextInput value={gothram} onChange={(e) => setGothram(e.target.value)} />
              </Field>
              <Field label="PAN">
                <TextInput value={pan} onChange={(e) => setPan(e.target.value)} />
              </Field>
              <Field label="Aadhaar">
                <TextInput value={aadhaar} onChange={(e) => setAadhaar(e.target.value)} />
              </Field>
              <Field label="Country">
                <Select value={countryId} onChange={(e) => setCountryId(Number(e.target.value))}>
                  <option value={0}>Select…</option>
                  {(lookups?.countries ?? []).map((c) => (
                    <option key={c.id} value={c.id}>{c.name}</option>
                  ))}
                </Select>
              </Field>
              <Field label="State">
                <Select value={stateId} onChange={(e) => { setStateId(Number(e.target.value)); setDistrictId(0); }}>
                  <option value={0}>Select…</option>
                  {(lookups?.states ?? []).map((s) => (
                    <option key={s.stateId} value={s.stateId}>{s.stateName}</option>
                  ))}
                </Select>
              </Field>
              <Field label="District">
                <Select value={districtId} onChange={(e) => setDistrictId(Number(e.target.value))}>
                  <option value={0}>Select…</option>
                  {districts.map((d) => (
                    <option key={d.districtId} value={d.districtId}>{d.districtName}</option>
                  ))}
                </Select>
              </Field>
            </>
          )}
        </Card>

        <Card>
          <h3 style={{ marginBottom: 14 }}>Occasion &amp; Payment</h3>
          <Field label="Occasion">
            <Select value={occasionId} onChange={(e) => setOccasionId(Number(e.target.value))}>
              <option value={0}>Select…</option>
              {(lookups?.occasions ?? []).map((o) => (
                <option key={o.id} value={o.id}>{o.name}</option>
              ))}
            </Select>
          </Field>
          <Field label="In the name of">
            <TextInput value={intheNameOf} onChange={(e) => setIntheNameOf(e.target.value)} />
          </Field>
          <Field label="Telugu In the name of">
            <TextInput value={teluguIntheNameOf} onChange={(e) => setTeluguIntheNameOf(e.target.value)} />
          </Field>
          <Field label="Others">
            <TextInput value={others} onChange={(e) => setOthers(e.target.value)} />
          </Field>

          <div style={{ display: 'flex', gap: 16, margin: '4px 0 14px' }}>
            <label className="checkbox-row">
              <input type="radio" checked={dateMode === 'E'} onChange={() => setDateMode('E')} /> English Date
            </label>
            <label className="checkbox-row">
              <input type="radio" checked={dateMode === 'T'} onChange={() => setDateMode('T')} /> Telugu Calendar
            </label>
          </div>

          {dateMode === 'E' ? (
            <Field label="Anandanam Date">
              <input type="date" value={annadanamDate} onChange={(e) => setAnnadanamDate(e.target.value)} />
            </Field>
          ) : (
            <>
              <Field label="Telugu Masam">
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
            </>
          )}

          <div className="divider" />

          <Field label="Amount">
            <input type="number" value={amount} onChange={(e) => setAmount(Number(e.target.value))} />
          </Field>
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
          <Field label="Bond No">
            <TextInput value={bondNo} onChange={(e) => setBondNo(e.target.value)} />
          </Field>
          <Field label="Room Days">
            <input type="number" value={roomDays} onChange={(e) => setRoomDays(Number(e.target.value))} />
          </Field>
          <Field label="Donation Date">
            <input type="date" value={donationDate} onChange={(e) => setDonationDate(e.target.value)} />
          </Field>

          <Button variant="primary" busy={busy} onClick={handleSubmit} style={{ width: '100%', marginTop: 6 }}>
            Save Donation
          </Button>
        </Card>
      </div>
    </div>
  );
}
