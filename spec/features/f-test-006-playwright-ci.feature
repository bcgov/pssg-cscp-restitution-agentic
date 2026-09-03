Feature: Playwright health-and-routing E2E runs in CI
  As a delivery reviewer
  I want CI to execute localhost Playwright routing tests
  So that SPA routing regressions fail the pipeline

  @R-16.1 @tier:medium
  Scenario: CI executes Playwright against localhost
    Given Playwright tests exist but are not invoked by CI
    When the change is merged
    Then CI runs Playwright `--project=localhost` for the health-and-routing specs
    And a failing E2E fails the workflow job

  @R-16.2
  Scenario: CI E2E does not require live Dynamics
    Given the CI Playwright job
    When it runs
    Then it targets a locally served SPA (localhost)
    And it does not call remote dev/test Justice hosts as the primary target
