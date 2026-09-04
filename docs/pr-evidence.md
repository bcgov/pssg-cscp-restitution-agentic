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

---

# PR evidence — [RA DEP-006] OpenShift net8 Dockerfile removed; net10 base images digest-pinned

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-dep-006-fix-dockerfile-runtime |
| Spec refs | `spec/spec.md`; `spec/features/dep-006-dotnet10-dockerfile.feature` (`@R-12.1`, `@R-12.2`) |
| Constitution articles touched | J5 |
| Tasks | TASK-001–TASK-005 |
| Authoring agent | Copilot |
| Generated | 2026-09-03T22:05:40.231Z |

## Intent

`openshift/Dockerfile.ubi8.net8_customized` built on `registry.access.redhat.com/ubi8/dotnet-80-runtime` (.NET 8) even though the application targets `net10.0`, so it could only ever have produced a broken deployment. It is unused by CD and has been deleted in favour of a short `openshift/README.md` that points at the real build paths. The active API Dockerfile now pins its .NET 10 runtime and SDK base images by `sha256` digest so builds are reproducible and immune to moving tags.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-12.1` Misleading net8 OpenShift Dockerfile is removed or superseded | Yes | `openshift/Dockerfile.ubi8.net8_customized` deleted; `openshift/README.md` added stating that CD builds the API from `restitution-app/Dockerfile` (.NET 10) and the UI from `restitution-app/ClientApp/Dockerfile`. `.github/workflows/cd-restitution-api.yml` still references `file: ./restitution-app/Dockerfile`, so CD is unaffected. |
| `@R-12.2` Active API Dockerfile pins .NET 10 base images by digest | Yes | `mcr.microsoft.com/dotnet/aspnet:10.0-alpine@sha256:c4b29bf368004ad9076c1ab9bc91fb373561e3905b4345637e14e8b8c57e3be8` and `mcr.microsoft.com/dotnet/sdk:10.0-alpine@sha256:620e765fe18186c08399f7aa978f79f04b6bbf0ee1b3b8a91e2d5c9619e59da1`. Tags are retained alongside the digests, so the .NET 10 family stays explicit and readable. |

## Refreshing base image digests

Resolve the current digest for a tag before bumping it:

```bash
docker buildx imagetools inspect mcr.microsoft.com/dotnet/aspnet:10.0-alpine
docker buildx imagetools inspect mcr.microsoft.com/dotnet/sdk:10.0-alpine
```

Update both `FROM` lines in `restitution-app/Dockerfile` together so the SDK and runtime stay on the same patch level.

## Design system & accessibility

| Check | Result |
| --- | --- |
| DS components used (list) | N/A — container build change, no UI |
| Tokens used (not hard-coded colour) | N/A — container build change, no UI |
| BC Sans imported | N/A — container build change, no UI |
| Manual a11y notes | N/A — container build change, no UI |

## Public-service minimums

Checklist IDs addressed this PR: N/A — container build change, no user-facing surface

## Tests

| Type | Command / path | Result |
| --- | --- | --- |
| Unit | N/A — no application code change | Not run |
| Acceptance / feature | `spec/features/dep-006-dotnet10-dockerfile.feature` (`@R-12.1`, `@R-12.2`) | Verified by diff review: net8 file removed, both `FROM` lines digest-pinned, workflow path unchanged. |
| Build | Registry manifest check for both pinned digests (`HTTP 200` from `mcr.microsoft.com/v2/dotnet/{aspnet,sdk}/manifests/sha256:…`) | Passed — both digests resolve and were read from the `10.0-alpine` tags |
| A11y automation | N/A — container build change, no UI | |

## Risks & follow-ups

- Digest pins must be refreshed deliberately to pick up base image security patches; without an automated bump (e.g. Dependabot/Renovate for Docker) the images will drift behind upstream fixes.
- A .NET 10 UBI/S2I image was not created; if OpenShift S2I builds are needed later, the new image must use a .NET 10 base and be digest-pinned in the same way.

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-12.1` and `@R-12.2` — the net8 OpenShift Dockerfile is gone, `openshift/README.md` documents the superseding build paths, `cd-restitution-api.yml` still builds `./restitution-app/Dockerfile`, and both .NET 10 `FROM` lines carry verified `sha256` digests.

**Could not check:** A full `docker build` of `restitution-app/Dockerfile` was not executed in this environment (no container runtime with registry pull access), so the pinned images were validated via registry manifest lookups rather than an end-to-end image build.

**Residual risk:** Pinned digests age; base image CVE fixes will not be picked up until the digests are intentionally bumped.

- Reviewer: _______________ Date: _______________

---

# PR evidence — [RA F-TEST-001] Run dotnet test in the CI gate

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-f-test-001-add-test-execution-to-ci |
| Spec refs | `spec/spec.md`; `spec/features/f-test-001-ci-dotnet-test.feature` (`@R-13.1`, `@R-13.2`) |
| Constitution articles touched | P5, J3, J5 |
| Tasks | TASK-001–TASK-003 |
| Authoring agent | unspecified |
| Generated | 2026-09-03T22:17:41.687Z |

## Intent

