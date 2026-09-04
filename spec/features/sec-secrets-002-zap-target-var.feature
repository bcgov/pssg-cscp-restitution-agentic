Feature: ZAP scan target from repository variable
  As a security reviewer
  I want the ZAP workflow target URL to come from a repo variable
  So that a public workflow file does not advertise the development hostname

  @R-20.1 @tier:medium
  Scenario: ZAP workflow YAML contains no hardcoded development hostname
    Given the zap-coast-restitution-dev-scan workflow
    When the change is merged
    Then ZAP_TARGET is referenced from repository vars or secrets
    And no real development hostname remains as a literal in that workflow YAML

  @R-20.2
  Scenario: Operators are told to set ZAP_TARGET in repo settings
    Given the ZAP scan workflow or adjacent docs
    When an operator prepares to run the scan
    Then documentation states that ZAP_TARGET must be set in repository settings
