Feature: AllowedHosts host filtering enabled
  As a security reviewer
  I want AllowedHosts not set to a wildcard
  So that ASP.NET Core host header filtering is active

  @R-28.1 @tier:low
  Scenario: AllowedHosts is specific and configurable
    Given application host configuration is loaded
    When AllowedHosts is evaluated
    Then AllowedHosts is not the unrestricted wildcard
    And hosts are env-configurable for deployment
    And residual multi-host OpenShift risk is documented if applicable
