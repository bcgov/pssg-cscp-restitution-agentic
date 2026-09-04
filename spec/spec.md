# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#32](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/32) — rapid assessment **LOG-003**.

## Problem

On Dynamics submit failure, the API logs the full OrganizationResponse with Serilog `{@Response}` destructuring. That can serialize internal Dataverse fields, entity identifiers, and operation metadata beyond what operators need for diagnostics.

## Outcome

When a restitution submit fails because Dynamics reports unsuccessful, the error log records IsSuccess and any error code / result keys needed for diagnosis — not the full destructured OrganizationResponse body. Successful submits and HTTP behaviour are unchanged.

## Scope

### In scope

- Change failure logging in RestitutionsController (and any thin helper) off `{@Response}`
- Log IsSuccess and error-code-like result metadata only
- Unit test(s) asserting no full response body destructuring in the failure log path
- Evidence

### Out of scope

- Changing Dynamics request/response contracts
- Broader Serilog configuration overhaul
- Live Dynamics

## Journeys

1. Failure log without full response body — `features/log-003-response-logging.feature` (@R-32.1)

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | | |
| BA | | |
| QA (acceptance ownership) | | |
