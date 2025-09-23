using Application.Dtos;
using Application.Dtos.Users;
using Application.Interfaces;
using AutoMapper;
using Domain.Configs;
using Domain.Constants;
using Domain.Entities;
using Domain.Interface.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly RoleManager<Role> _roleManager;    
        private readonly IValidator<SaveUserDto> _validator;
        private readonly IValidator<UserDetailsDto> _updatedUserValidator;
        private readonly ApiConfig _apiConfig;

        public UserService(RoleManager<Role> roleManager,IUserRepository userRepository, UserManager<User> userManager, IMapper mapper, IValidator<SaveUserDto> validator,ApiConfig apiConfig, IValidator<UserDetailsDto> updatedUserValidator)
        {
            _userManager = userManager;
            _mapper = mapper;
            _validator = validator;
            _apiConfig = apiConfig;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _updatedUserValidator = updatedUserValidator;
        }
        public async Task<AuthenticationDto> LoginAsync(LoginDto loginModel, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            if (user == null)
                throw new Exception("User not found");

            if (!await _userManager.CheckPasswordAsync(user, loginModel.Password))
                throw new Exception("Incorrect password");

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,  user.Id.ToString()),
                new Claim(ClaimTypes.Email, user!.Email!),
            };

            var userRoles = await _userManager.GetRolesAsync(user);


            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            Guid? departmentId = await _userRepository.Query()
                .Where(x => x.Id == user.Id && x.Employee != null)
                .Select(x => x.Employee!.DepartmentId)
                .FirstOrDefaultAsync(cancellationToken);

            if (departmentId.HasValue && departmentId != Guid.Empty)
            {
                authClaims.Add(new Claim(ClaimConstants.Department, departmentId.ToString()!));
            }
            
            var token = CreateToken(authClaims);

            IdentityModelEventSource.ShowPII = true;

            var userData = _mapper.Map<UserDetailsDto>(user);

            return new AuthenticationDto()
            { 
                ExpiresAt = DateTime.Now.AddMinutes(_apiConfig.AccessTokenExpirationMinutes),
                Token = token.ToString(),
                UserData = userData, 
                UserRole = userRoles.ToList()
            };
        }

        public  string CreateToken(IEnumerable<Claim> claims)
        {
            byte[] key = Encoding.ASCII.GetBytes(_apiConfig.ApiSecret);

            var tokenHandler = new JwtSecurityTokenHandler();
            var subject = new ClaimsIdentity(claims);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Issuer = _apiConfig.TokenIssuer,
                Audience = _apiConfig.TokenAudience,
                Subject = subject,
                Expires = DateTime.Now.AddMinutes(_apiConfig.AccessTokenExpirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }

        public async Task<List<UserDetailsDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUsersWithRole(cancellationToken);

            return _mapper.Map<List<UserDetailsDto>>(users);
        }

        public async Task<UserDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserWithRole(id, cancellationToken);

            var model = _mapper.Map<UserDetailsDto>(user);

            return model;
        }
        public async Task<UserDetailsDto> AddAsync(SaveUserDto model, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(model);
            
            var user = _mapper.Map<User>(model);
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new Exception();
            }
            var roles = _roleManager.Roles.Where(role => model.Roles.Contains(role.Key)).Select(role => role.Name).ToList();
           
            await _userManager.AddToRolesAsync(user, roles);

            return await GetByIdAsync(user.Id, cancellationToken);
        }

        public async Task<UserDetailsDto> UpdateUserAsync(UserDetailsDto model, CancellationToken cancellationToken)
        {
            await _updatedUserValidator.ValidateAndThrowAsync(model);

            var existingUser = await _userRepository.GetByIdAsync(model.Id,cancellationToken);

            if(existingUser is null)
            {
                throw new Exception();
            }
            existingUser.UserName = model.UserName;
            existingUser.Email  = model.Email;
            existingUser.LastName = model.LastName;
            existingUser.PhoneNumber = model.PhoneNumber;

            await _userRepository.UpdateAsync(existingUser, cancellationToken);


            return _mapper.Map<UserDetailsDto>(existingUser);
        }

    }
}
