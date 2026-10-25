using EmployeeManagement.Application.Command.EmployeeCommand;
using EmployeeManagement.Application.DTOs.Employees;
using EmployeeManagement.Application.Query.EmployeeQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator mediator;
        public EmployeesController(IMediator mediator) { this.mediator = mediator; }
       
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
        {
            var employee = await mediator.Send(new GetEmployeeByIdQuery(id));
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            return Ok(await mediator.Send(new GetEmployeesQuery()));
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> AddEmployee(EmployeeDto employee)
        {
            await mediator.Send(new AddEmployeeCommand(employee));
            return Ok(employee);
        }
    }
}
