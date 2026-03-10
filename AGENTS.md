# Repository Guidelines

## Project Structure & Module Organization
This repository is split into `backend/` and `frontend/`. The backend is a .NET 8 solution in `backend/HabitTracker.sln` with layered projects: `Controllers/` for API entry points, `Services/` for business logic, `Repositories/` for data access, `Models/` for entities, `Dtos/` for request/response contracts, `Configuration/` for EF Core and exception handling, and `Tests/` for xUnit coverage. The frontend lives in `frontend/` as a Vite + React app; application code is under `frontend/src/`, and static assets are under `frontend/public/`.

## Build, Test, and Development Commands
Backend:coo
- `dotnet restore backend/HabitTracker.sln` installs solution dependencies.
- `dotnet build backend/HabitTracker.sln` compiles all backend projects.
- `dotnet run --project backend/Controllers/Api.csproj` starts the API with Swagger in development.
- `dotnet test backend/Tests/Tests.csproj` runs the xUnit test suite.

Frontend:
- `npm install --prefix frontend` installs Node dependencies.
- `npm run dev --prefix frontend` starts the Vite dev server.
- `npm run build --prefix frontend` creates a production bundle in `frontend/dist/`.
- `npm run lint --prefix frontend` runs ESLint.

## Coding Style & Naming Conventions
Follow the existing project style instead of reformatting opportunistically. C# uses PascalCase for types and public members, camelCase for locals/parameters, and one class per file. React files use PascalCase for components such as `App.jsx`; keep hooks and local helpers in camelCase. The frontend currently uses 2-space indentation and the backend mostly uses tabs; preserve the surrounding file style. Run `npm run lint --prefix frontend` before submitting frontend changes.

## Testing Guidelines
Backend tests use xUnit with Moq in `backend/Tests/Tests/`. Name test files after the service under test, and use descriptive method names such as `Should_Create_User_When_Email_Not_Exists`. Add or update tests for service-layer behavior when changing validation, authentication, or persistence rules. There is no frontend test setup yet, so document manual verification steps in PRs for UI work.

## Commit & Pull Request Guidelines
Recent commits use short, lowercase, imperative summaries such as `adding authorize`, `init front`, and `feature get check in`. Prefer concise subject lines focused on one change. PRs should include a clear summary, affected area (`backend`, `frontend`, or both), linked issue if available, test results, and screenshots or sample API payloads for UI or contract changes.

## Security & Configuration Tips
The API reads SQLite and JWT settings from `backend/Controllers/appsettings.json`. Treat secrets there as development-only, avoid committing real credentials, and do not commit local database changes unless the schema itself changed.