The CI `gate` job now builds `restitution-app/restitution-app.sln` and runs `dotnet test` against it, so the API and Database unit suites execute on every pull request and a failing test fails the gate. Workflow path filters also cover the two test projects and the workflow file itself, so test-only changes still trigger CI.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-13.1` CI gate runs the solution test suite | Yes | `dotnet test restitution-app/restitution-app.sln -c Release --no-build` runs after the solution build; a non-zero exit fails the job. |
| `@R-13.2` test project path changes trigger CI | Yes | `restitution-app.Tests/**`, `Database.Tests/**`, and `.github/workflows/ci-restitution.yml` added to the `pull_request` and `push` path filters. |


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
| Unit | `dotnet build restitution-app/restitution-app.sln -c Release` then `dotnet test restitution-app/restitution-app.sln -c Release --no-build` | Passed — 24 tests (14 API, 10 Database) |
| Acceptance / feature | `spec/features/f-test-001-ci-dotnet-test.feature` — workflow diff review | Passed |
| A11y automation | N/A — workflow-only change | |

## Risks & follow-ups

- Angular Karma and Playwright suites remain out of CI (F-TEST-005 / F-TEST-006).
- The build step now compiles the whole solution rather than only the API project, which slightly lengthens the gate but also widens CodeQL C# extraction.

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-13.1` and `@R-13.2`; the build/test steps and the `pull_request` / `push` path filters in `.github/workflows/ci-restitution.yml`; the solution test run locally (24 passing tests).

**Could not check:** A live GitHub-hosted CI run of the updated workflow, including CodeQL analysis timing.

**Residual risk:** Test execution covers only the .NET projects; front-end suites are still unguarded until the follow-up findings land.

- Reviewer: _______________ Date: _______________

# PR evidence — [RA F-TEST-002] All security-scanning quality gates configured as non-blocking

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-f-test-002-fix-security-gates |
| Spec refs | `spec/spec.md`; `spec/features/f-test-002-blocking-security-scans.feature` (`@R-14.1`, `@R-14.2`) |
| Constitution articles touched | P5, J3, J5 |
| Tasks | TASK-001–TASK-003 |
| Authoring agent | unspecified |
| Generated | 2026-09-03T22:34:26.000Z |

## Intent

The CI `gate` job in `ci-restitution.yml` now runs a Trivy filesystem vulnerability scan (`scan-type: fs`, `severity: CRITICAL,HIGH`, `exit-code: "1"`) after the CodeQL analyze step, with its SARIF output uploaded via `codeql-action/upload-sarif` (`if: always()`). CRITICAL or HIGH filesystem findings now fail the PR-time CI gate, not only the CD image scans.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-14.1` CI runs a blocking filesystem vulnerability scan | Yes | New `aquasecurity/trivy-action` step scans the repo (`scan-ref: .`) with `exit-code: "1"`; a CRITICAL/HIGH finding fails the job. |
| `@R-14.2` CodeQL analysis is not `continue-on-error` | Yes (already true) | `github/codeql-action/analyze@v4` in `ci-restitution.yml` carries no `continue-on-error`; confirmed unchanged. |

## Residual / already-fixed context

- `development` already removed the `continue-on-error: true` from CodeQL/Sonar steps referenced in the original assessment finding, and SonarCloud is absent from the current workflow — it is intentionally **not** re-added.
- CD image scans (`cd-restitution-api.yml`, `cd-restitution-ui.yml`) already run Trivy with `exit-code: "1"` (CONFIG-001, previously fixed); this slice closes the remaining gap by adding an equivalent **filesystem** gate to the PR-time CI workflow.
- SEC-SECRETS-003 (dedicated Trivy secret-scanner exit code) is out of scope for this slice per `spec/spec.md`.

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
| YAML validation | `python3 -c "import yaml; yaml.safe_load(open('.github/workflows/ci-restitution.yml'))"` | Passed |
| Acceptance / feature | `spec/features/f-test-002-blocking-security-scans.feature` — workflow diff review | Passed |
| A11y automation | N/A — workflow-only change | |

## Risks & follow-ups

- First live CI run may surface pre-existing CRITICAL/HIGH filesystem findings; per `spec/plan.md`, this is the intended gate behaviour and should not be disabled to force a green build.
- SEC-SECRETS-003 (secret-specific exit code) remains a separate follow-up.

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-14.1` and `@R-14.2` against the updated `.github/workflows/ci-restitution.yml`; confirmed CodeQL analyze has no `continue-on-error`; confirmed CD workflows already use `exit-code: "1"` for Trivy image scans; YAML syntax validated locally.

**Could not check:** A live GitHub-hosted run of the new Trivy filesystem scan step against the current repository contents (sandbox has no outbound access to run `trivy-action`).

**Residual risk:** The new blocking scan may surface pre-existing filesystem vulnerabilities on first run; this is the intended behaviour of the fix, not a regression.

- Reviewer: _______________ Date: _______________
# Evidence — F-TEST-005 Angular unit test substance

| Field | Value |
| --- | --- |
| Spec refs | `spec/features/f-test-005-angular-unit-substance.feature` (`@R-15.1`, `@R-15.2`) |
| Tasks | `TASK-001`–`TASK-003` |
| Verification | `npm test -- --watch=false --include=src/app/not-found/not-found.component.spec.ts` |

## Review receipt

- Checked: `not-found.component.spec.ts` uses a Router spy and asserts navigation to `/404`.
- Checked: the tautological `expect(true)` stub was removed.
- Could not check: browser-based execution is unavailable; the configured Angular Vitest test environment passed the focused spec.
- Residual risk: the remaining Angular spec files are outside this focused slice.

# Evidence — F-TEST-006 Playwright in CI

| Field | Value |
| --- | --- |
| Spec refs | `spec/features/f-test-006-playwright-ci.feature` (`@R-16.1`, `@R-16.2`) |
| Tasks | `TASK-001`–`TASK-003` |
| Verification | Separate `e2e` job in `ci-restitution.yml`; Chromium (no Chrome channel); offline API mocks |

## Review receipt

- Checked: `e2e` job runs `playwright test --project=localhost e2e/tests/health-and-routing.spec.ts`.
- Checked: Playwright config uses bundled Chromium (dropped `channel: 'chrome'`) and `webServer` for local SPA.
- Checked: health-and-routing mocks `/restwebforms/hc` + configuration/lookups so CI does not need Dynamics.
- Fixed: `mat-vertical-stepper` selector (used by the spec and shared `e2e/helpers/form.helpers.ts`) did not match the real markup (`<mat-stepper orientation="vertical">`), so every stepper-visibility assertion was a silent false negative — direct evidence the suite had never actually run. Corrected to `mat-stepper` in both files; re-ran the full suite locally in CI mode (`CI=true npx playwright test --project=localhost e2e/tests/health-and-routing.spec.ts`), 7/7 passed.
- Could not check: full form E2E suites remain out of scope / remote; an actual GitHub Actions run of the `e2e` job (no outbound access in this sandbox to trigger one).
- Residual risk: `gate` may still fail on known CodeQL default-setup noise; `e2e` is independent.

# Evidence — LOG-001 Developer exception page disclosure guard

| Field | Value |
| --- | --- |
| Spec refs | `spec/features/log-001-dev-exception-disclosure.feature` (`@R-17.1`, `@R-17.2`) |
| Tasks | `TASK-001`–`TASK-003` |
| Verification | `dotnet test restitution-app.Tests/restitution-app.Tests.csproj --filter FullyQualifiedName~ExceptionPagePolicyTests` |

## Review receipt

- Checked: `Program.cs` already only registered `UseDeveloperExceptionPage()` behind `IsDevelopment()` (CONFIG-003); behaviour unchanged.
- Checked: extracted `ExceptionPagePolicy.AllowDeveloperExceptionPage(IHostEnvironment)` as a small, directly-testable helper and wired `Program.cs` to call it instead of inlining `IsDevelopment()`.
- Checked: added `ExceptionPagePolicyTests` covering Development (allowed) and Staging/Test/Production (not allowed); 4/4 passed.
- Could not check: a live Staging/Production deployment; verification is via the unit tests plus code inspection of the wired call site.
- Residual risk: VULN-003 references the same underlying behaviour and remains open as a separate ticket per `spec/spec.md`.

# Evidence — LOG-002 Success-path submit audit log

| Field | Value |
| --- | --- |
| Spec refs | `spec/features/log-002-submit-audit-log.feature` (`@R-18.1`, `@R-18.2`) |
| Tasks | `TASK-001`–`TASK-003` |
| Verification | `dotnet test restitution-app.Tests/restitution-app.Tests.csproj --filter FullyQualifiedName~RestitutionSubmitAuditTests` |

## Review receipt

- Checked: added `RestitutionSubmitAudit.WriteSuccess(ILogger, formType, correlationId)` — a small, directly-testable helper that writes one information-level audit entry with non-PII fields only (`FormType`, `CorrelationId`, `Success`).
- Checked: `RestitutionsController.SubmitRestitutionInternal` calls the helper immediately before `return Ok(response)`, with the form type supplied by the `victim` / `victim-entity` / `offender` actions and the correlation id taken from `HttpContext.TraceIdentifier`.
- Checked: the controller now takes an injected `ILogger<RestitutionsController>` (Serilog remains the backing sink via `UseSerilog`) instead of the static `Log.Logger`, so the success audit is exercised through standard logging abstractions; existing error messages are unchanged.
- Checked: `RestitutionSubmitAuditTests` asserts `LogLevel.Information`, the three non-PII properties for each form type, the `unknown` fallback when no correlation id is available, and that no other state values (no form payload, no Dynamics `OrganizationResponse`) are logged; 6/6 new tests pass, 24/24 in the project.
- Could not check: a live Dynamics/Dataverse submission and the resulting entry in the deployed Splunk/console sink; no outbound Dataverse access in this sandbox.
- Residual risk: the failure-path `{@Response}` `OrganizationResponse` destructuring is unchanged and remains LOG-003 (#32).

---

# PR evidence — [RA SEC-SECRETS-001] Replace internal infrastructure URLs with placeholders

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-sec-secrets-001-fix-hardcoded-urls |
| Spec refs | `spec/spec.md`; `spec/features/sec-secrets-001-readme-placeholders.feature` (`@R-19.1`, `@R-19.2`) |
| Constitution articles touched | P3, P5 |
| Tasks | TASK-001, TASK-002, TASK-003 |
| Authoring agent | copilot |
| Generated | 2026-09-04T00:02:49.256Z |

## Intent

Replaced hardcoded internal BC Government service and authentication URLs in `restitution-app/README.md` user-secrets template with generic angle-bracket placeholders. All JSON configuration keys and existing credential placeholders are preserved.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-19.1` User-secrets template contains no real internal hostnames | Yes | Replaced Dataverse proxy, ADFS token endpoint, JAG Dataverse resource, and Dynamics cloud URLs with generic placeholders. |
| `@R-19.2` Developers can still identify which secrets keys to set | Yes | JSON structure, key names, and credential placeholders (`<onpremise_client_id>`, etc.) remain intact. |


## Design system & accessibility

| Check | Result |
| --- | --- |
| DS components used (list) | N/A — documentation-only change |
| Tokens used (not hard-coded colour) | N/A — documentation-only change |
| BC Sans imported | N/A — documentation-only change |
| Manual a11y notes | N/A — documentation-only change |

## Public-service minimums

Checklist IDs addressed this PR: N/A — documentation-only change

## Tests

| Type | Command / path | Result |
| --- | --- | --- |
| Unit | N/A — documentation-only change | |
| Acceptance / feature | `spec/features/sec-secrets-001-readme-placeholders.feature` — manual review & grep verification of `restitution-app/README.md` | Passed — zero real internal hostnames remain in template |
| A11y automation | N/A — documentation-only change | |

## Risks & follow-ups

- SEC-SECRETS-002 (ZAP workflow hostname) is out of scope for this slice and remains open as a separate ticket (#20).

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-19.1` and `@R-19.2`; verified all 5 internal URLs in `restitution-app/README.md` were replaced with angle-bracket placeholders; verified JSON keys and existing credential placeholders are unchanged; ran grep for internal domain patterns.

**Could not check:** N/A

**Residual risk:** None. Documentation-only change.

- Reviewer: _______________ Date: _______________

---

# PR evidence — [RA SEC-SECRETS-002] ZAP_TARGET sourced from repository variable

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-sec-secrets-002-zap-target-var |
| Spec refs | `spec/spec.md`; `spec/features/sec-secrets-002-zap-target-var.feature` (`@R-20.1`, `@R-20.2`) |
| Constitution articles touched | P3, P5 |
| Tasks | TASK-001, TASK-002, TASK-003 |
| Authoring agent | copilot |
| Generated | 2026-09-04T00:17:49.181Z |

## Intent

Replaced the hardcoded development environment hostname (`https://coast-restitution-dev.silver.devops.bcgov`) in the OWASP ZAP dynamic scan workflow with a reference to the repository Actions variable `${{ vars.ZAP_TARGET }}`. Added an inline workflow comment documenting that repository admins must configure the `ZAP_TARGET` variable before the scan can run.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-20.1` ZAP workflow YAML contains no hardcoded development hostname | Yes | `ZAP_TARGET` now reads from `${{ vars.ZAP_TARGET }}`; grep confirms no `silver.devops.bcgov` literal remains in the workflow file. |
| `@R-20.2` Operators are told to set `ZAP_TARGET` in repo settings | Yes | Added an inline comment above `ZAP_TARGET` in the workflow stating that repository admins must set the Actions variable before running the scan. |

## Design system & accessibility

| Check | Result |
| --- | --- |
| DS components used (list) | N/A — CI workflow-only change |
| Tokens used (not hard-coded colour) | N/A — CI workflow-only change |
| BC Sans imported | N/A — CI workflow-only change |
| Manual a11y notes | N/A — CI workflow-only change |

## Public-service minimums

Checklist IDs addressed this PR: N/A — CI workflow-only change

## Tests

| Type | Command / path | Result |
| --- | --- | --- |
| Unit | N/A — CI workflow-only change | |
| Acceptance / feature | `spec/features/sec-secrets-002-zap-target-var.feature` — manual review of `.github/workflows/zap-coast-restitution-dev-scan.yml`; YAML parsed with `python3 -c "import yaml; yaml.safe_load(...)"`; grep verified no real hostname literal remains | Passed |
| A11y automation | N/A — CI workflow-only change | |

## Risks & follow-ups

- The scan will fail at `workflow_dispatch` time until a repository admin sets the `ZAP_TARGET` Actions variable in repo settings (expected — out of scope for this PR per `spec/spec.md`).

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-20.1` and `@R-20.2`; verified `ZAP_TARGET` is sourced from `${{ vars.ZAP_TARGET }}`; verified no real `silver.devops.bcgov` hostname literal remains anywhere in `.github/workflows/zap-coast-restitution-dev-scan.yml`; verified an inline comment documents the required Actions variable setup; validated workflow YAML syntax.

**Could not check:** Live scan execution against a real `ZAP_TARGET` value (no repository variable configured in this sandbox; setting the live value is explicitly out of scope per `spec/spec.md`).

**Residual risk:** None beyond the expected/documented scan failure until an operator sets `ZAP_TARGET`.

- Reviewer: _______________ Date: _______________

---

# PR evidence — [RA VULN-001] Mailing address XSS via unencoded innerHTML

| Field | Value |
| --- | --- |
| PR / branch | copilot/fix-vuln-001-mailing-address-xss |
| Spec refs | `spec/spec.md`; `spec/features/vuln-001-mailing-address-xss.feature` (`@R-21.1`, `@R-21.2`) |
| Constitution articles touched | P3, P5 |
| Tasks | TASK-001, TASK-002, TASK-003, TASK-004 |
| Authoring agent | copilot |
| Generated | 2026-09-04T17:18:53.903Z |

## Intent

`displayMailingSubAddress` and `displayMailingAddress` in `form-base.ts` concatenated raw, user-controlled reactive form values (`line1`, `line2`, `city`, `province`, `country`, `postalCode`) with literal `<br />` tags and returned the result for an Angular `[innerHTML]` binding on the review page, allowing HTML structural markup entered in address fields to render as live HTML. Both helpers now HTML-entity-encode each field (`&`, `<`, `>`, `"`, `'`) via a new `escapeHtml` helper before joining with `<br />`, so line breaks are preserved but markup-like input is rendered as inert text.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-21.1` No unencoded innerHTML address bind | Yes | `displayMailingSubAddress` and `displayMailingAddress` now escape each field with `escapeHtml` before concatenation; normal address values still render with `<br />` line breaks (`form-base.spec.ts`). |
| `@R-21.2` Script-like input is not live HTML | Yes | `form-base.spec.ts` asserts `<img src=x onerror=alert(1)>` input produces `&lt;img src=x onerror=alert(1)&gt;` in the returned string, not a raw `<img` tag. |

## Design system & accessibility

| Check | Result |
| --- | --- |
| DS components used (list) | N/A — no template/UI markup changed, only string-building logic in `form-base.ts` |
| Tokens used (not hard-coded colour) | N/A |
| BC Sans imported | N/A |
| Manual a11y notes | No visual change; address still displays with the same line breaks as before. |

## Public-service minimums

Checklist IDs addressed this PR: N/A — backend-of-UI string encoding fix, no new UI surface

## Tests

| Type | Command / path | Result |
| --- | --- | --- |
| Unit | `npx ng test --include='**/form-base.spec.ts'` (`restitution-app/ClientApp/src/app/shared/form-base.spec.ts`) | Passed (3/3) |
| Acceptance / feature | `spec/features/vuln-001-mailing-address-xss.feature` — covered by the unit tests above | Passed |
| A11y automation | N/A — no template change | |

