Feature: Server-side document file extension validation on submit
  As a security reviewer
  I want restitution submit endpoints to reject disallowed document extensions
  So that API clients cannot bypass the Angular allowlist

  @R-22.1 @tier:medium
  Scenario: Disallowed document extension is rejected with 400
    Given a restitution submit request that includes a document filename with a disallowed extension
    When the server validates document filenames before Dynamics submit
    Then the request is rejected with HTTP 400
    And Dynamics is not called for that request

  @R-22.2
  Scenario: Allowed extensions from the client allowlist are accepted by validation
    Given document filenames whose extensions are in the client allowlist (pdf, png, jpeg, jpg, doc, docx, ppt)
    When server-side extension validation runs in a unit test
    Then those filenames pass validation
    And filenames outside that allowlist fail validation
