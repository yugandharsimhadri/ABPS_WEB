import { expect, test } from '@playwright/test';
import { loginAs, mockCoreApi } from './fixtures/mockApi';

test.describe('New Donation', () => {
  test.beforeEach(async ({ page }) => {
    await mockCoreApi(page);
    await loginAs(page);
    await page.getByRole('link', { name: 'New Donation', exact: true }).click();
    await expect(page.getByRole('heading', { name: 'New Donation' })).toBeVisible();
  });

  test('defaults to the New Donor form', async ({ page }) => {
    await expect(page.getByLabel('Donation Type')).toBeVisible();
    await expect(page.getByLabel('Name', { exact: true })).toBeVisible();
  });

  test('switching to Existing Donor swaps the identity fields for a search box', async ({ page }) => {
    await page.getByRole('radio', { name: 'Existing Donor' }).check();
    await expect(page.getByLabel('Search by Phone No')).toBeVisible();
    await expect(page.getByLabel('Donation Type')).not.toBeVisible();
  });

  test('searching and selecting an existing donor shows the selection banner', async ({ page }) => {
    await page.getByRole('radio', { name: 'Existing Donor' }).check();
    await page.getByLabel('Search by Phone No').fill('9222222222');
    await page.getByRole('button', { name: 'Search' }).click();
    await expect(page.getByText('Search Result Donor')).toBeVisible();
    await page.getByText('Search Result Donor').click();
    await expect(page.getByText('Selected: Search Result Donor (9222222222)')).toBeVisible();
  });

  test('submitting without a donor name shows a validation error', async ({ page }) => {
    await page.getByLabel('Amount').fill('500');
    await page.getByRole('button', { name: 'Save Donation' }).click();
    await expect(page.getByText('Donor name is required.')).toBeVisible();
  });

  test('submitting without an amount shows a validation error', async ({ page }) => {
    await page.getByLabel('Name', { exact: true }).fill('Test Donor');
    await page.getByRole('button', { name: 'Save Donation' }).click();
    await expect(page.getByText('Enter a donation amount.')).toBeVisible();
  });

  test('submitting a valid new donor donation succeeds', async ({ page }) => {
    await page.getByLabel('Name', { exact: true }).fill('Test Donor');
    await page.getByLabel('Amount').fill('500');
    await page.getByRole('button', { name: 'Save Donation' }).click();
    await expect(page.getByText('Success')).toBeVisible();
  });

  test('Telugu Calendar toggle swaps the date field for masam/paksham/thidi selects', async ({ page }) => {
    await expect(page.getByLabel('Anandanam Date')).toBeVisible();
    await page.getByRole('radio', { name: 'Telugu Calendar' }).check();
    await expect(page.getByLabel('Anandanam Date')).not.toBeVisible();
    await expect(page.getByLabel('Telugu Masam')).toBeVisible();
    await expect(page.getByLabel('Paksham')).toBeVisible();
    await expect(page.getByLabel('Thidi')).toBeVisible();
  });
});

test.describe('Historical Donation', () => {
  test('renders with the historical-specific subtitle', async ({ page }) => {
    await mockCoreApi(page);
    await loginAs(page);
    await page.getByRole('link', { name: 'Historical Donation', exact: true }).click();
    await expect(page.getByRole('heading', { name: 'Historical Donation' })).toBeVisible();
    await expect(page.getByText('Back-date a donation record.')).toBeVisible();
  });
});
