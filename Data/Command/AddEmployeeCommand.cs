using MediatR;
using EmployeeManagement.Api.Models;


namespace EmployeeManagement.Api.Data.Command
{
    public class AddEmployeeCommand : IRequest<Employee>
    {
        public Employee Employee { get; }

        public AddEmployeeCommand(Employee employee)
        {
            Employee = employee;
        }
    }
}
