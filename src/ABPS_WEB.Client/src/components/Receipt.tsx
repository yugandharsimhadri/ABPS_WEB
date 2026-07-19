import { useState } from 'react';
import { Button } from './ui';

/* Official letterhead details, transcribed from the satram's registered banner. */
const SATRAM_NAME_TELUGU = 'అఖిల భారత భక్త మార్కండేయ పద్మశాలి నిత్య అన్న సత్రం, శ్రీశైల పుణ్యక్షేత్రం';
const SATRAM_NAME_ENGLISH = 'Akhila Bharatha Bhaktha Markandeya Padmashali Nitya Anna Satram, Srisailam';
const SATRAM_REG_LINE = 'Regd. No. 985/84 · Srisailam Post, Nandyal District – 518 101 · Phone: 08524-287146';

export interface ReceiptData {
  title: string;
  receiptNo: string;
  date: Date;
  lines: { label: string; value: string }[];
  amount: number;
  amountLabel?: string;
}

const ONES = ['', 'One', 'Two', 'Three', 'Four', 'Five', 'Six', 'Seven', 'Eight', 'Nine', 'Ten', 'Eleven', 'Twelve', 'Thirteen', 'Fourteen', 'Fifteen', 'Sixteen', 'Seventeen', 'Eighteen', 'Nineteen'];
const TENS = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];

function twoDigits(n: number): string {
  if (n < 20) return ONES[n];
  return `${TENS[Math.floor(n / 10)]}${n % 10 ? ' ' + ONES[n % 10] : ''}`;
}

function threeDigits(n: number): string {
  const h = Math.floor(n / 100);
  const rest = n % 100;
  return `${h ? ONES[h] + ' Hundred' : ''}${h && rest ? ' ' : ''}${rest ? twoDigits(rest) : ''}`;
}

/** Indian-system amount in words (crore/lakh/thousand), as printed on formal receipts. */
export function amountInWords(amount: number): string {
  const n = Math.floor(Math.abs(amount));
  if (n === 0) return 'Zero Rupees Only';
  const crore = Math.floor(n / 10000000);
  const lakh = Math.floor((n % 10000000) / 100000);
  const thousand = Math.floor((n % 100000) / 1000);
  const rest = n % 1000;
  const parts: string[] = [];
  if (crore) parts.push(`${twoDigits(crore)} Crore`);
  if (lakh) parts.push(`${twoDigits(lakh)} Lakh`);
  if (thousand) parts.push(`${twoDigits(thousand)} Thousand`);
  if (rest) parts.push(threeDigits(rest));
  return `Rupees ${parts.join(' ')} Only`;
}

export function ReceiptOverlay({ data, onClose }: { data: ReceiptData; onClose: () => void }) {
  // The satram's physical receipt books are pre-printed stationery: banner, names, and
  // registration line are already on the paper. Default to printing ONLY the data, pushed
  // down by an adjustable offset so it lands in the blank area of the form. The letterhead
  // stays visible on screen purely as a reference preview.
  const [stationery, setStationery] = useState(true);
  const [topOffsetMm, setTopOffsetMm] = useState(45);
  return (
    <div className="receipt-overlay" role="dialog" aria-label={data.title}>
      <div
        className={`receipt-sheet ${stationery ? 'stationery' : ''}`}
        style={{ ['--receipt-top' as string]: `${topOffsetMm}mm` }}
      >
        <header className="receipt-head">
          <div className="receipt-glyph">ॐ</div>
          <div className="receipt-name-telugu">{SATRAM_NAME_TELUGU}</div>
          <div className="receipt-name-english">{SATRAM_NAME_ENGLISH}</div>
          <div className="receipt-reg">{SATRAM_REG_LINE}</div>
        </header>

        <div className="receipt-rule" />

        <div className="receipt-title-row">
          <h2>{data.title}</h2>
          <div className="receipt-meta">
            <div>{data.receiptNo}</div>
            <div>{data.date.toLocaleString('en-IN', { dateStyle: 'medium', timeStyle: 'short' })}</div>
          </div>
        </div>

        <dl className="receipt-lines">
          {data.lines.map((line) => (
            <div key={line.label}>
              <dt>{line.label}</dt>
              <dd>{line.value}</dd>
            </div>
          ))}
        </dl>

        <div className="receipt-amount">
          <div className="label">{data.amountLabel ?? 'Amount Received'}</div>
          <div className="value">₹{data.amount.toLocaleString('en-IN')}</div>
          <div className="words">{amountInWords(data.amount)}</div>
        </div>

        <footer className="receipt-foot">
          <div className="note">This is a computer-generated receipt.</div>
          <div className="sign">
            <div className="sign-line" />
            Authorised Signatory
          </div>
        </footer>

        <div className="receipt-actions">
          <div className="receipt-print-opts">
            <label className="checkbox-row">
              <input type="checkbox" checked={stationery} onChange={(e) => setStationery(e.target.checked)} />
              Pre-printed letterhead paper
            </label>
            {stationery && (
              <label className="receipt-offset">
                Top space
                <input
                  type="number"
                  min={0}
                  max={120}
                  value={topOffsetMm}
                  onChange={(e) => setTopOffsetMm(Number(e.target.value))}
                />
                mm
              </label>
            )}
          </div>
          {stationery && (
            <div className="receipt-print-hint">
              Letterhead is shown for reference only — the print will contain just the details, starting {topOffsetMm}mm from the top of the page.
            </div>
          )}
          <div className="receipt-buttons">
            <Button variant="primary" onClick={() => window.print()}>Print</Button>
            <Button variant="secondary" onClick={onClose}>Close</Button>
          </div>
        </div>
      </div>
    </div>
  );
}
