using MediatR;
using EmployeeManagement.Api.Models;

namespace EmployeeManagement.Api.Data.Query
{
    public class GetEmployeesQuery : IRequest<IEnumerable<Employee>>
    {
    }
}
