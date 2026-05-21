/** Shared test data used across all e2e tests. */

export const VICTIM_DATA = {
  firstName: 'Jane',
  middleName: 'Marie',
  lastName: 'Doe',
  birthDay: '15',
  birthMonth: 'June',
  birthYear: '1985',
  addressLine1: '123 Test Street',
  city: 'Vancouver',
  postalCode: 'V5K 0A1',
  phoneNumber: '6045550100',
  email: 'jane.doe.test@example.com',
  offenderFirstName: 'John',
  offenderLastName: 'Smith',
  courtFileNumber: 'TEST-2024-001'
};

export const OFFENDER_DATA = {
  firstName: 'Robert',
  middleName: 'James',
  lastName: 'Johnson',
  birthDay: '20',
  birthMonth: 'March',
  birthYear: '1978',
  addressLine1: '456 Example Ave',
  city: 'Victoria',
  postalCode: 'V8W 1G7',
  email: 'robert.johnson.test@example.com',
  courtFileNumber: 'TEST-2024-002'
};

export const VICTIM_ENTITY_DATA = {
  entityName: 'Test Business Corp',
  contactFirstName: 'Alice',
  contactLastName: 'Manager',
  addressLine1: '789 Corporate Blvd',
  city: 'Burnaby',
  postalCode: 'V5G 1A1',
  email: 'entity.contact.test@example.com',
  offenderFirstName: 'Bob',
  offenderLastName: 'Offender',
  courtFileNumber: 'TEST-2024-003',
  signingOfficerName: 'Alice Manager',
  signingOfficerTitle: 'President'
};
