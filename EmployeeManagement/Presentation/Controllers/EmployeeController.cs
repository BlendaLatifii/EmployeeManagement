using Application.Dtos;
using Application.Interfaces;
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

        [HttpGet]
        public async Task<ActionResult<List<EmployeeDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var employees = await _employeeService.GetAllAsync(cancellationToken);

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var model = await _employeeService.GetByIdAsync(id, cancellationToken);

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(EmployeeDto model, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.AddAsync(model, cancellationToken);

            return Ok(employee);
        }

        [HttpPut]
        public async Task<IActionResult> Updatesync([FromBody] EmployeeDto model, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.UpdateAsync(model, cancellationToken);

            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _employeeService.DeleteAsync(id, cancellationToken);

            return Ok();
        }
    }
}