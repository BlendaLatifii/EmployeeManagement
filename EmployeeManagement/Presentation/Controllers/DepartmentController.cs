using Application.Dtos.Department;
using Application.Interfaces;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<DepartmentDetailDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var departments = await _departmentService.GetAllAsync(cancellationToken);

            return Ok(departments);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDetailDto>> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var department = await _departmentService.GetByIdAsync(id, cancellationToken);

            return Ok(department);
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpPost]
        public async Task<ActionResult<DepartmentDetailDto>> AddAsync(AddDepartmentDto model, CancellationToken cancellationToken)
        {
            var department = await _departmentService.AddAsync(model, cancellationToken);

            return Ok(department);
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpPut]
        public async Task<ActionResult<DepartmentDetailDto>> Updatesync([FromBody] DepartmentDetailDto model, CancellationToken cancellationToken)
        {
            var department = await _departmentService.UpdateAsync(model, cancellationToken);

            return Ok(department);
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _departmentService.DeleteAsync(id, cancellationToken);

            return Ok();
        }
    }
}
