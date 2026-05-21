import { expect, test } from '@playwright/test';

test.describe('Routing & health guard', () => {
  test('TC-HC-01: root path redirects to /victim', async ({ page }) => {
    await page.goto('.');
    await expect(page).toHaveURL(/victim/);
    await expect(page.locator('mat-vertical-stepper')).toBeVisible({ timeout: 15_000 });
  });

  test('TC-HC-02: /outage page shows service unavailable with contact details', async ({ page }) => {
    // Simulate a failing health check so the app initializer navigates to /outage
    await page.route('**/restwebforms/hc', (route) => route.fulfill({ status: 503 }));
    await page.goto('.');
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
