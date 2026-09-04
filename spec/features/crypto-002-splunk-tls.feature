Feature: Splunk HEC TLS validation is not ambiently disabled
  As a security reviewer
  I want DangerousAcceptAnyServerCertificateValidator not tied only to Development
  So that a mis-set environment name cannot silently disable Splunk TLS verification

  @R-29.1 @tier:low
  Scenario: Splunk certificate bypass requires explicit opt-in or is removed
    Given Splunk Event Collector logging may be configured
    When TLS handler options are chosen
    Then certificate validation is not disabled solely because the environment is Development
    And any remaining bypass requires an explicit opt-in configuration flag
