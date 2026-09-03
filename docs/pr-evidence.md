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

---

# PR evidence — [RA CONFIG-003] Developer Exception Page active in all non-production environments

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-config-003-fix-dev-exception-page |
| Spec refs | `spec/spec.md`; `spec/features/config-003-dev-exception-page.feature` (`@R-08.1`, `@R-08.2`) |
| Constitution articles touched | P5, P7, J3, J5 |
| Tasks | TASK-001, TASK-002, TASK-003 |
| Authoring agent | Copilot |
| Generated | 2026-09-03T21:13:11.000Z |

## Intent

`app.UseDeveloperExceptionPage()` previously activated for any environment name other than Production (`!IsProduction()`), so Staging, Testing, UAT, and any other non-Production `ASPNETCORE_ENVIRONMENT` value received full stack-trace and request-detail disclosure. The gate now checks `IsDevelopment()` instead, so only local Development gets the developer exception page; every other environment name uses the generic `/Home/Error` handler. The API Dockerfile also now sets `ASPNETCORE_ENVIRONMENT=Production` explicitly at runtime so an unset environment variable cannot default to a non-Development value that previously would have exposed the developer page.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-08.1` Development still shows the developer exception page | Yes | `Program.cs` registers `UseDeveloperExceptionPage()` when `app.Environment.IsDevelopment()` is true. |
| `@R-08.2` Non-Development uses the generic exception handler | Yes | Every other environment name (Staging, Testing, UAT, Production, etc.) falls into the `else` branch and registers `UseExceptionHandler("/Home/Error")` instead. |

## Design system & accessibility

| Check | Result |
| --- | --- |
| DS components used (list) | N/A — server middleware change, no UI |
| Tokens used (not hard-coded colour) | N/A — no UI |
| BC Sans imported | N/A — no UI |
| Manual a11y notes | N/A — no UI |

## Public-service minimums

Checklist IDs addressed this PR: N/A — no UI

## Tests

| Type | Command / path | Result |
| --- | --- | --- |
| Build | `dotnet build restitution-app/restitution-app.csproj` | Succeeded, 0 warnings, 0 errors |
| Diff review | `restitution-app/Program.cs` gate condition | Confirmed `IsDevelopment()` replaces `!IsProduction()`; `else` branch unchanged |
| Acceptance / feature | `spec/features/config-003-dev-exception-page.feature` (`@R-08.1`, `@R-08.2`) | Covered by the environment gate change |
| A11y automation | N/A — no UI |

## Risks & follow-ups

- LOG-001 / VULN-003 reference the same underlying behaviour; leaving those tickets open per `spec/spec.md` open questions unless product asks to close as duplicates after this merges.

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-08.1` and `@R-08.2`; Development still registers the developer exception page, and every other environment name registers the generic exception handler instead.

**Could not check:** A live externally-reachable Staging/Test deployment; verified via code review and local build only.

**Residual risk:** None identified for this change; the gate is now environment-name-safe by construction.

- Reviewer: _______________ Date: _______________

---

# PR evidence — [RA CRYPTO-001] Data Protection Key Ring Persisted Without At-Rest Encryption

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-crypto-001-fix-key-ring-encryption |
| Spec refs | `spec/spec.md`; `spec/features/crypto-001-key-ring-protect.feature` (`@R-09.1`, `@R-09.2`) |
| Constitution articles touched | P5, P7, J3, J5 |
| Tasks | TASK-001, TASK-002, TASK-003, TASK-004 |
| Authoring agent | Copilot |
| Generated | 2026-09-03T21:28:50.000Z |

## Intent

