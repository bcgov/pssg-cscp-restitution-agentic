Feature: CI runs automated .NET tests
  As a delivery reviewer
  I want the CI gate to execute dotnet test
  So that a green pipeline means unit tests passed

  @R-13.1 @tier:medium
  Scenario: CI gate runs the solution test suite
    Given CI currently builds the API without running tests
    When the change is merged
    Then the CI gate job runs `dotnet test` for the restitution solution
    And a failing test causes the job to fail

  @R-13.2
  Scenario: Test project path changes trigger CI
    Given test projects live beside the API and Database projects
    When workflow path filters are updated
    Then changes under restitution-app.Tests and Database.Tests also trigger the CI workflow
