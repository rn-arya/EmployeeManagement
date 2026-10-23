using MediatR;
using EmployeeManagement.Api.Models;

namespace EmployeeManagement.Api.Data.Query
{
    public record GetEmployeesQuery : IRequest<IEnumerable<Employee>>;
}