`AddDataProtection().PersistKeysToFileSystem(...)` was called without a chained `.ProtectKeysWith*()`
provider. On Linux/OpenShift, Windows DPAPI is unavailable, so the key ring XML (containing raw
AES-256/HMAC-SHA256 key material protecting antiforgery tokens and other Data Protection payloads) was
written to the `KEY_RING_DIRECTORY` volume in plaintext. Registration is now extracted into a testable
`DataProtectionSetup.Configure` helper (`restitution-app/DataProtectionSetup.cs`) that, when
`KEY_RING_DIRECTORY` is set, loads an X.509 certificate from `KEY_RING_CERTIFICATE_PATH` (optional
`KEY_RING_CERTIFICATE_PASSWORD`) and chains `.ProtectKeysWithCertificate(cert)`. If the directory is set
but no usable certificate can be loaded, the application throws at startup in any environment other than
Development, so plaintext persistence can never happen silently. Local Development without
`KEY_RING_DIRECTORY` is unchanged (ephemeral keys, no new certificate requirement).

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-09.1` Filesystem key persistence uses a protector | Yes | `DataProtectionSetup.Configure` chains `ProtectKeysWithCertificate` after `PersistKeysToFileSystem` when a certificate loads; throws otherwise outside Development. `restitution-app/README.md` documents `KEY_RING_CERTIFICATE_PATH`/`KEY_RING_CERTIFICATE_PASSWORD`. |
| `@R-09.2` Local Development without a key-ring directory is unchanged | Yes | `Configure` returns immediately when `KEY_RING_DIRECTORY` is empty/unset — no Data Protection filesystem registration, no certificate requirement. |

## Design system & accessibility

| Check | Result |
| --- | --- |
| DS components used (list) | N/A — server startup change, no UI |
| Tokens used (not hard-coded colour) | N/A — no UI |
| BC Sans imported | N/A — no UI |
| Manual a11y notes | N/A — no UI |

## Public-service minimums

Checklist IDs addressed this PR: N/A — no UI

## Tests

| Type | Command / path | Result |
| --- | --- | --- |
| Build | `dotnet build restitution-app/restitution-app.csproj` | Succeeded, 0 warnings, 0 errors |
| Unit tests | `dotnet test restitution-app.Tests/restitution-app.Tests.csproj` | Passed: 14/14, including new `DataProtectionSetupTests` (no directory → no registration; directory + valid cert → `ProtectKeysWithCertificate` chained; directory + missing cert → throws outside Development; directory + missing cert → no throw in Development) |
| Acceptance / feature | `spec/features/crypto-001-key-ring-protect.feature` (`@R-09.1`, `@R-09.2`) | Covered by `DataProtectionSetupTests` and the `Configure` implementation |
| A11y automation | N/A — no UI |

## Risks & follow-ups

- Ops must mount a certificate and set `KEY_RING_CERTIFICATE_PATH` (and `KEY_RING_CERTIFICATE_PASSWORD`
  if applicable) before enabling `KEY_RING_DIRECTORY` in shared/OpenShift environments — tracked as an
  ops runbook item per `spec/spec.md`.
- Rotating any existing production key ring written in plaintext prior to this change is out of scope
  and remains an ops follow-up.
- Azure Key Vault protection was intentionally not introduced (not already wired) — X.509 certificate
  protection was chosen as the OpenShift-friendly option per `spec/plan.md`.

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-09.1` and `@R-09.2`; filesystem persistence is always paired with a certificate
protector or a startup failure outside Development, and Development without a key-ring directory is
unaffected.

**Could not check:** A live OpenShift deployment with a mounted certificate volume; verified via unit
tests and local build only.

**Residual risk:** Ops must mount a certificate before enabling `KEY_RING_DIRECTORY` in shared
environments; no automated enforcement of that mount exists outside this application's own fail-closed
check.

- Reviewer: _______________ Date: _______________

---

# PR evidence — [RA DEP-002] Remove EOL .NET Core meta-package references

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-dep-002-remove-eol-packages |
| Spec refs | `spec/spec.md`; `spec/features/dep-002-eol-netcore-packages.feature` (`@R-10.1`, `@R-10.2`) |
| Constitution articles touched | J5 |
| Tasks | TASK-001–TASK-003 |
| Authoring agent | Copilot |
| Generated | 2026-09-03T21:40:11.874Z |

## Intent

