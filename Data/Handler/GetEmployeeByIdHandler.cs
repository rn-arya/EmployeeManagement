using EmployeeManagement.Api.Data.Query;
using EmployeeManagement.Api.Models;

namespace EmployeeManagement.Api.Data.Handler
{
    public class GetEmployeeByIdHandler : IQueryHandler<GetEmployeeByIdQuery, Employee>
    {
        private readonly IEmployeeRepository employeeRepository;
        public GetEmployeeByIdHandler(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        public async Task<Employee> HandleAsync(GetEmployeeByIdQuery query, CancellationToken cancellationToken = default)
        {
           return await employeeRepository.GetEmployeeByIdAsync(query.Id);   
        }
    }
}
