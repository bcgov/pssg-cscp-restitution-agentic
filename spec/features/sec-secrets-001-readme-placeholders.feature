Feature: README user-secrets template without real hostnames
  As a security reviewer
  I want the developer secrets template to use placeholders instead of real infrastructure URLs
  So that a public repository does not advertise internal BC Government service topology

  @R-19.1 @tier:medium
  Scenario: User-secrets template contains no real internal hostnames
    Given the restitution-app README user-secrets template
    When the change is merged
    Then Dynamics and ADFS endpoint URL values are angle-bracket placeholders
    And no real internal hostname such as a Dataverse proxy or ADFS host remains in that template

  @R-19.2
  Scenario: Developers can still identify which secrets keys to set
    Given the restitution-app README user-secrets template
    When a developer copies the template into user-secrets
    Then the JSON keys for Dynamics ADFS and EntraId settings remain present and named
    And credential fields remain angle-bracket placeholders
