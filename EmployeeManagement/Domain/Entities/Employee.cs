using Domain.Entities.Abstraction;

namespace Domain.Entities
{
    public class Employee : SoftDeletableEntity
    {
        public Guid Id { get; set; }
        public DateTime DateOfJoining { get; set; }
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
