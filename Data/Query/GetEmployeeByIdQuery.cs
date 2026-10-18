using EmployeeManagement.Api.Data.Handler;
using EmployeeManagement.Api.Models;

namespace EmployeeManagement.Api.Data.Query
{
    public class GetEmployeeByIdQuery : IQuery<Employee>
    {
        public int Id { get; }

        public GetEmployeeByIdQuery(int id)
        {
            Id = id;
        }
    }
}
