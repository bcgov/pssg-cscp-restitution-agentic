Feature: Application-level audit log on successful restitution submit
  As an operations reviewer
  I want a structured info-level audit entry when a restitution form succeeds
  So that we have a non-PII operational trail beyond HTTP request logging

  @R-18.1 @tier:medium
  Scenario: Successful submit writes an info-level audit log
    Given a restitution form submission that succeeds against Dynamics
    When the success path returns OK
    Then an information-level audit log is written
    And the log includes non-PII identifiers such as form type, correlation id, and success

  @R-18.2
  Scenario: Success audit log excludes PII and Dynamics response body
    Given a successful restitution form submission
    When the success audit log is written
    Then the log does not include the full form PII payload
    And the log does not include the Dynamics OrganizationResponse body
