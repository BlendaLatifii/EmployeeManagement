namespace Domain.Entities.Abstraction
{
    public class SoftDeletableEntity : BaseEntity, IHasDeleteUser
    {
        public DateTime? DeleteDate { get; set; }
        public Guid? DeletedUserId { get; set; }
        public User? DeletedUser { get; set; }
    }
}
