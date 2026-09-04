# Plan — VULN-001 (mailing address XSS)

## Summary

Encode each address field (e.g. replace `& < > " '`) before joining with `<br />`, **or** change the review template to text/`innerText` with CSS line breaks. Fix both `displayMailingSubAddress` and `displayMailingAddress` if both build HTML. Add Jasmine/Karma (or pure TS) unit test that markup like `<img src=x onerror=alert(1)>` does not produce an unescaped tag in the return value / DOM. Append evidence.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Fix | HTML-encode fields then keep `<br />` separators | Minimal UI change |
| Also fix | `displayMailingAddress` | Same bug pattern |

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | Sam Okonkwo (simulated) | 2026-09-04 |
| Security (if required) | | |
