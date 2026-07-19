import { expect, test } from '@playwright/test';
import { loginAs, mockCoreApi } from './fixtures/mockApi';

test.describe('Login', () => {
  test('renders the sign-in form', async ({ page }) => {
    await page.goto('/login');
    await expect(page.getByRole('heading', { name: 'Akhila Bharatha Baktha Markandeya Satram, Srisailam' })).toBeVisible();
    await expect(page.getByLabel('User name')).toBeVisible();
    await expect(page.getByLabel('Password')).toBeVisible();
    await expect(page.getByRole('button', { name: 'Sign in' })).toBeVisible();
  });

  test('shows a validation banner when submitted empty', async ({ page }) => {
    await page.goto('/login');
    await page.getByRole('button', { name: 'Sign in' }).click();
    await expect(page.getByText('Enter a user name and password.')).toBeVisible();
  });

  test('shows the API error message on invalid credentials', async ({ page }) => {
    // Standalone 401 mock (no mockCoreApi) — this test only needs the login endpoint, and
    // avoids relying on route-registration order against mockCoreApi's own login mock. Must
    // only intercept POST: the browser also sends a CORS preflight OPTIONS to this same URL,
    // and fulfilling that with 401 fails the preflight, silently blocking the real POST.
    await page.route('**/api/auth/login', (route) => {
      if (route.request().method() !== 'POST') return route.fallback();
      return route.fulfill({ status: 401, contentType: 'application/json', body: JSON.stringify({ message: 'Invalid user name or password.' }) });
    });
    await page.goto('/login');
    await page.getByLabel('User name').fill('baduser');
    await page.getByLabel('Password').fill('badpass');
    await page.getByRole('button', { name: 'Sign in' }).click();
    await expect(page.getByText('Invalid user name or password.')).toBeVisible();
    await expect(page).toHaveURL(/\/login$/);
  });

  test('redirects to dashboard on successful login and shows user chip', async ({ page }) => {
    await mockCoreApi(page);
    await loginAs(page);
    await expect(page.getByRole('heading', { name: 'Dashboard' })).toBeVisible();
    await expect(page.getByText('testuser · Admin')).toBeVisible();
  });

  test('logout returns to the login page', async ({ page }) => {
    await mockCoreApi(page);
    await loginAs(page);
    await page.getByRole('button', { name: 'Sign out' }).click();
    await expect(page).toHaveURL(/\/login$/);
    await expect(page.getByRole('button', { name: 'Sign in' })).toBeVisible();
  });
});

test.describe('Route protection', () => {
  test('unauthenticated access to a protected route redirects to login', async ({ page }) => {
    await page.goto('/rooms/book');
    await expect(page).toHaveURL(/\/login$/);
  });

  test('unauthenticated access to the dashboard root redirects to login', async ({ page }) => {
    await page.goto('/');
    await expect(page).toHaveURL(/\/login$/);
  });
});

test.describe('Sidebar navigation', () => {
  test.beforeEach(async ({ page }) => {
    await mockCoreApi(page);
    await loginAs(page);
  });

  const screens: [string, string][] = [
    ['Book Room', 'Book Room'],
    ['Vacate Room', 'Vacate Room'],
    ['Extend Room', 'Extend Room'],
    ['Shift Room', 'Shift Room'],
    ['Cancel Room', 'Cancel Room'],
    ['Partial Checkout', 'Partial Checkout'],
    ['Add Room', 'Add Room'],
    ['New Donation', 'New Donation'],
    ['Historical Donation', 'Historical Donation'],
    ['Send Greetings', 'Send Greetings'],
    ['Room Master', 'Room Master'],
    ['Donation Types', 'Donation Types'],
    ['Executive Members', 'Executive Members'],
    ['User Registration', 'User Registration'],
    ['Reports', 'Reports'],
  ];

  for (const [linkText, heading] of screens) {
    test(`navigating to "${linkText}" renders its page`, async ({ page }) => {
      await page.getByRole('link', { name: linkText, exact: true }).click();
      await expect(page.getByRole('heading', { name: heading, exact: true })).toBeVisible();
    });
  }

  test('Dashboard link returns from a nested screen', async ({ page }) => {
    await page.getByRole('link', { name: 'Book Room', exact: true }).click();
    await page.getByRole('link', { name: 'Dashboard', exact: true }).click();
    await expect(page.getByRole('heading', { name: 'Dashboard' })).toBeVisible();
    await expect(page).toHaveURL('/');
  });
});
