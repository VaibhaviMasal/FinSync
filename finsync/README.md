# FinSync frontend

React and Vite frontend for the FinSync insurance CRM. Module metadata and the API adapter are written in TypeScript. The interface uses Lucide icons, CSS, and SVG charts.

## Run locally

Use Node.js 22 or later from the project directory.

```sh
npm ci
npm run dev
```

Open the URL printed by Vite. With no API URL configured, the frontend opens in demo mode. Demo changes are scoped to your current browser tab session. Dashboard headline totals and charts are illustrative. The recent-policy table follows the demo records.
The included `FinSync-demo.html` also opens directly in a browser without installing dependencies.

```sh
npm run build
npm run preview
```

## Connect to FinSync

Copy `.env.example` to `.env.local`. Set `VITE_API_BASE_URL` to your running ASP.NET API's actual origin and restart Vite. The hostname, port, HTTPS certificate and browser access must match your local environment. Sign in using an existing account.

The adapter resolves endpoints from Swagger by controller tag, HTTP method and route shape. Customize `VITE_OPENAPI_PATH` or set exact route overrides using `VITE_ROUTE_<CONTROLLER>_<KIND>_<METHOD>` when the route differs or Swagger is unavailable. Supported kinds are LOGIN, COLLECTION, SUMMARY and REPORT (dashboard report). For example, `VITE_ROUTE_AUTHENTICATION_LOGIN_POST` overrides the sign-in route. Collection response envelopes may contain a top-level array or an array inside `data`. Verify exact DTO field casing and list shapes against your API.

The frontend handles JWT sign-in and expiry, reads from configured endpoints and filters/paginates the records returned by each endpoint. Agent and other server-paged lists may show only the first page returned by the API. Direct vehicle listing depends on a backend capability that the audit reports is absent.

Live create, edit, delete, file upload, notification updates and settings updates require verified endpoint and DTO contracts. Demo record forms exercise local CRUD only. Live premium and breakdown charts are omitted because the supplied audit identifies inconsistencies in payment aggregation and coverage data. The actual backend and database were unavailable for verification.

The backend repository is external to this frontend package.
