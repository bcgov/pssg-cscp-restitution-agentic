# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#22](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/22) — rapid assessment **VULN-002**.

## Problem

Document file-type checks exist only in the Angular uploader. `DocumentDto` and the restitution submit path do not enforce an extension allowlist, so a direct API caller can send any filename/extension to Dataverse.

## Outcome

Server-side validation rejects documents whose filename extension is outside the client allowlist (`pdf`, `png`, `jpeg`, `jpg`, `doc`, `docx`, `ppt` from `ClientApp` `config.ts`) with **HTTP 400** before Dynamics is invoked. Unit tests cover allow/deny without live Dynamics.

## Scope

### In scope

- Validate `DocumentDto` / `DocumentCollection` filenames on the restitution submit path
- Allowlist aligned with client `accepted_file_extensions`
- Return 400 on disallowed extension
- Unit tests without Dynamics

### Out of scope

- Full MIME/content sniffing beyond extension
- Changing the Angular client allowlist itself
- VULN-004 string length constraints

## Journeys

1. Disallowed extension → 400 — `features/vuln-002-server-file-extension.feature` (@R-22.1)
2. Allowlist parity in unit tests — same feature (@R-22.2)

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | Alex Rivera (simulated) | 2026-09-04 |
| BA | Alex Rivera (simulated) | 2026-09-04 |
| QA (acceptance ownership) | | |
