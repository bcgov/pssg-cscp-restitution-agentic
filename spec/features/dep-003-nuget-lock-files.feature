Feature: NuGet lock files for API and Database projects
  As a supply-chain reviewer
  I want packages.lock.json for the .NET projects
  So that transitive restores are reproducible

  @R-11.1 @tier:medium
  Scenario: Lock files are enabled and committed
    Given restitution-app and Database have no packages.lock.json
    When the change is merged
    Then each project enables RestorePackagesWithLockFile
    And a packages.lock.json exists beside each project file

  @R-11.2
  Scenario: Restore and build succeed with locks present
    Given the lock files are committed
    When restore and build run for the solution projects
    Then they complete successfully