## Risks & follow-ups

- None noted; both address-building helpers now share the same encoding approach.

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-21.1` and `@R-21.2`; verified `displayMailingSubAddress` and `displayMailingAddress` HTML-encode `line1`, `line2`, `city`, `province`, `country`, `postalCode` before building the `<br />`-joined string; verified `<img src=x onerror=alert(1)>` is returned as `&lt;img src=x onerror=alert(1)&gt;`, not live markup; ran the new `form-base.spec.ts` unit tests (3 passed).

**Could not check:** End-to-end rendering in a live browser via the `restitution-review.component.html` `[innerHTML]` binding (no running app/backend in this sandbox); relied on unit-level verification of the encoded string returned to that binding.

**Residual risk:** None beyond the existing use of `[innerHTML]` itself, which is now fed only entity-encoded text plus literal `<br />` separators.

- Reviewer: _______________ Date: _______________


---

# PR evidence — [RA VULN-002] Server-side document file extension validation

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-vuln-002-client-side-file-validation |
| Spec refs | `spec/spec.md`; `spec/features/vuln-002-server-file-extension.feature` (`@R-22.1`, `@R-22.2`) |
| Constitution articles touched | P3, P5 |
| Tasks | TASK-001–TASK-004 |
| Authoring agent | operator (Copilot stalled after initial plan) |
| Generated | 2026-09-04T17:36:00.000Z |

## Intent

`DocumentDto` / restitution submit previously accepted any filename extension. Server now validates extensions against the ClientApp `config.ts` allowlist (`pdf`, `png`, `jpeg`, `jpg`, `doc`, `docx`, `ppt`) via `DocumentFileExtensionValidation` before Dynamics `ExecuteAsync`, returning HTTP 400 on failure.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-22.1` Disallowed extension → 400 before Dynamics | Yes | `RestitutionsController.SubmitRestitutionInternal` calls `TryValidateDocuments` and returns `BadRequest` |
| `@R-22.2` Allowlist parity unit tests | Yes | `DocumentFileExtensionValidationTests` covers allow/deny without Dynamics |

