# EmployeeManagement.Api

A lightweight .NET 8 Web API for managing employee records. It uses ASP.NET Core controllers, Entity Framework Core with SQL Server LocalDB, and MediatR to dispatch commands and queries through dedicated handlers.

## Features
- RESTful employee operations
- MediatR-based Pure CQRS request and handler separation
- Employee add, list, and lookup operations
- Repository-based Entity Framework Core data access
- Swagger/OpenAPI documentation in development
- SQL Server LocalDB configuration

## Current architecture
- `Controllers/EmployeesController.cs` receives HTTP requests and sends MediatR requests.
- `Data/Command` contains write requests such as `AddEmployeeCommand`.
- `Data/Query` contains read requests such as `GetEmployeeByIdQuery` and `GetEmployeesQuery`.
- `Data/Handler` contains the corresponding MediatR request handlers.
- `Data/IEmployeeRepository.cs` and `Data/EmployeeRepository.cs` encapsulate persistence operations.
- `Data/AppDbContext.cs` configures Entity Framework Core for the employee entity.
- `Models/Employee.cs` defines the employee data model.

This implementation separates application write and read requests into commands and queries. Controllers use MediatR instead of directly orchestrating employee operations, and dedicated handlers use the repository layer for persistence.

## Prerequisites
- .NET 8 SDK
- Visual Studio 2022 17.5 or later, or the .NET CLI
- SQL Server LocalDB for the default configuration

## Getting started

```powershell
git clone https://github.com/rn-arya/EmployeeManagement.git
cd EmployeeManagement/EmployeeManagement.Api
dotnet restore
dotnet build
dotnet run
```

The default `DefaultConnection` setting uses SQL Server LocalDB and the `EmployeeManagement` database. Update `appsettings.json`, user secrets, or an environment-specific settings file for a different database.

The Development launch profile opens Swagger. The default HTTP and HTTPS URLs are defined in `Properties/launchSettings.json`.

## API endpoints

| Method | Route | Description |
|---|---|---|
| `GET` | `/api/Employees/{id}` | Returns one employee by ID, or `404` when not found. |
| `GET` | `/api/Employees` | Returns all employees. |
| `POST` | `/api/Employees` | Adds an employee and returns it in the response. |

The API is documented with Swagger while running in the Development environment.

## Project structure

```text
Controllers/EmployeesController.cs  HTTP endpoints and mediator dispatch
Data/Command/                      MediatR write requests
Data/Query/                        MediatR read requests
Data/Handler/                      MediatR request handlers
Data/AppDbContext.cs               Entity Framework Core database context
Data/EmployeeRepository.cs         Employee persistence implementation
Data/IEmployeeRepository.cs        Persistence contract
Models/Employee.cs                 Employee entity
Program.cs                         Application startup and dependency injection
```

## Pure CQRS with MediatR (Unreleased)

This project uses Pure CQRS principles with MediatR. Controllers send typed write requests and read requests to dedicated application handlers instead of combining application orchestration with HTTP endpoint logic.

### Overview

- The application separates write operations (Commands) from read operations (Queries).
- Commands modify state and are handled by Command Handlers.
- Queries return read models and are handled by Query Handlers.
- Handlers are orchestrated via an in-process mediator (for example MediatR or a lightweight custom IMediator).

### Request structure

The current implementation uses these project folders:

- `Data/Command` - MediatR write requests.
- `Data/Query` - MediatR read requests.
- `Data/Handler` - MediatR request handlers.
- `Data` - database context and repository implementation.
- `Controllers` - HTTP API endpoints.
- `Models` - shared employee entity.

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

- Controllers should consume `IMediator` rather than repositories or `AppDbContext`.
- Add operations should be implemented as command requests with command handlers.
- Read operations should be implemented as query requests with query handlers.
- Existing custom `ICommandHandler` and `IQueryHandler` contracts have been replaced by MediatR `IRequestHandler<,>` interfaces.
- The repository remains an infrastructure dependency used inside handlers; it is not injected into controllers.

## Coding standards
This repository follows coding and formatting rules specified in `.editorconfig` and `CONTRIBUTING.md` when those files are available.

## Contributing
See `CONTRIBUTING.md` for contribution guidelines and commit message format when available.

## License
License information has not been added yet. Add a license file and update this section before distributing the project.

## Contact
For questions or support, open an issue in this repository.