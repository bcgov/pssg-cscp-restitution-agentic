Feature: Developer exception page disclosure limited to Development
  As a security reviewer
  I want a regression guard that Staging never gets the developer exception page
  So that LOG-001 stays closed after CONFIG-003

  @R-17.1 @tier:medium
  Scenario: Development may use the developer exception page
    Given an environment named Development
    When the exception-page policy is evaluated
    Then the developer exception page is allowed

  @R-17.2
  Scenario: Staging and Production do not use the developer exception page
    Given an environment named Staging or Production
    When the exception-page policy is evaluated
    Then the developer exception page is not allowed
