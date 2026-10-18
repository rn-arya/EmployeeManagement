# Changelog

All notable changes to this project will be documented in this file.

The format is based on "Keep a Changelog" and follows Semantic Versioning:
https://keepachangelog.com/en/1.0.0/

## [Unreleased]
### Added
- Implemented a Pure CQRS (Command Query Responsibility Segregation) architecture.
  - New folders under `src/EmployeeManagement.Api`:
    - `Application/Commands`, `Application/Queries`
    - `Application/Handlers/Commands`, `Application/Handlers/Queries`
    - `Application/ReadModels`, `Application/DTOs`
    - `Domain`, `Infrastructure/Persistence`
  - Introduced command and query DTOs and dedicated handlers for employee operations (for example `CreateEmployeeCommand` / `CreateEmployeeHandler`, `GetEmployeeByIdQuery` / `GetEmployeeByIdHandler`, `ListEmployeesQuery`).
  - Added in-process mediator registration (e.g., MediatR) and handler discovery.
  - Added mapping layer guidance (optional AutoMapper/Mapster) and read-models optimized for queries.
  - Updated DI registrations in `Program.cs` to register mediator and handlers.

### Changed
- Replaced the repository pattern with command/query handlers; controllers and application logic now send Commands and Queries to the mediator instead of calling repositories or `DbContext` directly.
- Controller endpoints updated to use `IMediator` (or custom mediator) for orchestration.
- Tests updated to focus on handler unit tests and mediator-based integration tests; repository mocks removed where applicable.

### Fixed
- n/a

### Breaking
- Removal of repository interfaces and direct `ApplicationDbContext` usage from controllers and services.
- Code that previously used `IEmployeeRepository` or direct `DbContext` access must be refactored to use commands/queries or tested via handler-level tests.
- Update existing tests that depended on repositories to either mock `IMediator` or test handlers directly.

## [0.1.0] - 2026-10-16
### Added
- Project initial commit and baseline API for employee management.
- .NET 8 target and project scaffolding.

---

Release and usage notes:
- Continue adding development changes under `Unreleased`.
- When cutting a release, rename `Unreleased` to the release version (for example `1.0.0`) and add the release date.
- See `CONTRIBUTING.md` for release and commit guidelines.