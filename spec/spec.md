# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#27](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/27) — rapid assessment **CONFIG-004**.

## Problem

Standard security headers are present (HSTS, X-Frame-Options, X-Content-Type-Options, Referrer-Policy, XSS protection, CSP) but **Permissions-Policy** is missing from both the API response path and the static UI server. Browsers therefore lack an explicit deny/allow list for powerful features (camera, microphone, geolocation, etc.).

## Outcome

Responses include a proportionate restrictive **Permissions-Policy** header (deny unused powerful features by default). Apply via API middleware and/or the static file server config so UI and API surfaces are covered. Do not redesign CSP or other existing headers.

## Scope

### In scope

- Add Permissions-Policy (restrictive defaults for unused browser features)
- API middleware and/or Caddyfile (whichever surfaces serve browsers)
- Evidence / light verification (config presence or response header assertion where practical)

### Out of scope

- Reworking CSP / HSTS / other headers
- Feature allowlists for camera/mic (app does not need them)
- Live Dynamics tests

## Journeys

1. Permissions-Policy present — `features/config-004-permissions-policy.feature` (@R-27.1)

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | | |
| BA | | |
| QA (acceptance ownership) | | |
