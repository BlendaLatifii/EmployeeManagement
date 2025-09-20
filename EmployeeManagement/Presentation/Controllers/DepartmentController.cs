using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService _departmentService)
        {
            this._departmentService = _departmentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DepartmentDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var departments = await _departmentService.GetAllAsync(cancellationToken);

            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var department = await _departmentService.GetByIdAsync(id, cancellationToken);

            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(DepartmentDto model, CancellationToken cancellationToken)
        {
            var department = await _departmentService.AddAsync(model, cancellationToken);

            return Ok(department);
        }

        [HttpPut]
        public async Task<IActionResult> Updatesync([FromBody] DepartmentDto model, CancellationToken cancellationToken)
        {
            var department = await _departmentService.UpdateAsync(model, cancellationToken);

            return Ok(department);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _departmentService.DeleteAsync(id, cancellationToken);

            return Ok();
        }
    }
}
