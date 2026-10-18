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

## Pure CQRS Pattern (Unreleased)

This project has been updated to use a Pure CQRS (Command Query Responsibility Segregation) architecture. The README should include the following guidance to help contributors understand the new structure, DI changes, and migration steps.

### Overview

- The application separates write operations (Commands) from read operations (Queries).
- Commands modify state and are handled by Command Handlers.
- Queries return read models and are handled by Query Handlers.
- Handlers are orchestrated via an in-process mediator (for example MediatR or a lightweight custom IMediator).

### Folder structure

Recommend the following layout under `src/EmployeeManagement.Api`:

- `Application/Commands` — command DTOs (e.g., `CreateEmployeeCommand`, `UpdateEmployeeCommand`).
- `Application/Queries` — query DTOs (e.g., `GetEmployeeByIdQuery`, `ListEmployeesQuery`).
- `Application/Handlers/Commands` — command handlers.
- `Application/Handlers/Queries` — query handlers.
- `Application/ReadModels` — read-optimized DTOs returned by queries.
- `Domain` — domain entities and domain services.
- `Infrastructure/Persistence` — EF Core context, migrations, data seeding.
- `Api/Controllers` — controllers that use `IMediator` to send commands/queries.

### DI and registration

Register mediator and handler scanning in `Program.cs`:

```csharp
// Program.cs (example)
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
```

Remove previous repository registrations and replace with handler-based composition. If using a custom mediator, register mediator and handlers similarly.

### Controller usage example

Controllers should use `IMediator` to send commands/queries instead of calling repositories or DbContext directly:

```csharp
[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetEmployeeByIdQuery(id);
        var employee = await _mediator.Send(query);
        return Ok(employee);
    }
}
```

### Testing guidance

- Unit-test command and query handlers in isolation by mocking dependencies (DbContext, repositories, external services).
- Integration tests can use in-memory database or test containers for persistence.

### Migration notes

- The repository pattern has been replaced by command/query handlers. Refactor code that consumes `IEmployeeRepository` or `ApplicationDbContext` directly to use commands/queries.
- Update existing tests that relied on repositories to either mock the mediator or test handlers directly.

### Breaking changes

- Removal of repository interfaces and direct DbContext usage in controllers and services. Update code accordingly.

## Project structure
- `src/` - API and production code
  - `Repositories/` - repository interfaces and implementations
  - `Application/` - command/query handlers and DTOs
  - `Domain/` - domain entities and services
  - `Infrastructure/` - EF Core context and migrations
  - `Api/Controllers/` - API controllers
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