Feature: First automated tests for the ASP.NET Core API
  As a delivery reviewer
  I want the API to have at least one real automated test
  So that configuration behaviour cannot regress unnoticed

  @R-03.1 @tier:high
  Scenario: An API test project exists and can be executed
    Given the restitution API currently has no *Tests.cs files
    When the change is merged
    Then a test project targeting the API is part of the solution
    And `dotnet test` on that project completes successfully without Dataverse credentials

  @R-03.2
  Scenario: Configuration endpoint mapping is covered
    Given in-memory settings for outage message and maintenance mode
    When the configuration action is exercised
    Then the response is success
    And maintenance mode and outage fields match the in-memory settings
    And the test is not an empty stub that always passes