## Design system & accessibility

| Check | Result |
| --- | --- |
| DS components used (list) | N/A — API validation only |
| Tokens used (not hard-coded colour) | N/A |
| BC Sans imported | N/A |
| Manual a11y notes | N/A |

## Public-service minimums

Checklist IDs addressed this PR: N/A — API validation

## Tests

| Type | Command / path | Result |
| --- | --- | --- |
| Unit | `dotnet test … --filter FullyQualifiedName~DocumentFileExtensionValidation` | Passed (16) |
| Acceptance / feature | `spec/features/vuln-002-server-file-extension.feature` | Covered by unit tests |
| A11y automation | N/A | |

## Risks & follow-ups

- Extension-only check (no MIME sniffing) — deferred per spec out of scope.
- Copilot initial checklist proposed magic-byte / size validation; operator shipped the signed extension-only scope.

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-22.1` and `@R-22.2`; helper allowlist matches client config; controller rejects before Dynamics; unit tests pass.

**Could not check:** Live Dynamics submit path end-to-end (no Dataverse in CI).

**Residual risk:** Extension spoofing without content sniffing remains; deferred to a later finding if filed.

- Reviewer: _______________ Date: _______________

---

# PR evidence — [RA AUTH-002] Set cookie SameSite policy to Lax

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-auth-002-fix-cookie-policy |
| Spec refs | `spec/spec.md`; `spec/features/auth-002-samesite-lax.feature` (`@R-23.1`, `@R-23.2`) |
| Constitution articles touched | P5, J5 |
| Tasks | TASK-001–TASK-003 |
| Authoring agent | unspecified |
| Generated | 2026-09-04T17:44:29.810Z |

## Intent

The global ASP.NET Core cookie policy now sets its minimum SameSite value to Lax. This preserves the existing Secure and HttpOnly requirements while preventing framework-managed cookies from defaulting to broad cross-site use.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-23.1` Cookie minimum SameSite policy is Lax | Yes | `Program.cs` sets `MinimumSameSitePolicy = SameSiteMode.Lax`. |
| `@R-23.2` Unsafe SameSite=None setting is detected | Yes | Focused configuration test asserts Lax is present and None is absent. |


