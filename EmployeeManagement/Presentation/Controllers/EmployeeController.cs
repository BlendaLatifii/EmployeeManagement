using Application.Dtos.Employee;
using Application.Interfaces;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService _employeeService)
        {
            this._employeeService = _employeeService;
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpGet]
        public async Task<ActionResult<List<EmployeeDetailDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var employees = await _employeeService.GetAllAsync(cancellationToken);

            return Ok(employees);
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDetailDto>> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var model = await _employeeService.GetByIdAsync(id, cancellationToken);

            return Ok(model);
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpPost]
        public async Task<ActionResult<EmployeeDetailDto>> AddAsync(AddEmployeeDto model, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.AddAsync(model, cancellationToken);

            return Ok(employee);
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpPut]
        public async Task<ActionResult<EmployeeDetailDto>> Updatesync([FromBody] EmployeeDetailDto model, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.UpdateAsync(model, cancellationToken);

            return Ok(employee);
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _employeeService.DeleteAsync(id, cancellationToken);

            return Ok();
        }
    }
}