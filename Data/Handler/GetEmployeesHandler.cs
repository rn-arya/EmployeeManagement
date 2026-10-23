using EmployeeManagement.Api.Data.Query;
using EmployeeManagement.Api.Models;
using MediatR;

namespace EmployeeManagement.Api.Data.Handler
{
    public class GetEmployeesHandler : IRequestHandler<GetEmployeesQuery, IEnumerable<Employee>>
    {
        private readonly IEmployeeRepository employeeRepository;
        public GetEmployeesHandler(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        public async Task<IEnumerable<Employee>> Handle(GetEmployeesQuery query, CancellationToken cancellationToken = default)
        {
            return await employeeRepository.GetEmployeesAsync();
        }
    }
}