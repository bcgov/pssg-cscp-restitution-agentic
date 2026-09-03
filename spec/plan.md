# Plan — DEP-006 (Dockerfile net10 + digests)

## Summary

Delete or replace `openshift/Dockerfile.ubi8.net8_customized` with a short `openshift/README.md` noting CD uses `restitution-app/Dockerfile`. Pin `mcr.microsoft.com/dotnet/aspnet:10.0-alpine` and `sdk:10.0-alpine` FROM lines with `@sha256:…` digests (resolve via `docker buildx imagetools inspect` or registry). Confirm `cd-restitution-api.yml` still points at `./restitution-app/Dockerfile`. Append evidence.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Stale file | Remove + README pointer | Not used by CD; avoids false net8 path |
| Runtime | Keep MCR alpine net10 | Already working in CD |
| Digests | Pin both stages | Supply-chain half of finding |

## Test approach

- Diff review of Dockerfiles + workflow path
- `@R-12.1` `@R-12.2`

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | Sam Okonkwo (simulated) | 2026-09-03 |
| Security (if required) | | |
