import { expect, test } from '@playwright/test';
import { OFFENDER_DATA } from '../fixtures/test-data';
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

test.describe('Offender form', () => {
  test('TC-O-01: overview page loads with offender-specific heading and stepper', async ({ page }) => {
    await navigateToForm(page, 'offender');

    await expect(page.locator('h1')).toContainText('Information for Accused/Offenders Applying');
    await expect(page.getByRole('button', { name: 'Accused/Offender Application' })).toBeDisabled();
    await expect(page.getByRole('button', { name: 'Review & Submit' })).toBeDisabled();
  });

  test('TC-O-02: CONTINUE navigates to offender form with correct sections', async ({ page }) => {
    await navigateToForm(page, 'offender');
    await continueToApplication(page);

    await expect(page.locator('h1')).toContainText('Offender Application');
    // Offender uses "Applicant Designate" wording, not "Victim Designate"
    await expect(page.locator('h3').filter({ hasText: 'Authorized Applicant Designate' })).toBeVisible();
    // No VSW section
    await expect(page.locator('h2').filter({ hasText: 'Victim Service Worker' })).not.toBeVisible();
    // "(If Applicable)" suffix on restitution order and documents sections
    await expect(page.locator('h2').filter({ hasText: 'Restitution Order Information' })).toContainText(
      'If Applicable'
    );
    await expect(page.locator('h2').filter({ hasText: 'Copy of Restitution Order' })).toContainText('If Applicable');
  });

  test('TC-O-03: submitting empty form shows required-field validation errors', async ({ page }) => {
    await navigateToForm(page, 'offender');
    await continueToApplication(page);
    await triggerValidation(page);

    expect(await hasErrorSummary(page)).toBe(true);
    await expect(page.getByText('Please enter your first name')).toBeVisible();
    await expect(page.getByText('Please enter your last name')).toBeVisible();
    await expect(page.getByText('Please enter your birth date')).toBeVisible();
  });

  test('TC-O-04: offender court file section has no offender name fields', async ({ page }) => {
    await navigateToForm(page, 'offender');
    await continueToApplication(page);

    const courtFileSection = page.locator('.court-file-info').first();
    await expect(courtFileSection.locator('input[formcontrolname="fileNumber"]')).toBeVisible();
    await expect(courtFileSection.locator('select[formcontrolname="location"]')).toBeVisible();
    await expect(courtFileSection.getByText('Offender First Name')).not.toBeVisible();
  });

  test('TC-O-05: full happy path reaches Review & Submit step', async ({ page }) => {
    await navigateToForm(page, 'offender');
    await continueToApplication(page);

    await fillPersonalInfo(page, {
      firstName: OFFENDER_DATA.firstName,
      middleName: OFFENDER_DATA.middleName,
      lastName: OFFENDER_DATA.lastName,
      birthDay: OFFENDER_DATA.birthDay,
      birthMonth: OFFENDER_DATA.birthMonth,
      birthYear: OFFENDER_DATA.birthYear
    });

    // Designate – No
    await page.locator('input[name="authorizeDesignate"]').last().click();

    await fillContactInfo(page, {
      addressLine1: OFFENDER_DATA.addressLine1,
      city: OFFENDER_DATA.city,
      postalCode: OFFENDER_DATA.postalCode,
      preferredContact: 'Email',
      email: OFFENDER_DATA.email
    });

    await acceptDeclaration(page);
    await drawSignature(page);

    await page.getByRole('button', { name: /^CONTINUE/i }).click();
    await expect(page.locator('h1').filter({ hasText: /Review/i })).toBeVisible({ timeout: 10_000 });
  });
});
