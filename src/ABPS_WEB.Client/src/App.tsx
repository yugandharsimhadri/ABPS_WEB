import { Navigate, Route, Routes } from 'react-router-dom';
import { AuthProvider, useAuth } from './auth/AuthContext';
import { ProtectedRoute } from './auth/ProtectedRoute';
import { LookupsProvider } from './lookups/LookupsContext';
import { BrandThemeProvider } from './theme/BrandThemeContext';
import { ShellLayout } from './layout/ShellLayout';
import { LoginPage } from './pages/LoginPage';
import { DashboardPage } from './pages/DashboardPage';
import { RoomMapPage } from './pages/RoomMapPage';
import { BookRoomPage } from './pages/rooms/BookRoomPage';
import { BookingActionPage } from './pages/rooms/BookingActionPage';
import { NewDonationPage } from './pages/donations/NewDonationPage';
import { HistoricalDonationPage } from './pages/donations/HistoricalDonationPage';
import { RoomMasterPage } from './pages/admin/RoomMasterPage';
import { DonorTypesPage } from './pages/admin/DonorTypesPage';
import { MembersPage } from './pages/admin/MembersPage';
import { UsersPage } from './pages/admin/UsersPage';
import { ReportsPage } from './pages/reports/ReportsPage';
import { SendGreetingsPage } from './pages/greetings/SendGreetingsPage';

function LoginRoute() {
  const { isAuthenticated } = useAuth();
  if (isAuthenticated) return <Navigate to="/" replace />;
  return <LoginPage />;
}

export default function App() {
  return (
    <BrandThemeProvider>
      <AuthProvider>
        <LookupsProvider>
          <Routes>
            <Route path="/login" element={<LoginRoute />} />
            <Route
              element={
                <ProtectedRoute>
                  <ShellLayout />
                </ProtectedRoute>
              }
            >
              <Route path="/" element={<DashboardPage />} />
              <Route path="/rooms/map" element={<RoomMapPage />} />
              <Route path="/rooms/book" element={<BookRoomPage />} />
              <Route path="/rooms/vacate" element={<BookingActionPage key="vacate" action="vacate" />} />
              <Route path="/rooms/extend" element={<BookingActionPage key="extend" action="extend" />} />
              <Route path="/rooms/shift" element={<BookingActionPage key="shift" action="shift" />} />
              <Route path="/rooms/cancel" element={<BookingActionPage key="cancel" action="cancel" />} />
              <Route path="/rooms/partial-checkout" element={<BookingActionPage key="partial-checkout" action="partial-checkout" />} />
              <Route path="/rooms/add-room" element={<BookingActionPage key="add-room" action="add-room" />} />
              <Route path="/donations/new" element={<NewDonationPage />} />
              <Route path="/donations/historical" element={<HistoricalDonationPage />} />
              <Route path="/greetings" element={<SendGreetingsPage />} />
              <Route path="/admin/rooms" element={<RoomMasterPage />} />
              <Route path="/admin/donor-types" element={<DonorTypesPage />} />
              <Route path="/admin/members" element={<MembersPage />} />
              <Route path="/admin/users" element={<UsersPage />} />
              <Route path="/reports" element={<ReportsPage />} />
            </Route>
            <Route path="*" element={<Navigate to="/" replace />} />
          </Routes>
        </LookupsProvider>
      </AuthProvider>
    </BrandThemeProvider>
  );
}
