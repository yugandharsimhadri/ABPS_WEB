import { expect, test } from '@playwright/test';
import { loginAs, mockCoreApi } from './fixtures/mockApi';

test.describe('Book Room', () => {
  test.beforeEach(async ({ page }) => {
    await mockCoreApi(page);
    await loginAs(page);
    await page.getByRole('link', { name: 'Book Room', exact: true }).click();
    await expect(page.getByRole('heading', { name: 'Book Room' })).toBeVisible();
  });

  test('lists available rooms grouped by category with price', async ({ page }) => {
    await expect(page.getByText('101')).toBeVisible();
    await expect(page.getByText('₹800/day')).toBeVisible();
    await expect(page.getByText('102')).toBeVisible();
  });

  test('selecting a room tile updates the confirm button count', async ({ page }) => {
    await expect(page.getByRole('button', { name: /Confirm Booking \(0 rooms\)/ })).toBeVisible();
    await page.getByText('101', { exact: true }).click();
    await expect(page.getByRole('button', { name: /Confirm Booking \(1 room\)/ })).toBeVisible();
  });

  test('single-select mode replaces the previous selection', async ({ page }) => {
    await page.getByText('101', { exact: true }).click();
    await page.getByText('102', { exact: true }).click();
    await expect(page.getByRole('button', { name: /Confirm Booking \(1 room\)/ })).toBeVisible();
  });

  test('"Select multiple rooms" allows choosing more than one', async ({ page }) => {
    await page.getByLabel('Select multiple rooms').check();
    await page.getByText('101', { exact: true }).click();
    await page.getByText('102', { exact: true }).click();
    await expect(page.getByRole('button', { name: /Confirm Booking \(2 rooms\)/ })).toBeVisible();
  });

  test('switching category to Donar reveals the Bond No field', async ({ page }) => {
    await expect(page.getByLabel('Bond No')).not.toBeVisible();
    await page.getByLabel('Category').selectOption({ label: 'Donar' });
    await expect(page.getByLabel('Bond No')).toBeVisible();
  });

  test('Bond No lookup shows the donor allowance banner and autofills guest fields', async ({ page }) => {
    await page.getByLabel('Category').selectOption({ label: 'Donar' });
    await page.getByLabel('Bond No').fill('B001');
    await page.getByRole('button', { name: 'Find' }).first().click();
    await expect(page.getByText(/Maharaja Poshakulu.*8 day\(s\) remaining.*2 used/)).toBeVisible();
    await expect(page.getByLabel('Guest Name')).toHaveValue('Lakshmi Devi');
    await expect(page.getByLabel('Phone No')).toHaveValue('8888888888');
  });

  test('Aadhaar lookup autofills a repeat guest', async ({ page }) => {
    await page.getByLabel('Aadhaar No').fill('999988887777');
    await page.getByRole('button', { name: 'Find' }).click();
    await expect(page.getByText('Found a previous guest record')).toBeVisible();
    await expect(page.getByLabel('Guest Name')).toHaveValue('Prior Guest');
    await expect(page.getByLabel('Phone No')).toHaveValue('7777777777');
  });

  test('selecting rooms prefills total amount and advance from room prices', async ({ page }) => {
    await page.getByLabel('Select multiple rooms').check();
    await page.getByText('101', { exact: true }).click();
    await page.getByText('102', { exact: true }).click();
    await expect(page.getByLabel('Total Amount')).toHaveValue('1300');
    await expect(page.getByLabel('Advance')).toHaveValue('650');
  });

  test('submitting a valid booking shows a success banner and resets the form', async ({ page }) => {
    await page.getByText('101', { exact: true }).click();
    await page.getByLabel('Aadhaar No').fill('123412341234');
    await page.getByLabel('Guest Name').fill('Test Guest');
    await page.getByLabel('Phone No').fill('9123456780');
    await page.getByRole('button', { name: /Confirm Booking/ }).click();
    await expect(page.getByText(/Success — Booking #9001/)).toBeVisible();
    await expect(page.getByRole('button', { name: /Confirm Booking \(0 rooms\)/ })).toBeVisible();
  });

  test('Print Receipt opens the letterhead receipt overlay after booking', async ({ page }) => {
    await page.getByText('101', { exact: true }).click();
    await page.getByLabel('Aadhaar No').fill('123412341234');
    await page.getByLabel('Guest Name').fill('Test Guest');
    await page.getByLabel('Phone No').fill('9123456780');
    await page.getByRole('button', { name: /Confirm Booking/ }).click();
    await page.getByRole('button', { name: 'Print Receipt' }).click();
    const receipt = page.getByRole('dialog', { name: 'Room Booking Receipt' });
    await expect(receipt).toBeVisible();
    await expect(receipt.getByText('Akhila Bharatha Bhaktha Markandeya Padmashali Nitya Anna Satram, Srisailam')).toBeVisible();
    await expect(receipt.getByText(/Regd\. No\. 985\/84/)).toBeVisible();
    await expect(receipt.getByText('Test Guest')).toBeVisible();
    // room 101's advance (₹400) is prefilled on selection and becomes the received amount
    await expect(receipt.getByText('Rupees Four Hundred Only')).toBeVisible();
    await receipt.getByRole('button', { name: 'Close' }).click();
    await expect(page.getByRole('dialog')).toHaveCount(0);
  });

  test('submitting without required guest fields shows a validation error', async ({ page }) => {
    await page.getByText('101', { exact: true }).click();
    await page.getByRole('button', { name: /Confirm Booking/ }).click();
    await expect(page.getByText('Guest name, phone, and Aadhaar are required.')).toBeVisible();
  });

  test('submitting with no room selected shows a validation error', async ({ page }) => {
    await page.getByRole('button', { name: /Confirm Booking/ }).click();
    await expect(page.getByText('Select at least one room.')).toBeVisible();
  });
});
