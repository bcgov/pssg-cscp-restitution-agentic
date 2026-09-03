Feature: Replace archived health-check package with platform library
  As a security reviewer
  I want the API to stop depending on an archived 2017 health-check package
  So that liveness checks rest on a maintained platform library

  @R-02.1 @tier:high
  Scenario: Archived community health-check package is not referenced
    Given the API project file that currently lists Microsoft.AspNetCore.HealthChecks version 1.0.0
    When the change is merged
    Then that package reference is absent
    And health-check types come from the supported Microsoft.Extensions diagnostics library already used in code

  @R-02.2
  Scenario: Health endpoint behaviour is preserved
    Given the API is running
    When a client requests the existing health path `/hc`
    Then the process still returns a JSON health document with an API self-check
    And the Dataverse check remains registered (it may be unhealthy without credentials — that is not a regression of this slice)
