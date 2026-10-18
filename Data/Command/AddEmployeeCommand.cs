using EmployeeManagement.Api.Data.Handler;
using EmployeeManagement.Api.Models;
using System.Windows.Input;

namespace EmployeeManagement.Api.Data.Command
{
    public class AddEmployeeCommand : ICommand<Employee>
    {
        public Employee Employee { get; }

        public AddEmployeeCommand(Employee employee)
        {
            Employee = employee;
        }
    }
}
