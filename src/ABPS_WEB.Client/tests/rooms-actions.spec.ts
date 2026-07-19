import { expect, test } from '@playwright/test';
import { loginAs, mockCoreApi } from './fixtures/mockApi';

async function searchBooking(page: import('@playwright/test').Page) {
  await page.getByLabel('Room No. or Booking ID').fill('101');
  await page.getByRole('button', { name: 'Search' }).click();
  await expect(page.getByText(/Ravi Kumar.*Booking #5001/)).toBeVisible();
}

test.describe('Vacate Room', () => {
  test.beforeEach(async ({ page }) => {
    await mockCoreApi(page);
    await loginAs(page);
    await page.getByRole('link', { name: 'Vacate Room', exact: true }).click();
  });

  test('searching an unknown room shows an error', async ({ page }) => {
    await page.route('**/api/rooms/by-room-no/**', (route) => route.fulfill({ status: 200, contentType: 'application/json', body: '0' }));
    await page.route('**/is-active', (route) => route.fulfill({ status: 200, contentType: 'application/json', body: 'false' }));
    await page.getByLabel('Room No. or Booking ID').fill('999');
    await page.getByRole('button', { name: 'Search' }).click();
    await expect(page.getByText('No active booking found for that room/booking ID.')).toBeVisible();
  });

  test('searching a valid room shows the booking summary and settlement fields', async ({ page }) => {
    await searchBooking(page);
    await expect(page.getByLabel('Adjusted Total Amount')).toHaveValue('2600');
    await expect(page.getByText('Additional payable: ₹1600.00')).toBeVisible();
  });

  test('lowering the adjusted amount below advance shows a refund instead of a payable', async ({ page }) => {
    await searchBooking(page);
    await page.getByLabel('Adjusted Total Amount').fill('700');
    await expect(page.getByText('Refund due: ₹300.00')).toBeVisible();
  });

  test('"Change" clears the search and returns to a fresh lookup', async ({ page }) => {
    await searchBooking(page);
    await page.getByRole('button', { name: 'Change' }).click();
    await expect(page.getByLabel('Room No. or Booking ID')).toHaveValue('');
    await expect(page.getByRole('button', { name: 'Search' })).toBeVisible();
  });

  test('confirming vacate shows a success banner', async ({ page }) => {
    await searchBooking(page);
    await page.getByRole('button', { name: 'Confirm Vacate Room' }).click();
    await expect(page.getByText('Success')).toBeVisible();
  });
});

test.describe('Extend Room', () => {
  test.beforeEach(async ({ page }) => {
    await mockCoreApi(page);
    await loginAs(page);
    await page.getByRole('link', { name: 'Extend Room', exact: true }).click();
    await searchBooking(page);
  });

  test('shows extend-specific fields, not vacate fields', async ({ page }) => {
    await expect(page.getByLabel('Extend By (days)')).toBeVisible();
    await expect(page.getByLabel('New Checkout Date/Time')).toBeVisible();
    await expect(page.getByLabel('Additional Advance')).toBeVisible();
    await expect(page.getByLabel('Adjusted Total Amount')).not.toBeVisible();
  });
});

test.describe('Shift Room', () => {
  test.beforeEach(async ({ page }) => {
    await mockCoreApi(page);
    await loginAs(page);
    await page.getByRole('link', { name: 'Shift Room', exact: true }).click();
    await searchBooking(page);
  });

  test('shows from/to room selects populated from the booking and available rooms', async ({ page }) => {
    await expect(page.getByLabel('From Room')).toBeVisible();
    await expect(page.getByLabel('From Room')).toContainText('101');
    await expect(page.getByLabel('To Room')).toBeVisible();
    await expect(page.getByLabel('To Room')).toContainText('101');
  });
});

test.describe('Cancel Room', () => {
  test.beforeEach(async ({ page }) => {
    await mockCoreApi(page);
    await loginAs(page);
    await page.getByRole('link', { name: 'Cancel Room', exact: true }).click();
    await searchBooking(page);
  });

  test('lists both booking rooms pre-selected', async ({ page }) => {
    await expect(page.getByRole('checkbox', { name: '101' })).toBeChecked();
    await expect(page.getByRole('checkbox', { name: '102' })).toBeChecked();
  });

  test('full cancellation toggle reveals refund/payment fields', async ({ page }) => {
    await expect(page.getByLabel('Refund Amount')).not.toBeVisible();
    await page.getByLabel(/Full cancellation with settlement/).check();
    await expect(page.getByLabel('Refund Amount')).toBeVisible();
    await expect(page.getByLabel('Additional Payment')).toBeVisible();
  });

  test('confirming a partial cancellation (one room unchecked) succeeds', async ({ page }) => {
    await page.getByRole('checkbox', { name: '102' }).uncheck();
    await page.getByRole('button', { name: 'Confirm Cancel Room' }).click();
    await expect(page.getByText('Success')).toBeVisible();
  });
});

test.describe('Partial Checkout', () => {
  test.beforeEach(async ({ page }) => {
    await mockCoreApi(page);
    await loginAs(page);
    await page.getByRole('link', { name: 'Partial Checkout', exact: true }).click();
    await searchBooking(page);
  });

  test('allows unchecking a room before confirming', async ({ page }) => {
    await page.getByRole('checkbox', { name: '101' }).uncheck();
    await expect(page.getByRole('checkbox', { name: '101' })).not.toBeChecked();
    await expect(page.getByRole('checkbox', { name: '102' })).toBeChecked();
  });
});

test.describe('Add Room', () => {
  test.beforeEach(async ({ page }) => {
    await mockCoreApi(page);
    await loginAs(page);
    await page.getByRole('link', { name: 'Add Room', exact: true }).click();
    await searchBooking(page);
  });

  test('lists available rooms to add, unrelated to the current booking rooms', async ({ page }) => {
    await expect(page.getByText('101 (₹800/day)')).toBeVisible();
    await expect(page.getByText('102 (₹500/day)')).toBeVisible();
  });
});

test.describe('Action page state isolation (regression)', () => {
  test('switching from Vacate to Cancel does not carry over the previous search', async ({ page }) => {
    await mockCoreApi(page);
    await loginAs(page);

    await page.getByRole('link', { name: 'Vacate Room', exact: true }).click();
    await searchBooking(page);

    await page.getByRole('link', { name: 'Cancel Room', exact: true }).click();
    await expect(page.getByRole('heading', { name: 'Cancel Room' })).toBeVisible();
    await expect(page.getByLabel('Room No. or Booking ID')).toHaveValue('');
    await expect(page.getByText(/Ravi Kumar/)).not.toBeVisible();
    await expect(page.getByRole('button', { name: 'Search' })).toBeVisible();
  });
});
