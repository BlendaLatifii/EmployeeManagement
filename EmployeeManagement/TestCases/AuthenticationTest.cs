using Application.Dtos.Users;
using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;

namespace TestCases
{
    public class AuthenticationTest
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly AuthenticationController _controller;

        public AuthenticationTest()
        {
            _userServiceMock = new Mock<IUserService>();
            _controller = new AuthenticationController(_userServiceMock.Object);
        }
        [Fact]
        public async Task LoginAsync_ReturnsAuthenticationDto()
        {

            var loginDto = new LoginDto
            {
                Email = "testuser@example.com",
                Password = "Password123!"
            };

            var userDetails = new UserDetailsDto
            {
                Id = Guid.NewGuid(),
                Email = loginDto.Email,
                UserName = "TestUser",
                LastName = "User",
                Roles = new List<string> { "Admin" }
            };

            var authDto = new AuthenticationDto
            {
                Token = "sampletoken",
                ExpiresAt = DateTime.Now.AddMinutes(30),
                UserData = userDetails,
                UserRole = new List<string> { "Admin" }
            };

            _userServiceMock
                .Setup(s => s.LoginAsync(loginDto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(authDto);

            // Act
            var result = await _controller.LoginAsync(loginDto, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<ActionResult<AuthenticationDto>>(result);
            var returnedAuth = Assert.IsType<AuthenticationDto>(okResult.Value);

            Assert.Equal("sampletoken", returnedAuth.Token);
            Assert.Contains("Admin", returnedAuth.UserRole);

            _userServiceMock.Verify(s => s.LoginAsync(loginDto, It.IsAny<CancellationToken>()), Times.Once);
        }


    }
}
