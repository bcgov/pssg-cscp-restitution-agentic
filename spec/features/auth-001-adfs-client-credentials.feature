Feature: ADFS token provider stops using password grant
  As a security reviewer
  I want Dataverse tokens from ADFS without posting a service-account password
  So that the integration path does not depend on deprecated ROPC

  @R-06.1 @tier:medium
  Scenario: Resource owner password grant is not used
    Given the ADFS token provider currently posts a username and password to the token endpoint
    When the change is merged
    Then the provider no longer calls a password-token request API
    And the token request body does not include username or password fields

  @R-06.2
  Scenario: Client credentials grant is used instead
    Given a mocked ADFS token HTTP endpoint that accepts client credentials
    When AcquireToken runs with client id and client secret configured
    Then the request uses the client-credentials grant
    And a successful response returns the access token to the caller
