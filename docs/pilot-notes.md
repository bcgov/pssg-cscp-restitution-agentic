# Agentic SDLC pilot notes (pssg-cscp-restitution-agentic)

Tier **2 v3** enrolment fork for pipeline testing. Default branch: **`development`**.

## Local stand-up

```bash
# API (needs .NET SDK 10)
cd restitution-app
dotnet restore && ASPNETCORE_ENVIRONMENT=Development ASPNETCORE_URLS=http://localhost:5000 dotnet run --no-launch-profile

# SPA
cd restitution-app/ClientApp
npm install && npm start
```

- API: http://localhost:5000/swagger
- UI: http://localhost:4200/restwebforms

Dynamics user-secrets are **not** required to boot the shell. Lookups and submit fail until secrets exist or a Development stub is added.

## Backlog

Rapid-assessment ticket inventory:

- [`pssg-cscp-restitution-rapid-assessment-tickets.md`](pssg-cscp-restitution-rapid-assessment-tickets.md)
- [`pssg-cscp-restitution-rapid-assessment-tickets.csv`](pssg-cscp-restitution-rapid-assessment-tickets.csv)

Pick one row with **File?** = `yes` and **GitHub** = `pending`, then run checkpoints 1 → 2 → `ready-for-agent` → 3.

## Suggested first stories

**CONFIG-001** — Trivy `exit-code: 0` in CI. Small workflow change; no Dataverse.

Alternatives that stay local/CI-friendly:

- **DEP-001** — replace archived `Microsoft.AspNetCore.HealthChecks` 1.0.0
- **F-TEST-003** — first API test project (does not need Dynamics if testing DTO mapping)

## Pipeline checklist

1. File GitHub issue from ticket body in the backlog doc
2. **Checkpoint 1** — spec + Gherkin PR (`docs(spec)`)
3. **Checkpoint 2** — plan + tasks PR (`docs(plan)`)
4. Label issue `ready-for-agent` (or implement locally)
5. Implementation PR → checkpoint gate + spec review
6. **Checkpoint 3** — human merge

## Enrol (2026-09-03)

- Tier 1 + Tier 2 v3 (`patterns/tier2-v3/enrol.sh --with-tier1 --with-gh-aw`)
- `coding_agent.base_branch`: `development`
- Constitution filled for brownfield Angular + ASP.NET Core + OpenShift
