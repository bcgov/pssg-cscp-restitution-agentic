# Restitution API

ASP.NET Core API for the Restitution application.

## Requirements

- .NET SDK 10

## Local development

From `restitution-app`:

```bash
dotnet restore
dotnet run
```

Default local URL (from launch settings): `http://localhost:5000`

## User secrets

This project uses ASP.NET Core user secrets (`UserSecretsId` is set in `restitution-app.csproj`).

Use Microsoft docs to configure secrets locally:

- https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets

## Swagger

- UI: `http://localhost:5000/swagger`
- OpenAPI JSON: `http://localhost:5000/swagger/v1/swagger.json`
