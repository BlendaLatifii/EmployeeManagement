using Application.Dtos.Department;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;

namespace TestCases
{
    public class DepartmentControllerTests
    {
        private readonly Mock<IDepartmentService> _mockService;
        private readonly DepartmentController _controller;

        public DepartmentControllerTests()
        {
            _mockService = new Mock<IDepartmentService>();
            _controller = new DepartmentController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsListOfDepartments()
         {
            var departments = new List<DepartmentDetailDto>
            {
                new DepartmentDetailDto { Name = "HR",Description = "Some Description" },
                new DepartmentDetailDto { Name = "IT", Description = "Some Description" }
            };

            _mockService.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
                        .ReturnsAsync(departments);

            var result = await _controller.GetAllAsync(CancellationToken.None);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedDepartments = Assert.IsType<List<DepartmentDetailDto>>(okResult.Value);
            Assert.Equal(2, returnedDepartments.Count);
            Assert.Equal("HR", returnedDepartments[0].Name);

            _mockService.Verify(s => s.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsDepartment()
        {
             var id = Guid.NewGuid();
            var department = new DepartmentDetailDto { Id= id, Name = "Finance",Description = "some data" };
            _mockService.Setup(s => s.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(department);

            var result = await _controller.GetByIdAsync(id, CancellationToken.None);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var returnedDepartment = Assert.IsType<DepartmentDetailDto>(okResult.Value);
            Assert.Equal(id, returnedDepartment.Id);
            Assert.Equal("Finance", returnedDepartment.Name);
            _mockService.Verify(s => s.GetByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ReturnsCreatedDepartment()
        {
            // Arrange
            var addDto = new AddDepartmentDto { Name = "Marketing", Description = "some data" };
            var createdDepartment = new DepartmentDetailDto {   Name = "Marketing", Description = "some data" };
            _mockService.Setup(s => s.AddAsync(addDto, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(createdDepartment);

            // Act
            var result = await _controller.AddAsync(addDto, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedDepartment = Assert.IsType<DepartmentDetailDto>(okResult.Value);
            Assert.Equal("Marketing", returnedDepartment.Name);
            Assert.Equal("some data", returnedDepartment.Description);
        }
        [Fact]
        public async Task UpdateDepartment_ReturnsUpdatedDepartment()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var updateModel = new UpdateDepartmentDto
            {  
                Id = departmentId, 
                Name = "Updated Department",
                Description = "Updated Description"
            };

            var updatedModel = new DepartmentDetailDto
            {
                Id = departmentId,
                Name = "Updated Department",
                Description = "Updated Description"
            };

            _mockService.Setup(s => s.UpdateAsync(updateModel, It.IsAny<CancellationToken>()))
                .ReturnsAsync(updatedModel);

            // Act
            var result = await _controller.UpdateAsync(updateModel, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedDepartment = Assert.IsType<DepartmentDetailDto>(okResult.Value);

            Assert.Equal("Updated Department", returnedDepartment.Name);
            Assert.Equal("Updated Description", returnedDepartment.Description);

            _mockService.Verify(s => s.UpdateAsync(updateModel, It.IsAny<CancellationToken>()), Times.Once);
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
