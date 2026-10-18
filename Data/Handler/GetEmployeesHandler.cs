using EmployeeManagement.Api.Data.Query;
using EmployeeManagement.Api.Models;

namespace EmployeeManagement.Api.Data.Handler
{
    public class GetEmployeesHandler : IQueryHandler<GetEmployeesQuery, IEnumerable<Employee>>
    {
        private readonly IEmployeeRepository employeeRepository;
        public GetEmployeesHandler(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        public async Task<IEnumerable<Employee>> HandleAsync(GetEmployeesQuery query, CancellationToken cancellationToken = default)
        {
            return await employeeRepository.GetEmployeesAsync();
        }
    }
}