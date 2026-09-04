Feature: Authorization middleware enforces Authorize attributes
  As a security reviewer
  I want UseAuthorization in the ASP.NET Core pipeline
  So that future [Authorize] attributes are enforced rather than silently ignored

  @R-26.1 @tier:low
  Scenario: Pipeline registers and invokes authorization
    Given the API host configures the HTTP pipeline
    When the application starts
    Then authorization services are registered
    And authorization middleware runs after routing and before controller endpoints
    And a unit or configuration test documents UseAuthorization in Program

  @R-26.2
  Scenario: Anonymous health probe remains unauthenticated
    Given OpenShift-style liveness depends on anonymous /hc
    When the health endpoint is mapped
    Then /hc does not require authentication
