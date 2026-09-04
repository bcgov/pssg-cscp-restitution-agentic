Feature: Safe mailing address display on restitution review
  As a security reviewer
  I want address fields rendered as text rather than raw HTML
  So that user-controlled mailing address values cannot inject markup into the review page

  @R-21.1 @tier:medium
  Scenario: Mailing address display does not bind unencoded values via innerHTML
    Given a restitution review that shows the mailing address
    When address fields contain ordinary text
    Then the address is shown to the user with line breaks preserved
    And the implementation does not concatenate unencoded user values into an innerHTML binding

  @R-21.2
  Scenario: Script-like address input is not interpreted as HTML
    Given mailing address fields that contain script-like or markup-like text
    When the display helper or review binding is exercised in a unit test
    Then the script-like input is not interpreted as live HTML
    And the test fails if unencoded innerHTML concatenation returns
