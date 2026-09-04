Feature: Submission DTO string MaxLength constraints
  As a security reviewer
  I want key restitution submit DTO strings to have MaxLength attributes
  So that overlong inputs fail model validation before Dynamics

  @R-36.1 @tier:low
  Scenario: Overlong key string fields fail model validation
    Given a submission DTO with an overlong name, address, email, or document filename
    When model validation runs
    Then validation fails for the overlong field
