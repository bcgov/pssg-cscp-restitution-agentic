Feature: Security scans fail CI on CRITICAL and HIGH findings
  As a security reviewer
  I want PR CI to fail when filesystem vulnerability scans find CRITICAL or HIGH issues
  So that non-blocking scanner configurations cannot silently ship risk

  @R-14.1 @tier:medium
  Scenario: CI runs a blocking filesystem vulnerability scan
    Given CI previously had no failing Trivy filesystem gate
    When the change is merged
    Then the CI gate includes a Trivy filesystem vulnerability scan
    And CRITICAL or HIGH findings fail that step

  @R-14.2
  Scenario: CodeQL analysis is not continue-on-error
    Given the CI CodeQL analyze step
    When the change is merged
    Then that step does not set continue-on-error to true
