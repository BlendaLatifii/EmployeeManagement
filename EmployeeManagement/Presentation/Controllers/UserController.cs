using Application.Dtos.Users;
using Application.Interfaces;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpGet]
        public async Task<ActionResult<List<UserDetailsDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllAsync(cancellationToken);

            return Ok(users);
        }

        [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Employee)]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailsDto>> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        { 
            var user = await _userService.GetByIdAsync(id, cancellationToken);

            return Ok(user);
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpPost]
        public async Task<ActionResult<UserDetailsDto>> AddAsync(SaveUserDto model, CancellationToken cancellationToken)
        {
            var user = await _userService.AddAsync(model, cancellationToken);

            return Ok(user);
        }
    }
}
