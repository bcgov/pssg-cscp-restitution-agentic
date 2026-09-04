Feature: Anonymous health surface limited to process liveness
  As a security reviewer
  I want unauthenticated /hc responses not to expose Dataverse readiness details
  So that OpenShift liveness probes still work without disclosing backend connectivity

  @R-24.1 @tier:low
  Scenario: Anonymous /hc only includes self or process tagged checks
    Given health checks registered with self/process and Dataverse/ready tags
    When an anonymous caller hits the public liveness endpoint
    Then only self/process tagged checks are included in the response
    And Dataverse/ready check details are not returned on that anonymous surface

  @R-24.2
  Scenario: OpenShift-style self probe remains anonymous
    Given a process liveness check tagged self or process
    When the anonymous health endpoint is requested
    Then the endpoint does not require authentication for that self check
    And a unit or configuration test documents the anonymous predicate
