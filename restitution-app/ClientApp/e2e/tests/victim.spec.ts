import { expect, test } from '@playwright/test';
import { VICTIM_DATA } from '../fixtures/test-data';
import {
  acceptDeclaration,
  continueToApplication,
  drawSignature,
  fillContactInfo,
  fillPersonalInfo,
  hasErrorSummary,
  navigateToForm,
  triggerValidation
} from '../helpers/form.helpers';

test.describe('Victim form', () => {
  test('TC-V-01: overview page loads with correct heading and stepper', async ({ page }) => {
    await navigateToForm(page, 'victim');

    await expect(page.locator('h1')).toContainText('Information for Victims Applying');
    await expect(page.getByRole('button', { name: 'Overview' })).toBeVisible();
    await expect(page.getByRole('button', { name: 'Victim Application' })).toBeDisabled();
    await expect(page.getByRole('button', { name: 'Review & Submit' })).toBeDisabled();
  });

  test('TC-V-02: CONTINUE from overview navigates to victim application form', async ({ page }) => {
    await navigateToForm(page, 'victim');
    await continueToApplication(page);

    await expect(page.locator('h1')).toContainText('Victim Application');
    await expect(page.locator('h2').filter({ hasText: 'Victim Information' })).toBeVisible();
    await expect(page.locator('h3').filter({ hasText: 'Authorized Victim Designate' })).toBeVisible();
    await expect(page.getByRole('heading', { name: 'Contact Information', exact: true })).toBeVisible();
    await expect(page.locator('h2').filter({ hasText: 'Restitution Order Information' })).toBeVisible();
    await expect(page.locator('h2').filter({ hasText: 'Victim Service Worker' })).toBeVisible();
    await expect(page.locator('h2').filter({ hasText: 'Declaration & Signature' })).toBeVisible();
  });

  test('TC-V-03: submitting empty form shows required-field validation errors', async ({ page }) => {
    await navigateToForm(page, 'victim');
    await continueToApplication(page);
    await triggerValidation(page);

    expect(await hasErrorSummary(page)).toBe(true);
    await expect(page.getByText('Please enter your first name')).toBeVisible();
    await expect(page.getByText('Please enter your last name')).toBeVisible();
    await expect(page.getByText('Please enter your birth date')).toBeVisible();
    await expect(page.getByText('Please make a selection')).toBeVisible();
  });

  test('TC-V-04: "I also go by other names" checkbox reveals conditional name fields', async ({ page }) => {
    await navigateToForm(page, 'victim');
    await continueToApplication(page);

    await expect(page.locator('input[formcontrolname="otherFirstName"]')).not.toBeVisible();

    await page.locator('input[formcontrolname="iHaveOtherNames"]').check();
    await expect(page.locator('input[formcontrolname="otherFirstName"]')).toBeVisible();
    await expect(page.locator('input[formcontrolname="otherLastName"]')).toBeVisible();

    await page.locator('input[formcontrolname="iHaveOtherNames"]').uncheck();
    await expect(page.locator('input[formcontrolname="otherFirstName"]')).not.toBeVisible();
  });

  test('TC-V-05: selecting Yes for designate reveals designate fields', async ({ page }) => {
    await navigateToForm(page, 'victim');
    await continueToApplication(page);

    await page.locator('input[name="authorizeDesignate"]').first().click();

    await expect(page.locator('.designate-info')).toBeVisible();
    await expect(page.getByText('Authorized Designate First Name')).toBeVisible();
  });

  test('TC-V-06: Phone Call contact method marks phone number as required', async ({ page }) => {
    await navigateToForm(page, 'victim');
    await continueToApplication(page);

    await page.locator('select[formcontrolname="preferredMethodOfContact"]').selectOption('Phone Call');
    await triggerValidation(page);

    await expect(page.getByText('Please enter your phone number')).toBeVisible();
  });

  test('TC-V-07: mismatched confirm email shows validation error', async ({ page }) => {
    await navigateToForm(page, 'victim');
    await continueToApplication(page);

    await page.locator('select[formcontrolname="preferredMethodOfContact"]').selectOption('Email');
    await page.locator('input[formcontrolname="email"]').first().fill('jane@example.com');
    await page.locator('input[formcontrolname="confirmEmail"]').first().fill('other@example.com');
    await page.locator('input[formcontrolname="confirmEmail"]').first().blur();

    await expect(page.getByText('Please confirm your email address')).toBeVisible();
  });

  test('TC-V-08: court location dropdown is populated from the API', async ({ page }) => {
    await navigateToForm(page, 'victim');
    await continueToApplication(page);

    const courtSelect = page.locator('select[formcontrolname="location"]').first();
    await expect(courtSelect).toBeVisible();
    expect(await courtSelect.locator('option').count()).toBeGreaterThan(1);
  });

  test('TC-V-09: Cancel Application shows a confirmation dialog', async ({ page }) => {
    await navigateToForm(page, 'victim');
    await continueToApplication(page);

    await page.locator('.cancel-link').click();
    await expect(page.locator('mat-dialog-container, [role="dialog"]')).toBeVisible({ timeout: 5_000 });
  });

  test('TC-V-10: full happy path reaches Review & Submit step', async ({ page }) => {
    await navigateToForm(page, 'victim');
    await continueToApplication(page);

    await fillPersonalInfo(page, {
      firstName: VICTIM_DATA.firstName,
      middleName: VICTIM_DATA.middleName,
      lastName: VICTIM_DATA.lastName,
      birthDay: VICTIM_DATA.birthDay,
      birthMonth: VICTIM_DATA.birthMonth,
      birthYear: VICTIM_DATA.birthYear
    });

    // Designate – No
    await page.locator('input[name="authorizeDesignate"]').last().click();

    await fillContactInfo(page, {
      addressLine1: VICTIM_DATA.addressLine1,
      city: VICTIM_DATA.city,
      postalCode: VICTIM_DATA.postalCode,
      preferredContact: 'Email',
      email: VICTIM_DATA.email
    });

    await acceptDeclaration(page);
    await drawSignature(page);

    await page.getByRole('button', { name: /^CONTINUE/i }).click();
    await expect(page.locator('h1').filter({ hasText: /Review/i })).toBeVisible({ timeout: 10_000 });
  });
});
