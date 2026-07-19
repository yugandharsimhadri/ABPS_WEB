import type { Page, Route } from '@playwright/test';

const API_BASE = 'http://localhost:5297/api';

function json(route: Route, body: unknown, status = 200) {
  return route.fulfill({ status, contentType: 'application/json', body: JSON.stringify(body) });
}

/** Intercepts a single endpoint, filtered by HTTP method (defaults to GET). The method filter
 * matters because several fixtures below share a URL substring with a write endpoint on the
 * same resource (e.g. GET and POST both hit `/admin/donor-types`) — without it, a read-shaped
 * fixture would also answer the write request. Traffic that doesn't match falls through to
 * `mockCoreApi`'s generic POST/PUT success handler (or further down the chain). */
export async function mockEndpoint(
  page: Page,
  matcher: string | RegExp,
  handler: (route: Route) => unknown | Promise<unknown>,
  method: 'GET' | 'POST' | 'PUT' = 'GET',
) {
  await page.route(`${API_BASE}/**`, async (route) => {
    if (route.request().method() !== method) {
      await route.fallback();
      return;
    }
    const url = route.request().url();
    const matches = typeof matcher === 'string' ? url.includes(matcher) : matcher.test(url);
    if (!matches) {
      await route.fallback();
      return;
    }
    const body = await handler(route);
    if (body !== undefined) await json(route, body);
  });
}

export const DEFAULT_LOOKUPS = {
  beds: [],
  countries: [{ id: 1, name: 'India' }],
  states: [{ stateId: 1, countryId: 1, stateName: 'Telangana' }],
  districts: [{ districtId: 1, stateId: 1, countryId: 1, districtName: 'Hyderabad' }],
  donationTypes: [{ id: 1, name: 'General' }],
  paymentTypes: [{ id: 1, name: 'Cash' }, { id: 2, name: 'Online' }],
  paymentSubTypes: [{ paymentSubTypeId: 1, paymentSubType: 'UPI', paymentTypeId: 2 }],
  occasions: [{ id: 1, name: 'Birthday' }],
  bookingCategories: [{ id: 1, name: 'Guest' }, { id: 2, name: 'Donar' }],
  smsTemplates: [],
  teluguMasams: [{ id: 1, name: 'Chaitra' }],
  teluguPakshams: [{ id: 1, name: 'Shukla' }],
  teluguThidis: [{ id: 1, name: 'Padyami' }],
  transactionTypes: [{ id: 1, name: 'Advance' }],
  transactionCategories: [{ id: 1, name: 'RoomBooking' }],
  bookingStatuses: [],
  roomStatuses: [{ id: 1, name: 'Available' }],
  roomTypes: [{ rtId: 1, rtDesc: 'AC Double', roomCategory: 'AC' }],
  floors: [{ floorId: 1, floorDescription: 'Ground Floor' }],
  userRoles: [{ id: 1, name: 'Admin' }, { id: 2, name: 'User' }],
};

export const DEFAULT_ROOMS = [
  { roomId: 1, roomCategory: 'AC', rtId: 1, roomNo: '101', rtPrice: 800, roomAdvance: 400, floorId: 1, floorDescription: 'Ground Floor', beds: 2, allowed: 3, isAvailable: true },
  { roomId: 2, roomCategory: 'Non-AC', rtId: 2, roomNo: '102', rtPrice: 500, roomAdvance: 250, floorId: 1, floorDescription: 'Ground Floor', beds: 2, allowed: 2, isAvailable: true },
];

export const DEFAULT_BOOKING_SUMMARY = {
  bookingId: 5001, bookingDate: '2026-07-15T10:00:00', name: 'Ravi Kumar', phoneNo: '9999999999',
  details: null, advance: 1000, totalAmount: 2600, checkinDateTime: '2026-07-15T10:00:00',
};

export const DEFAULT_BOOKING_ROOMS = [
  { roomId: 1, roomNo: '101', roomAmount: 800 },
  { roomId: 2, roomNo: '102', roomAmount: 500 },
];

/**
 * Wires up a full set of default-happy-path mocks: login always succeeds as the given user,
 * lookups/rooms/etc return small fixed fixtures. Call before navigating. Individual tests can
 * layer additional `mockEndpoint` calls afterwards to override specific routes (Playwright uses
 * the most-recently-registered matching handler first).
 */
