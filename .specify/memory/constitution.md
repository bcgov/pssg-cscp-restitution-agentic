# Constitution — CSCP Restitution (web forms)

> Constitution for the **CSCP Restitution** public intake app (`pssg-cscp-restitution` / pilot fork `pssg-cscp-restitution-agentic`).
> Amend via pull request. Platform articles stay; brownfield exceptions are called out under **Project articles** (do not silently delete platform text).

**Ministry / program:** Ministry of Public Safety and Solicitor General (PSSG) — Crime Victim Assistance / CSCP Restitution  
**Service:** Restitution web forms (victim, victim-entity, offender)  
**Product URLs:** https://justice.gov.bc.ca/restwebforms/victim (prod); `dev.justice.gov.bc.ca` / `test.justice.gov.bc.ca` for lower environments  
**Data classification:** Public-facing intake that collects **victim and offender PII** and persists it to Microsoft Dataverse. Treat submissions, attachments, and Dynamics identifiers as sensitive. Do not send live PII to unapproved public model endpoints. `COMPLIANCE.yaml` still has PIA/STRA placeholders — confirm current status with the product owner before expanding data use.  
**Deploy target:** OpenShift (Private Cloud PaaS) — see `openshift/`.  
**Last reviewed:** 2026-09-03

---

## Platform articles (do not remove)

### P1 — Accessibility

All user-facing interfaces SHALL meet **WCAG 2.1 Level AA**. Accessibility is a legal duty for public-facing services, not a tier opt-in. Prefer components and patterns that encode accessible behaviour. Do not ship colour-only status, unlabeled icon buttons, or missing form labels.

### P2 — Design system

For **new greenfield** BC Gov services, UI SHOULD use `@bcgov/design-system-react-components` and `@bcgov/design-tokens`, and agents SHOULD query the BC Design System MCP before generating UI.

**This repository (brownfield):** UI SHALL follow the existing **Angular + Bootstrap** stack already in use under `restitution-app/ClientApp/` and match patterns already present. Do **not** introduce a parallel React Design System or hard-coded one-off brand colours. A migration to the provincial React Design System requires an explicit ADR and product approval (see J6).

### P3 — Privacy

No personal information MAY enter a system, log, model prompt, or third-party AI API until a **Privacy Impact Assessment (PIA)** appropriate to the classification is complete and recorded. Prefer synthetic or anonymized fixtures in lower environments. Do not commit secrets, tokens, or live Dynamics credentials.

### P4 — Deploy target

Production and primary lower environments SHALL target **OpenShift** on the BC Gov Private Cloud PaaS unless an ADR explicitly documents an exception. Follow Private Cloud conventions (health probes, resource limits, no privileged containers by default).

### P5 — Spec as source of truth

Intent lives in versioned git under `spec/` (`spec.md`, `plan.md`, `tasks.md`, `features/*.feature`, this constitution). Chat is not the system of record. FOI-relevant decisions MUST be reconstructable from git artifacts.

### P6 — Human checkpoints

Humans own: (1) spec sign-off, (2) plan/architecture approval, (3) review & ship. Agents MUST NOT self-merge. Agent branches only; Actions on agent PRs require human approval where org policy requires it.

### P7 — Test integrity

Default: acceptance criteria owned under human review. The same agent session SHOULD NOT both author production code and solely author the only acceptance proof for high-risk behaviour without human QA sign-off. Prefer extending existing Angular unit tests and Playwright e2e (`restitution-app/ClientApp`) plus any new API tests for behaviour changes.

### P8 — Approved tools

Agents MAY only use approved MCP servers and model routes for this classification. Sensitive workloads MUST NOT send source or operational data to public model endpoints outside policy. Local coding agents: see `.github/mcp/mcp.json.example` and `.github/tier2-v3/LOCAL.md`.

---

## Project articles (customize)

### J1 — Service purpose

**CSCP Restitution web forms** let victims, victim entities, and offenders submit restitution applications. The Angular SPA collects the form; the ASP.NET Core API maps DTOs to Dynamics entities and persists via a privileged Dataverse service identity (`VSd_CreateRestitutionCase`).

