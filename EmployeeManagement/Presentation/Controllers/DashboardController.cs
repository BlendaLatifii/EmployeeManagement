using Application.Interfaces;
using Domain.Constants;
using Domain.Interface.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;

        public DashboardController(IDepartmentService departmentService, IEmployeeService employeeService, IAuthorizationManager authorizationManager)
        {
            _departmentService = departmentService;
            _employeeService = employeeService;
        }
        [Authorize(Roles = RoleConstants.Admin)]
        [HttpGet("[action]")]
        public async Task<ActionResult<int>> GetNumberOfDepartmentsAsync(CancellationToken cancellationToken)
        {
            var numberOfDepartments = await _departmentService.GetNumberOfDepartmentsAsync(cancellationToken);

            return Ok(numberOfDepartments);
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpGet("[action]")]
        public async Task<ActionResult<int>> CountAllEmployeesAsync(CancellationToken cancellationToken)
        {
            var numberOfEmployees = await _employeeService.CountAllEmployeesAsync(cancellationToken);

            return Ok(numberOfEmployees);
        }
        [Authorize(Roles = RoleConstants.Admin)]
        [HttpGet("[action]/{nameOfDepartment}")]
        public async Task<ActionResult<int>> CountEmployeesInDepartmentAsync(string nameOfDepartment, CancellationToken cancellationToken)
        {
            var numberOfEmployees = await _employeeService.CountEmployeesInDepartmentAsync(nameOfDepartment, cancellationToken);

            return Ok(numberOfEmployees);
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpGet("[action]/{daysAgo}")]
        public async Task<ActionResult<int>> GetEmployeeCountByJoiningDateAsync(int daysAgo, CancellationToken cancellationToken)
        {
            var numberOfEmployeesByJoiningDate = await _employeeService.GetEmployeeCountByJoiningDateAsync(daysAgo, cancellationToken);

            return Ok(numberOfEmployeesByJoiningDate);
        }
    }
}
