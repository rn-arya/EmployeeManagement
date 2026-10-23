using MediatR;
using EmployeeManagement.Api.Models;


namespace EmployeeManagement.Api.Data.Command
{
    public record AddEmployeeCommand(Employee Employee) : IRequest<Employee>;    
}
