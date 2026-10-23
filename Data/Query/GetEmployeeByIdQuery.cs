using EmployeeManagement.Api.Models;
using MediatR;

namespace EmployeeManagement.Api.Data.Query
{
        public record GetEmployeeByIdQuery(int Id) : IRequest<Employee>;
}
