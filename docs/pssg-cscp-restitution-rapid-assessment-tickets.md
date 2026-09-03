# CSCP Restitution — Rapid Assessment ticket backlog

Source: rapid assessment `ra-2026-07-16T153047Z` (copied from ai-sdlc `comms/drafts/pssg-cscp-restitution-rapid-assessment/`).
Target repo for issues: [`bcgov/pssg-cscp-restitution-agentic`](https://github.com/bcgov/pssg-cscp-restitution-agentic).

Companion CSV (same rows): [`pssg-cscp-restitution-rapid-assessment-tickets.csv`](pssg-cscp-restitution-rapid-assessment-tickets.csv)

**How to use:** pick one row with **GitHub** = `pending` and **File?** = `yes`, open the matching detail section below, then create the issue. After creating, replace `pending` with the issue number/URL.

Informational rows are **optional**. No synthesis duplicate groups — file each `yes` row as its own issue.

Generated: 2026-09-03 · Raw findings: **39** · Recommended to file: **36** · Optional (Informational): **3**

## Index

| ID | Severity | Domain | Component | Title | File? | GitHub |
| --- | --- | --- | --- | --- | --- | --- |
| CONFIG-001 | High | CONFIGURATION | cicd-pipeline | CI/CD security gate disabled — Trivy exit-code set to 0 on all scan s... | yes | pending |
| DEP-001 | High | DEPENDENCIES | health-checks | Microsoft.AspNetCore.HealthChecks 1.0.0 is an archived community pack... | yes | pending |
| F-TEST-003 | High | TESTING | aspnet-api | ASP.NET Core API has zero automated tests | yes | pending |
| F-TEST-004 | High | TESTING | dataverse-client | Dataverse Client Library has zero automated tests | yes | pending |
| F-TEST-007 | High | TESTING | angular-spa | No security-focused tests exist in any component | yes | pending |
| AUTH-001 | Medium | AUTHENTICATION | dataverse-client | ADFS Token Provider Uses Deprecated Resource Owner Password Credentia... | yes | pending |
| CONFIG-002 | Medium | CONFIGURATION | aspnet-api | CSP header includes unsafe-eval and unsafe-inline in script-src witho... | yes | pending |
| CONFIG-003 | Medium | CONFIGURATION | aspnet-api | Developer Exception Page active in all non-production environments | yes | pending |
| CRYPTO-001 | Medium | CRYPTOGRAPHY | aspnet-api | Data Protection Key Ring Persisted Without At-Rest Encryption | yes | pending |
| DEP-002 | Medium | DEPENDENCIES | aspnet-api | EOL Microsoft.NETCore.App 2.2.8 / Microsoft.NETCore.Jit 2.0.8 in a ne... | yes | pending |
| DEP-003 | Medium | DEPENDENCIES | aspnet-api | No NuGet lock files; unpinned transitive dependencies (supply-chain r... | yes | pending |
| DEP-006 | Medium | DEPENDENCIES | cicd-pipeline | OpenShift Dockerfile uses .NET 8 base image while app targets net10.0... | yes | pending |
| F-TEST-001 | Medium | TESTING | cicd-pipeline | No automated test execution stage in CI pipeline | yes | pending |
| F-TEST-002 | Medium | TESTING | cicd-pipeline | All security-scanning quality gates configured as non-blocking | yes | pending |
| F-TEST-005 | Medium | TESTING | angular-spa | Angular unit tests contain only boilerplate stubs with no meaningful ... | yes | pending |
| F-TEST-006 | Medium | TESTING | e2e-tests | Playwright E2E tests exist but are not integrated into the CI pipeline | yes | pending |
| LOG-001 | Medium | SECURITY_LOGGING | aspnet-api | Developer Exception Page exposes full stack traces in non-production ... | yes | pending |
| LOG-002 | Medium | SECURITY_LOGGING | aspnet-api | No application-level audit logging for successful restitution form su... | yes | pending |
| SEC-SECRETS-001 | Medium | SECRETS | aspnet-api | Internal BC Government infrastructure URLs hardcoded in developer doc... | yes | pending |
| SEC-SECRETS-002 | Medium | SECRETS | cicd-pipeline | Development environment hostname hardcoded in OWASP ZAP security scan... | yes | pending |
| VULN-001 | Medium | CODE_VULNERABILITY | angular-spa | DOM-based XSS via [innerHTML] binding with unencoded user-controlled ... | yes | pending |
| VULN-002 | Medium | CODE_VULNERABILITY | aspnet-api | Client-side-only file type validation — no server-side MIME or extens... | yes | pending |
| AUTH-002 | Low | AUTHENTICATION | aspnet-api | Global Cookie Policy Sets MinimumSameSitePolicy to SameSiteMode.None | yes | pending |
| AUTH-003 | Low | AUTHENTICATION | health-checks | Health Check Endpoint /hc Accessible Without Authentication | yes | pending |
| AUTHZ-001 | Low | AUTHORIZATION | health-checks | Health Check Endpoint Publicly Accessible Without Authorization — Dis... | yes | pending |
| AUTHZ-002 | Low | AUTHORIZATION | aspnet-api | UseAuthorization() Middleware Absent from Pipeline — [Authorize] Attr... | yes | pending |
| CONFIG-004 | Low | CONFIGURATION | aspnet-api | Missing Permissions-Policy header | yes | pending |
| CONFIG-005 | Low | CONFIGURATION | aspnet-api | AllowedHosts set to wildcard — host header filtering disabled | yes | pending |
| CRYPTO-002 | Low | CRYPTOGRAPHY | aspnet-api | TLS Certificate Validation Disabled for Splunk HTTP Sink in Developme... | yes | pending |
| DEP-004 | Low | DEPENDENCIES | e2e-tests | ts-node pinned to 7.0.1 (2018), three major versions behind (dev-only... | yes | pending |
| DEP-005 | Low | DEPENDENCIES | angular-spa | moment.js 2.30.1 in maintenance mode; no known CVEs at assessment time | yes | pending |
| LOG-003 | Low | SECURITY_LOGGING | aspnet-api | Full Dynamics OrganizationResponse object logged via Serilog destruct... | yes | pending |
| LOG-004 | Low | SECURITY_LOGGING | angular-spa | Incomplete console output suppression in production — console.error a... | yes | pending |
| SEC-SECRETS-003 | Low | SECRETS | cicd-pipeline | Trivy secret scanner configured with non-enforcing exit code across a... | yes | pending |
| VULN-003 | Low | CODE_VULNERABILITY | aspnet-api | Developer exception page enabled for all non-production environments ... | yes | pending |
| VULN-004 | Low | CODE_VULNERABILITY | aspnet-api | No input length or format constraints on submission API DTO string fi... | yes | pending |
| AUTHZ-003 | Informational | AUTHORIZATION | aspnet-api | [AllowAnonymous] Attribute Applied Without Authorization Framework — ... | optional | pending |
| CRYPTO-003 | Informational | CRYPTOGRAPHY | dataverse-client | OAuth Access Tokens Cached as Plaintext in In-Process Memory | optional | pending |
| LOG-005 | Informational | SECURITY_LOGGING | dataverse-client | Token endpoint URLs including Entra ID tenant ID logged at Debug level | optional | pending |

---

## Ticket details (copy into GitHub)

### CONFIG-001

- **Suggested title:** [RA CONFIG-001] CI/CD security gate disabled — Trivy exit-code set to 0 on all scan steps
- **Severity:** High
- **Domain / OWASP / CWE:** CONFIGURATION / A05:2021 / CWE-16
- **Component:** cicd-pipeline
- **Location:** `.github/workflows/ci-restitution.yml:161-169`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:high, domain:configuration`
- **Cross-refs:** —

<details>
<summary>Issue body for CONFIG-001</summary>

```markdown
## Problem

All Trivy vulnerability and secret scanner steps across ci-restitution.yml and build-template.yml have exit-code: '0', including the step explicitly designated as the security gate. CRITICAL and HIGH findings are surfaced in SARIF reports but do not fail the build or block deployment. An inline comment ('# 1 Will fail CI if CRITICAL or HIGH vulns or secrets are found') confirms the intended value was 1. The same pattern is present in the reusable build-template.yml for both filesystem and container image scans.

## Evidence / location

- `.github/workflows/ci-restitution.yml:161-169`

```
ci-restitution.yml lines 161-169: '- name: Run Trivy for SARIF Report and Security Gate' with 'exit-code: "0" # 1 Will fail CI if CRITICAL or HIGH vulns or secrets are found'. build-template.yml lines 125-135: '- name: Scan Image for Vulnerabilities' comment states 'fails build if needed' but uses exit-code: "0".
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `CONFIG-001`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### DEP-001

- **Suggested title:** [RA DEP-001] Microsoft.AspNetCore.HealthChecks 1.0.0 is an archived community package (...
- **Severity:** High
- **Domain / OWASP / CWE:** DEPENDENCIES / A06:2021 / CWE-1104
- **Component:** health-checks
- **Location:** `restitution-app/restitution-app.csproj:14`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:high, domain:dependencies`
- **Cross-refs:** —

<details>
<summary>Issue body for DEP-001</summary>

```markdown
## Problem

Microsoft.AspNetCore.HealthChecks v1.0.0 is an archived community pre-release package from the aspnet/HealthChecks GitHub repository. This repository was archived after ASP.NET Core 2.2 incorporated health checks natively into Microsoft.Extensions.Diagnostics.HealthChecks. Version 1.0.0 has not received security updates since approximately 2017 and no maintainer is active.

## Impact

No security patches can be applied to this package if vulnerabilities are discovered. The archived package lacks the security guarantees and active maintenance of the official in-box ASP.NET Core health checks implementation.

## Evidence / location

- `restitution-app/restitution-app.csproj:14`

```
<PackageReference Include="Microsoft.AspNetCore.HealthChecks" Version="1.0.0" />
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `DEP-001`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### F-TEST-003

- **Suggested title:** [RA F-TEST-003] ASP.NET Core API has zero automated tests
- **Severity:** High
- **Domain / OWASP / CWE:** TESTING / — / —
- **Component:** aspnet-api
- **Location:** `restitution-app/Controllers/RestitutionsController.cs:1-13`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:high, domain:testing`
- **Cross-refs:** —

<details>
<summary>Issue body for F-TEST-003</summary>

```markdown
## Problem

using Microsoft.AspNetCore.Mvc;
using restitution_app.Services;
using restitution_app.ViewModels;

namespace restitution_app.Controllers
{
    public class RestitutionsController : Controller
    {

## Evidence / location

- `restitution-app/Controllers/RestitutionsController.cs:1-13`

```
Filesystem search for `*Tests.cs` across the entire workspace returned zero results. The `RestitutionsController`, `LookupController`, `ConfigurationController`, and `LookupQueryService` have no unit or integration tests. Form submission logic, input validation, and Dataverse write operations are entirely unverified by automated tests.
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `F-TEST-003`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### F-TEST-004

- **Suggested title:** [RA F-TEST-004] Dataverse Client Library has zero automated tests
- **Severity:** High
- **Domain / OWASP / CWE:** TESTING / — / —
- **Component:** dataverse-client
- **Location:** `Database/Extensions/EntraIdTokenProvider.cs:1-10`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:high, domain:testing`
- **Cross-refs:** —

<details>
<summary>Issue body for F-TEST-004</summary>

```markdown
## Problem

// Filesystem search for '*Tests.cs' returned 0 results across the entire workspace.
// Filesystem search for test directories under Database/ returned no matches.

## Evidence / location

- `Database/Extensions/EntraIdTokenProvider.cs:1-10`

```
No test files exist under the `Database/` project. The `EntraIdTokenProvider`, `ADFSTokenProvider`, `DataverseContext`, `ServiceCollectionExtensions`, and `MemoryCache` classes — which manage authentication token acquisition, caching, and Dataverse connectivity — have no unit or integration tests. Errors in token refresh, cache invalidation, or ADFS configuration cannot be detected automatically.
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `F-TEST-004`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### F-TEST-007

- **Suggested title:** [RA F-TEST-007] No security-focused tests exist in any component
- **Severity:** High
- **Domain / OWASP / CWE:** TESTING / — / —
- **Component:** angular-spa
- **Location:** `restitution-app/ClientApp/e2e/tests/victim.spec.ts:1-13`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:high, domain:testing`
- **Cross-refs:** —

<details>
<summary>Issue body for F-TEST-007</summary>

```markdown
## Problem

import { expect, test } from '@playwright/test';
import { VICTIM_DATA } from '../fixtures/test-data';

## Evidence / location

- `restitution-app/ClientApp/e2e/tests/victim.spec.ts:1-13`

```
All 13 spec files (4 Playwright E2E + 9 Karma unit) were inspected. No test verifies: rejection of unauthenticated API requests; CSRF token validation; input sanitisation against injection payloads; file upload type/size enforcement at the API level; or rate-limiting on form submission. E2E tests cover UI functional flows and validation messages only, not security properties.
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `F-TEST-007`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### AUTH-001

- **Suggested title:** [RA AUTH-001] ADFS Token Provider Uses Deprecated Resource Owner Password Credentials (...
- **Severity:** Medium
- **Domain / OWASP / CWE:** AUTHENTICATION / A07:2021 / CWE-287
- **Component:** dataverse-client
- **Location:** `Database/Extensions/ADFSTokenProvider.cs:35-58`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:medium, domain:authentication`
- **Cross-refs:** —

<details>
<summary>Issue body for AUTH-001</summary>

```markdown
## Problem

The ADFSTokenProvider acquires Dataverse access tokens using the OAuth 2.0 Resource Owner Password Credentials grant (RequestPasswordTokenAsync), passing a service account username and password directly to the ADFS token endpoint. ROPC is explicitly removed from OAuth 2.1, does not support multi-factor authentication, and prevents adoption of conditional-access or phishing-resistant authentication policies.

## Evidence / location

- `Database/Extensions/ADFSTokenProvider.cs:35-58`

```
RequestPasswordTokenAsync called with PasswordTokenRequest containing UserName = options.ServiceAccountName and Password = options.ServiceAccountPassword at lines 40-53. The ROPC grant type transmits service account credentials directly and does not permit MFA enforcement on the Dataverse integration path.
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `AUTH-001`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### CONFIG-002

- **Suggested title:** [RA CONFIG-002] CSP header includes unsafe-eval and unsafe-inline in script-src without...
- **Severity:** Medium
- **Domain / OWASP / CWE:** CONFIGURATION / A05:2021 / CWE-16
- **Component:** aspnet-api
- **Location:** `restitution-app/Program.cs:223-233`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:medium, domain:configuration`
- **Cross-refs:** —

<details>
<summary>Issue body for CONFIG-002</summary>

```markdown
## Problem

A Content-Security-Policy header is appended unconditionally (all environments) by an inline middleware lambda. The script-src directive includes 'unsafe-eval' and 'unsafe-inline', weakening XSS protection. In production, a second stricter CSP from NWebsec UseCsp() mitigates this via dual-header enforcement; however, the permissive header is still emitted in every production response. The correctness of enforcement depends on the NWebsec block remaining present and unmodified.

## Evidence / location

- `restitution-app/Program.cs:223-233`

```
restitution-app/Program.cs lines 223-233: app.Use(async (ctx, next) => { ctx.Response.Headers.Append("Content-Security-Policy", "script-src 'self' 'unsafe-eval' 'unsafe-inline' ..."); }).
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `CONFIG-002`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### CONFIG-003

- **Suggested title:** [RA CONFIG-003] Developer Exception Page active in all non-production environments
- **Severity:** Medium
- **Domain / OWASP / CWE:** CONFIGURATION / A05:2021 / CWE-489
- **Component:** aspnet-api
- **Location:** `restitution-app/Program.cs:131-138`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:medium, domain:configuration`
- **Cross-refs:** —

<details>
<summary>Issue body for CONFIG-003</summary>

```markdown
## Problem

app.UseDeveloperExceptionPage() is activated via !app.Environment.IsProduction(), enabling full stack trace and request detail disclosure for any ASPNETCORE_ENVIRONMENT value other than Production — including Staging, Testing, UAT, and Development. An externally accessible development environment (coast-restitution-dev.silver.devops.bcgov) is confirmed by the ZAP scan workflow. The Dockerfile does not set ASPNETCORE_ENVIRONMENT at build time.

## Evidence / location

- `restitution-app/Program.cs:131-138`

```
restitution-app/Program.cs lines 131-133: if (!app.Environment.IsProduction()) { app.UseDeveloperExceptionPage(); }.
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `CONFIG-003`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### CRYPTO-001

- **Suggested title:** [RA CRYPTO-001] Data Protection Key Ring Persisted Without At-Rest Encryption
- **Severity:** Medium
- **Domain / OWASP / CWE:** CRYPTOGRAPHY / A02:2021 / CWE-312
- **Component:** aspnet-api
- **Location:** `restitution-app/Program.cs:83-86`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:medium, domain:cryptography`
- **Cross-refs:** —

<details>
<summary>Issue body for CRYPTO-001</summary>

```markdown
## Problem

AddDataProtection().PersistKeysToFileSystem(...) is called without a chained .ProtectKeysWith*() call. On Linux-based OpenShift containers, Windows DPAPI is unavailable, so ASP.NET Core writes the Data Protection key ring as plaintext XML files to the KEY_RING_DIRECTORY volume. These files contain raw AES-256 and HMAC-SHA256 key material protecting CSRF antiforgery tokens and any other Data Protection payloads.

## Evidence / location

- `restitution-app/Program.cs:83-86`

```
services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(builder.Configuration["KEY_RING_DIRECTORY"]!)); — no .ProtectKeysWith*() call follows
```

## Expected

Chain a key-protection provider: .ProtectKeysWithAzureKeyVault() or .ProtectKeysWith509Certificate(cert) using a deployment X.509 certificate. On Linux/OpenShift, Azure Key Vault or a custom X.509 certificate are the recommended approaches.

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `CRYPTO-001`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### DEP-002

- **Suggested title:** [RA DEP-002] EOL Microsoft.NETCore.App 2.2.8 / Microsoft.NETCore.Jit 2.0.8 in a net10.0...
- **Severity:** Medium
- **Domain / OWASP / CWE:** DEPENDENCIES / A06:2021 / CWE-1104
- **Component:** aspnet-api
- **Location:** `restitution-app/restitution-app.csproj:15-16`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:medium, domain:dependencies`
- **Cross-refs:** —

<details>
<summary>Issue body for DEP-002</summary>

```markdown
## Problem

Microsoft.NETCore.App v2.2.8 and Microsoft.NETCore.Jit v2.0.8 are declared as PackageReferences in a project targeting net10.0. .NET Core 2.2 reached end-of-life on December 23, 2019, and .NET Core 2.0 reached EOL on October 1, 2018, per Microsoft's official .NET release lifecycle policy. These references are vestigial from an incomplete migration from .NET Core 2.x to .NET 10.

## Impact

Because the project targets net10.0, the .NET SDK overrides these EOL meta-package references with the framework-implicit versions. The immediate runtime risk is low. However, these entries create dependency confusion, may produce build warnings, and represent incomplete migration hygiene that could mask real version-pinning issues.

## Evidence / location

- `restitution-app/restitution-app.csproj:15-16`

```
<PackageReference Include="Microsoft.NETCore.App" Version="2.2.8" />
<PackageReference Include="Microsoft.NETCore.Jit" Version="2.0.8" />
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `DEP-002`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### DEP-003

- **Suggested title:** [RA DEP-003] No NuGet lock files; unpinned transitive dependencies (supply-chain risk)
- **Severity:** Medium
- **Domain / OWASP / CWE:** DEPENDENCIES / A08:2021 / CWE-1357
- **Component:** aspnet-api
- **Location:** `restitution-app/restitution-app.csproj:1`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:medium, domain:dependencies`
- **Cross-refs:** —

<details>
<summary>Issue body for DEP-003</summary>

```markdown
## Problem

Neither restitution-app/restitution-app.csproj nor Database/Database.csproj enables NuGet lock files (packages.lock.json). The RestorePackagesWithLockFile MSBuild property is not set and no packages.lock.json files are present in the repository. Without lock files, transitive NuGet dependencies are resolved at build time and can vary between developer workstations and CI/CD environments. Direct dependencies are version-pinned with exact versions in the .csproj files, limiting but not eliminating the supply chain risk from unpinned transitives.

## Impact

Transitive NuGet packages (including security-critical libraries such as MSAL transitives from Microsoft.PowerPlatform.Dataverse.Client and Azure.Identity) can be resolved to different versions between builds. This enables subtle supply chain attacks via malicious transitive package updates and complicates forensic analysis of deployed builds.

## Evidence / location

- `restitution-app/restitution-app.csproj:1`

```
No packages.lock.json found in repository. No <RestorePackagesWithLockFile> property in restitution-app/restitution-app.csproj or Database/Database.csproj.
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `DEP-003`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### DEP-006

- **Suggested title:** [RA DEP-006] OpenShift Dockerfile uses .NET 8 base image while app targets net10.0; no ...
- **Severity:** Medium
- **Domain / OWASP / CWE:** DEPENDENCIES / A08:2021 / CWE-1357
- **Component:** cicd-pipeline
- **Location:** `openshift/Dockerfile.ubi8.net8_customized:1-5`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:medium, domain:dependencies`
- **Cross-refs:** —

<details>
<summary>Issue body for DEP-006</summary>

```markdown
## Problem

openshift/Dockerfile.ubi8.net8_customized uses registry.access.redhat.com/ubi8/dotnet-80-runtime as its base image (.NET 8), while the application's project files target net10.0. If this OpenShift Dockerfile is used to build and deploy the application, the .NET 10 compiled binaries would be run on a .NET 8 ASP.NET Core runtime, causing a startup failure or incorrect runtime behavior. Additionally, neither Dockerfile pins its base image via SHA256 digest, meaning the resolved base image can change between CI builds without a manifest change.

## Impact

If the OpenShift Dockerfile is used for production deployments, the application will fail to start or behave incorrectly due to the .NET runtime version mismatch. Floating image tags without digest pinning allow unintended base image changes between builds, introducing supply chain risk.

## Evidence / location

- `openshift/Dockerfile.ubi8.net8_customized:1-5`

```
FROM registry.access.redhat.com/ubi8/dotnet-80-runtime
# Application targets net10.0 per restitution-app/restitution-app.csproj
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `DEP-006`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### F-TEST-001

- **Suggested title:** [RA F-TEST-001] No automated test execution stage in CI pipeline
- **Severity:** Medium
- **Domain / OWASP / CWE:** TESTING / — / —
- **Component:** cicd-pipeline
- **Location:** `.github/workflows/ci-restitution.yml:53-97`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:medium, domain:testing`
- **Cross-refs:** —

<details>
<summary>Issue body for F-TEST-001</summary>

```markdown
## Problem

- name: Build Angular Application
        working-directory: restitution-app/ClientApp
        run: npm run buildprod

      # ---- Build .NET API ----
      - name: Restore .NET Dependencies
        run: dotnet restore restitution-app/restitution-app.csproj

      - name: Build .NET Application
        run: dotnet publish restitution-app/restitution-app.csproj -c Release -o /tmp/publish --no-restore --verbosity minimal

## Evidence / location

- `.github/workflows/ci-restitution.yml:53-97`

```
The `build_and_scan` job in ci-restitution.yml installs Node.js/Angular dependencies and builds the application (lines 62-97) but never invokes `ng test`, `dotnet test`, or `playwright test`. The pipeline proceeds directly from build to CodeQL analysis with no test execution step. No test results are generated or uploaded.
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `F-TEST-001`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### F-TEST-002

- **Suggested title:** [RA F-TEST-002] All security-scanning quality gates configured as non-blocking
- **Severity:** Medium
- **Domain / OWASP / CWE:** TESTING / — / —
- **Component:** cicd-pipeline
- **Location:** `.github/workflows/ci-restitution.yml:98-121`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:medium, domain:testing`
- **Cross-refs:** —

<details>
<summary>Issue body for F-TEST-002</summary>

```markdown
## Problem

- name: Perform CodeQL Analysis (C#)
        uses: github/codeql-action/analyze@v3
        with:
          category: "/language:csharp"
        continue-on-error: true

## Evidence / location

- `.github/workflows/ci-restitution.yml:98-121`

```
CodeQL C# and JS/TS analysis steps both carry `continue-on-error: true`. SonarCloud scan also carries `continue-on-error: true`. All Trivy filesystem scans use `exit-code: '0'`, meaning CRITICAL/HIGH findings do not fail the pipeline. A build containing critical vulnerabilities or exposed secrets will still proceed to deployment.
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `F-TEST-002`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### F-TEST-005

- **Suggested title:** [RA F-TEST-005] Angular unit tests contain only boilerplate stubs with no meaningful as...
- **Severity:** Medium
- **Domain / OWASP / CWE:** TESTING / — / —
- **Component:** angular-spa
- **Location:** `restitution-app/ClientApp/src/app/not-found/not-found.component.spec.ts:16-20`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:medium, domain:testing`
- **Cross-refs:** —

<details>
<summary>Issue body for F-TEST-005</summary>

```markdown
## Problem

it('should do something', async () => {
    expect(true).toEqual(true);
  });

## Evidence / location

- `restitution-app/ClientApp/src/app/not-found/not-found.component.spec.ts:16-20`

```
8 of 9 unit spec files contain a single `should create` assertion only. `not-found.component.spec.ts` contains `expect(true).toEqual(true)` — a tautology that always passes. `app.component.spec.ts` asserts the title is `'Put your title here'` (stale placeholder). No unit test exercises component logic, reactive form validation, state management, or error handling.
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `F-TEST-005`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### F-TEST-006

- **Suggested title:** [RA F-TEST-006] Playwright E2E tests exist but are not integrated into the CI pipeline
- **Severity:** Medium
- **Domain / OWASP / CWE:** TESTING / — / —
- **Component:** e2e-tests
- **Location:** `restitution-app/ClientApp/playwright.config.ts:1-31`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:medium, domain:testing`
- **Cross-refs:** —

<details>
<summary>Issue body for F-TEST-006</summary>

```markdown
## Problem

export default defineConfig({
  testDir: './e2e/tests',
  timeout: 60_000,
  forbidOnly: !!process.env['CI'],
  retries: process.env['CI'] ? 2 : 0,

## Evidence / location

- `restitution-app/ClientApp/playwright.config.ts:1-31`

```
A well-structured Playwright E2E suite exists at `restitution-app/ClientApp/e2e/` (4 spec files, ~50 test cases covering victim, victim-entity, offender, and health/routing). `playwright.config.ts` defines `localhost`, `dev`, and `test` project targets. However, no CI step in `.github/workflows/ci-restitution.yml` invokes `npx playwright test` or `npm run e2e`. E2E tests must be run manually.
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `F-TEST-006`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### LOG-001

- **Suggested title:** [RA LOG-001] Developer Exception Page exposes full stack traces in non-production envir...
- **Severity:** Medium
- **Domain / OWASP / CWE:** SECURITY_LOGGING / A05:2021 / CWE-209
- **Component:** aspnet-api
- **Location:** `restitution-app/Program.cs:131-138`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:medium, domain:security-logging`
- **Cross-refs:** —

<details>
<summary>Issue body for LOG-001</summary>

```markdown
## Problem

app.UseDeveloperExceptionPage() is enabled for all non-production environments (development, test, staging) via the condition `if (!app.Environment.IsProduction())`. Any unhandled exception in these environments triggers the ASP.NET Core developer exception page, which returns a full HTML response containing the stack trace, exception type and message, query strings, HTTP headers, and request body to the client.

## Evidence / location

- `restitution-app/Program.cs:131-138`

```
restitution-app/Program.cs lines 131-138: `if (!app.Environment.IsProduction()) { app.UseDeveloperExceptionPage(); } else { app.UseExceptionHandler("/Home/Error"); }`
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `LOG-001`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### LOG-002

- **Suggested title:** [RA LOG-002] No application-level audit logging for successful restitution form submiss...
- **Severity:** Medium
- **Domain / OWASP / CWE:** SECURITY_LOGGING / A09:2021 / CWE-778
- **Component:** aspnet-api
- **Location:** `restitution-app/Controllers/RestitutionsController.cs:79-80`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:medium, domain:security-logging`
- **Cross-refs:** —

<details>
<summary>Issue body for LOG-002</summary>

```markdown
## Problem

The restitution form submission success path in SubmitRestitutionInternal executes `return Ok(response)` without writing any application-level log entry. No record of submission type (victim/offender/victim-entity), timing, or Dataverse case ID is written to the structured log. The UseSerilogRequestLogging middleware captures HTTP-level data only (method, path, status code), insufficient for an operational audit trail in a government justice application handling victim and offender data.

## Evidence / location

- `restitution-app/Controllers/RestitutionsController.cs:79-80`

```
restitution-app/Controllers/RestitutionsController.cs line 80: `return Ok(response);` — no log statement precedes this return on the success path.
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `LOG-002`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### SEC-SECRETS-001

- **Suggested title:** [RA SEC-SECRETS-001] Internal BC Government infrastructure URLs hardcoded in developer ...
- **Severity:** Medium
- **Domain / OWASP / CWE:** SECRETS / A07:2021 / CWE-798
- **Component:** aspnet-api
- **Location:** `restitution-app/README.md:35-49`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:medium, domain:secrets`
- **Cross-refs:** —

<details>
<summary>Issue body for SEC-SECRETS-001</summary>

```markdown
## Problem

Five internal BC Government service endpoint URLs are embedded in the developer README user-secrets configuration template. Exposed identifiers include: an on-premise Dataverse proxy (dev-coast-dataverse-proxy.silver.devops.bcgov), a BC Government ADFS test token endpoint (ststest.gov.bc.ca), a JAG Dataverse resource URL (cscp-vs.dev.jag.gov.bc.ca), and Dynamics 365 cloud environment hostnames (csvs-coast-dev.crm3.dynamics.com). Credential values in the template are correctly marked as angle-bracket placeholders; no actual credentials are exposed. However, the internal network hostnames and service topology are committed to a public bcgov GitHub repository, enabling reconnaissance of active service endpoints, authentication infrastructure, and internal network naming conventions without credentials.

## Evidence / location

- `restitution-app/README.md:35-49`

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `SEC-SECRETS-001`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### SEC-SECRETS-002

- **Suggested title:** [RA SEC-SECRETS-002] Development environment hostname hardcoded in OWASP ZAP security s...
- **Severity:** Medium
- **Domain / OWASP / CWE:** SECRETS / A07:2021 / CWE-798
- **Component:** cicd-pipeline
- **Location:** `.github/workflows/zap-coast-restitution-dev-scan.yml:15`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:medium, domain:secrets`
- **Cross-refs:** —

<details>
<summary>Issue body for SEC-SECRETS-002</summary>

```markdown
## Problem

The OWASP ZAP dynamic scan workflow hardcodes the development environment target URL (https://coast-restitution-dev.silver.devops.bcgov) as a literal environment variable rather than referencing a GitHub variable (vars.*). The hostname confirms the existence of a live development deployment, reveals the internal OpenShift Silver Cluster naming convention, and exposes the full service path. This assists adversaries in mapping BC Government internal service topology without credential access.

## Evidence / location

- `.github/workflows/zap-coast-restitution-dev-scan.yml:15`

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `SEC-SECRETS-002`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### VULN-001

- **Suggested title:** [RA VULN-001] DOM-based XSS via [innerHTML] binding with unencoded user-controlled addr...
- **Severity:** Medium
- **Domain / OWASP / CWE:** CODE_VULNERABILITY / A03:2021 / CWE-79
- **Component:** angular-spa
- **Location:** `restitution-app/ClientApp/src/app/shared/restitution/review/restitution-review.component.html:228`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:medium, domain:code-vulnerability`
- **Cross-refs:** —

<details>
<summary>Issue body for VULN-001</summary>

```markdown
## Problem

The displayMailingSubAddress() function in form-base.ts (lines 414-435) concatenates raw user-controlled reactive form values (line1, line2, city, province, country, postalCode) with literal '<br />' HTML tags without HTML-encoding the values. The resulting string is bound to the DOM via Angular's [innerHTML] at restitution-review.component.html line 228. Angular DomSanitizer runs on [innerHTML] bindings and strips script tags and event-handler attributes but does not entity-encode text content. HTML structural markup entered in address fields renders as live HTML in the review page.

## Evidence / location

- `restitution-app/ClientApp/src/app/shared/restitution/review/restitution-review.component.html:228`

```
Source — form-base.ts:428-433: `let address = line1 + '<br />';` then concatenation of line2, city, province, country, postalCode without encoding. Sink — restitution-review.component.html:228: `<span [innerHTML]="displayMailingSubAddress(...)"></span>`. Angular DomSanitizer partially mitigates by removing script tags and event handlers. Component only renders the submitting user's own form data.
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `VULN-001`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### VULN-002

- **Suggested title:** [RA VULN-002] Client-side-only file type validation — no server-side MIME or extension ...
- **Severity:** Medium
- **Domain / OWASP / CWE:** CODE_VULNERABILITY / A04:2021 / CWE-434
- **Component:** aspnet-api
- **Location:** `restitution-app/ClientApp/src/app/shared/file-uploader/file-uploader.component.ts:52-56`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:medium, domain:code-vulnerability`
- **Cross-refs:** VULN-004

<details>
<summary>Issue body for VULN-002</summary>

```markdown
## Problem

File extension validation is performed exclusively in the Angular FileUploaderComponent by checking the last dot-delimited segment of the filename against a client-side allowlist. The server-side DocumentDto model has no validation attributes. The submission controller forwards any base64-encoded DocumentCollection entry to Dataverse without file type inspection. An API consumer calling the endpoints directly can submit documents with any extension and any content.

## Evidence / location

- `restitution-app/ClientApp/src/app/shared/file-uploader/file-uploader.component.ts:52-56`

```
file-uploader.component.ts:52 — `let file_extenstion = files.item(i).name.trim().split('.').pop();` then allowlist check at line 53. DocumentDto.cs (lines 1-12) has three unvalidated string fields (Filename, Body, Subject). No server-side file type inspection in RestitutionsController.cs.
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `VULN-002`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### AUTH-002

- **Suggested title:** [RA AUTH-002] Global Cookie Policy Sets MinimumSameSitePolicy to SameSiteMode.None
- **Severity:** Low
- **Domain / OWASP / CWE:** AUTHENTICATION / A01:2021 / CWE-1275
- **Component:** aspnet-api
- **Location:** `restitution-app/Program.cs:277-284`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:low, domain:authentication`
- **Cross-refs:** —

<details>
<summary>Issue body for AUTH-002</summary>

```markdown
## Problem

The application-wide CookiePolicyOptions sets MinimumSameSitePolicy = SameSiteMode.None, which removes SameSite enforcement from all framework-managed cookies including ASP.NET Core Data Protection cookies. This eliminates the SameSite CSRF defence layer for any current or future authentication cookies set by the framework.

## Evidence / location

- `restitution-app/Program.cs:277-284`

```
app.UseCookiePolicy(new CookiePolicyOptions { HttpOnly = HttpOnlyPolicy.Always, Secure = CookieSecurePolicy.Always, MinimumSameSitePolicy = SameSiteMode.None }) at lines 277-284. Secure and HttpOnly are correctly enforced; SameSite=None removes the CSRF protection layer from all framework cookies.
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `AUTH-002`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### AUTH-003

- **Suggested title:** [RA AUTH-003] Health Check Endpoint /hc Accessible Without Authentication
- **Severity:** Low
- **Domain / OWASP / CWE:** AUTHENTICATION / A07:2021 / CWE-287
- **Component:** health-checks
- **Location:** `restitution-app/Program.cs:171-209`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:low, domain:authentication`
- **Cross-refs:** —

<details>
<summary>Issue body for AUTH-003</summary>

```markdown
## Problem

The /hc health check endpoint is registered via app.MapHealthChecks without chaining .RequireAuthorization() and without any shown network-level restriction. The JSON response discloses named internal check components (API, Dataverse) and their connectivity status and description strings to unauthenticated callers.

## Evidence / location

- `restitution-app/Program.cs:171-209`

```
app.MapHealthChecks("/hc", new HealthCheckOptions { ... }) at lines 171-209 with no .RequireAuthorization() chained. ResponseWriter serialises check names (e.Key), status strings, and description strings, revealing Dataverse connectivity to unauthenticated callers.
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `AUTH-003`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### AUTHZ-001

- **Suggested title:** [RA AUTHZ-001] Health Check Endpoint Publicly Accessible Without Authorization — Disclo...
- **Severity:** Low
- **Domain / OWASP / CWE:** AUTHORIZATION / A01:2021 / CWE-862
- **Component:** health-checks
- **Location:** `restitution-app/Program.cs:171-209`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:low, domain:authorization`
- **Cross-refs:** —

<details>
<summary>Issue body for AUTHZ-001</summary>

```markdown
## Problem

The /hc endpoint is mapped with app.MapHealthChecks without any RequireAuthorization() clause, API key requirement, or host restriction. It returns a JSON document containing registered check names ("API", "Dataverse") and their live status and description strings. This discloses that the backend uses Microsoft Dataverse and reveals real-time Dataverse connectivity status to any unauthenticated caller on the public internet.

## Evidence / location

- `restitution-app/Program.cs:171-209`

```
restitution-app/Program.cs lines 191–200: the ResponseWriter serialises e.Key (check name) and e.Value.Description (e.g., "Dataverse connection is healthy." / "Dataverse health check failed.") directly into the HTTP response. The MapHealthChecks call at line 171 carries no authorization requirement.
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `AUTHZ-001`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### AUTHZ-002

- **Suggested title:** [RA AUTHZ-002] UseAuthorization() Middleware Absent from Pipeline — [Authorize] Attribu...
- **Severity:** Low
- **Domain / OWASP / CWE:** AUTHORIZATION / A01:2021 / CWE-862
- **Component:** aspnet-api
- **Location:** `restitution-app/Program.cs:286-290`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:low, domain:authorization`
- **Cross-refs:** —

<details>
<summary>Issue body for AUTHZ-002</summary>

```markdown
## Problem

The ASP.NET Core middleware pipeline calls UseRouting() and MapControllers() but does not call UseAuthorization(). Without it, any future [Authorize] attribute added to a controller or action is silently ignored rather than enforced, creating an invisible authorization bypass path when the application is extended.

## Evidence / location

- `restitution-app/Program.cs:286-290`

```
restitution-app/Program.cs lines 286–290: `app.UseHttpsRedirection(); app.UseRouting(); app.MapControllers(); app.Run();` — UseAuthorization() is absent. Neither AddAuthentication() nor AddAuthorization() appears in service registration (lines 44–114).
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `AUTHZ-002`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### CONFIG-004

- **Suggested title:** [RA CONFIG-004] Missing Permissions-Policy header
- **Severity:** Low
- **Domain / OWASP / CWE:** CONFIGURATION / A05:2021 / CWE-16
- **Component:** aspnet-api
- **Location:** `restitution-app/ClientApp/Caddyfile:9-22`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:low, domain:configuration`
- **Cross-refs:** —

<details>
<summary>Issue body for CONFIG-004</summary>

```markdown
## Problem

No Permissions-Policy header is configured in either the ASP.NET Core middleware pipeline or the Caddy static file server. All other standard security headers (HSTS, X-Frame-Options, X-Content-Type-Options, Referrer-Policy, X-XSS-Protection, CSP) are present. Permissions-Policy is the sole absent standard security header.

## Evidence / location

- `restitution-app/ClientApp/Caddyfile:9-22`

```
restitution-app/ClientApp/Caddyfile lines 9-22: header block contains Strict-Transport-Security, X-XSS-Protection, X-Content-Type-Options, X-Frame-Options, Content-Security-Policy, Referrer-Policy — no Permissions-Policy.
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `CONFIG-004`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### CONFIG-005

- **Suggested title:** [RA CONFIG-005] AllowedHosts set to wildcard — host header filtering disabled
- **Severity:** Low
- **Domain / OWASP / CWE:** CONFIGURATION / A05:2021 / CWE-16
- **Component:** aspnet-api
- **Location:** `restitution-app/appsettings.json:7`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:low, domain:configuration`
- **Cross-refs:** —

<details>
<summary>Issue body for CONFIG-005</summary>

```markdown
## Problem

appsettings.json sets AllowedHosts to "*", disabling ASP.NET Core's built-in host filtering middleware. The application accepts requests with any Host header value. The reviewed code does not currently use the Host header for sensitive URL construction, but the protective control is absent.

## Evidence / location

- `restitution-app/appsettings.json:7`

```
restitution-app/appsettings.json line 7: "AllowedHosts": "*".
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `CONFIG-005`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### CRYPTO-002

- **Suggested title:** [RA CRYPTO-002] TLS Certificate Validation Disabled for Splunk HTTP Sink in Development...
- **Severity:** Low
- **Domain / OWASP / CWE:** CRYPTOGRAPHY / A02:2021 / CWE-295
- **Component:** aspnet-api
- **Location:** `restitution-app/Program.cs:329-338`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:low, domain:cryptography`
- **Cross-refs:** —

<details>
<summary>Issue body for CRYPTO-002</summary>

```markdown
## Problem

When ASPNETCORE_ENVIRONMENT is Development, the Serilog Splunk sink is configured with HttpClientHandler.DangerousAcceptAnyServerCertificateValidator, disabling all TLS certificate verification for the Splunk HEC connection. Properly gated by env.IsDevelopment(), but if ASPNETCORE_ENVIRONMENT is accidentally set to Development in a non-isolated deployment, MITM attacks on the log transport channel become possible.

## Evidence / location

- `restitution-app/Program.cs:329-338`

```
handler = new HttpClientHandler { ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator }; inside if (env.IsDevelopment()) block
```

## Expected

Remove the DangerousAcceptAnyServerCertificateValidator assignment. Add the Splunk server CA certificate to the development trust store instead. Confirm ASPNETCORE_ENVIRONMENT is never Development in test, staging, or production deployments.

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `CRYPTO-002`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### DEP-004

- **Suggested title:** [RA DEP-004] ts-node pinned to 7.0.1 (2018), three major versions behind (dev-only tool...
- **Severity:** Low
- **Domain / OWASP / CWE:** DEPENDENCIES / A06:2021 / CWE-1104
- **Component:** e2e-tests
- **Location:** `restitution-app/ClientApp/package-lock.json:14759-14775`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:low, domain:dependencies`
- **Cross-refs:** —

<details>
<summary>Issue body for DEP-004</summary>

```markdown
## Problem

ts-node is pinned to ~7.0.0 in package.json and resolves to version 7.0.1 in the npm lock file. ts-node 7.x dates to 2018. The current stable release is v10.9.x (released 2022), making this 3 major versions behind. ts-node is used at build/test time to execute TypeScript configuration files (playwright.config.ts, orval.config.ts). This is a development-only dependency with no production runtime exposure.

## Impact

Severely outdated build tooling may carry unpatched vulnerabilities affecting the CI/CD build environment. ts-node 7.x carries outdated transitive dependencies including older versions of mkdirp and minimist. Build environment compromise through outdated dev tooling is a lower-probability but real supply chain risk.

## Evidence / location

- `restitution-app/ClientApp/package-lock.json:14759-14775`

```
"node_modules/ts-node": { "version": "7.0.1", ... }
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `DEP-004`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### DEP-005

- **Suggested title:** [RA DEP-005] moment.js 2.30.1 in maintenance mode; no known CVEs at assessment time
- **Severity:** Low
- **Domain / OWASP / CWE:** DEPENDENCIES / A06:2021 / CWE-1104
- **Component:** angular-spa
- **Location:** `restitution-app/ClientApp/package.json:41-42`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:low, domain:dependencies`
- **Cross-refs:** —

<details>
<summary>Issue body for DEP-005</summary>

```markdown
## Problem

moment.js 2.30.1 (declared as ^2.22.2, resolved to 2.30.1) is included as a runtime dependency. The Moment.js team officially designated the project as a 'legacy project' in maintenance mode in September 2020. The team recommends migration to modern date libraries (date-fns, Luxon, Day.js) and no longer adds new features. moment-timezone 0.5.48 and @angular/material-moment-adapter 21.2.0 are also present, extending the moment dependency surface.

## Impact

The Moment.js bundle is large (~67 KB gzipped) and cannot be tree-shaken, increasing the JavaScript payload served to end users. While security patches are still released for critical issues, the library receives no new development. No known CVEs exist in moment 2.30.1 as of this assessment run.

## Evidence / location

- `restitution-app/ClientApp/package.json:41-42`

```
"moment": "^2.22.2",
"moment-timezone": "^0.5.46",
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `DEP-005`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### LOG-003

- **Suggested title:** [RA LOG-003] Full Dynamics OrganizationResponse object logged via Serilog destructuring...
- **Severity:** Low
- **Domain / OWASP / CWE:** SECURITY_LOGGING / A09:2021 / CWE-532
- **Component:** aspnet-api
- **Location:** `restitution-app/Controllers/RestitutionsController.cs:72-75`
- **Confidence:** medium
- **Suggested labels:** `rapid-assessment, severity:low, domain:security-logging`
- **Cross-refs:** —

<details>
<summary>Issue body for LOG-003</summary>

```markdown
## Problem

When response.Results["IsSuccess"] is not true, the full OrganizationResponse object is logged using Serilog's {@Response} destructuring operator. Serilog destructuring recursively serializes the object graph, capturing internal Dataverse fields, entity identifiers, error codes, and operation metadata beyond what is necessary for diagnostics.

## Evidence / location

- `restitution-app/Controllers/RestitutionsController.cs:72-75`

```
restitution-app/Controllers/RestitutionsController.cs lines 72-75: `_logger.Error("Error while saving victim restitution. Response from Dynamics was:\n{@Response}", response);`
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `LOG-003`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### LOG-004

- **Suggested title:** [RA LOG-004] Incomplete console output suppression in production — console.error and co...
- **Severity:** Low
- **Domain / OWASP / CWE:** SECURITY_LOGGING / A05:2021 / CWE-489
- **Component:** angular-spa
- **Location:** `restitution-app/ClientApp/src/main.ts:10-15`
- **Confidence:** medium
- **Suggested labels:** `rapid-assessment, severity:low, domain:security-logging`
- **Cross-refs:** —

<details>
<summary>Issue body for LOG-004</summary>

```markdown
## Problem

Production mode suppresses only console.log via `window.console.log = function () {}` in main.ts. console.error and console.debug are not overridden. Two active call sites expose internal error objects in production browser developer tools: (1) app.component.ts line 53 logs the full configuration store error via console.error; (2) file-uploader.component.ts line 80 logs file read errors via console.debug.

## Evidence / location

- `restitution-app/ClientApp/src/main.ts:10-15`

```
restitution-app/ClientApp/src/main.ts lines 12-14: `window.console.log = function () {};` (console.error and console.debug not suppressed). restitution-app/ClientApp/src/app/app.component.ts line 53: `console.error('Failed to fetch configuration:', this.configurationStore.error());`
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `LOG-004`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### SEC-SECRETS-003

- **Suggested title:** [RA SEC-SECRETS-003] Trivy secret scanner configured with non-enforcing exit code acros...
- **Severity:** Low
- **Domain / OWASP / CWE:** SECRETS / A07:2021 / CWE-693
- **Component:** cicd-pipeline
- **Location:** `.github/workflows/ci-restitution.yml:140-180`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:low, domain:secrets`
- **Cross-refs:** —

<details>
<summary>Issue body for SEC-SECRETS-003</summary>

```markdown
## Problem

Trivy secret scanning is integrated into both the CI pipeline (ci-restitution.yml) and the reusable build template (build-template.yml), but all scan steps use exit-code: 0. An inline comment at line 169 of ci-restitution.yml explicitly acknowledges that exit code 1 would enforce the gate but was intentionally not applied. Builds and deployments are never blocked even when committed secrets are detected by the scanner. The tooling produces SARIF reports uploaded to GitHub Security but relies entirely on manual review.

## Evidence / location

- `.github/workflows/ci-restitution.yml:140-180`

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `SEC-SECRETS-003`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### VULN-003

- **Suggested title:** [RA VULN-003] Developer exception page enabled for all non-production environments — st...
- **Severity:** Low
- **Domain / OWASP / CWE:** CODE_VULNERABILITY / A05:2021 / CWE-200
- **Component:** aspnet-api
- **Location:** `restitution-app/Program.cs:131-138`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:low, domain:code-vulnerability`
- **Cross-refs:** —

<details>
<summary>Issue body for VULN-003</summary>

```markdown
## Problem

app.UseDeveloperExceptionPage() is enabled for every environment whose name is not exactly 'Production'. When an unhandled exception occurs the developer exception page returns a full HTML response containing the exception stack trace with source file references, exception type, request path, query string values, cookies, HTTP headers, and .NET framework version. Staging and test base URLs are internet-accessible per playwright.config.ts.

## Evidence / location

- `restitution-app/Program.cs:131-138`

```
Program.cs:131-138 — `if (!app.Environment.IsProduction()) { app.UseDeveloperExceptionPage(); } else { app.UseExceptionHandler("/Home/Error"); }`. Staging environments dev.justice.gov.bc.ca and test.justice.gov.bc.ca referenced in playwright.config.ts lines 26-36.
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `VULN-003`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### VULN-004

- **Suggested title:** [RA VULN-004] No input length or format constraints on submission API DTO string fields
- **Severity:** Low
- **Domain / OWASP / CWE:** CODE_VULNERABILITY / A03:2021 / CWE-20
- **Component:** aspnet-api
- **Location:** `restitution-app/Models/RestitutionApplicationDtoBase.cs:1-70`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:low, domain:code-vulnerability`
- **Cross-refs:** VULN-002

<details>
<summary>Issue body for VULN-004</summary>

```markdown
## Problem

The submission model hierarchy carries only minimal [Required] annotations on a small subset of fields. No [MaxLength], [StringLength], [EmailAddress], [Phone], or [RegularExpression] constraints are present on any string field. Arbitrarily long strings, malformed email addresses, non-numeric phone values, and special characters can be submitted to the restitution endpoints and are forwarded verbatim to Dataverse. Dataverse enforces field-length limits at the CRM layer; violations generate exceptions returned as generic 500 responses.

## Evidence / location

- `restitution-app/Models/RestitutionApplicationDtoBase.cs:1-70`

```
RestitutionApplicationDtoBase.cs lines 22-24 — PrimaryPhoneNumber, AlternatePhoneNumber, Email fields have no format annotations. ParticipantDto.cs lines 6-56 — 28 string fields, zero validation annotations. Search for MaxLength|StringLength|RegularExpression|EmailAddress|Phone across Models/*.cs returned zero results.
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `VULN-004`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### AUTHZ-003

- **Suggested title:** [RA AUTHZ-003] [AllowAnonymous] Attribute Applied Without Authorization Framework — Cre...
- **Severity:** Informational
- **Domain / OWASP / CWE:** AUTHORIZATION / A01:2021 / CWE-862
- **Component:** aspnet-api
- **Location:** `restitution-app/Controllers/ConfigurationController.cs:23-25`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:informational, domain:authorization`
- **Cross-refs:** —

<details>
<summary>Issue body for AUTHZ-003</summary>

```markdown
## Problem

The [AllowAnonymous] attribute is applied to ConfigurationController.GetConfiguration(). Since no authorization service is registered and no UseAuthorization() middleware is in the pipeline, the attribute is entirely inert. Its presence implies to future maintainers that other endpoints without [AllowAnonymous] are implicitly protected — which is false.

## Evidence / location

- `restitution-app/Controllers/ConfigurationController.cs:23-25`

```
restitution-app/Controllers/ConfigurationController.cs lines 23–25: `[AllowAnonymous]` / `[HttpGet]` / `public IActionResult GetConfiguration()`. No corresponding AddAuthorization() or UseAuthorization() exists in Program.cs.
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `AUTHZ-003`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### CRYPTO-003

- **Suggested title:** [RA CRYPTO-003] OAuth Access Tokens Cached as Plaintext in In-Process Memory
- **Severity:** Informational
- **Domain / OWASP / CWE:** CRYPTOGRAPHY / A02:2021 / CWE-312
- **Component:** dataverse-client
- **Location:** `Database/Caching/MemoryCache.cs:16-27`
- **Confidence:** medium
- **Suggested labels:** `rapid-assessment, severity:informational, domain:cryptography`
- **Cross-refs:** —

<details>
<summary>Issue body for CRYPTO-003</summary>

```markdown
## Problem

ADFS and Entra ID OAuth 2.0 access tokens are stored as plaintext strings in an in-process IMemoryCache for up to 5 minutes (cacheKey constants adfs_token and entraid_token). An attacker with a memory-read primitive post-RCE could extract live bearer tokens granting Dataverse API access. The 5-minute TTL and server-side in-process nature limit risk; this is the standard token-caching pattern.

## Evidence / location

- `Database/Caching/MemoryCache.cs:16-27`

```
memoryCache.Set(key, value, expiration) — tokens stored unencrypted with TimeSpan.FromMinutes(5) TTL
```

## Expected

No immediate remediation required. If the threat model escalates to include memory-inspection attacks, consider encrypting the token string with IDataProtector before caching.

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `CRYPTO-003`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

### LOG-005

- **Suggested title:** [RA LOG-005] Token endpoint URLs including Entra ID tenant ID logged at Debug level
- **Severity:** Informational
- **Domain / OWASP / CWE:** SECURITY_LOGGING / A09:2021 / CWE-532
- **Component:** dataverse-client
- **Location:** `Database/Extensions/EntraIdTokenProvider.cs:37`
- **Confidence:** high
- **Suggested labels:** `rapid-assessment, severity:informational, domain:security-logging`
- **Cross-refs:** —

<details>
<summary>Issue body for LOG-005</summary>

```markdown
## Problem

Both token providers emit the full OAuth2 token endpoint URL at Debug level. The Entra ID endpoint embeds the tenant ID in the URL path. The ADFS endpoint may reveal internal infrastructure hostnames. These messages are below the Splunk sink's restrictedToMinimumLevel: Information threshold and do not reach the centralized log aggregator. They appear in console output in development environments only.

## Evidence / location

- `Database/Extensions/EntraIdTokenProvider.cs:37`

```
Database/Extensions/EntraIdTokenProvider.cs line 37: `logger.LogDebug("Acquiring Entra ID token from {0}", tokenEndpoint);` — Database/Extensions/ADFSTokenProvider.cs line 37: `logger.LogDebug("Acquiring ADFS token from {0}", options.OAuth2TokenEndpoint);`
```

## Expected

_See assessment finding detail; propose fix in PR._

## Source

- Rapid assessment `ra-2026-07-16T153047Z` finding `LOG-005`
- Backlog: `docs/pssg-cscp-restitution-rapid-assessment-tickets.md`
```

</details>

