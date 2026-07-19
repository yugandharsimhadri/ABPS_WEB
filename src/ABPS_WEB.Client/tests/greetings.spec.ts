import { expect, test } from '@playwright/test';
import { loginAs, mockCoreApi } from './fixtures/mockApi';

test.describe('Send Greetings', () => {
  test.beforeEach(async ({ page }) => {
    await mockCoreApi(page);
    await loginAs(page);
    await page.getByRole('link', { name: 'Send Greetings', exact: true }).click();
    await expect(page.getByRole('heading', { name: 'Send Greetings' })).toBeVisible();
  });

  test('shows the sent-today count on load', async ({ page }) => {
    await expect(page.locator('.stat-tile .value')).toHaveText('3');
    await expect(page.getByText('Sent Today')).toBeVisible();
  });

  test('defaults to English Date mode', async ({ page }) => {
    await expect(page.getByLabel('Date', { exact: true })).toBeVisible();
  });

  test('Telugu Calendar mode swaps in masam/paksham/thidi selects', async ({ page }) => {
    await page.getByRole('radio', { name: 'Telugu Calendar' }).check();
    await expect(page.getByLabel('Date', { exact: true })).not.toBeVisible();
    await expect(page.getByLabel('Masam')).toBeVisible();
    await expect(page.getByLabel('Paksham')).toBeVisible();
    await expect(page.getByLabel('Thidi')).toBeVisible();
  });

  test('Find Recipients populates the grid', async ({ page }) => {
    await expect(page.getByText('0 donor(s) found')).toBeVisible();
    await page.getByRole('button', { name: 'Find Recipients' }).click();
    await expect(page.getByText('1 donor(s) found')).toBeVisible();
    await expect(page.getByText('Anitha')).toBeVisible();
    await expect(page.getByText('Death Anniversary')).toBeVisible();
  });

  test('Send to Selected is disabled until recipients are found', async ({ page }) => {
    await expect(page.getByRole('button', { name: 'Send to Selected' })).toBeDisabled();
    await page.getByRole('button', { name: 'Find Recipients' }).click();
    await expect(page.getByRole('button', { name: 'Send to Selected' })).toBeEnabled();
  });

  test('unchecking the only recipient then sending shows a validation error', async ({ page }) => {
    await page.getByRole('button', { name: 'Find Recipients' }).click();
    await page.getByRole('checkbox').uncheck();
    await page.getByRole('button', { name: 'Send to Selected' }).click();
    await expect(page.getByText('Select at least one recipient.')).toBeVisible();
  });

  test('sending shows the sent/attempted result and clears the grid', async ({ page }) => {
    await page.getByRole('button', { name: 'Find Recipients' }).click();
    await page.getByRole('button', { name: 'Send to Selected' }).click();
    await expect(page.getByText('Sent 1 of 1 selected.')).toBeVisible();
    await expect(page.getByText('0 donor(s) found')).toBeVisible();
  });
});
