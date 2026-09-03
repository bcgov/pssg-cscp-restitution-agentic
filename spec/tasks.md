# Tasks — DEP-006

## Milestone 1

- [x] **TASK-001** — Delete `openshift/Dockerfile.ubi8.net8_customized` (or replace with a short superseded notice). Covers `@R-12.1`.
- [x] **TASK-002** — Add `openshift/README.md` pointing CD/API builds at `restitution-app/Dockerfile`. Covers `@R-12.1`.
- [x] **TASK-003** — Pin `FROM` digests on `restitution-app/Dockerfile` for aspnet/sdk 10.0-alpine (keep .NET 10 family). Covers `@R-12.2`.
- [x] **TASK-004** — Confirm `.github/workflows/cd-restitution-api.yml` still uses `./restitution-app/Dockerfile`. Covers `@R-12.1`.
- [x] **TASK-005** — Append `docs/pr-evidence.md`. Covers both.

## Backlog

- [ ] ClientApp/Caddy Dockerfile hygiene — out of scope
- [ ] UBI net10 S2I migration — out of scope this slice
