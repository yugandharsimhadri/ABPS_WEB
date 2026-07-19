import { expect, test } from '@playwright/test';
import { loginAs, mockCoreApi } from './fixtures/mockApi';

test.describe('Reports', () => {
  test.beforeEach(async ({ page }) => {
    await mockCoreApi(page);
    await loginAs(page);
    await page.getByRole('link', { name: 'Reports', exact: true }).click();
    await expect(page.getByRole('heading', { name: 'Reports' })).toBeVisible();
  });

  test('shows all report tabs and defaults to Donation Report', async ({ page }) => {
    for (const label of ['Donation Report', 'Transaction Report', 'Audit Transaction Report', 'Occupancy Report', 'Expiring Bookings']) {
      await expect(page.getByText(label, { exact: true })).toBeVisible();
    }
    await expect(page.getByText('Choose filters and run the report.')).toBeVisible();
  });

  test('running the Donation Report renders a dynamic table with the returned columns', async ({ page }) => {
    await page.getByRole('button', { name: 'Run Report' }).click();
    await expect(page.getByRole('columnheader', { name: 'Donar Name' })).toBeVisible();
    await expect(page.getByRole('columnheader', { name: 'Bond No' })).toBeVisible();
    await expect(page.getByText('Lakshmi Devi')).toBeVisible();
  });

  test('Transaction Report tab reveals category/type filters', async ({ page }) => {
    await page.getByText('Transaction Report', { exact: true }).click();
    await expect(page.getByLabel('Category')).toBeVisible();
    await expect(page.getByLabel('Type')).toBeVisible();
  });

  test('Occupancy Report has no date range filter and runs directly', async ({ page }) => {
    await page.getByText('Occupancy Report', { exact: true }).click();
    await expect(page.getByLabel('From')).not.toBeVisible();
    await page.getByRole('button', { name: 'Run Report' }).click();
    await expect(page.getByRole('cell', { name: 'AC', exact: true })).toBeVisible();
  });

  test('switching tabs clears previous results', async ({ page }) => {
    await page.getByRole('button', { name: 'Run Report' }).click();
    await expect(page.getByText('Lakshmi Devi')).toBeVisible();
    await page.getByText('Occupancy Report', { exact: true }).click();
    await expect(page.getByText('Choose filters and run the report.')).toBeVisible();
  });

  test('a report with zero results shows a friendly empty message', async ({ page }) => {
    await page.route('**/api/reports/donations**', (route) => route.fulfill({ status: 200, contentType: 'application/json', body: '[]' }));
    await page.getByRole('button', { name: 'Run Report' }).click();
    await expect(page.getByText('No records found.')).toBeVisible();
  });
});
