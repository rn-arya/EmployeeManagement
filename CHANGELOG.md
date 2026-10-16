# Changelog

All notable changes to this project will be documented in this file.

The format is based on "Keep a Changelog" and follows Semantic Versioning:
https://keepachangelog.com/en/1.0.0/

## [Unreleased]
### Added
- Implemented repository pattern for data access.
  - New folder: `src/EmployeeManagement.Api/Repositories`
  - Added `IEmployeeRepository` and `EmployeeRepository` (EF Core-backed) as examples.
  - Repository interfaces abstract CRUD operations for employee entities.
- DI registration for repositories added to `Program.cs`.

### Changed
- Controllers and service code updated to use repository interfaces instead of direct `DbContext` access.
- Improved testability by allowing repository mocking.

### Fixed
- n/a

### Breaking
- Controllers and custom code that previously used `ApplicationDbContext` directly may need to be refactored to use the new repository interfaces.
- Update tests that depended on `DbContext` to use repository mocks or adapt to the new design.

## [0.1.0] - 2026-10-16
### Added
- Project initial commit and baseline API for employee management.
- .NET 8 target and project scaffolding.

---

Release and usage notes:
- Continue adding development changes under `Unreleased`.
- When cutting a release, rename `Unreleased` to the release version (for example `1.0.0`) and add the release date.
- See `CONTRIBUTING.md` for release and commit guidelines.