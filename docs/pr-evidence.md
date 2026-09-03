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

---

# PR evidence — [RA F-TEST-004] First automated tests for the Dataverse client library

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-f-test-004-add-automated-tests |
| Spec refs | `spec/spec.md`; `spec/features/f-test-004-database-tests.feature` (`@R-04.1`, `@R-04.2`) |
| Constitution articles touched | P5, J3 |
| Tasks | TASK-001–TASK-004 |
| Authoring agent | unspecified |
| Generated | 2026-09-03T19:57:32.342Z |

## Intent

The Dataverse client library (`Database/`) now has a `Database.Tests` xUnit project in `restitution-app/restitution-app.sln`. It covers the in-process token cache wrap (`MemoryCache.GetOrSet`) and Dynamics API endpoint selection (`DynamicsTokenProviderOptions.GetDynamicsApiEndpointUrl`), so those paths can no longer regress unnoticed. No test needs Dynamics credentials or network access.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-04.1` a Database test project exists and can be executed | Yes | `Database.Tests/Database.Tests.csproj` (xUnit, net10.0) references `Database/Database.csproj` and is registered in `restitution-app/restitution-app.sln`. |
| `@R-04.2` cache wrap and endpoint selection are covered | Yes | `MemoryCacheTests` asserts the factory runs once across two `GetOrSet` calls for one key; `DynamicsTokenProviderOptionsTests` asserts OnPremise vs Cloud endpoint selection with synthetic `https://example.test/...` URLs. No test performs HTTP. |

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
| Unit | `dotnet test restitution-app/restitution-app.sln` | Passed — 9 Database.Tests, 10 restitution-app.Tests, run with no Dynamics environment variables |
| Acceptance / feature | `spec/features/f-test-004-database-tests.feature` (`@R-04.1`, `@R-04.2`) | Covered by the unit tests above |
| A11y automation | N/A — test-only change, no UI | |

## Risks & follow-ups

- Token acquisition over HTTP (`ADFSTokenProvider`, `EntraIdTokenProvider`) and `ServiceClient` construction remain untested — deferred to AUTH-001 / F-TEST-007.
- CI still builds only `restitution-app.csproj` and does not run `dotnet test` — deferred to F-TEST-001.

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-04.1` and `@R-04.2`; `dotnet test restitution-app/restitution-app.sln` passes offline without Dynamics credentials; fixtures use synthetic URLs only.

**Could not check:** Live Dataverse connectivity, real ADFS/Entra token acquisition, and `ServiceClient` readiness — explicitly out of scope for this slice.

**Residual risk:** The Dataverse connection and token HTTP paths in `Database/Extensions` are still only exercised in a live environment.

- Reviewer: _______________ Date: _______________

---

# PR evidence — [RA F-TEST-007] Add security-focused uploader tests

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-f-test-007-add-security-tests |
| Spec refs | `spec/spec.md`; `spec/features/f-test-007-security-tests.feature` (`@R-05.1`, `@R-05.2`) |
| Constitution articles touched | P5, J3, J5 |
| Tasks | see spec/tasks.md |
| Authoring agent | unspecified |
| Generated | 2026-09-03T20:14:00.827Z |

## Intent

The file uploader unit spec now exercises security-relevant rejection controls instead of only checking component creation. Synthetic unsupported and oversized files are asserted to remain out of the documents array and to produce the corresponding snackbar notification.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-05.1` security-focused uploader unit test exists | Yes | The spec retains the create assertion and adds behavioural rejection assertions. |
| `@R-05.2` disallowed file types are not added | Yes | `malware.exe` leaves the documents array empty and opens the unsupported-type snackbar. |
| Oversized files are not added | Yes | A synthetic file larger than `MAX_FILE_SIZE` leaves the documents array empty and opens the size-limit snackbar. |


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
| Unit | `npx ng test --no-watch --no-progress --include='src/app/shared/file-uploader/file-uploader.component.spec.ts'` | Passed — 3 tests |
| Acceptance / feature | `spec/features/f-test-007-security-tests.feature` (`@R-05.1`, `@R-05.2`) | Covered by the focused uploader unit spec |
| A11y automation | N/A — test-only change, no UI | |

## Risks & follow-ups

