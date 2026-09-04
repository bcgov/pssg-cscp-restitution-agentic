Feature: Anonymous health response is status-only
  As a security reviewer
  I want unauthenticated /hc responses not to list check names or descriptions
  So that OpenShift liveness still works without disclosing readiness detail

  @R-25.1 @tier:low
  Scenario: Anonymous /hc returns overall status without check inventory
    Given the public liveness endpoint is mapped for anonymous callers
    When an anonymous caller hits /hc
    Then the response body includes an overall status signal
    And the response body does not include a checks array with names or descriptions

  @R-25.2
  Scenario: OpenShift-style self probe remains anonymous
    Given a process liveness check is available for probes
    When the anonymous health endpoint is requested
    Then the endpoint does not require authentication
    And a unit or configuration test documents the status-only anonymous writer
