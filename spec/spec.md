# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#18](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/18) — rapid assessment **LOG-002**.

## Problem

Successful restitution form submissions return `Ok(response)` from `SubmitRestitutionInternal` **without** an application-level audit log. Serilog request logging only captures HTTP method/path/status — not form type or a clear success audit for a justice application handling victim/offender data.

## Outcome

On successful submit, the API writes a **structured information-level audit log** with **non-PII** identifiers (for example correlation id, form type, success). Operators can see that a victim / victim-entity / offender submission succeeded without reading Dynamics response bodies or form PII (those remain LOG-003 / privacy concerns).

## Scope

### In scope

- Info-level audit on the **success** path of restitution submit (victim, victim-entity, offender)
- Non-PII fields only (form type, correlation id, success flag — or equivalent)
- Unit test with a mock/fake logger verifying the audit / `LogInformation` call on the success path
- Evidence in `docs/pr-evidence.md`

### Out of scope

- LOG-003: logging full Dynamics `OrganizationResponse` on failure (do not expand body logging)
- Live Dynamics / Dataverse integration tests
- Splunk sinks or new log shipping
- Changing invalid-model or exception error logging shape beyond what’s needed to call the success audit

## Journeys

1. Success audit written — `features/log-002-submit-audit-log.feature` (@R-18.1)
2. No PII / OrganizationResponse on success audit — same feature (@R-18.2)

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | | |
| BA | | |
| QA (acceptance ownership) | | |
