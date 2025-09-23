using Application.Dtos.Employee;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;

namespace TestCases
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeService> _mockService;
        private readonly EmployeeController _controller;

        public EmployeeControllerTests()
        {
            _mockService = new Mock<IEmployeeService>();
            _controller = new EmployeeController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsListOfEmployees()
        { 
            var employees = new List<EmployeeDetailDto>
            {
                new EmployeeDetailDto { Name = "Filane",LastName="Fisteku", Email="fisteku@gmail.com", DateOfJoining = DateTime.ParseExact("12/12/2022", "dd/MM/yyyy", null),DepartmentId = Guid.NewGuid()  },
                new EmployeeDetailDto { Name = "Filan",LastName="Fisteku", Email="fisteku2@gmail.com", DateOfJoining = DateTime.ParseExact("12/12/2022", "dd/MM/yyyy", null),DepartmentId = Guid.NewGuid()  }
            };

            _mockService.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
                        .ReturnsAsync(employees);

            var result = await _controller.GetAllAsync(CancellationToken.None);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmployees = Assert.IsType<List<EmployeeDetailDto>>(okResult.Value);
            Assert.Equal(2, returnedEmployees.Count);
            Assert.Equal("Filane", returnedEmployees[0].Name);
            Assert.Equal("fisteku2@gmail.com", returnedEmployees[1].Email);

            _mockService.Verify(s => s.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsEmployee()
        {
            var id = Guid.NewGuid();
            var employee = new EmployeeDetailDto {Id=id, Name = "Filane", LastName = "Fisteku", Email = "fisteku@gmail.com", DateOfJoining = DateTime.ParseExact("12/12/2022", "dd/MM/yyyy", null), DepartmentId = Guid.NewGuid() };
            _mockService.Setup(s => s.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(employee);

            var result = await _controller.GetByIdAsync(id, CancellationToken.None);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmployee = Assert.IsType<EmployeeDetailDto>(okResult.Value);
            Assert.Equal(id, returnedEmployee.Id);
            Assert.Equal("Filane", returnedEmployee.Name);
            _mockService.Verify(s => s.GetByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ReturnsCreatedEmployee()
        {
            // Arrange
            Guid departmentId = Guid.NewGuid();
            var addDto = new AddEmployeeDto { Name = "Filane", LastName = "Fisteku", Email = "fisteku@gmail.com", DateOfJoining = DateTime.ParseExact("12/12/2022", "dd/MM/yyyy", null), DepartmentId = departmentId };
            var createdDepartment = new EmployeeDetailDto { Name = "Filane", LastName = "Fisteku", Email = "fisteku@gmail.com", DateOfJoining = DateTime.ParseExact("12/12/2022", "dd/MM/yyyy", null) , DepartmentId = departmentId };
            _mockService.Setup(s => s.AddAsync(addDto, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(createdDepartment);

            // Act
            var result = await _controller.AddAsync(addDto, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmployee = Assert.IsType<EmployeeDetailDto>(okResult.Value);
            Assert.Equal("Filane", returnedEmployee.Name);
            Assert.Equal("Fisteku", returnedEmployee.LastName);
            Assert.Equal("fisteku@gmail.com", returnedEmployee.Email);
            Assert.Equal(DateTime.ParseExact("12/12/2022", "dd/MM/yyyy", null), returnedEmployee.DateOfJoining);
            Assert.Equal(departmentId, returnedEmployee.DepartmentId);

        }
        [Fact]
        public async Task UpdateEmployee_ReturnsUpdatedEmployee()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var updateModel = new EmployeeDetailDto
            {
                Id = employeeId,
                Name = "UpdatedName",
                LastName = "UpdatedLastName",
                Email = "updated@example.com",
                PhoneNumber = "123456789",
                DateOfJoining = DateTime.Now,
                DepartmentId = Guid.NewGuid()
            };

            _mockService.Setup(s => s.UpdateAsync(updateModel, It.IsAny<CancellationToken>()))
                .ReturnsAsync(updateModel);

            // Act
            var result = await _controller.UpdateAsync(updateModel, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmployee = Assert.IsType<EmployeeDetailDto>(okResult.Value);

            Assert.Equal("UpdatedName", returnedEmployee.Name);
            Assert.Equal("UpdatedLastName", returnedEmployee.LastName);
            Assert.Equal("updated@example.com", returnedEmployee.Email);
            Assert.Equal("123456789", returnedEmployee.PhoneNumber);

            _mockService.Verify(s => s.UpdateAsync(updateModel, It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async Task FilterEmployeeByDepartment()
        {
            Guid departmentId = Guid.NewGuid();
            var employees = new List<EmployeeDetailDto>
            {
                new EmployeeDetailDto { Name = "Filane",LastName="Fisteku", Email="fisteku@gmail.com", DateOfJoining = DateTime.ParseExact("12/12/2022", "dd/MM/yyyy", null),DepartmentId= departmentId  },
                new EmployeeDetailDto { Name = "Filan",LastName="Fisteku", Email="fisteku2@gmail.com", DateOfJoining = DateTime.ParseExact("12/12/2022", "dd/MM/yyyy", null),DepartmentId = Guid.NewGuid()  }
            };
            _mockService.Setup(s => s.FilterEmployeeByDepartmentAsync(departmentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(employees.Where(e => e.DepartmentId == departmentId).ToList());

            var result = await _controller.FilterEmployeeByDepartmentAsync(departmentId, CancellationToken.None);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmployees = Assert.IsType<List<EmployeeDetailDto>>(okResult.Value);
            Assert.Equal(1, returnedEmployees.Count);

            _mockService.Verify(s => s.FilterEmployeeByDepartmentAsync(departmentId, It.IsAny<CancellationToken>()), Times.Once);

        }

        [Fact]
        public async Task DeleteAsync_CallsServiceOnce()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await _controller.DeleteAsync(id, CancellationToken.None);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockService.Verify(s => s.DeleteAsync(id, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
