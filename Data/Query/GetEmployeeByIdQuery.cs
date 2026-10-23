using EmployeeManagement.Api.Models;
using MediatR;

namespace EmployeeManagement.Api.Data.Query
{
    public class GetEmployeeByIdQuery : IRequest<Employee>
    {
        public int Id { get; }

        public GetEmployeeByIdQuery(int id)
        {
            Id = id;
        }
    }
}
