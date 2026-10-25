# Employee Management API

A .NET 8 ASP.NET Core Web API for managing employee records. The solution separates HTTP endpoints, application requests, domain contracts, and persistence into distinct projects. MediatR dispatches employee commands and queries, while Entity Framework Core stores data in SQL Server.

## Features

- List employees, retrieve an employee by ID, and add an employee
- MediatR request handling with a CQRS-style separation of reads and writes
- Repository abstraction between application logic and persistence
- Strongly typed employee DTOs and configuration options
- Entity Framework Core with SQL Server / LocalDB
- Swagger UI in the Development environment

## Solution structure

```text
EmployeeManagement.Api/
  Controllers/EmployeesController.cs  HTTP endpoints
  Program.cs                          Application startup and service registration

EmployeeManagement.Application/
  Command/EmployeeCommand/            Employee write request and handler
  Query/EmployeeQuery/                Employee read requests and handlers
  DTOs/Employees/                     Employee API DTO
  Options/ConnectionStringOptions.cs  Strongly typed connection settings

EmployeeManagement.Domain/
  Entities/Employee.cs                Employee entity and column mapping
  Interface/IEmployeeRepository.cs    Persistence contract

EmployeeManagement.Infrastructure/
  Persistence/AppDbContext.cs         EF Core database context
  Persistence/Repositories/           Repository implementation
```

## Requirements

- .NET 8 SDK
- SQL Server LocalDB for the default development connection, or another configured SQL Server instance

## Run locally

From the repository root:

```powershell
dotnet restore EmployeeManagement.Api\EmployeeManagement.sln
dotnet build EmployeeManagement.Api\EmployeeManagement.sln
dotnet run --project EmployeeManagement.Api
```

The default connection is configured in `EmployeeManagement.Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=EmployeeManagement;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Change `DefaultConnection` to target another SQL Server. For local development, prefer user secrets or environment-specific configuration for connection details rather than committing sensitive values. In Development, Swagger UI is available at `/swagger`.

## API

| Method | Route | Description |
|---|---|---|
| `GET` | `/api/Employees` | Returns the employee collection. |
| `GET` | `/api/Employees/{id}` | Retrieves an employee by ID. |
| `POST` | `/api/Employees` | Adds an employee and returns the employee DTO. |

Example `POST /api/Employees` request:

```json
{
  "empNo": 9999,
  "birthDate": "1990-01-15T00:00:00",
  "firstName": "Jane",
  "lastName": "Doe",
  "gender": "F",
  "hireDate": "2024-01-10T00:00:00"
}
```

The current POST action returns `200 OK`. The GET-by-ID action is intended to return `404 Not Found` for an unknown ID, but that path should be verified/fixed in the handler before relying on the status code: the repository can return `null`, and the handler currently dereferences the result.

## Architecture and request flow

The application uses a CQRS-style split: commands perform writes and queries perform reads. Both use the same repository and database; this is not a separate read/write database architecture.

```text
Client
  | HTTP request
  v
EmployeesController
  | sends command/query
  v
IMediator --> Matching request handler
                    |
                    v
             IEmployeeRepository
                    |
                    v
               AppDbContext
                    |
                    v
              SQL Server
                    |
                    v
           Employee entity / DTO
                    |
                    v
             HTTP response
```

1. The controller binds the HTTP request and sends a MediatR request, such as `GetEmployeesQuery` or `AddEmployeeCommand`.
2. MediatR resolves the corresponding handler registered from the Application assembly.
3. The handler calls `IEmployeeRepository`; read handlers map returned entities to `EmployeeDto`.
4. The repository uses `AppDbContext` and EF Core to access SQL Server.
5. The controller translates the handler result into an HTTP response.

## Dependency injection and configuration

`Program.cs` composes the application and infrastructure registrations:

```csharp
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure();
```

- **Options binding:** `AddApplication` binds the `ConnectionStrings` configuration section to `ConnectionStringOptions`. Its `DefaultConnection` property provides a strongly typed way to read the configured connection string.
- **MediatR registration:** `AddApplication` scans the Application assembly to register request handlers.
- **DbContext registration:** `AddInfrastructure` registers `AppDbContext` with `AddDbContext` and configures the SQL Server provider using `IOptionsMonitor<ConnectionStringOptions>`. `AddDbContext` uses a scoped lifetime by default.
- **Repository registration:** Infrastructure maps `IEmployeeRepository` to `EmployeeRepository` with a scoped lifetime. Handlers depend on the interface, not the concrete database implementation.
- **Constructor injection:** ASP.NET Core provides `IMediator` to `EmployeesController`, and the DI container supplies the repository and DbContext dependencies to handlers and infrastructure services.

## License

No license file is currently included. Add a license before redistributing or publishing the project.