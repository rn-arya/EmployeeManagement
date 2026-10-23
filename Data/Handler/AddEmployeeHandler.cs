using EmployeeManagement.Api.Data.Command;
using EmployeeManagement.Api.Models;
using MediatR;

namespace EmployeeManagement.Api.Data.Handler
{
    public class AddEmployeeHandler : IRequestHandler<AddEmployeeCommand, Employee>
    {
        private readonly IEmployeeRepository employeeRepository;
        public AddEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task<Employee> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            return await employeeRepository.AddEmployeeAsync(request.Employee);
        }
    }
}
