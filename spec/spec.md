# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#28](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/28) — rapid assessment **CONFIG-005**.

## Problem

`AllowedHosts` is set to `*`, which disables ASP.NET Core host filtering. The app accepts any Host header value, leaving a protective control unused.

## Outcome

`AllowedHosts` is tightened away from the wildcard: use env-configurable specific host values (or a documented comma-separated list) suitable for local/dev and OpenShift. Residual risk for multi-host OpenShift routing is documented in evidence. Do not invent production hostnames in committed config beyond placeholders/env wiring.

## Scope

### In scope

- Replace `AllowedHosts: "*"` with specific / env-driven hosts
- Document residual OpenShift / multi-host concerns in evidence
- Config/unit assertion that AllowedHosts is not `*`
- Keep app bootable locally without Dynamics

### Out of scope

- Live Dynamics tests
- Inventing real production hostnames in repo secrets
- Changing authentication

## Journeys

1. Host filtering enabled — `features/config-005-allowed-hosts.feature` (@R-28.1)

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | | |
| BA | | |
| QA (acceptance ownership) | | |
