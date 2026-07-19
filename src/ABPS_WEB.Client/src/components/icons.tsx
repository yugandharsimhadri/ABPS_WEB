import type { ReactNode, SVGProps } from 'react';

/* Hand-drawn 24×24 stroke icon set for the sidebar and dashboard. Stroke-based (not filled)
 * so every icon inherits currentColor and reads consistently at 16px, unlike the mixed
 * emoji glyphs these replaced (emoji render from OS fonts — inconsistent weight/color and
 * a distinctly non-commercial look). */

function Icon({ children, ...rest }: SVGProps<SVGSVGElement> & { children: ReactNode }) {
  return (
    <svg
      viewBox="0 0 24 24"
      width="1em"
      height="1em"
      fill="none"
      stroke="currentColor"
      strokeWidth="1.9"
      strokeLinecap="round"
      strokeLinejoin="round"
      aria-hidden="true"
      style={{ display: 'block' }}
      {...rest}
    >
      {children}
    </svg>
  );
}

export const HomeIcon = () => (
  <Icon>
    <path d="M3 10.5 12 3l9 7.5" />
    <path d="M5 9.5V21h14V9.5" />
    <path d="M10 21v-6h4v6" />
  </Icon>
);

export const MapIcon = () => (
  <Icon>
    <path d="M9 4 3 6v14l6-2 6 2 6-2V4l-6 2-6-2Z" />
    <path d="M9 4v14M15 6v14" />
  </Icon>
);

export const BedIcon = () => (
  <Icon>
    <path d="M3 7v11M3 14h18v4" />
    <path d="M3 11h18v3" />
    <circle cx="7.5" cy="9.5" r="1.6" />
  </Icon>
);

export const DoorOutIcon = () => (
  <Icon>
    <path d="M13 4H6v16h7" />
    <path d="M16 8.5 19.5 12 16 15.5" />
    <path d="M19.5 12H10" />
  </Icon>
);

export const ClockIcon = () => (
  <Icon>
    <circle cx="12" cy="12" r="8.5" />
    <path d="M12 7.5V12l3 2" />
  </Icon>
);

export const SwapIcon = () => (
  <Icon>
    <path d="M4 8h13M14 4.5 17.5 8 14 11.5" />
    <path d="M20 16H7M10 12.5 6.5 16l3.5 3.5" />
  </Icon>
);

export const CancelIcon = () => (
  <Icon>
    <circle cx="12" cy="12" r="8.5" />
    <path d="m9 9 6 6M15 9l-6 6" />
  </Icon>
);

export const HalfCircleIcon = () => (
  <Icon>
    <circle cx="12" cy="12" r="8.5" />
    <path d="M12 3.5v17" />
    <path d="M12 3.5a8.5 8.5 0 0 1 0 17" fill="currentColor" stroke="none" opacity="0.35" />
  </Icon>
);

export const PlusSquareIcon = () => (
  <Icon>
    <rect x="4" y="4" width="16" height="16" rx="2.5" />
    <path d="M12 8.5v7M8.5 12h7" />
  </Icon>
);

export const HeartHandIcon = () => (
  <Icon>
    <path d="M12 8.2c1.2-2.5 4.6-2.6 5.7-.4 1 2-.4 4-2.3 5.7L12 16.5l-3.4-3c-1.9-1.7-3.3-3.7-2.3-5.7 1.1-2.2 4.5-2.1 5.7.4Z" />
    <path d="M4 19.5h16" />
  </Icon>
);

export const HistoryIcon = () => (
  <Icon>
    <path d="M4.5 12a7.5 7.5 0 1 1 2.2 5.3" />
    <path d="M4.5 12H8M4.5 12V8.5" />
    <path d="M12 8.5V12l2.5 1.7" />
  </Icon>
);

export const MessageIcon = () => (
  <Icon>
    <path d="M20 15a2 2 0 0 1-2 2H8l-4 3.5V6a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v9Z" />
    <path d="M8 9h8M8 12.5h5" />
  </Icon>
);

export const KeyIcon = () => (
  <Icon>
    <circle cx="8" cy="15.5" r="4" />
    <path d="M11 12.5 20 4M16 7.5l2.5 2.5M13.5 10l2 2" />
  </Icon>
);

export const ListIcon = () => (
  <Icon>
    <path d="M9 6h11M9 12h11M9 18h11" />
    <path d="M4.5 6h.01M4.5 12h.01M4.5 18h.01" strokeWidth="2.6" />
  </Icon>
);

export const UsersIcon = () => (
  <Icon>
    <circle cx="9" cy="8.5" r="3.2" />
    <path d="M3.5 19.5c0-3 2.5-5 5.5-5s5.5 2 5.5 5" />
    <path d="M15.5 5.6a3.2 3.2 0 0 1 0 5.8M17.5 14.9c1.8.7 3 2.3 3 4.6" />
  </Icon>
);

export const UserPlusIcon = () => (
  <Icon>
    <circle cx="10" cy="8.5" r="3.2" />
    <path d="M4.5 19.5c0-3 2.5-5 5.5-5s5.5 2 5.5 5" />
    <path d="M18.5 8v5M16 10.5h5" />
  </Icon>
);

export const ChartIcon = () => (
  <Icon>
    <path d="M4 4v16h16" />
    <path d="M8.5 16v-5M13 16V8M17.5 16v-3" />
  </Icon>
);