- The test remains client-side; server MIME enforcement is explicitly deferred to VULN-002.

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-05.1`, `@R-05.2`, and the in-scope oversized-file control; the focused uploader spec passes with synthetic files and no Dataverse or browser.

**Could not check:** Server-side MIME enforcement, CSRF, rate limiting, and unauthenticated API behaviour because they are out of scope for this slice.

**Residual risk:** The extension allow-list is a client-side control until VULN-002 adds server-side MIME validation.

- Reviewer: _______________ Date: _______________

---

# PR evidence — [RA AUTH-001] Replace ADFS password grant

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-auth-001-adfs-token-provider-update |
| Spec refs | `spec/spec.md`; `spec/features/auth-001-adfs-client-credentials.feature` (`@R-06.1`, `@R-06.2`) |
| Constitution articles touched | P5, J2, J3, J5 |
| Tasks | TASK-001–TASK-004 |
| Authoring agent | unspecified |
| Generated | 2026-09-03T20:37:02.608Z |

## Intent

ADFS token acquisition now uses the OAuth client-credentials grant with the configured client ID, client secret, and resource. The provider no longer posts a service-account username or password; operators must enable client credentials on the ADFS application.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-06.1` Password grant is not used | Yes | `ADFSTokenProvider` uses `RequestClientCredentialsTokenAsync`; its request has no username or password fields. |
| `@R-06.2` Client credentials obtain token | Yes | Mocked HTTP test asserts the form grant and returns a synthetic access token. |


## Design system & accessibility

| Check | Result |
| --- | --- |
| DS components used (list) | N/A — database authentication and documentation change |
| Tokens used (not hard-coded colour) | N/A — no UI |
| BC Sans imported | N/A — no UI |
| Manual a11y notes | N/A — no UI |

## Public-service minimums

Checklist IDs addressed this PR: N/A — no UI

## Tests

| Type | Command / path | Result |
| --- | --- | --- |
| Unit | `dotnet test Database.Tests/Database.Tests.csproj --no-restore` | Passed |
| Acceptance / feature | `spec/features/auth-001-adfs-client-credentials.feature` (`@R-06.1`, `@R-06.2`) | Covered by `ADFSTokenProviderTests` |
| A11y automation | N/A — no UI | |

## Risks & follow-ups

- ADFS application registration must support client credentials before deployment; registration changes are outside this repository.

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-06.1` and `@R-06.2`; the mocked request contains `grant_type=client_credentials` and resource, authenticates with the configured client ID and secret, has no username or password form fields, and returns the synthetic access token.

**Could not check:** Live ADFS token acquisition because credentials and ADFS configuration are environment-managed.

**Residual risk:** On-premise token acquisition fails until the ADFS application permits client-credentials authentication.

- Reviewer: _______________ Date: _______________

---

# PR evidence — [RA CONFIG-002] Gate permissive CSP to Development

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-config-002-fix-csp-header-security |
| Spec refs | `spec/spec.md`; `spec/features/config-002-csp-unsafe.feature` (`@R-07.1`, `@R-07.2`) |
| Constitution articles touched | P5, P7, J3, J5 |
| Tasks | TASK-001, TASK-002, TASK-004 |
| Authoring agent | Copilot |
| Generated | 2026-09-03T20:52:34.125Z |

## Intent

The permissive Content-Security-Policy with `unsafe-eval` and `unsafe-inline` now applies only in Development for local Angular tooling. Non-Development responses retain the existing NWebsec CSP and no longer receive the weak appended policy.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-07.1` Non-development does not emit the weak always-on CSP | Yes | The unsafe CSP append is conditional on `IsDevelopment()`; the existing non-Development `UseCsp` block is unchanged. |
| `@R-07.2` Development may keep a looser CSP for local tooling | Yes | Development retains the prior CDN allow-list and unsafe script directives. |


## Design system & accessibility

| Check | Result |
| --- | --- |
| DS components used (list) | N/A — server security-header change, no UI |
| Tokens used (not hard-coded colour) | N/A — no UI |
| BC Sans imported | N/A — no UI |
| Manual a11y notes | N/A — no UI |

## Public-service minimums

Checklist IDs addressed this PR: N/A — no UI

## Tests

| Type | Command / path | Result |
| --- | --- | --- |
| Unit | `dotnet test restitution-app.Tests/restitution-app.Tests.csproj --no-restore` | Passed |
| Manual runtime | Production host request to `/hc` | Confirmed no `unsafe-eval` or `unsafe-inline` in `Content-Security-Policy` |
| Acceptance / feature | `spec/features/config-002-csp-unsafe.feature` (`@R-07.1`, `@R-07.2`) | Covered by the environment gate and manual production-header check |
| A11y automation | N/A — no UI |

## Risks & follow-ups

- Development intentionally retains `unsafe-eval` and `unsafe-inline` for local Angular tooling; a nonce/hash CSP redesign remains out of scope.

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-07.1` and `@R-07.2`; non-Development omits the permissive CSP while the NWebsec configuration is unchanged.

**Could not check:** A browser-level production deployment with its managed reverse-proxy headers.

**Residual risk:** Development retains unsafe script directives pending the explicitly deferred nonce/hash CSP redesign.

- Reviewer: _______________ Date: _______________
