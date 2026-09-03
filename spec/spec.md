# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#19](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/19) — rapid assessment **SEC-SECRETS-001**.

## Problem

The developer README user-secrets template embeds real internal BC Government hostnames (Dataverse proxy, ADFS token endpoint, JAG Dataverse resource, Dynamics cloud endpoints). Credential values are already placeholders, but the hostnames themselves are reconnaissance material in a public `bcgov` repository.

## Outcome

The README secrets template uses **generic placeholders** for every Dynamics / ADFS / Entra endpoint URL and resource name (for example `<dynamics-api-endpoint-url>`, `<adfs-token-endpoint>`, `<dynamics-resource-url>`). No real internal hostname remains in that template. Developers still understand which keys to set locally.

## Scope

### In scope

- Replace hardcoded hostnames/URLs in `restitution-app/README.md` user-secrets template with angle-bracket placeholders
- Keep credential placeholders as they are (`<onpremise_client_id>`, etc.)
- Evidence in `docs/pr-evidence.md`

### Out of scope

- Changing runtime config, OpenShift secrets, or appsettings
- Inventing or documenting new real hostnames
- SEC-SECRETS-002 (ZAP workflow hostname) — separate issue
- Rotating or inventing live credentials

## Journeys

1. Template has no real hostnames — `features/sec-secrets-001-readme-placeholders.feature` (@R-19.1)
2. Placeholder keys remain clear for local setup — same feature (@R-19.2)

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | | |
| BA | | |
| QA (acceptance ownership) | | |
