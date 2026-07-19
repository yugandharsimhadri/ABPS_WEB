import { Children, cloneElement, isValidElement, useEffect, useId, useState, type ButtonHTMLAttributes, type InputHTMLAttributes, type ReactNode, type SelectHTMLAttributes } from 'react';
import { useBrandTheme } from '../theme/BrandThemeContext';

export function PageHeader({ title, subtitle, actions }: { title: string; subtitle?: string; actions?: ReactNode }) {
  return (
    <div className="page-header">
      <div>
        <h1>{title}</h1>
        {subtitle && <div className="subtitle">{subtitle}</div>}
      </div>
      {actions}
    </div>
  );
}

export function Card({ children, style }: { children: ReactNode; style?: React.CSSProperties }) {
  return (
    <div className="card" style={style}>
      {children}
    </div>
  );
}

interface ButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
  variant?: 'primary' | 'secondary' | 'danger' | 'gold';
  size?: 'md' | 'sm';
  busy?: boolean;
}

export function Button({ variant = 'secondary', size = 'md', busy, children, className, disabled, ...rest }: ButtonProps) {
  const cls = `btn btn-${variant} ${size === 'sm' ? 'btn-sm' : ''} ${className ?? ''}`.trim();
  return (
    <button className={cls} disabled={disabled || busy} {...rest}>
      {busy && <span className="spinner" />}
      {children}
    </button>
  );
}

function isFormControlType(type: unknown): boolean {
  return type === TextInput || type === Select || (typeof type === 'string' && ['input', 'select', 'textarea'].includes(type));
}

/** Recursively finds the first form-control element in a (possibly nested, e.g. a
 * `<div><TextInput/><Button/></div>` wrapper) node tree and clones it with the given id, so a
 * `<Field>`'s label stays correctly associated (for accessibility and for test locators like
 * `getByLabel`) no matter how the field's contents are laid out. */
function withIdOnFirstControl(node: ReactNode, fallbackId: string): { node: ReactNode; usedId?: string } {
  if (!isValidElement(node)) return { node };

  if (isFormControlType(node.type)) {
    const props = node.props as { id?: string };
    const usedId = props.id || fallbackId;
    return { node: cloneElement(node, { id: usedId } as object), usedId };
  }

  const children = (node.props as { children?: ReactNode }).children;
  if (children === undefined) return { node };

  let usedId: string | undefined;
  const newChildren = Children.map(children, (child) => {
    if (usedId) return child;
    const result = withIdOnFirstControl(child, fallbackId);
    if (result.usedId) {
      usedId = result.usedId;
      return result.node;
    }
    return child;
  });

  if (!usedId) return { node };
  return { node: cloneElement(node, undefined, newChildren), usedId };
}

export function Field({
  label,
  hint,
  error,
  children,
}: {
  label: string;
  hint?: string;
  error?: string;
  children: ReactNode;
}) {
  const generatedId = useId();
  // Children.toArray (not Children.only) because several call sites pass more than one
  // top-level child — e.g. a wrapper <div> holding an input+button plus a conditional hint
  // <span> sibling. Children.only throws on anything but exactly one element, which would
  // crash the whole tree the moment such a conditional sibling renders.
  let fieldId: string | undefined;
  const labeledChildren = Children.toArray(children).map((child) => {
    if (fieldId) return child;
    const result = withIdOnFirstControl(child, generatedId);
    if (result.usedId) {
      fieldId = result.usedId;
      return result.node;
    }
    return child;
  });

  return (
    <div className="field">
      <label htmlFor={fieldId}>{label}</label>
      {labeledChildren}
      {hint && !error && <span className="hint">{hint}</span>}
      {error && <span className="error">{error}</span>}
    </div>
  );
}

export function TextInput(props: InputHTMLAttributes<HTMLInputElement>) {
  return <input type="text" {...props} />;
}

/** A plain 3-blade ceiling fan, viewed from below — the standard Indian ceiling fan silhouette,
 * used for Non-AC rooms. Unicode has no usable fan glyph (☀ reads as "sun/weather", not "fan"
 * or "no AC"), so this is drawn directly rather than borrowed from an emoji font. */
