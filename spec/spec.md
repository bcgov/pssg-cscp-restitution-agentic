# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#12](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/12) — rapid assessment **DEP-006**.

## Problem

An OpenShift Dockerfile still advertises a **.NET 8** runtime while the app targets **net10.0**. If anyone used that file to ship the API, the runtime would be wrong. CD today builds from `restitution-app/Dockerfile` (already net10), but the stale file remains confusing. Base images are also not digest-pinned.

## Outcome

1. The stale .NET 8 OpenShift Dockerfile is **removed or clearly marked unused / superseded** so it cannot be mistaken for the deploy path.
2. The **active** API Dockerfile continues to use a **.NET 10** runtime/SDK and documents that it is the CD path.
3. Active API Dockerfile base images are **pinned by digest** (or an equivalent immutable reference).

## Users & personas

| Persona | Goal |
| --- | --- |
| Platform / CD | No net8/net10 mismatch on the real build path |
| Security | Digests stop floating tag drift |

## Scope

### In scope

- Deprecate/remove `openshift/Dockerfile.ubi8.net8_customized` (or replace with a short README pointing at `restitution-app/Dockerfile`)
- Confirm CD workflow still references the net10 Dockerfile
- Pin `FROM` digests on the active API Dockerfile runtime/SDK images

### Out of scope

- Rewriting ClientApp/Caddy Dockerfile
- Migrating to UBI net10 custom S2I (unless a real UBI net10 image is already approved — prefer MCR net10 already in use)
- OpenShift template rewrites beyond Dockerfile hygiene

## Journeys

1. No misleading net8 deploy Dockerfile — `features/dep-006-dotnet10-dockerfile.feature` (@R-12.1)
2. Active path pinned — same feature (@R-12.2)

## Open questions

- [x] Assessment path is stale relative to CD: prefer fixing active `restitution-app/Dockerfile` + remove/deprecate openshift net8 file rather than inventing a UBI net10 S2I tree.

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | Alex Rivera (simulated) | 2026-09-03 |
| BA | Alex Rivera (simulated) | 2026-09-03 |
| QA (acceptance ownership) | | |