## Design system & accessibility

| Check | Result |
| --- | --- |
| DS components used (list) | N/A — server cookie policy change |
| Tokens used (not hard-coded colour) | N/A — server cookie policy change |
| BC Sans imported | N/A — server cookie policy change |
| Manual a11y notes | N/A — server cookie policy change |

## Public-service minimums

Checklist IDs addressed this PR: N/A — server cookie policy change.

## Tests

| Type | Command / path | Result |
| --- | --- | --- |
| Unit | `dotnet test restitution-app.Tests/restitution-app.Tests.csproj --no-restore` | Passed (41) |
| Acceptance / feature | `spec/features/auth-002-samesite-lax.feature` | Covered by focused configuration test |
| Build | `dotnet build restitution-app/restitution-app.csproj --no-restore` | Passed |
| A11y automation | N/A — server cookie policy change | |

## Risks & follow-ups

- No documented cross-site cookie use requires `SameSite=None`.

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-23.1` and `@R-23.2`; the global policy uses Lax and the regression test rejects None.

**Could not check:** Browser-specific cookie behaviour against a deployed environment; this change only sets the framework policy minimum.

**Residual risk:** An individual cookie explicitly configured with a less restrictive policy could require separate review.

- Reviewer: _______________ Date: _______________


---

# PR evidence — [RA AUTH-003] Anonymous /hc limited to self/process

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-auth-003-fix-health-check-authorization |
| Spec refs | `spec/spec.md`; `spec/features/auth-003-health-check-surface.feature` (`@R-24.1`, `@R-24.2`) |
| Constitution articles touched | P5, J5 |
| Tasks | TASK-001–TASK-004 |
| Authoring agent | operator (Copilot stalled after initial plan) |
| Generated | 2026-09-04T17:56:00.000Z |

## Intent

Anonymous `/hc` now uses `HealthCheckOptions.Predicate = HealthCheckEndpointPolicy.IsAnonymousLivenessCheck`, so only checks tagged `self`/`process` run and appear in the JSON. Dataverse (`dataverse`/`ready`) is excluded from the anonymous detailed payload. No `RequireAuthorization` was added to `/hc`, preserving OpenShift liveness probes. AUTHZ-001 may still address authorizing a ready/detailed endpoint later.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-24.1` Anonymous surface excludes Dataverse details | Yes | Predicate filters to self/process tags |
| `@R-24.2` Self probe remains anonymous | Yes | `/hc` still mapped without RequireAuthorization; covered by Program.cs assertion |

## Design system & accessibility

| Check | Result |
| --- | --- |
| DS components used (list) | N/A — health endpoint |
| Tokens used (not hard-coded colour) | N/A |
| BC Sans imported | N/A |
| Manual a11y notes | N/A |

## Public-service minimums

Checklist IDs addressed this PR: N/A — health endpoint

## Tests

| Type | Command / path | Result |
| --- | --- | --- |
| Unit | `dotnet test … --filter FullyQualifiedName~HealthCheckEndpointPolicy` | Passed (3) |
| Acceptance / feature | `spec/features/auth-003-health-check-surface.feature` | Covered by unit tests |
| A11y automation | N/A | |

## Risks & follow-ups

