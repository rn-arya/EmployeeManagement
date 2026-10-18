using EmployeeManagement.Api.Data.Command;
using EmployeeManagement.Api.Models;

namespace EmployeeManagement.Api.Data.Handler
{
    public class AddEmployeeHandler : ICommandHandler<AddEmployeeCommand, Employee>
    {
        private readonly IEmployeeRepository employeeRepository;
        public AddEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        public async Task<Employee> HandleAsync(AddEmployeeCommand command, CancellationToken cancellationToken = default)
        {
            return await employeeRepository.AddEmployeeAsync(command.Employee);
        }
    }
}
