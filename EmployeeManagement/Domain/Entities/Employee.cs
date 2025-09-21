using Domain.Entities.Abstraction;

namespace Domain.Entities
{
    public class Employee : SoftDeletableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public DateTime DateOfJoining { get; set; }
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
    }
}