- AUTHZ-001 (#25) may still require auth or network restriction for any ready/detailed health surface.
- No separate `/hc/ready` added in this slice.

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-24.1` and `@R-24.2`; predicate excludes Dataverse tags; Program keeps anonymous MapHealthChecks for `/hc`.

**Could not check:** Live OpenShift probe against a deployed pod.

**Residual risk:** AUTHZ-001 overlap remains for ready/authZ hardening.

- Reviewer: _______________ Date: _______________

---

# PR evidence — [RA AUTHZ-001] Health check status-only anonymous response

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-authz-001-fix-health-check-access |
| Spec refs | `spec/spec.md`; `spec/features/authz-001-health-status-only.feature` (`@R-25.1`, `@R-25.2`) |
| Constitution articles touched | P5, P7, J5 |
| Tasks | TASK-001–TASK-004 |
| Authoring agent | Copilot coding agent |
| Generated | 2026-09-04T18:12:29.347Z |

## Intent

Anonymous `/hc` remains available for OpenShift-style liveness probes, but its JSON body is now status-only. The response no longer serializes health-check names, a checks array, or description strings, and the existing AUTH-003 predicate continues to limit the anonymous surface to self/process checks.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-25.1` Anonymous `/hc` returns overall status without check inventory | Yes | `HealthCheckStatusResponseWriter.WriteStatusOnlyAsync` serializes only `{ status }`; tests assert no check names/descriptions are present. |
| `@R-25.2` OpenShift-style self probe remains anonymous | Yes | `/hc` remains mapped without `RequireAuthorization`; the existing self/process predicate is preserved. |

## Design system & accessibility

| Check | Result |
| --- | --- |
| DS components used (list) | N/A — API health endpoint |
| Tokens used (not hard-coded colour) | N/A |
| BC Sans imported | N/A |
| Manual a11y notes | N/A |

## Public-service minimums

Checklist IDs addressed this PR: N/A — non-UI health endpoint response hardening.

## Tests

| Type | Command / path | Result |
| --- | --- | --- |
| Unit | `dotnet test restitution-app.Tests/restitution-app.Tests.csproj --filter FullyQualifiedName~HealthCheckEndpointPolicy` | Passed (4) |
| Acceptance / feature | `spec/features/authz-001-health-status-only.feature` | Covered by focused unit/config tests |
| A11y automation | N/A | Non-UI change |

## Risks & follow-ups

- Live OpenShift probe verification is deferred to environment CI/deployment because this local sandbox lacks a deployed pod.
- AUTHZ-002 (`UseAuthorization`) remains explicitly out of scope for this slice.

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-25.1` and `@R-25.2`; Program keeps anonymous `/hc`, preserves the AUTH-003 predicate, and uses the status-only response writer.

**Could not check:** Live OpenShift probe against a deployed pod.

**Residual risk:** Detailed/ready health endpoint design remains a separate future slice if product requires authenticated readiness detail.

- Reviewer: _______________ Date: _______________

---

# PR evidence — [RA AUTHZ-002] UseAuthorization middleware in pipeline

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-authz-002-fix-useauthorization-middleware |
| Spec refs | `spec/spec.md`; `spec/features/authz-002-use-authorization.feature` (`@R-26.1`, `@R-26.2`) |
| Constitution articles touched | P5, J5 |
| Tasks | TASK-001–TASK-005 |
| Authoring agent | Copilot coding agent |
| Generated | 2026-09-04T18:25:05.097Z |

## Intent

Authorization services are now registered (`services.AddAuthorization()`) and `app.UseAuthorization()` runs after `UseRouting()` and before `MapControllers()`. Any future `[Authorize]` attribute on a controller or action is therefore enforced instead of silently ignored. No authentication scheme is added, no existing endpoint is protected, and anonymous `/hc` liveness is unchanged.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-26.1` Pipeline registers and invokes authorization | Yes | `services.AddAuthorization()` in service registration; `app.UseAuthorization()` between `UseRouting()` and `MapControllers()`; `AuthorizationPipelineTests` asserts registration and ordering in `Program.cs`. |
| `@R-26.2` Anonymous health probe remains unauthenticated | Yes | `/hc` is still mapped without `RequireAuthorization`, and no fallback authorization policy is configured; asserted by `AuthorizationPipelineTests`. |

## Design system & accessibility

| Check | Result |
| --- | --- |
| DS components used (list) | N/A — API middleware change |
| Tokens used (not hard-coded colour) | N/A |
| BC Sans imported | N/A |
| Manual a11y notes | N/A |

## Public-service minimums

Checklist IDs addressed this PR: N/A — non-UI middleware pipeline change.

## Tests

| Type | Command / path | Result |
| --- | --- | --- |
| Unit | `dotnet test restitution-app.Tests/restitution-app.Tests.csproj --filter FullyQualifiedName~AuthorizationPipeline` | Passed (3) |
| Acceptance / feature | `spec/features/authz-002-use-authorization.feature` | Covered by focused unit/config tests |
| A11y automation | N/A | Non-UI change |

## Risks & follow-ups

- Authentication schemes (`AddAuthentication`) and protecting specific endpoints remain out of scope; `[Authorize]` without a scheme would fail closed with a 401/500 until authentication is designed.

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-26.1` and `@R-26.2`; `Program.cs` service registration and middleware order; anonymous `/hc` mapping unchanged; focused tests pass.

**Could not check:** End-to-end enforcement of a real `[Authorize]` endpoint, since no authentication scheme exists in this slice.

**Residual risk:** Authentication design (scheme, token validation, per-endpoint policies) remains a separate future slice.

- Reviewer: _______________ Date: _______________

---

# PR evidence — [RA CONFIG-004] Missing Permissions-Policy header

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-config-004-missing-permissions-policy-header |
| Spec refs | `spec/spec.md`; `spec/features/config-004-permissions-policy.feature` (`@R-27.1`) |
| Constitution articles touched | P5, J5 |
| Tasks | TASK-001, TASK-002, TASK-003 |
| Authoring agent | Copilot coding agent |
| Generated | 2026-09-04T18:35:54.198Z |

## Intent

Configured a restrictive `Permissions-Policy` HTTP response header denying unused powerful browser features (`accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=(), usb=()`) in both the Caddy static file server config (`Caddyfile`) and the ASP.NET Core middleware pipeline (`Program.cs`).

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-27.1` Permissions-Policy header is configured | Yes | Added `Permissions-Policy` header to `Caddyfile` and `Program.cs`; verified presence in both configurations via unit tests in `PermissionsPolicyConfigurationTests.cs`. |

## Design system & accessibility

| Check | Result |
| --- | --- |
| DS components used (list) | N/A — server / reverse proxy security header change |
| Tokens used (not hard-coded colour) | N/A |
| BC Sans imported | N/A |
| Manual a11y notes | N/A |

## Public-service minimums

Checklist IDs addressed this PR: N/A — security header configuration change.

## Tests

| Type | Command / path | Result |
| --- | --- | --- |
| Unit | `dotnet test restitution-app.Tests/restitution-app.Tests.csproj --filter FullyQualifiedName~PermissionsPolicyConfiguration` | Passed (2) |
| Acceptance / feature | `spec/features/config-004-permissions-policy.feature` | Covered by focused unit/config tests |
| A11y automation | N/A | Non-UI change |

## Risks & follow-ups

- None. Unused powerful browser features are denied by default without impacting existing functionality.

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-27.1`; `Program.cs` and `Caddyfile` security header configuration; verified restrictive directives deny unused features; unit tests pass.

**Could not check:** Live HTTP response headers from a running Caddy instance or OpenShift deployment (sandbox lacks running deployment).

**Residual risk:** None.

- Reviewer: _______________ Date: _______________

---

# PR evidence — [RA CONFIG-005] AllowedHosts set to wildcard — host header filtering disabled

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-config-005-fix-allowed-hosts |
| Spec refs | `spec/spec.md`; `spec/features/config-005-allowed-hosts.feature` (`@R-28.1`) |
| Constitution articles touched | P5, J5 |
| Tasks | TASK-001, TASK-002, TASK-003 |
| Authoring agent | Copilot coding agent |
| Generated | 2026-09-04T18:48:38.489Z |

## Intent

Replaced `AllowedHosts: "*"` in `restitution-app/appsettings.json` with a specific loopback default (`localhost;127.0.0.1;[::1]`) so ASP.NET Core host header filtering is active. Added unit/config test to ensure committed base config is not set to `*`. Production/OpenShift hostnames remain environment-configurable via ASP.NET Core `AllowedHosts` or `ASPNETCORE_ALLOWEDHOSTS` environment variable settings without hardcoding hostnames in repo files.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-28.1` AllowedHosts is specific and configurable | Yes | Tightened `AllowedHosts` in `appsettings.json` away from `*` to `localhost;127.0.0.1;[::1]`. Added config unit test in `AllowedHostsConfigurationTests.cs`. Env-configurable for deployment via standard ASP.NET Core configuration environment variable overlay. |

## Design system & accessibility

| Check | Result |
| --- | --- |
| DS components used (list) | N/A — backend configuration change |
| Tokens used (not hard-coded colour) | N/A |
| BC Sans imported | N/A |
| Manual a11y notes | N/A |

## Public-service minimums

Checklist IDs addressed this PR: N/A — backend security configuration change.

## Tests

| Type | Command / path | Result |
| --- | --- | --- |
| Unit | `dotnet test restitution-app.Tests/restitution-app.Tests.csproj --filter FullyQualifiedName~AllowedHostsConfiguration` | Passed (1) |
| Acceptance / feature | `spec/features/config-005-allowed-hosts.feature` | Covered by focused unit/config tests |
| A11y automation | N/A | Non-UI change |

## Risks & follow-ups

- When deploying to OpenShift or staging/production environments, operations/deployment manifests must supply the appropriate environment overlay (`AllowedHosts` or `ASPNETCORE_ALLOWEDHOSTS`) matching the environment route hostnames if they differ from localhost loopback addresses.

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-28.1`; `restitution-app/appsettings.json` configuration; verified AllowedHosts is not `*`; unit tests pass.

**Could not check:** Deployment environment runtime behavior across multi-host OpenShift routes (requires OpenShift deployment environment).

**Residual risk:** OpenShift deployments with custom route hostnames require environment configuration overlay (`AllowedHosts` or `ASPNETCORE_ALLOWEDHOSTS`) to match route hostnames.

- Reviewer: _______________ Date: _______________

---

# PR evidence — [RA CRYPTO-002] Remove Splunk Development TLS certificate bypass

| Field | Value |
| --- | --- |
| PR / branch | copilot/fix-tls-certificate-validation |
| Spec refs | `spec/spec.md`; `spec/features/crypto-002-splunk-tls.feature` (`@R-29.1`) |
| Constitution articles touched | P5, J3, J5 |
| Tasks | TASK-001, TASK-002, TASK-003 |
| Authoring agent | Copilot coding agent |
| Generated | 2026-09-04T19:00:24.738Z |

## Intent

Removed the `env.IsDevelopment()`-gated `HttpClientHandler.DangerousAcceptAnyServerCertificateValidator` assignment used for the Serilog Splunk HTTP Event Collector sink. TLS certificate validation for the Splunk HEC connection is now always enforced by the default `HttpClient`/`HttpClientHandler` certificate validation, regardless of `ASPNETCORE_ENVIRONMENT`, closing the MITM risk if `Development` were mis-set in a non-isolated deployment. No opt-in insecure bypass flag was introduced since local Splunk connectivity is out of scope for this change; if a local dev Splunk instance uses a self-signed/internal CA certificate, that CA should be trusted in the local OS trust store instead.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-29.1` Certificate validation is not disabled solely because the environment is Development | Yes | Removed the `if (env.IsDevelopment())` block that built an `HttpClientHandler` with `DangerousAcceptAnyServerCertificateValidator` in `restitution-app/Program.cs`; `WriteTo.EventCollector(...)` no longer passes a custom `messageHandler`, so default TLS certificate validation applies in all environments. No remaining bypass exists, so no opt-in flag was needed. Added `SplunkTlsConfigurationTests.cs` asserting the source no longer references `DangerousAcceptAnyServerCertificateValidator`. |

## Design system & accessibility

| Check | Result |
| --- | --- |
| DS components used (list) | N/A — backend logging configuration change |
| Tokens used (not hard-coded colour) | N/A |
| BC Sans imported | N/A |
| Manual a11y notes | N/A |

## Public-service minimums

Checklist IDs addressed this PR: N/A — backend security configuration change.

## Tests

| Type | Command / path | Result |
| --- | --- | --- |
| Unit | `dotnet test restitution-app.Tests/restitution-app.Tests.csproj --filter FullyQualifiedName~SplunkTlsConfiguration` | Passed (1) |
| Build | `dotnet build` (restitution-app) | Succeeded, 0 warnings, 0 errors |
| Acceptance / feature | `spec/features/crypto-002-splunk-tls.feature` | Covered by focused unit/config test |
| A11y automation | N/A | Non-UI change |

## Risks & follow-ups

- If a local development Splunk instance requires a self-signed/internal CA certificate, that certificate should be installed into the OS trust store rather than reintroducing a code-level bypass.
- Confirm `ASPNETCORE_ENVIRONMENT` is never set to `Development` in test, staging, or production deployments (deployment/ops responsibility, outside this code change).

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-29.1`; `restitution-app/Program.cs` Splunk `EventCollector` sink configuration; verified no `DangerousAcceptAnyServerCertificateValidator` reference remains; unit test and build pass.

**Could not check:** Live Splunk HEC connectivity (no Splunk instance available in this environment); OpenShift/production `ASPNETCORE_ENVIRONMENT` values (requires deployment environment).

**Residual risk:** None identified for this code path; operational risk remains if `ASPNETCORE_ENVIRONMENT` is mis-set to `Development` in a non-isolated deployment, but that no longer disables Splunk TLS certificate validation.

- Reviewer: _______________ Date: _______________


---

# PR evidence — [RA DEP-004] ts-node bumped to ^10.x

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-dep-004-update-ts-node-version |
| Spec refs | `spec/spec.md`; `spec/features/dep-004-ts-node.feature` (`@R-30.1`) |
| Constitution articles touched | P5 |
| Tasks | TASK-001–TASK-003 |
| Authoring agent | operator (Copilot quota insufficient) |
| Generated | 2026-09-04T19:17:32.000Z |

## Intent

ClientApp `ts-node` moved from `~7.0.0` / lockfile 7.0.1 to `^10.9.2` with lockfile resolving a 10.x release. Dev-only tooling for TypeScript configs; no production runtime change.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-30.1` ts-node ^10.x with lockfile | Yes | package.json + package-lock.json |

## Design system & accessibility

| Check | Result |
| --- | --- |
| DS components used (list) | N/A — dependency bump |
| Tokens used (not hard-coded colour) | N/A |
| BC Sans imported | N/A |
| Manual a11y notes | N/A |

## Public-service minimums

Checklist IDs addressed this PR: N/A — dependency bump

## Tests

| Type | Command / path | Result |
| --- | --- | --- |
| Lockfile | Inspect `node_modules/ts-node` version in package-lock.json | 10.x |
| Acceptance / feature | `spec/features/dep-004-ts-node.feature` | Covered by package metadata |
| CI | CI RESTITUTION npm + e2e after push | Pending |

## Risks & follow-ups

- Broader Angular/npm upgrades remain out of scope.

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-30.1`; package.json range; lockfile 10.x resolve.

**Could not check:** Copilot cloud session (quota blocked).

**Residual risk:** None material for this slice beyond normal lockfile drift.

- Reviewer: _______________ Date: _______________

---

# PR evidence — [RA DEP-005] Remove moment.js from ClientApp

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-dep-005-remove-moment |
| Spec refs | `spec/spec.md`; `spec/features/dep-005-moment.feature` (`@R-31.1`, `@R-31.2`) |
| Constitution articles touched | P5 |
| Tasks | TASK-001–TASK-005 |
| Authoring agent | operator (Copilot quota insufficient) |
| Generated | 2026-09-04T19:34:00.000Z |

## Intent

ClientApp no longer depends on maintenance-mode moment.js. Date field compare/patch and review formatting use native `Date` / `Intl`; Material date providers use `NativeDateAdapter` + `MAT_NATIVE_DATE_FORMATS`. Packages `moment` and `@angular/material-moment-adapter` removed.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-31.1` packages and imports removed | Yes | package.json + lockfile; no TS imports |
| `@R-31.2` native Date compare/format | Yes | date-field + form-base.datesOrEmpty + unit tests |

## Design system & accessibility

| Check | Result |
| --- | --- |
| DS components used (list) | N/A — date library migration |
| Tokens used (not hard-coded colour) | N/A |
| BC Sans imported | N/A |
| Manual a11y notes | N/A |

## Public-service minimums

Checklist IDs addressed this PR: N/A — dependency migration

## Tests

| Type | Command / path | Result |
| --- | --- | --- |
| Unit | `npx ng test --watch=false --include='**/form-base.spec.ts'` | 5 passed |
| Build | `npm run buildprod` | Success |
| Acceptance / feature | `spec/features/dep-005-moment.feature` | Covered by package + unit |

## Risks & follow-ups

- Review date display uses Intl + ordinal day approximating former moment `MMM Do, Y`; Material pickers use native formats (YYYY-MM-DD style may differ slightly from prior moment MY_FORMATS).
- Unused `MY_FORMATS` export left in `enums-list.ts` (harmless).

## Review receipt (checkpoint 3)

Same shape as `REVIEW.md` — required before merge. Do not replace with a free-form sign-off.

**Checked:** `@R-31.1`, `@R-31.2`; date-field/form-base; NativeDateAdapter providers; package removal; unit tests; production build.

**Could not check:** Copilot cloud session (quota blocked); live browser date-picker smoke.

**Residual risk:** Cosmetic date display token differences vs prior moment formats.

- Reviewer: _______________ Date: _______________

---

# PR evidence — [RA LOG-003] Failure log without full OrganizationResponse

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-log-003-response-logging |
| Spec refs | `spec/spec.md`; `spec/features/log-003-response-logging.feature` (`@R-32.1`) |
| Constitution articles touched | P5, J5 |
| Tasks | TASK-001–TASK-003 |
| Authoring agent | operator (Copilot quota insufficient) |
| Generated | 2026-09-04T19:55:00.000Z |

## Intent

Dynamics submit failure no longer logs `{@Response}`. `RestitutionSubmitAudit.WriteDynamicsFailure` records IsSuccess, ErrorCode (when present), and result key names only.

## Spec traceability

| Scenario / requirement | Implemented? | Notes |
| --- | --- | --- |
| `@R-32.1` no full OrganizationResponse destructuring | Yes | Controller + helper + unit tests |

## Tests

| Type | Command / path | Result |
| --- | --- | --- |
| Unit | `dotnet test --filter RestitutionSubmitAudit` | 8 passed |

## Review receipt (checkpoint 3)

**Checked:** `@R-32.1`; no `{@Response}` in Controllers; helper scalars only.

**Could not check:** Live Dynamics failure payload shapes.

**Residual risk:** ErrorCode value content depends on Dynamics; key names only otherwise.

- Reviewer: _______________ Date: _______________

---

# PR evidence — [RA LOG-004] Production console suppression

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-log-004-console-suppression |
| Spec refs | `spec/spec.md`; `spec/features/log-004-console-suppression.feature` (`@R-33.1`) |
| Tasks | TASK-001–TASK-002 |
| Authoring agent | operator (Copilot quota insufficient) |
| Generated | 2026-09-04T20:00:00.000Z |

## Intent

Production `main.ts` now no-ops `console.log`, `console.error`, `console.debug`, and `console.warn`.

## Spec traceability

| Scenario | Implemented? |
| --- | --- |
| `@R-33.1` | Yes |

## Review receipt (checkpoint 3)

**Checked:** main.ts production block.

**Residual risk:** None material; call sites remain but are silent in production.

- Reviewer: _______________ Date: _______________

---

# PR evidence — [RA SEC-SECRETS-003] Enforcing Trivy secret scan

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-sec-secrets-003-trivy-secret |
| Spec refs | `spec/features/sec-secrets-003-trivy-secret.feature` (`@R-34.1`) |
| Authoring agent | operator (Copilot quota insufficient) |
| Generated | 2026-09-04T20:05:00.000Z |

## Intent

CI gate adds Trivy FS `scanners: secret` with `exit-code: 1` and SARIF upload. Assessment `build-template.yml` / non-enforcing secret exit-code comments are stale; vuln FS was already enforcing.

## Spec traceability

| `@R-34.1` | Yes — ci-restitution.yml |

## Review receipt (checkpoint 3)

**Checked:** workflow YAML secret scanners + exit-code 1.

**Residual risk:** False positives could fail gate — keep enforcement; scope scanners to secret only.

- Reviewer: _______________ Date: _______________

---

# PR evidence — [RA VULN-003] Development-only exception page proof

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-vuln-003-exception-page |
| Spec refs | `vuln-003-exception-page.feature` (`@R-35.1`) |
| Authoring agent | operator (Copilot quota insufficient) |
| Generated | 2026-09-04T20:15:00.000Z |

## Intent

ExceptionPagePolicy already Development-only (CONFIG-003/LOG-001). Expanded regression coverage for QA/UAT/PreProduction/Staging/Test/Production and Program.cs gate assertion.

## Spec traceability

| `@R-35.1` | Yes — tests prove staging-like names denied |

## Review receipt (checkpoint 3)

**Checked:** policy + tests; no policy change required.

- Reviewer: _______________ Date: _______________

---

# PR evidence — [RA VULN-004] MaxLength on submission DTO strings

| Field | Value |
| --- | --- |
| PR / branch | copilot/ra-vuln-004-dto-maxlength |
| Spec refs | `vuln-004-dto-maxlength.feature` (`@R-36.1`) |
| Authoring agent | operator (Copilot quota insufficient) |
| Generated | 2026-09-04T20:20:00.000Z |

## Intent

Proportionate `[MaxLength]` on ParticipantDto, DocumentDto, RestitutionApplicationDtoBase, and VictimApplicationDto name fields. Unit tests prove overlong values fail `Validator.TryValidateObject`.

## Spec traceability

| `@R-36.1` | Yes |

## Review receipt (checkpoint 3)

**Checked:** attributes + SubmissionDtoMaxLengthTests.

- Reviewer: _______________ Date: _______________
