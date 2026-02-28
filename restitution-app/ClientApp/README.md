# ClientApp

Angular frontend for Restitution web forms.

## Requirements

- Node.js (LTS, 20+ recommended)
- npm
- .NET API running locally on `http://localhost:5000`

## Run locally

From `restitution-app/ClientApp`:

```bash
npm install
npm start
```

App runs on `http://localhost:4200`.

Proxy config forwards `/api` and `/restwebforms/api` to `http://localhost:5000`.

## API client generation (Orval)

This project uses Orval to generate typed Angular API clients from Swagger.

```bash
npm run generate:api
```

Current config:

- Config file: `orval.config.ts`
- Swagger source: `http://localhost:5000/swagger/v1/swagger.json`
- Generated output under `src/api` and `src/model`

## Useful scripts

- `npm start` - run dev server with proxy
- `npm run build` - build app
- `npm run buildprod` - production build
- `npm test` - run tests
- `npm run lint` - lint
- `npm run format` - check formatting
