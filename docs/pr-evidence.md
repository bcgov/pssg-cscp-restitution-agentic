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

---

# PR evidence — [RA DEP-001] Remove archived Microsoft.AspNetCore.HealthChecks package

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-dep-001-remove-archived-health-check-package |
| Spec refs | `spec/spec.md`; `spec/features/dep-001-healthchecks-inbox.feature` (`@R-02.1`, `@R-02.2`) |
| Constitution articles touched | P5, J5 |
| Tasks | see spec/tasks.md |
| Authoring agent | unspecified |
| Generated | 2026-09-03T19:25:52.361Z |

## Intent

The archived `Microsoft.AspNetCore.HealthChecks` 1.0.0 package was removed from the API project. Existing health-check registrations, the `/hc` path, and JSON response shaping remain unchanged and continue to use the in-box `Microsoft.Extensions.Diagnostics.HealthChecks` APIs.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-02.1` Archived community health-check package is not referenced | Yes | Removed the package reference from `restitution-app/restitution-app.csproj`; existing checks already use `Microsoft.Extensions.Diagnostics.HealthChecks`. |
| `@R-02.2` Health endpoint behaviour is preserved | Yes | `Program.cs` was not changed; `/hc`, API and Dataverse checks, and JSON shaping remain intact. |


## Design system & accessibility

| Check | Result |
| --- | --- |
| DS components used (list) | N/A — dependency-only API change |
| Tokens used (not hard-coded colour) | N/A — dependency-only API change |
| BC Sans imported | N/A — dependency-only API change |
| Manual a11y notes | N/A — dependency-only API change |

## Public-service minimums

Checklist IDs addressed this PR: N/A — dependency-only API change

## Tests

| Type | Command / path | Result |
| --- | --- | --- |
| Unit | N/A — no behavioural code change | Not run |
| Acceptance / feature | `spec/features/dep-001-healthchecks-inbox.feature` | Package reference assertion reviewed; endpoint mapping preserved by unchanged `Program.cs`. |
| Build | `dotnet build restitution-app/restitution-app.csproj --no-restore` | Passed |
| A11y automation | N/A — dependency-only API change | |

## Risks & follow-ups

- No new package was introduced. The Dataverse health check still requires configured credentials to report healthy locally.

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-02.1`, `@R-02.2`, `spec/spec.md` scope, and `spec/plan.md`; project reference removed and API build passed.

**Could not check:** Live `/hc` smoke test with Dataverse connectivity because environment credentials are unavailable.

**Residual risk:** The existing Dataverse health check may be unhealthy without credentials; this is expected by the feature and unchanged by this slice.

- Reviewer: _______________ Date: _______________

---

# PR evidence — [RA F-TEST-003] First automated tests for the ASP.NET Core API

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-f-test-003-add-automated-tests |
| Spec refs | `spec/spec.md`; `spec/features/f-test-003-api-tests.feature` (`@R-03.1`, `@R-03.2`) |
| Constitution articles touched | P5, J3, J5 |
| Tasks | TASK-001–TASK-004 |
| Authoring agent | unspecified |
| Generated | 2026-09-03T19:45:02.548Z |

## Intent

The API is no longer a zero-test surface. A new xUnit project `restitution-app.Tests` targets `net10.0`, references the API project, and is part of `restitution-app/restitution-app.sln`. `ConfigurationControllerTests` exercises `ConfigurationController.GetConfiguration` with in-memory settings and asserts the outage, maintenance-mode, and feature-flag mapping. No production behaviour or public JSON was changed.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-03.1` An API test project exists and can be executed | Yes | `restitution-app.Tests/restitution-app.Tests.csproj` added to the solution; `dotnet test` passes with no Dataverse credentials configured. |
| `@R-03.2` Configuration endpoint mapping is covered | Yes | 10 assertions across outage fields, `MaintenanceMode` parsing (including invalid values), and `UseUpdatedComplianceFields`. Temporarily inverting the maintenance-mode expression made a test fail, confirming the tests are not stubs. |

## Design system & accessibility

| Check | Result |
| --- | --- |
| DS components used (list) | N/A — test-only change, no UI |
| Tokens used (not hard-coded colour) | N/A — test-only change, no UI |
| BC Sans imported | N/A — test-only change, no UI |
| Manual a11y notes | N/A — test-only change, no UI |

## Public-service minimums

Checklist IDs addressed this PR: N/A — test-only change, no UI

## Tests

| Type | Command / path | Result |
| --- | --- | --- |
| Unit | `dotnet test restitution-app/restitution-app.sln` | Passed — 10 passed, 0 failed |
| Acceptance / feature | `spec/features/f-test-003-api-tests.feature` (`@R-03.1`, `@R-03.2`) | Passed |
| A11y automation | N/A — test-only change, no UI | |

## Risks & follow-ups

- `dotnet test` is not yet wired into CI RESTITUTION — tracked separately as **F-TEST-001**.
- Lookup, restitution submission, and Dataverse client paths remain untested — **F-TEST-004** / **F-TEST-007**.

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-03.1` and `@R-03.2`; new test project builds and runs offline via the solution, and the configuration mapping assertions fail when the mapping is mutated.

**Could not check:** Behaviour of the endpoint under a live host with Dataverse configured; a full `WebApplicationFactory` host is out of scope for this slice.

**Residual risk:** Coverage is limited to `ConfigurationController`; the submission and lookup write paths stay unverified until the follow-up findings are addressed.

- Reviewer: _______________ Date: _______________
