using EmployeeManagement.Api.Data;
using EmployeeManagement.Api.Data.Command;
using EmployeeManagement.Api.Data.Handler;
using EmployeeManagement.Api.Data.Query;
using EmployeeManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id, [FromServices] IQueryHandler<GetEmployeeByIdQuery, Employee> handler)
        {
            var employee = await handler.HandleAsync(new GetEmployeeByIdQuery(id));
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees([FromServices] IQueryHandler<GetEmployeesQuery, IEnumerable<Employee>> handler)
        {
            return Ok(await handler.HandleAsync(new GetEmployeesQuery()));
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee, [FromServices] ICommandHandler<AddEmployeeCommand, Employee> handler)
        {
            await handler.HandleAsync(new AddEmployeeCommand(employee));
            return Ok(employee);
        }
    }
}
