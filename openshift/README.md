# OpenShift assets

Container images are **not** built from this directory.

| Image | Dockerfile | Workflow |
| --- | --- | --- |
| `restitution-api` | `restitution-app/Dockerfile` (.NET 10) | `.github/workflows/cd-restitution-api.yml` |
| `restitution-ui` | `restitution-app/ClientApp/Dockerfile` | `.github/workflows/cd-restitution-ui.yml` |

`Dockerfile.ubi8.net8_customized` was removed: it used a `ubi8/dotnet-80-runtime`
(.NET 8) base image while the application targets `net10.0`, so it could only have
produced a broken deployment. Any future S2I/UBI image must use a .NET 10 base and
pin it by `sha256` digest, matching `restitution-app/Dockerfile`.

`settings.sh` remains for the OpenShift deployment scripts (namespace/component names).