function FanIcon() {
  return (
    <svg viewBox="0 0 24 24" width="1em" height="1em" fill="currentColor" aria-hidden="true" style={{ display: 'block' }}>
      <ellipse cx="12" cy="5.5" rx="2.6" ry="4.6" transform="rotate(0 12 12)" />
      <ellipse cx="12" cy="5.5" rx="2.6" ry="4.6" transform="rotate(120 12 12)" />
      <ellipse cx="12" cy="5.5" rx="2.6" ry="4.6" transform="rotate(240 12 12)" />
      <circle cx="12" cy="12" r="2.1" />
    </svg>
  );
}

const ROOM_CATEGORY_META: Record<string, { icon: ReactNode; label: string; cls: string }> = {
  AC: { icon: '❄', label: 'Air Conditioned', cls: 'room-cat-ac' },
  NON: { icon: <FanIcon />, label: 'Non-AC', cls: 'room-cat-non' },
  LUX: { icon: '✦', label: 'Luxury', cls: 'room-cat-lux' },
  DOR: { icon: '🛌', label: 'Dormitory', cls: 'room-cat-other' },
  LOC: { icon: '🔒', label: 'Locker', cls: 'room-cat-other' },
  KL: { icon: '🎪', label: 'Function Hall', cls: 'room-cat-hall' },
};

/** Icon badge for a room's category code (AC/Non-AC/Luxury/etc, as stored in
 * tbl_RoomType.RoomCategory). Falls back to showing the raw code for categories not in the
 * known set above, so unfamiliar codes still render something instead of nothing.
 * `inline` renders a pill (icon + label) for table cells; the default is a small round badge
 * meant to sit absolutely-positioned in the corner of a room tile. */
export function RoomCategoryBadge({ category, inline }: { category: string; inline?: boolean }) {
  const meta = ROOM_CATEGORY_META[category.toUpperCase()];
  const icon = meta?.icon ?? category.slice(0, 3);
  const cls = meta?.cls ?? 'room-cat-other';
  const label = meta?.label ?? category;

  if (inline) {
    return (
      <span className={`room-cat-pill ${cls}`}>
        <span aria-hidden="true">{icon}</span> {label}
      </span>
    );
  }

  return (
    <span className={`room-cat-badge ${cls}`} title={label}>
      {icon}
    </span>
  );
}

export function Select(props: SelectHTMLAttributes<HTMLSelectElement> & { children: ReactNode }) {
  return <select {...props} />;
}

export function Badge({ tone, children }: { tone: 'success' | 'danger' | 'warning' | 'neutral'; children: ReactNode }) {
  return <span className={`badge badge-${tone}`}>{children}</span>;
}

/** Eases a number from 0 to its target over ~0.9s. Only kicks in for finite numbers — string
 * placeholders like '…' render as-is. Tests asserting final text are unaffected because
 * Playwright's toHaveText retries past the animation window. */
function useCountUp(target: number, durationMs = 900) {
  const [value, setValue] = useState(0);
  useEffect(() => {
    let raf = 0;
    const start = performance.now();
    const tick = (now: number) => {
      const t = Math.min(1, (now - start) / durationMs);
      const eased = 1 - Math.pow(1 - t, 3);
      setValue(Math.round(target * eased));
      if (t < 1) raf = requestAnimationFrame(tick);
    };
    raf = requestAnimationFrame(tick);
    // rAF is throttled/paused in background tabs — guarantee the final value lands even if
    // no animation frame ever fires (e.g. the page loaded while its tab wasn't visible).
    const settle = window.setTimeout(() => setValue(target), durationMs + 100);
    return () => {
      cancelAnimationFrame(raf);
      clearTimeout(settle);
    };
  }, [target, durationMs]);
  return value;
}

function CountUp({ target }: { target: number }) {
  return <>{useCountUp(target)}</>;
}

export function StatTile({ value, label, icon }: { value: ReactNode; label: string; icon?: ReactNode }) {
  const display = typeof value === 'number' && Number.isFinite(value) ? <CountUp target={value} /> : value;
  return (
    <div className="stat-tile">
      {icon && <div className="stat-icon" aria-hidden="true">{icon}</div>}
      <div>
        <div className="value">{display}</div>
        <div className="label">{label}</div>
      </div>
    </div>
  );
}

export function Banner({ tone, children }: { tone: 'error' | 'success' | 'info'; children: ReactNode }) {
  return <div className={`banner banner-${tone}`}>{children}</div>;
}

