Feature: Angular unit tests assert real component behaviour
  As a delivery reviewer
  I want NotFound specs to assert navigation behaviour
  So that always-true stubs cannot hide regressions

  @R-15.1 @tier:medium
  Scenario: NotFound navigates to the 404 route
    Given the NotFound component currently has an always-true stub
    When the change is merged
    Then a unit test asserts the router navigates to `/404` when the component is constructed

  @R-15.2
  Scenario: The always-true stub is removed from NotFound
    Given the NotFound spec file
    When the change is merged
    Then it does not contain an `expect(true).toEqual(true)` (or equivalent) stub as its only assertion
