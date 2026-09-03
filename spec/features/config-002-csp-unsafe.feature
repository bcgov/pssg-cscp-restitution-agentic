Feature: Production CSP does not advertise unsafe script sources
  As a security reviewer
  I want production responses free of unsafe-eval and unsafe-inline in the always-on CSP
  So that XSS hardening does not depend on a second header overriding a weak first one

  @R-07.1 @tier:medium
  Scenario: Non-development does not emit the weak always-on CSP
    Given the API previously appended a CSP with unsafe-eval and unsafe-inline on every request
    When the change is merged
    Then that permissive always-on CSP is not applied in non-Development environments
    And the existing stricter non-Development CSP configuration remains

  @R-07.2
  Scenario: Development may keep a looser CSP for local tooling
    Given a Development environment
    When a response is produced
    Then a Development-only CSP may still include the previous CDN allow-list
    And any residual unsafe-* use is limited to Development
