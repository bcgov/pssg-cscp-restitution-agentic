Feature: Permissions-Policy security header
  As a security reviewer
  I want Responses to include a restrictive Permissions-Policy
  So that unused powerful browser features are denied by default

  @R-27.1 @tier:low
  Scenario: Permissions-Policy header is configured
    Given the application serves browser-facing responses
    When security headers are applied
    Then a Permissions-Policy header is present with restrictive defaults for unused features
    And existing security headers remain in place
