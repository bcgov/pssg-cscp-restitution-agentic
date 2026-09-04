Feature: Cookie MinimumSameSitePolicy uses Lax
  As a security reviewer
  I want the global cookie policy not to default to SameSite=None
  So that cookies are not broadly cross-site by default without a documented need

  @R-23.1 @tier:low
  Scenario: MinimumSameSitePolicy is Lax
    Given the ASP.NET cookie policy configuration in Program.cs
    When MinimumSameSitePolicy is inspected
    Then it is set to SameSiteMode.Lax
    And it is not SameSiteMode.None

  @R-23.2
  Scenario: Change is covered by an automated check
    Given the SameSite policy setting
    When a unit or focused configuration test runs
    Then the test fails if MinimumSameSitePolicy is SameSiteMode.None
