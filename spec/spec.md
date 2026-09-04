# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#36](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/36) — rapid assessment **VULN-004**.

## Problem

Submission API DTO string fields lack length constraints, so overlong payloads can pass model binding before business validation.

## Outcome

Key submission DTO string fields (ParticipantDto, DocumentDto, and main application DTO strings) carry proportionate `[MaxLength]` (or equivalent) attributes. Overlong strings fail model validation. Not every field in the universe — main submit surface only.

## Scope

### In scope

- MaxLength on key strings in ParticipantDto, DocumentDto, RestitutionApplicationDtoBase / related application DTOs
- Unit tests that overlong strings fail validation
- Evidence

### Out of scope

- Validating every nested Dynamics field
- Changing HTTP error shape beyond existing ModelState behaviour
- Live Dynamics

## Journeys

1. Overlong strings fail validation — `features/vuln-004-dto-maxlength.feature` (@R-36.1)

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | | |
| BA | | |
| QA (acceptance ownership) | | |
