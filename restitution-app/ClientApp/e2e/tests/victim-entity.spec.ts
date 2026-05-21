import { expect, test } from '@playwright/test';
import { VICTIM_ENTITY_DATA } from '../fixtures/test-data';
import {
  acceptDeclaration,
  continueToApplication,
  drawSignature,
  fillContactInfo,
  fillCourtFile,
  fillEntitySignatureFields,
  hasErrorSummary,
  navigateToForm,
  triggerValidation
} from '../helpers/form.helpers';

test.describe('Victim Entity form', () => {
  test('TC-E-01: overview page loads with entity-specific heading and stepper', async ({ page }) => {
    await navigateToForm(page, 'victim-entity');

    await expect(page.locator('h1')).toContainText('Information for Victims Applying');
    await expect(page.getByRole('button', { name: 'Entity Victim Application' })).toBeDisabled();
  });

  test('TC-E-02: CONTINUE navigates to entity form with unique fields', async ({ page }) => {
    await navigateToForm(page, 'victim-entity');
    await continueToApplication(page);

    await expect(page.locator('h1')).toContainText('Entity Victim Application');
    await expect(
      page.getByText('This application must be completed by an individual who has signing authority')
    ).toBeVisible();
    await expect(page.getByText('Entity Name')).toBeVisible();
    await expect(page.getByText('Signing Officer Full Name')).toBeVisible();
    await expect(page.getByText('Signing Officer Title')).toBeVisible();
    // VSW section present (unlike offender form)
    await expect(page.locator('h2').filter({ hasText: 'Victim Service Worker' })).toBeVisible();
  });

  test('TC-E-03: submitting empty form shows entity-specific validation error', async ({ page }) => {
    await navigateToForm(page, 'victim-entity');
    await continueToApplication(page);
    await triggerValidation(page);

    expect(await hasErrorSummary(page)).toBe(true);
    await expect(page.getByText('Please enter your Entity name')).toBeVisible();
  });

  test('TC-E-04: court file section contains offender name and relationship fields', async ({ page }) => {
    await navigateToForm(page, 'victim-entity');
    await continueToApplication(page);

    const courtFileSection = page.locator('.court-file-info').first();
    await expect(courtFileSection.getByText('Offender First Name')).toBeVisible();
    await expect(courtFileSection.getByText('Offender Last Name')).toBeVisible();
    await expect(courtFileSection.getByText('Relationship to the Offender')).toBeVisible();
    await expect(courtFileSection.locator('input[formcontrolname="fileNumber"]')).toBeVisible();
  });

  test('TC-E-05: ADD CONTACT button appends a second entity contact group', async ({ page }) => {
    await navigateToForm(page, 'victim-entity');
    await continueToApplication(page);

    await expect(page.locator('.entity-contact-info')).toHaveCount(1);
    await page.getByRole('button', { name: 'ADD CONTACT' }).click();
    await expect(page.locator('.entity-contact-info')).toHaveCount(2);
  });

  test('TC-E-06: declaration section has signing officer fields with entity-specific text', async ({ page }) => {
    await navigateToForm(page, 'victim-entity');
    await continueToApplication(page);

    await expect(page.locator('input[formcontrolname="signatureName"]')).toBeVisible();
    await expect(page.locator('input[formcontrolname="signerTitle"]')).toBeVisible();
    await expect(page.locator('input[formcontrolname="signatureDate"]')).toBeVisible();
    await expect(page.getByText('I confirm that I have signing authority')).toBeVisible();
  });

  test('TC-E-07: full happy path reaches Review & Submit step', async ({ page }) => {
    await navigateToForm(page, 'victim-entity');
    await continueToApplication(page);

    // Entity name is stored in the lastName control of the Entity Name section
    await page
      .locator('section')
      .filter({ hasText: 'Entity Name' })
      .locator('input[formcontrolname="lastName"]')
      .fill(VICTIM_ENTITY_DATA.entityName);

    // Entity contact
    await page
      .locator('.entity-contact-info input[formcontrolname="firstName"]')
      .fill(VICTIM_ENTITY_DATA.contactFirstName);
    await page
      .locator('.entity-contact-info input[formcontrolname="lastName"]')
      .fill(VICTIM_ENTITY_DATA.contactLastName);
    await page.locator('.entity-contact-info input[formcontrolname="isPrimaryContact"]').first().click();
    await page.locator('.entity-contact-info select[formcontrolname="preferredMethodOfContact"]').selectOption('Email');
    await page.locator('.entity-contact-info input[formcontrolname="email"]').fill(VICTIM_ENTITY_DATA.email);
    await page.locator('.entity-contact-info input[formcontrolname="confirmEmail"]').fill(VICTIM_ENTITY_DATA.email);

    // Mailing address
    await fillContactInfo(page, {
      addressLine1: VICTIM_ENTITY_DATA.addressLine1,
      city: VICTIM_ENTITY_DATA.city,
      postalCode: VICTIM_ENTITY_DATA.postalCode
    });

    // Court file
    await fillCourtFile(page, {
      offenderFirstName: VICTIM_ENTITY_DATA.offenderFirstName,
      offenderLastName: VICTIM_ENTITY_DATA.offenderLastName,
      courtFileNumber: VICTIM_ENTITY_DATA.courtFileNumber
    });

    await acceptDeclaration(page);

    await fillEntitySignatureFields(page, {
      signingOfficerName: VICTIM_ENTITY_DATA.signingOfficerName,
      signingOfficerTitle: VICTIM_ENTITY_DATA.signingOfficerTitle
    });

    await drawSignature(page);

    await page.getByRole('button', { name: /^CONTINUE/i }).click();
    await expect(page.locator('h1').filter({ hasText: /Review/i })).toBeVisible({ timeout: 10_000 });
  });
});
