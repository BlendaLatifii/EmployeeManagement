namespace Application.Dtos.Employee
{
    public class EmployeeDto
    {
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public DateTime DateOfJoining { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
