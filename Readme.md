# EmployeeManagement.Api

A lightweight Web API for managing employee data. Built with .NET 8 and intended to be used with Visual Studio 2022 or the .NET CLI.

## Features
- RESTful endpoints for employee CRUD operations
- Designed for easy extension and integration with common data stores
- Built with .NET 8 SDK

## Prerequisites
- .NET 8 SDK
- Visual Studio 2022 (17.5+) or later (recommended)
- Optional: SQL Server / PostgreSQL / other DB depending on configured provider

## Getting started
1. Clone the repository:

   git clone https://github.com/rn-arya/EmployeeManagement.git
   cd EmployeeManagement.Api

2. Restore packages and build (dotnet CLI):

   dotnet restore
   dotnet build

3. Configure the application:
- Edit `appsettings.json` or user secrets to set the connection string and other environment-specific settings.
- If the project uses EF Core, update the `ConnectionStrings:Default` entry appropriately and run migrations.

4. Run the API:

- Using Visual Studio 2022: Open the solution and press F5 or use `Debug > Start Debugging`.
- Using the CLI:
  ```bash
  cd src/EmployeeManagement.Api
  dotnet run
  ```

The API will be available at `https://localhost:5xxx` or the URL shown in the console.

## Repository pattern update (RepositoryPattern branch)
Recent changes introduce a repository pattern to improve separation of concerns and testability.

### What changed
- New folder: `src/EmployeeManagement.Api/Repositories`
- New interfaces (examples): `IEmployeeRepository` (defines CRUD operations for employees)
- Implementations (examples): `EmployeeRepository` (EF Core-backed repository)
- DI registration: repositories are registered in `Program.cs`/`Startup` and injected into services/controllers
- Controllers/services updated to depend on repository interfaces instead of accessing `DbContext` directly

### Why this matters
- Easier unit testing of controllers and services
- Centralized data access logic and clearer boundaries
- Simplifies swapping data providers in the future

### How to use the new pattern
1. Switch to the branch containing the changes:

## Project structure
- `src/` - API and production code
  - `Repositories/` - repository interfaces and implementations
- `tests/` - Unit and integration tests
- `docs/` - Documentation (if present)

## Coding standards
This repository follows coding and formatting rules specified in `.editorconfig` and `CONTRIBUTING.md`. Please follow those files when contributing.

## Contributing
See `CONTRIBUTING.md` for contribution guidelines, commit message format, and branching strategy.

## License
Add license information here (e.g., MIT). Replace this placeholder with the appropriate license file or link.

## Contact
For questions or support, open an issue in this repository.