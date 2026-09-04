Feature: Dynamics submit failure logs without full OrganizationResponse body
  As an operations reviewer
  I want failure logs to record IsSuccess and error codes only
  So that diagnostics do not dump the full Dynamics OrganizationResponse graph

  @R-32.1 @tier:low
  Scenario: Unsuccessful Dynamics response is logged without full body destructuring
    Given a restitution submit whose Dynamics response IsSuccess is not true
    When the failure log is written
    Then the log includes IsSuccess (or equivalent success flag)
    And the log may include error code or result key metadata
    And the log does not destructure the full OrganizationResponse body via {@Response}
