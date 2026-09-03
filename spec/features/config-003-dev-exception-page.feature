Feature: Developer exception page limited to Development
  As a security reviewer
  I want detailed exception pages only on local Development
  So that Staging and Test do not leak stack traces

  @R-08.1 @tier:medium
  Scenario: Development still shows the developer exception page
    Given ASPNETCORE_ENVIRONMENT is Development
    When an unhandled exception reaches the middleware pipeline
    Then the developer exception page middleware is registered

  @R-08.2
  Scenario: Non-Development uses the generic exception handler
    Given ASPNETCORE_ENVIRONMENT is not Development (including Staging or Testing)
    When the application starts
    Then the developer exception page is not registered
    And the generic exception handler path is used instead
