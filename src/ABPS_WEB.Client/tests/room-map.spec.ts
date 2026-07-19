import { test, expect } from '@playwright/test';
import { mockCoreApi, mockEndpoint, loginAs } from './fixtures/mockApi';

// bookingId 101 matches the expiring-bookings fixture in mockCoreApi, so room 102 must be
// classified "near checkout"; 555 does not, so room 201 stays plain "occupied".
const ROOMS = [
  { roomId: 1, roomNo: '101', category: 'AC', floor: 'Ground Floor', beds: 2, allowed: 3, price: 800, status: 'Available', customerName: null, phoneNo: null, bookingDate: null, bookingId: null },
  { roomId: 2, roomNo: '102', category: 'NON', floor: 'Ground Floor', beds: 2, allowed: 2, price: 500, status: 'Occupied', customerName: 'Ravi Kumar', phoneNo: '9999999999', bookingDate: '2026-07-16T10:00:00', bookingId: 101 },
  { roomId: 3, roomNo: '201', category: 'AC', floor: 'First Floor', beds: 2, allowed: 3, price: 900, status: 'Occupied', customerName: 'Sita Devi', phoneNo: '8888888888', bookingDate: '2026-07-16T10:00:00', bookingId: 555 },
];

test.describe('Room Map', () => {
  test.beforeEach(async ({ page }) => {
    await mockCoreApi(page);
    await mockEndpoint(page, '/reports/rooms', () => ROOMS);
    await loginAs(page);
    await page.getByRole('link', { name: 'Room Map', exact: true }).click();
    await expect(page.getByRole('heading', { name: 'Room Map' })).toBeVisible();
  });

  test('groups rooms by floor with availability counts', async ({ page }) => {
    await expect(page.getByRole('heading', { name: 'Ground Floor' })).toBeVisible();
    await expect(page.getByRole('heading', { name: 'First Floor' })).toBeVisible();
    await expect(page.getByText('1 of 2 available')).toBeVisible();
    await expect(page.getByText('0 of 1 available')).toBeVisible();
  });

  test('classifies rooms as available, near-checkout, and occupied', async ({ page }) => {
    await expect(page.locator('.room-map-tile.available')).toHaveCount(1);
    await expect(page.locator('.room-map-tile.expiring')).toHaveCount(1);
    await expect(page.locator('.room-map-tile.occupied')).toHaveCount(1);
  });

  test('clicking an occupied room shows guest details and vacate/extend actions', async ({ page }) => {
    await page.locator('.room-map-tile.expiring').click();
    await expect(page.getByText('Ravi Kumar')).toBeVisible();
    await expect(page.getByText('₹500')).toBeVisible();
    await expect(page.getByRole('button', { name: 'Vacate' })).toBeVisible();
    await expect(page.getByRole('button', { name: 'Extend stay' })).toBeVisible();
  });

  test('clicking an available room offers booking', async ({ page }) => {
    await page.locator('.room-map-tile.available').click();
    await expect(page.getByRole('button', { name: 'Book a room' })).toBeVisible();
  });

  test('filter narrows the map to matching rooms', async ({ page }) => {
    await page.getByLabel('Filter rooms').fill('201');
    await expect(page.locator('.room-map-tile')).toHaveCount(1);
    await expect(page.getByRole('heading', { name: 'Ground Floor' })).toHaveCount(0);
  });
});