/** A small line-art diya (oil lamp) — warmer than a bare "no records" line. */
function DiyaArt() {
  return (
    <svg viewBox="0 0 64 44" width="58" height="40" fill="none" stroke="currentColor" strokeWidth="1.6" strokeLinecap="round" strokeLinejoin="round" aria-hidden="true">
      <path d="M32 20c0-4 1.4-6.8 0-10 4.4 2.2 5.6 6.6 3.2 10" />
      <path d="M32 20c-2.6-2.6-2.4-6 0-8" opacity="0.55" />
      <path d="M14 27h36c-1.5 6-8.5 10-18 10s-16.5-4-18-10Z" />
      <path d="M10 27h44" />
      <path d="M22 41h20" opacity="0.55" />
    </svg>
  );
}

export function EmptyState({ children }: { children: ReactNode }) {
  return (
    <div className="empty-state">
      <div className="empty-art"><DiyaArt /></div>
      {children}
    </div>
  );
}

export function Table<T>({
  columns,
  rows,
  rowKey,
  emptyMessage = 'No records found.',
}: {
  columns: { header: string; render: (row: T) => ReactNode; width?: string }[];
  rows: T[];
  rowKey: (row: T, index: number) => string | number;
  emptyMessage?: string;
}) {
  if (rows.length === 0) return <EmptyState>{emptyMessage}</EmptyState>;
  return (
    <div style={{ overflowX: 'auto' }}>
      <table className="grid">
        <thead>
          <tr>
            {columns.map((c) => (
              <th key={c.header} style={{ width: c.width }}>
                {c.header}
              </th>
            ))}
          </tr>
        </thead>
        <tbody>
          {rows.map((row, i) => (
            <tr key={rowKey(row, i)}>
              {columns.map((c) => (
                <td key={c.header}>{c.render(row)}</td>
              ))}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export function Tabs({ tabs, active, onChange }: { tabs: { id: string; label: string }[]; active: string; onChange: (id: string) => void }) {
  return (
    <div className="tabs">
      {tabs.map((t) => (
        <div key={t.id} className={`tab ${active === t.id ? 'active' : ''}`} onClick={() => onChange(t.id)}>
          {t.label}
        </div>
      ))}
    </div>
  );
}

/** Switches the app's whole visual identity between the "Classic" temple-sanctum look and the
 * newer "Srisailam" light-blue/photo theme — persisted via BrandThemeContext. Shown in the
 * topbar (signed in) and on the login page (before signing in), so either theme can be
 * previewed at any point. */
export function BrandThemeSwitch() {
  const { brand, setBrand, mode, setMode } = useBrandTheme();
  const dusk = mode === 'dusk';
  return (
    <div className="brand-switch" role="tablist" aria-label="Theme">
      <button type="button" aria-pressed={brand === 'classic'} onClick={() => setBrand('classic')}>
        <span className="dot" style={{ background: '#a3342c' }} />
        Classic
      </button>
      <button type="button" aria-pressed={brand === 'srisailam'} onClick={() => setBrand('srisailam')}>
        <span className="dot" style={{ background: '#2b7bc4' }} />
        Srisailam
      </button>
      <button
        type="button"
        className="mode-toggle"
        aria-pressed={dusk}
        aria-label={dusk ? 'Switch to day mode' : 'Switch to dusk mode'}
        title={dusk ? 'Day mode' : 'Dusk mode'}
        onClick={() => setMode(dusk ? 'day' : 'dusk')}
      >
        {dusk ? (
          <svg viewBox="0 0 24 24" width="1em" height="1em" fill="none" stroke="currentColor" strokeWidth="1.9" strokeLinecap="round" aria-hidden="true">
            <circle cx="12" cy="12" r="4.2" />
            <path d="M12 3.5v2M12 18.5v2M3.5 12h2M18.5 12h2M6 6l1.4 1.4M16.6 16.6 18 18M18 6l-1.4 1.4M7.4 16.6 6 18" />
          </svg>
        ) : (
          <svg viewBox="0 0 24 24" width="1em" height="1em" fill="none" stroke="currentColor" strokeWidth="1.9" strokeLinecap="round" strokeLinejoin="round" aria-hidden="true">
            <path d="M20 14.5A8.5 8.5 0 0 1 9.5 4a8.5 8.5 0 1 0 10.5 10.5Z" />
          </svg>
        )}
      </button>
    </div>
  );
}
