using EmployeeManagement.Api.Data.Query;
using EmployeeManagement.Api.Models;
using MediatR;

namespace EmployeeManagement.Api.Data.Handler
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, Employee>
    {
        private readonly IEmployeeRepository employeeRepository;
        public GetEmployeeByIdHandler(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        public async Task<Employee> Handle(GetEmployeeByIdQuery query, CancellationToken cancellationToken = default)
        {
            return await employeeRepository.GetEmployeeByIdAsync(query.Id);
        }
    }
}
