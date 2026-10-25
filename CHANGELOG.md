# Changelog

All notable changes to this project will be documented in this file.

The format is based on "Keep a Changelog" and follows Semantic Versioning:
https://keepachangelog.com/en/1.0.0/

## [Unreleased]
### Added
- Added a layered .NET 8 solution structure with API, Application, Domain, and Infrastructure projects.
- Added MediatR-based CQRS request handling for employee operations.
- Added `AddEmployeeCommand`, `GetEmployeesQuery`, and `GetEmployeeByIdQuery` request types.
- Added DTO-based employee responses and repository-backed handler logic.
- Added EF Core SQL Server LocalDB configuration for employee persistence.
- Added Swagger support for local API exploration in development.

### Changed
- Updated `EmployeesController` to mediate employee operations through `IMediator`.
- Reworked the project documentation to reflect the current folder layout and architecture.
- Centralized dependency registration using the Application and Infrastructure extension methods.
- Clarified the default LocalDB connection string and startup behavior in the project setup docs.

### Fixed
- Corrected the README to match the actual project structure and request flow.
- Ensured the documented API routes reflect the current endpoints: `GET /api/Employees`, `GET /api/Employees/{id}`, and `POST /api/Employees`.

### Notes
- The current implementation uses a shared repository abstraction for both reads and writes; the design is CQRS-oriented without a separate read/write database implementation.

## [0.1.0] - 2026-10-16
### Added
- Initial project scaffolding for the employee management API.
- .NET 8 ASP.NET Core application setup with basic employee API endpoints.
- Initial project documentation and change tracking for the baseline release.

---

Release and usage notes:
- Continue adding changes under `Unreleased` as the project evolves.
- When preparing a release, rename `Unreleased` to the target version and include the release date.
- Keep the changelog aligned with the actual code and usage of the API.