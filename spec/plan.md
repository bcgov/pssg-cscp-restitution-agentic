# Plan — CONFIG-004 (Permissions-Policy header)

## Summary

Add a restrictive `Permissions-Policy` header on browser-facing surfaces. Prefer Caddyfile for the Angular static server (where other security headers already live) and optionally API middleware for JSON/API responses. Use a proportionate deny-by-default policy for unused features (e.g. camera, microphone, geolocation, payment, usb). Do not change CSP or other existing headers. Verify via config presence test and/or response header assertion. Append evidence.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Policy shape | Restrictive defaults (disable unused features) | App does not need camera/mic/geo |
| Surfaces | Caddy and/or API middleware | Finding cites both gaps |
| CSP / others | Unchanged | Out of scope |

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | | |
| Security (if required) | | |
