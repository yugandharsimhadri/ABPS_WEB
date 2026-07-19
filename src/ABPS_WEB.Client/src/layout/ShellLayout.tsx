import type { ReactNode } from 'react';
import { NavLink, Outlet, useLocation, useNavigate } from 'react-router-dom';
import { useAuth } from '../auth/AuthContext';
import { BrandThemeSwitch, Button } from '../components/ui';
import {
  BedIcon, CancelIcon, ChartIcon, ClockIcon, DoorOutIcon, HalfCircleIcon, HeartHandIcon,
  HistoryIcon, HomeIcon, KeyIcon, ListIcon, MapIcon, MessageIcon, PlusSquareIcon, SwapIcon,
  UserPlusIcon, UsersIcon,
} from '../components/icons';

interface NavItem {
  to: string;
  label: string;
  icon: ReactNode;
}

interface NavSection {
  title: string;
  items: NavItem[];
}

const NAV: NavSection[] = [
  {
    title: 'Overview',
    items: [
      { to: '/', label: 'Dashboard', icon: <HomeIcon /> },
      { to: '/rooms/map', label: 'Room Map', icon: <MapIcon /> },
    ],
  },
  {
    title: 'Room Ops',
    items: [
      { to: '/rooms/book', label: 'Book Room', icon: <BedIcon /> },
      { to: '/rooms/vacate', label: 'Vacate Room', icon: <DoorOutIcon /> },
      { to: '/rooms/extend', label: 'Extend Room', icon: <ClockIcon /> },
      { to: '/rooms/shift', label: 'Shift Room', icon: <SwapIcon /> },
      { to: '/rooms/cancel', label: 'Cancel Room', icon: <CancelIcon /> },
      { to: '/rooms/partial-checkout', label: 'Partial Checkout', icon: <HalfCircleIcon /> },
      { to: '/rooms/add-room', label: 'Add Room', icon: <PlusSquareIcon /> },
    ],
  },
  {
    title: 'Donors',
    items: [
      { to: '/donations/new', label: 'New Donation', icon: <HeartHandIcon /> },
      { to: '/donations/historical', label: 'Historical Donation', icon: <HistoryIcon /> },
      { to: '/greetings', label: 'Send Greetings', icon: <MessageIcon /> },
    ],
  },
  {
    title: 'Admin',
    items: [
      { to: '/admin/rooms', label: 'Room Master', icon: <KeyIcon /> },
      { to: '/admin/donor-types', label: 'Donation Types', icon: <ListIcon /> },
      { to: '/admin/members', label: 'Executive Members', icon: <UsersIcon /> },
      { to: '/admin/users', label: 'User Registration', icon: <UserPlusIcon /> },
    ],
  },
  { title: 'Reports', items: [{ to: '/reports', label: 'Reports', icon: <ChartIcon /> }] },
];

export function ShellLayout() {
  const { user, logout } = useAuth();
  const navigate = useNavigate();
  const location = useLocation();

  const activePage = NAV.flatMap((s) => s.items).find((i) => i.to === location.pathname)?.label ?? 'Dashboard';

  return (
    <div className="app-shell">
      <nav className="sanctum">
        <div className="sanctum-brand">
          <div className="glyph">ॐ</div>
          <div className="name">Akhila Bharatha Baktha Markandeya Satram, Srisailam</div>
        </div>
        {NAV.map((section) => (
          <div key={section.title}>
            <div className="sanctum-section">{section.title}</div>
            {section.items.map((item) => (
              <NavLink
                key={item.to}
                to={item.to}
                end={item.to === '/'}
                className={({ isActive }) => `sanctum-link ${isActive ? 'active' : ''}`}
              >
                <span className="icon" aria-hidden="true">{item.icon}</span>
                {item.label}
              </NavLink>
            ))}
          </div>
        ))}
      </nav>
      <div className="courtyard">
        <header className="topbar">
          <div className="title">{activePage}</div>
          <div className="user-chip">
            <BrandThemeSwitch />
            <span>
              {user?.username} · {user?.roleName}
            </span>
            <Button
              size="sm"
              variant="secondary"
              onClick={() => {
                logout();
                navigate('/login', { replace: true });
              }}
            >
              Sign out
            </Button>
          </div>
        </header>
        <main className="content">
          <Outlet />
        </main>
      </div>
    </div>
  );
}