The obsolete EOL `Microsoft.NETCore.App` 2.2.8 and `Microsoft.NETCore.Jit` 2.0.8 PackageReferences were removed from the net10.0 API project. Framework-provided runtime components remain supplied by the net10.0 SDK.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-10.1` EOL PackageReferences are absent | Yes | Removed both obsolete references from `restitution-app/restitution-app.csproj`. |
| `@R-10.2` API project still builds on net10.0 | Yes | Verified by the API project build. |


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
| Acceptance / feature | `spec/features/dep-002-eol-netcore-packages.feature` (`@R-10.1`, `@R-10.2`) | Package-reference absence verified manually; API build run. |
| Build | `dotnet build restitution-app/restitution-app.csproj` | Passed — 0 warnings, 0 errors |
| A11y automation | N/A — dependency-only API change | |

## Risks & follow-ups

- No new package was introduced. Broader dependency upgrades remain out of scope.

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-10.1` and `@R-10.2`; both specified EOL PackageReferences are absent and the API project builds on net10.0.

**Could not check:** A live application run is unnecessary for this dependency-only change.

**Residual risk:** None identified; the project continues to target net10.0.

- Reviewer: _______________ Date: _______________

---

# PR evidence — [RA DEP-003] No NuGet lock files; unpinned transitive dependencies

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-dep-003-nuget-lock-files |
| Spec refs | `spec/spec.md`; `spec/features/dep-003-nuget-lock-files.feature` (`@R-11.1`, `@R-11.2`) |
| Constitution articles touched | J5 |
| Tasks | TASK-001–TASK-004 |
| Authoring agent | Copilot |
| Generated | 2026-09-03T21:52:39.000Z |

## Intent

`RestorePackagesWithLockFile` was enabled on `restitution-app.csproj` and `Database.csproj`, and `packages.lock.json` was generated and committed for both projects so transitive NuGet dependencies resolve to a pinned, reproducible graph across developer workstations and CI/CD.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-11.1` Lock files are enabled and committed | Yes | Added `<RestorePackagesWithLockFile>true</RestorePackagesWithLockFile>` to both csproj files and committed `restitution-app/packages.lock.json` and `Database/packages.lock.json`. |
| `@R-11.2` Restore and build succeed with locks present | Yes | `dotnet restore --locked-mode` and `dotnet build -c Release` both succeed with the committed locks. |

## Refreshing locks

Run `dotnet restore <project>.csproj --force-evaluate` (or delete the `packages.lock.json` and run `dotnet restore`) after intentionally changing a `PackageReference`, then commit the updated lock file.

## Design system & accessibility

| Check | Result |
| --- | --- |
| DS components used (list) | N/A — dependency-only change |
| Tokens used (not hard-coded colour) | N/A — dependency-only change |
| BC Sans imported | N/A — dependency-only change |
| Manual a11y notes | N/A — dependency-only change |

## Public-service minimums

Checklist IDs addressed this PR: N/A — dependency-only change

## Tests

| Type | Command / path | Result |
| --- | --- | --- |
| Unit | N/A — no behavioural code change | Not run |
| Acceptance / feature | `spec/features/dep-003-nuget-lock-files.feature` (`@R-11.1`, `@R-11.2`) | Verified manually: lock files present, restore/build succeed. |
| Build | `dotnet restore restitution-app/restitution-app.csproj --locked-mode` then `dotnet build restitution-app/restitution-app.csproj -c Release` | Passed — 0 warnings, 0 errors |
| A11y automation | N/A — dependency-only change | |

## Risks & follow-ups

- CI `RestoreLockedMode` enforcement is an optional follow-up (not required by this slice).
- Test projects (`restitution-app.Tests`, `Database.Tests`) were left unchanged; they build via project references and are out of scope for this slice.

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-11.1` and `@R-11.2`; both csproj files enable lock-file restore, both lock files are committed, and locked-mode restore plus build succeed.

**Could not check:** CI enforcement of `RestoreLockedMode` was not added since it is explicitly out of scope for this slice.

**Residual risk:** Lock files must be regenerated whenever a `PackageReference` is intentionally added, removed, or upgraded; this is a manual developer step with no automated check yet.

- Reviewer: _______________ Date: _______________
