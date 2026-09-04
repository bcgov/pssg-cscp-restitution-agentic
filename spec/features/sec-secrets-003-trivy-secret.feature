Feature: Enforcing Trivy secret scan in CI
  As a security reviewer
  I want Trivy secret scanning to fail the CI gate when secrets are found
  So that committed credentials cannot merge unnoticed

  @R-34.1 @tier:low
  Scenario: CI runs Trivy secret scanner with exit-code 1
    Given the restitution CI workflow includes security scanning
    When the Trivy secret scan step is configured
    Then scanners include secret
    And exit-code is 1 so findings fail the job