export async function mockCoreApi(page: Page, opts: { roleName?: string } = {}) {
  const roleName = opts.roleName ?? 'Admin';

  // Playwright runs route handlers in reverse registration order (last registered runs
  // first), so the generic POST/PUT fallback is registered FIRST here — every mockEndpoint
  // call below it is registered later and therefore takes priority, falling back down to
  // this catch-all only when nothing more specific matched.
  await page.route(`${API_BASE}/**`, async (route) => {
    const method = route.request().method();
    if (method === 'POST' || method === 'PUT') {
      await json(route, { message: 'Success', bookingId: 9001, advanceBookingId: 1 });
      return;
    }
    await route.fallback();
  });

  await mockEndpoint(page, '/auth/login', () => ({ token: 'fake.jwt.token', username: 'testuser', roleId: 1, roleName }), 'POST');
  await mockEndpoint(page, '/lookups', () => DEFAULT_LOOKUPS);
  await mockEndpoint(page, '/rooms/counts/dorm-locker', () => ({ available: 3, occupied: 1 }));
  await mockEndpoint(page, '/rooms/counts', () => ({ available: 12, occupied: 5 }));
  await mockEndpoint(page, '/reports/expiring-bookings', () => [
    { bookingId: 101, name: 'Ravi Kumar', phoneNo: '9999999999', noOfRooms: 2, totalAmount: 2400, bookingLedgerId: 1, noOfDays: 2, checkinDateTime: '2026-07-16T10:00:00', checkoutDateTime: null, expectedCheckoutDate: '2026-07-18T10:00:00' },
  ]);
  await mockEndpoint(page, '/rooms/available/dorm-locker', () => []);
  await mockEndpoint(page, '/rooms/available', () => DEFAULT_ROOMS);
  await mockEndpoint(page, '/rooms/categories', () => [{ catId: 1, category: 'Guest' }, { catId: 2, category: 'Donar' }]);
  await mockEndpoint(page, '/rooms/by-room-no/', () => 5001);
  await mockEndpoint(page, /\/is-active/, () => true);
  await mockEndpoint(page, /\/rooms\/bookings\/\d+\/rooms/, () => DEFAULT_BOOKING_ROOMS);
  await mockEndpoint(page, /\/rooms\/bookings\/\d+$/, () => DEFAULT_BOOKING_SUMMARY);
  await mockEndpoint(page, '/rooms/donors/by-bond-no/', () => ({
    donarId: 7, name: 'Lakshmi Devi', teluguName: null, phoneNo: '8888888888', aadhaar: '123456789012',
    address: 'Hyderabad', bondNo: 'B001', roomDays: 8, donationAmount: 5000, donationDate: '2026-01-01T00:00:00',
    donationType: 'Maharaja Poshakulu', usedDays: 2,
  }));
  await mockEndpoint(page, '/rooms/guests/by-aadhaar/', () => ({ name: 'Prior Guest', phoneNo: '7777777777', address: 'Warangal', aadharNo: '999988887777' }));
  await mockEndpoint(page, '/admin/donor-types', () => [{ dtid: 1, donartype: 'One Day Annadanam', code: 'ODA', amount: 500, days: 1 }]);
  await mockEndpoint(page, '/admin/members/all', () => [
    { memberId: 1, memberName: 'Suresh Rao', memberTname: null, memberType: 'Executive', phone: '9000000000', email: 'suresh@example.com', address: 'Hyderabad', isActive: 'Y', effectiveFrom: '2026-01-01T00:00:00', effectiveTo: '2026-12-31T00:00:00' },
  ]);
  await mockEndpoint(page, '/admin/users', () => [
    { userId: 0, userName: 'Select User', email: null, mobileNumber: null, roleId: 0, isActive: null },
    { userId: 1, userName: 'admin1', email: 'admin1@example.com', mobileNumber: '9000000001', roleId: 1, isActive: 'Y' },
  ]);
  await mockEndpoint(page, '/admin/users/exists', () => false);
  await mockEndpoint(page, '/reports/rooms', () => [
    { roomId: 1, roomNo: '101', category: 'AC', floor: 'Ground Floor', beds: 2, allowed: 3, price: 800, status: 'Available', customerName: null, phoneNo: null, bookingDate: null, bookingId: null },
  ]);
  await mockEndpoint(page, '/reports/donations', () => [{ donarName: 'Lakshmi Devi', bondNo: 'B001', amount: 5000, donationDate: '2026-01-01T00:00:00' }]);
  await mockEndpoint(page, '/reports/transactions', () => [{ transactionId: 1, transactionDate: '2026-07-01T00:00:00', categoryName: 'RoomBooking', typeName: 'Advance', amount: 800 }]);
  await mockEndpoint(page, '/reports/occupancy', () => [{ roomCategory: 'AC', total: 10, occupied: 4, available: 6 }]);
  await mockEndpoint(page, '/reports/audit', () => [{ transactionId: 1, notes: 'Sample audit row' }]);
  await mockEndpoint(page, '/greetings/sent-today-count', () => 3);
  await mockEndpoint(page, '/greetings/recipients/by-date', () => [
    { name: 'Anitha', phoneNo: '9111111111', bondNo: 'B010', intheNameof: 'Late Rama Rao', ocassionName: 'Death Anniversary', ocassionId: 1 },
  ]);
  await mockEndpoint(page, '/greetings/send', () => ({ sent: 1, attempted: 1 }), 'POST');
  await mockEndpoint(page, '/donations/donors/search', () => [
    { donarId: 9, donarTypeId: 1, name: 'Search Result Donor', teluguName: null, gothram: null, phoneNo: '9222222222', emailId: null, pan: null, aadhaar: null, address: 'Nalgonda', districtId: 1, stateId: 1, countryId: 1 },
  ]);
}

/** Logs in through the real UI form (mocks must already be installed) and waits for the shell to render. */
export async function loginAs(page: Page, username = 'testuser', password = 'testpass123') {
  await page.goto('/login');
  await page.getByLabel('User name').fill(username);
  await page.getByLabel('Password').fill(password);
  await page.getByRole('button', { name: 'Sign in' }).click();
  await page.waitForURL('/');
}