### J2 — In scope / out of scope

- **In (this repo):**
  - Angular ClientApp (`restitution-app/ClientApp/`) — victim / victim-entity / offender wizards
  - ASP.NET Core API (`restitution-app/`) — configuration, lookups, submission endpoints
  - Dataverse client (`Database/`) — token providers, `ServiceClient`, generated model
  - OpenShift / GitHub Actions deploy config already in-tree
- **Out (this repo):**
  - Implementing or administering the Dynamics / Dataverse org (COAST Victim Services)
  - ADFS / Entra app registration changes
  - Standing up a local Dynamics instance (use fixtures or a Development stub when secrets are absent)
  - Drive-by hosting migrations or replacing the existing Angular/Bootstrap UI with an unrelated design system

### J3 — Forbidden patterns

- Calling Dataverse from the Angular UI — all persistence goes through the ASP.NET API
- Committing Dynamics client secrets, service-account passwords, or production `user-secrets`
- Logging full Dynamics `OrganizationResponse` bodies, stack traces, or PII in shared environments
- Disabling Tier 1 / Tier 2 workflows “to make CI green”
- Adding a second parallel UI framework (e.g. React) inside this Angular app
- Weakening Trivy / CodeQL / Sonar gates without an explicit security rationale in the PR

### J4 — Domain language

| Term | Meaning |
| --- | --- |
| CSCP | Crime Victim Assistance / restitution program area |
| Restitution web forms | This public intake SPA + API |
| Victim / victim-entity / offender | The three application variants |
| COAST / Dataverse | Dynamics 365 org that stores `vsd_*` restitution entities |
| `VSd_CreateRestitutionCase` | Dataverse custom request used to persist a submission |
| Lookups | Countries, provinces, cities, courts, relationships, police detachments from Dataverse |
| BASE_PATH | Path prefix `/restwebforms` used in deployed environments |

### J5 — Non-functional baselines

- **Users / access:** Public anonymous intake (no end-user login). Authorization for stored data is delegated to Dataverse / program staff tools outside this repo.
- **Quality gate (dev):** Prefer `dotnet` build for the API and `npm test` / Playwright for the ClientApp on PRs that change application code. Do not merge while introducing new untested PII paths.
- **Security scanning:** Do not weaken Trivy / CodeQL / Sonar `continue-on-error` or `exit-code: 0` without an explicit security rationale.
- **Runtime config:** Dynamics endpoints and secrets live in user-secrets / OpenShift secrets — not in source. Feature flags such as `FEATURE_UPDATED_COMPLIANCE_FIELDS` are configuration.
- **Uploads:** Multipart limit is currently 1 GB on an anonymous surface — do not raise it; prefer tightening with product agreement.
- **Retention / records:** Server-side retention is owned by Dataverse / program policy.

### J6 — Architecture exceptions (brownfield)

| Platform article | Exception | Status |
| --- | --- | --- |
| P2 Design system | Existing Angular + Bootstrap ClientApp instead of provincial React Design System | Accepted until product schedules a migration ADR |
| P4 Deploy target | None — this service already targets OpenShift | — |

### J7 — Agent / local development notes

- Full interactive submit needs Dynamics credentials (`dotnet user-secrets` for `UserSecretsId` `76d21faa-788c-4a11-806e-5926d3169453`). Prefer stories that can be verified with **build / unit / Playwright** when Dataverse is unavailable.
- API default: `http://localhost:5000` (Swagger in Development). SPA: `http://localhost:4200/restwebforms` (proxies `/api` to the API).
- Do not invent a production Dynamics connection from this fork. Local mock/stub of lookups and submit is acceptable for pipeline slices that only need the UI/API shell.
- Default branch for this fork is **`development`** (not `main`).

---

## Amendment

Changes to platform articles require platform / architecture guild agreement (or an explicit ADR recorded in-repo).  
Changes to project articles require the project's usual PR review (checkpoint 2 reviewers for material architecture impact).
