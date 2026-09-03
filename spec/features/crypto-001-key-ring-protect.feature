Feature: Data Protection key ring protected at rest
  As a security reviewer
  I want persisted Data Protection keys encrypted at rest
  So that a volume read does not expose raw key material

  @R-09.1 @tier:medium
  Scenario: Filesystem key persistence uses a protector
    Given KEY_RING_DIRECTORY is configured for container key persistence
    When Data Protection is registered
    Then keys are persisted to that directory
    And a ProtectKeysWith* certificate (or equivalent) protector is chained
    And configuration documents how to supply the certificate

  @R-09.2
  Scenario: Local Development without a key-ring directory is unchanged
    Given KEY_RING_DIRECTORY is empty or unset
    When the application starts
    Then filesystem key persistence is not registered
    And no new certificate requirement blocks local stand-up
