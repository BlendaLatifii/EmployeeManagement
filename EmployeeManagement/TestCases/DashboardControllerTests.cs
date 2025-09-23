using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;

namespace TestCases
{
    public class DashboardControllerTests
    {
        private readonly Mock<IDepartmentService> _mockDepartmentService;
        private readonly Mock<IEmployeeService> _mockEmployeeService;
        private readonly DashboardController _controller;

        public DashboardControllerTests()
        {
            _mockDepartmentService = new Mock<IDepartmentService>();
            _mockEmployeeService = new Mock<IEmployeeService>();

            _controller = new DashboardController(
                _mockDepartmentService.Object,
                _mockEmployeeService.Object,
                null!);
        }

        [Fact]
        public async Task GetNumberOfDepartmentsAsync_ReturnsOkWithValue()
        {
            _mockDepartmentService.Setup(s => s.GetNumberOfDepartmentsAsync(It.IsAny<CancellationToken>()))
                                  .ReturnsAsync(5);

            var result = await _controller.GetNumberOfDepartmentsAsync(CancellationToken.None);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(5, okResult.Value);
            _mockDepartmentService.Verify(department => department.GetNumberOfDepartmentsAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CountAllEmployeesAsync_ReturnsOkWithValue()
        {
            _mockEmployeeService.Setup(s => s.CountAllEmployeesAsync(It.IsAny<CancellationToken>()))
                                .ReturnsAsync(10);

            var result = await _controller.CountAllEmployeesAsync(CancellationToken.None);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(10, okResult.Value);
            _mockEmployeeService.Verify(employee => employee.CountAllEmployeesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CountEmployeesInDepartmentAsync_ReturnsOkWithValue()
        {
            var departmentName = "IT";
            _mockEmployeeService.Setup(employee => employee.CountEmployeesInDepartmentAsync(departmentName, It.IsAny<CancellationToken>()))
                                .ReturnsAsync(7);

            var result = await _controller.CountEmployeesInDepartmentAsync(departmentName, CancellationToken.None);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(7, okResult.Value);
            _mockEmployeeService.Verify(s => s.CountEmployeesInDepartmentAsync(departmentName, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetEmployeeCountByJoiningDateAsync_ReturnsOkWithValue()
        {
            int daysAgo = 30;
            _mockEmployeeService.Setup(employee => employee.GetEmployeeCountByJoiningDateAsync(daysAgo, It.IsAny<CancellationToken>()))
                                .ReturnsAsync(3);

            var result = await _controller.GetEmployeeCountByJoiningDateAsync(daysAgo, CancellationToken.None);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(3, okResult.Value);
            _mockEmployeeService.Verify(employee => employee.GetEmployeeCountByJoiningDateAsync(daysAgo, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}