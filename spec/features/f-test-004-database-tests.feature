Feature: First automated tests for the Dataverse client library
  As a delivery reviewer
  I want the Database project to have automated tests
  So that cache and endpoint selection cannot regress unnoticed

  @R-04.1 @tier:high
  Scenario: A Database test project exists and can be executed
    Given the Dataverse client library currently has no tests under Database/
    When the change is merged
    Then a test project targeting Database is part of the solution
    And `dotnet test` on that project completes successfully without Dynamics credentials

  @R-04.2
  Scenario: Cache wrap and endpoint selection are covered
    Given an in-memory cache and Dynamics token-provider options
    When cache GetOrSet is exercised twice for the same key
    Then the factory runs once and the second call returns the cached value
    And OnPremise versus Cloud options return the matching Dynamics API endpoint URL
    And no test opens a network connection to ADFS or Entra
