Feature: Security-focused test for attachment allow-list
  As a security reviewer
  I want an automated test that rejected uploads stay rejected
  So that the client allow-list cannot silently disappear

  @R-05.1 @tier:high
  Scenario: A security-focused unit test exists
    Given the file uploader currently only has a create stub
    When the change is merged
    Then a unit test file for the uploader asserts security-relevant rejection behaviour
    And that test is not a always-true stub

  @R-05.2
  Scenario: Disallowed file types are not added to documents
    Given an uploader bound to an empty documents list
    When a file with an extension that is not on the accepted list is offered
    Then the documents list stays empty
    And the user is notified that the type is unsupported
