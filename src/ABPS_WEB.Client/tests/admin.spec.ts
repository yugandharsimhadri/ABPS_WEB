import { expect, test } from '@playwright/test';
import { loginAs, mockCoreApi } from './fixtures/mockApi';

test.describe('Donation Types', () => {
  test.beforeEach(async ({ page }) => {
    await mockCoreApi(page);
    await loginAs(page);
    await page.getByRole('link', { name: 'Donation Types', exact: true }).click();
    await expect(page.getByRole('heading', { name: 'Donation Types' })).toBeVisible();
  });

  test('lists existing donation types', async ({ page }) => {
    await expect(page.getByText('One Day Annadanam')).toBeVisible();
    await expect(page.getByText('₹500')).toBeVisible();
  });

  test('Edit prefills the form and New clears it', async ({ page }) => {
    await page.getByRole('button', { name: 'Edit' }).click();
    await expect(page.getByLabel('Name')).toHaveValue('One Day Annadanam');
    await expect(page.getByLabel('Code')).toHaveValue('ODA');
    await expect(page.getByRole('heading', { name: /Editing #1/ })).toBeVisible();

    await page.getByRole('button', { name: 'New' }).click();
    await expect(page.getByLabel('Name')).toHaveValue('');
    await expect(page.getByRole('heading', { name: 'New Donation Type' })).toBeVisible();
  });

  test('name shorter than 3 characters is rejected', async ({ page }) => {
    await page.getByLabel('Name').fill('AB');
    await page.getByLabel('Code').fill('X');
    await page.getByRole('button', { name: 'Save' }).click();
    await expect(page.getByText('Donation type name must be at least 3 characters.')).toBeVisible();
  });

  test('saving a valid new donation type succeeds', async ({ page }) => {
    await page.getByLabel('Name').fill('Special Donation');
    await page.getByLabel('Code').fill('SPD');
    await page.getByRole('button', { name: 'Save' }).click();
    await expect(page.getByText('Success')).toBeVisible();
  });
});

test.describe('Executive Members', () => {
  test.beforeEach(async ({ page }) => {
    await mockCoreApi(page);
    await loginAs(page);
    await page.getByRole('link', { name: 'Executive Members', exact: true }).click();
    await expect(page.getByRole('heading', { name: 'Executive Members' })).toBeVisible();
  });

  test('lists members and allows editing', async ({ page }) => {
    await expect(page.getByText('Suresh Rao')).toBeVisible();
    await page.getByRole('button', { name: 'Edit' }).click();
    await expect(page.getByLabel('Name', { exact: true })).toHaveValue('Suresh Rao');
    await expect(page.getByLabel('Member Type')).toHaveValue('Executive');
  });

  test('requires a member type before saving', async ({ page }) => {
    await page.getByLabel('Name', { exact: true }).fill('New Member Name');
    await page.getByLabel('Phone').fill('9000000002');
    await page.getByLabel('Email').fill('new@example.com');
    await page.getByLabel('Address').fill('Some address');
    await page.getByRole('button', { name: 'Save' }).click();
    await expect(page.getByText('Please select a member type.')).toBeVisible();
  });

  test('effective-from after effective-to is rejected', async ({ page }) => {
    await page.getByLabel('Name', { exact: true }).fill('New Member Name');
    await page.getByLabel('Phone').fill('9000000002');
    await page.getByLabel('Email').fill('new@example.com');
    await page.getByLabel('Address').fill('Some address');
    await page.getByLabel('Member Type').selectOption({ label: 'General' });
    await page.getByLabel('Effective From').fill('2027-01-01');
    await page.getByLabel('Effective To').fill('2026-01-01');
    await page.getByRole('button', { name: 'Save' }).click();
    await expect(page.getByText('Effective From must be earlier than Effective To.')).toBeVisible();
  });
});

test.describe('User Registration', () => {
  test.beforeEach(async ({ page }) => {
    await mockCoreApi(page);
    await loginAs(page);
    await page.getByRole('link', { name: 'User Registration', exact: true }).click();
    await expect(page.getByRole('heading', { name: 'User Registration' })).toBeVisible();
  });

  test('lists real users but omits the "Select User" placeholder row', async ({ page }) => {
    await expect(page.getByRole('cell', { name: 'admin1', exact: true })).toBeVisible();
    await expect(page.getByText('Select User')).not.toBeVisible();
  });

  test('new user requires a password', async ({ page }) => {
    await page.getByLabel('User Name').fill('newuser');
    await page.getByLabel('Role').selectOption({ label: 'Admin' });
    await page.getByRole('button', { name: 'Save' }).click();
    await expect(page.getByText('Password is required.')).toBeVisible();
  });

  test('editing an existing user disables the User Name field and re-requires a password', async ({ page }) => {
    await page.getByRole('button', { name: 'Edit' }).click();
    await expect(page.getByLabel('User Name')).toBeDisabled();
    await expect(page.getByText(/Must re-enter to update/)).toBeVisible();
  });
});

test.describe('Room Master', () => {
  test('lists physical rooms with category, floor, and price', async ({ page }) => {
    await mockCoreApi(page);
    await loginAs(page);
    await page.getByRole('link', { name: 'Room Master', exact: true }).click();
    await expect(page.getByRole('heading', { name: 'Room Master' })).toBeVisible();
    await expect(page.getByText('Air Conditioned')).toBeVisible();
    await expect(page.getByRole('cell', { name: 'Ground Floor', exact: true })).toBeVisible();
    await expect(page.getByText('₹800')).toBeVisible();
  });

  test('New Room requires type/floor/status selection', async ({ page }) => {
    await mockCoreApi(page);
    await loginAs(page);
    await page.getByRole('link', { name: 'Room Master', exact: true }).click();
    await page.getByLabel('Room No').fill('201');
    await page.getByRole('button', { name: 'Save' }).click();
    await expect(page.getByText('Select a room type.')).toBeVisible();
  });
});
