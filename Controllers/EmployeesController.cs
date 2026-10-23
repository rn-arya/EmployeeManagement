using EmployeeManagement.Api.Data;
using EmployeeManagement.Api.Data.Command;
using EmployeeManagement.Api.Data.Handler;
using EmployeeManagement.Api.Data.Query;
using EmployeeManagement.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator mediator;
        public EmployeesController(IMediator mediator) { this.mediator = mediator; }
       
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await mediator.Send(new GetEmployeeByIdQuery(id));
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return Ok(await mediator.Send(new GetEmployeesQuery()));
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            await mediator.Send(new AddEmployeeCommand(employee));
            return Ok(employee);
        }
    }
}
