# PR evidence — [RA CONFIG-001] Enforce Trivy CD security gate

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-config-001-fix-trivy-exit-code |
| Spec refs | `spec/spec.md`; `spec/features/config-001-trivy-gate.feature` (`@R-01.1`, `@R-01.2`) |
| Constitution articles touched | P5, J3, J5 |
| Tasks | TASK-001–TASK-004 |
| Authoring agent | unspecified |
| Generated | 2026-09-03T19:17:56.650Z |

## Intent

The API and UI delivery workflows now fail their Trivy image scan when CRITICAL or HIGH vulnerabilities are found. SARIF upload runs even after a failed scan so findings remain available in GitHub Security.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-01.1` CRITICAL or HIGH findings fail the scan job | Yes | Both Trivy image scan steps use `severity: CRITICAL,HIGH` and `exit-code: "1"`. |
| `@R-01.2` scan results remain published | Yes | Both SARIF upload steps use `if: always()`. |


## Design system & accessibility

| Check | Result |
| --- | --- |
| DS components used (list) | N/A — workflow-only change |
| Tokens used (not hard-coded colour) | N/A — workflow-only change |
| BC Sans imported | N/A — workflow-only change |
| Manual a11y notes | N/A — workflow-only change |

## Public-service minimums

Checklist IDs addressed this PR: N/A — workflow-only change

## Tests

| Type | Command / path | Result |
| --- | --- | --- |
| Unit | N/A — workflow-only change | |
| Acceptance / feature | Focused workflow configuration assertion for both CD workflows | Passed |
| A11y automation | N/A — workflow-only change | |

## Risks & follow-ups

- The first CD run may fail on pre-existing CRITICAL/HIGH vulnerabilities; this is the intended gate behaviour.

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-01.1` and `@R-01.2`; both CD workflow Trivy steps and their SARIF upload steps.

**Could not check:** A live CD scan requires OpenShift registry credentials and a deployment-triggering merge.

**Residual risk:** The gate runs after merge in CD; PR-time filesystem/secret scanning remains explicitly out of scope.

- Reviewer: _______________ Date: _______________
