# Changelog

All notable changes to this project will be documented in this file.

The format is based on "Keep a Changelog" and follows Semantic Versioning:
https://keepachangelog.com/en/1.0.0/

## [Unreleased]
### Added
- Added MediatR-based command/query handling for employee operations.
- Added `AddEmployeeCommand` and `GetEmployeesQuery` request types.
- Added dedicated handlers for adding, listing, and retrieving employees by ID.
- Added automatic MediatR handler discovery during application startup.

### Changed
- Updated `EmployeesController` to send commands and queries through `IMediator` instead of orchestrating employee operations directly.
- Updated MediatR request handlers to implement the library interfaces `IRequestHandler<,>` and return application responses.
- Updated employee commands and queries to implement `IRequest<,>` or `IRequest<>`.
- Updated `Program.cs` to register MediatR and remove manual custom handler registrations.
- Added MediatR 14.2.0 to the project dependencies.

### Fixed
- Removed the custom `ICommandHandler` and `IQueryHandler` abstractions that were redundant with MediatR interfaces.
- Corrected the employee controller flow to use the current request/handler contracts.

### Breaking
- Existing custom code that implements `ICommandHandler` or `IQueryHandler` must use the MediatR `IRequestHandler<,>` interface instead.
- Direct controller-level orchestration has been replaced with mediator sends.

### Architecture note
- Employee reads and writes now use MediatR handlers, while persistence remains behind `IEmployeeRepository` and `EmployeeRepository`. This is a CQRS-style command/query split with a shared repository and database context; it is not a separate read/write database implementation.

## [0.1.0] - 2026-10-16
### Added
- Project initial commit and baseline API for employee management.
- .NET 8 target and project scaffolding.

---

Release and usage notes:
- Continue adding development changes under `Unreleased`.
- When cutting a release, rename `Unreleased` to the release version (for example `1.0.0`) and add the release date.
- See `CONTRIBUTING.md` for release and commit guidelines.