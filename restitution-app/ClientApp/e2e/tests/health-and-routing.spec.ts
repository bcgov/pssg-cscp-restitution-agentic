import { expect, test, type Page } from '@playwright/test';

const healthyHc = {
  status: 'Healthy',
  checks: [{ name: 'self', status: 'Healthy', description: 'ok' }]
};

const emptyLookup = { value: [] as unknown[] };

const defaultConfig = {
  outageStartDate: '',
  outageEndDate: '',
  outageMessage: '',
  maintenanceMode: false,
  featureFlags: { useUpdatedComplianceFields: false }
};

async function mockOfflineApis(page: Page, opts: { healthStatus?: number } = {}) {
  const healthStatus = opts.healthStatus ?? 200;

  await page.route('**/restwebforms/hc', async (route) => {
    if (healthStatus >= 400) {
      await route.fulfill({ status: healthStatus, body: 'unavailable' });
      return;
    }
    await route.fulfill({
      status: 200,
      contentType: 'application/json',
      body: JSON.stringify(healthyHc)
    });
  });

  await page.route('**/restwebforms/api/configuration', async (route) => {
    await route.fulfill({
      status: 200,
      contentType: 'application/json',
      body: JSON.stringify(defaultConfig)
    });
  });

  for (const path of ['countries', 'provinces', 'courts'] as const) {
    await page.route(`**/restwebforms/api/lookups/${path}`, async (route) => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify(emptyLookup)
      });
    });
  }
}

test.describe('Routing & health guard', () => {
  test.beforeEach(async ({ page }) => {
    await mockOfflineApis(page);
  });

  test('TC-HC-01: root path redirects to /victim', async ({ page }) => {
    // Path-relative so Playwright keeps /restwebforms/ baseHref.
    await page.goto('./');
    await expect(page).toHaveURL(/\/restwebforms\/victim/);
    // Angular Material 21 uses mat-stepper (not mat-vertical-stepper).
    await expect(page.locator('mat-stepper')).toBeVisible({ timeout: 15_000 });
  });

  test('TC-HC-02: /outage page shows service unavailable with contact details', async ({ page }) => {
    await page.unroute('**/restwebforms/hc');
    await mockOfflineApis(page, { healthStatus: 503 });
    await page.goto('./');
    await expect(page.locator('h1')).toContainText('Service Unavailable');
    await expect(page.getByText('604-660-4898')).toBeVisible();
    await expect(page.getByRole('link', { name: 'restitution@gov.bc.ca' })).toBeVisible();
  });

  test('TC-HC-03: unknown route renders the not-found page', async ({ page }) => {
    await page.goto('this-route-does-not-exist-xyz');
    await expect(page).not.toHaveURL(/outage/);
  });

  test('TC-HC-04: quick-exit element is always visible on form pages', async ({ page }) => {
    await page.goto('victim');
    await expect(page.locator('body')).toContainText('Click here to close this site quickly.');
  });

  // TC-HC-05
  for (const { route, label } of [
    { route: 'victim', label: 'Victim Application' },
    { route: 'offender', label: 'Offender Application' },
    { route: 'victim-entity', label: 'Entity Victim Application' }
  ] as const) {
    test(`TC-HC-05: /${route} renders stepper label "${label}"`, async ({ page }) => {
      await page.goto(route);
      await expect(page.getByRole('button', { name: label })).toBeVisible({ timeout: 15_000 });
    });
  }
});
