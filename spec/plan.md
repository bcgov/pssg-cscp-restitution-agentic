# Plan — F-TEST-007 (security-focused tests)

## Summary

Extend `restitution-app/ClientApp/src/app/shared/file-uploader/file-uploader.component.spec.ts` so `onFilesAdded` is exercised with a fake `FileList`. Disallowed extension (e.g. `.exe`) must not push into `documents`. Mock `MatSnackBar`. Provide parent form `totalAttachmentSize`. Do not add Playwright. Do not add server MIME.

## Architecture

```text
FileUploaderComponent.onFilesAdded
  → config.accepted_file_extensions
  → snackBar 'Unsupported file type'
  → documents FormArray unchanged
```

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Layer | Karma/Jasmine unit test | Finding cites SPA; no Dynamics |
| Size check | Optional extra assertion | Same method; keep primary on extension |
| CI | Not this slice | F-TEST-001 |

## Security & privacy

- Residual: client-only allow-list (VULN-002)

## Test approach

- `npx ng test --watch=false --browsers=ChromeHeadless` from ClientApp if possible; else document Karma command in evidence
- `@R-05.1` `@R-05.2`
- Append `docs/pr-evidence.md`

## Rollout

- Merge to `development`

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | Sam Okonkwo (simulated) | 2026-09-03 |
| Security (if required) | | |
