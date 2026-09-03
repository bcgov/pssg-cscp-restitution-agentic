import { Page, expect } from '@playwright/test';

// ─── Navigation ───────────────────────────────────────────────────────────────

export async function navigateToForm(page: Page, route: 'victim' | 'offender' | 'victim-entity') {
  await page.goto(route);
  await expect(page.locator('mat-stepper')).toBeVisible({ timeout: 15_000 });
}

export async function continueToApplication(page: Page) {
  await page.getByRole('button', { name: /^CONTINUE/i }).click();
  await expect(page.locator('h1').filter({ hasText: /Application/i })).toBeVisible({ timeout: 10_000 });
}

// ─── Personal information ─────────────────────────────────────────────────────

export interface PersonalInfo {
  firstName: string;
  middleName?: string;
  lastName: string;
  birthDay: string;
  birthMonth: string;
  birthYear: string;
}

export async function fillPersonalInfo(page: Page, info: PersonalInfo) {
  await page.locator('input[formcontrolname="firstName"]').first().fill(info.firstName);
  if (info.middleName) {
    await page.locator('input[formcontrolname="middleName"]').first().fill(info.middleName);
  }
  await page.locator('input[formcontrolname="lastName"]').first().fill(info.lastName);

  // Birth date – three separate <select> elements rendered by app-date-field
  await page.locator('app-date-field select').nth(0).selectOption(info.birthDay);
  await page.locator('app-date-field select').nth(1).selectOption(info.birthMonth);
  await page.locator('app-date-field select').nth(2).selectOption(info.birthYear);
}

// ─── Contact information ──────────────────────────────────────────────────────

export interface ContactInfo {
  addressLine1: string;
  city: string;
  postalCode: string;
  preferredContact?: 'Phone Call' | 'Email' | 'Mail';
  phoneNumber?: string;
  email?: string;
  confirmEmail?: string;
}

export async function fillContactInfo(page: Page, info: ContactInfo) {
  await page.locator('input[formcontrolname="line1"]').fill(info.addressLine1);
  await page.locator('input[formcontrolname="city"]').fill(info.city);
  await page.locator('input[formcontrolname="postalCode"]').fill(info.postalCode);

  if (info.preferredContact) {
    await page.locator('select[formcontrolname="preferredMethodOfContact"]').selectOption(info.preferredContact);
  }
  if (info.phoneNumber) {
    await page.locator('input[formcontrolname="phoneNumber"]').fill(info.phoneNumber);
  }
  if (info.email) {
    await page.locator('input[formcontrolname="email"]').first().fill(info.email);
  }
  const confirmValue = info.confirmEmail ?? info.email;
  if (confirmValue) {
    await page.locator('input[formcontrolname="confirmEmail"]').first().fill(confirmValue);
  }
}

// ─── Court file ───────────────────────────────────────────────────────────────

export interface CourtFileInfo {
  offenderFirstName?: string;
  offenderLastName?: string;
  courtFileNumber?: string;
  courtLocation?: string;
}

export async function fillCourtFile(page: Page, info: CourtFileInfo, index = 0) {
  const group = page.locator('.court-file-info').nth(index);
  if (info.offenderFirstName) {
    await group.locator('input[formcontrolname="firstName"]').fill(info.offenderFirstName);
  }
  if (info.offenderLastName) {
    await group.locator('input[formcontrolname="lastName"]').fill(info.offenderLastName);
  }
  if (info.courtFileNumber) {
    await group.locator('input[formcontrolname="fileNumber"]').fill(info.courtFileNumber);
  }
  if (info.courtLocation) {
    await group.locator('select[formcontrolname="location"]').selectOption(info.courtLocation);
  }
}

// ─── Declaration & Signature ──────────────────────────────────────────────────

export async function acceptDeclaration(page: Page) {
  await page.locator('input[formcontrolname="declaredAndSigned"]').check();
}

export async function drawSignature(page: Page) {
  await page.locator('.signature-trigger').click();

  const canvas = page.locator('mat-dialog-container canvas').first();
  await expect(canvas).toBeVisible({ timeout: 5_000 });

  const box = await canvas.boundingBox();
  if (!box) throw new Error('Signature canvas bounding box not found');

  const x = box.x + box.width / 2;
  const y = box.y + box.height / 2;
  await page.mouse.move(x - 40, y - 20);
  await page.mouse.down();
  await page.mouse.move(x, y + 20);
  await page.mouse.move(x + 40, y - 20);
  await page.mouse.up();

  await page
    .locator('mat-dialog-container')
    .getByRole('button', { name: /accept|confirm|done|save|ok/i })
    .click();

  await expect(page.locator('.signature-preview')).toBeVisible({ timeout: 5_000 });
}

// ─── Validation helpers ───────────────────────────────────────────────────────

export async function triggerValidation(page: Page) {
  await page.getByRole('button', { name: /^CONTINUE/i }).click();
}

export async function hasErrorSummary(page: Page): Promise<boolean> {
  return page.locator('.error-summary').isVisible();
}

// ─── Entity-specific ─────────────────────────────────────────────────────────

export async function fillEntitySignatureFields(
  page: Page,
  info: { signingOfficerName: string; signingOfficerTitle: string }
) {
  await page.locator('input[formcontrolname="signatureName"]').fill(info.signingOfficerName);
  await page.locator('input[formcontrolname="signerTitle"]').fill(info.signingOfficerTitle);

  // signatureDate is a readonly Material datepicker — click to open calendar, then pick today
  await page.locator('input[formcontrolname="signatureDate"]').click();
  await page.locator('mat-datepicker-content .mat-calendar-body-today').click();
}
