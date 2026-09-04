Feature: ts-node development dependency on a supported major
  As a security reviewer
  I want ClientApp ts-node not pinned to the 2018 7.x line
  So that CI/build tooling uses a maintained major with fewer transitive risks

  @R-30.1 @tier:low
  Scenario: ts-node is bumped to ^10.x with lockfile updated
    Given ClientApp declares ts-node as a development dependency
    When package metadata is inspected after the upgrade
    Then ts-node is declared as ^10.x (or equivalent current 10.x range)
    And the lockfile resolves a 10.x release
