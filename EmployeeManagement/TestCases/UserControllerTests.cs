using Application.Dtos.Users;
using Application.Interfaces;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;

namespace TestCases
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _controller = new UserController(_userServiceMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnListOfUsers()
        {
            var users = new List<UserDetailsDto>
            {
                new UserDetailsDto{ Id= Guid.NewGuid(),Email="filane1@gmail.com", UserName="Filane", LastName="Fisteku", Roles = new List<string> { "Admin" }}
            };
            _userServiceMock.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
                 .ReturnsAsync(users);

            var result= await _controller.GetAllAsync(CancellationToken.None);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUser = Assert.IsType<List<UserDetailsDto>>(okResult.Value);
            Assert.Equal(1, returnedUser.Count);
            Assert.Equal("filane1@gmail.com", returnedUser[0].Email);

            _userServiceMock.Verify(user => user.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);

        }
        [Fact]
        public async Task GetByIdAsync_ReturnsUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new UserDetailsDto { Id = userId, Email = "filane1@gmail.com", UserName = "Filane", LastName = "Fisteku", Roles = new List<string> { "Admin" } };

            _userServiceMock
                .Setup(s => s.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            // Act
            var result = await _controller.GetByIdAsync(userId, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUser = Assert.IsType<UserDetailsDto>(okResult.Value);
            Assert.Equal(userId, returnedUser.Id);
            Assert.Equal("Filane", returnedUser.UserName);
            Assert.Equal("Admin", returnedUser.Roles[0]);

            _userServiceMock.Verify(s => s.GetByIdAsync(userId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ReturnsOkWithCreatedUser()
        {
            // Arrange
         
            var addedUser = new SaveUserDto { 
                Email = "filane1@gmail.com", 
                UserName = "Filane",
                LastName = "Fisteku", 
                Password="Admin123!", 
                ConfirmPassword = "Admin123!", 
                Roles = new List<Role> { Role.Admin } 
            };
            var createdUser = new UserDetailsDto { Id = Guid.NewGuid(), Email = addedUser.Email, UserName = addedUser.UserName, LastName = addedUser.LastName, Roles = new List<string> { "Admin" } };

            _userServiceMock
                .Setup(s => s.AddAsync(addedUser, It.IsAny<CancellationToken>()))
                .ReturnsAsync(createdUser);

            // Act
            var result = await _controller.AddAsync(addedUser, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUser = Assert.IsType<UserDetailsDto>(okResult.Value);
            Assert.Equal(addedUser.Email, returnedUser.Email);
            Assert.Equal(addedUser.UserName, returnedUser.UserName);
            Assert.Equal(addedUser.LastName, returnedUser.LastName);
            Assert.Contains("Admin", returnedUser.Roles);


            _userServiceMock.Verify(s => s.AddAsync(addedUser, It.IsAny<CancellationToken>()), Times.Once);
        }

    }
}
