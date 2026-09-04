Feature: ClientApp no longer depends on legacy moment.js
  As a security reviewer
  I want restitution UI date handling without moment.js
  So that the runtime bundle drops a maintenance-mode library without changing applicant date behaviour

  @R-31.1 @tier:low
  Scenario: Direct moment imports and packages are removed
    Given ClientApp previously declared moment and material-moment-adapter
    When package metadata and TypeScript sources are inspected after the change
    Then neither moment nor @angular/material-moment-adapter is a declared dependency
    And application TypeScript does not import from moment or material-moment-adapter

  @R-31.2 @tier:low
  Scenario: Date field compare and display formatting use native Date
    Given an applicant date value is set or formatted for review
    When min/max bounds are checked and a date is shown for review
    Then comparisons and display formatting use native Date or Intl APIs
    And behaviour remains equivalent for valid calendar dates
