Feature: Vulnerability scan gate fails the pipeline on CRITICAL and HIGH
  As a delivery reviewer
  I want CRITICAL and HIGH scan findings to fail the job
  So that a green pipeline means the gate ran and passed

  @R-01.1 @tier:high
  Scenario: CRITICAL or HIGH findings fail the scan job
    Given a delivery pipeline job that scans built container images for vulnerabilities
    When the scanner reports at least one CRITICAL or HIGH finding
    Then that job completes as failed
    And a later deploy step in the same job does not succeed after the failed gate

  @R-01.2
  Scenario: Scan results are still published when the gate is configured
    Given the same image scan job
    When the scanner finishes
    Then a SARIF (or equivalent) report is still written for GitHub Security
    And the job’s pass or fail is driven by the CRITICAL/HIGH threshold, not by ignoring the scanner exit status
