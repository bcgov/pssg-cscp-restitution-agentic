Feature: Developer exception page is Development-only
  As a security reviewer
  I want staging-like environments to use the generic error handler
  So that stack traces are not disclosed outside local Development

  @R-35.1 @tier:low
  Scenario: Staging-like environments do not allow the developer exception page
    Given ExceptionPagePolicy gates UseDeveloperExceptionPage
    When the host environment name is Staging, Test, QA, UAT, or Production
    Then AllowDeveloperExceptionPage returns false
    And only Development returns true
