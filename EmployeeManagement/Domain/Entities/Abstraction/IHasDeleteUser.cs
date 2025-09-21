namespace Domain.Entities.Abstraction
{
    public interface IHasDeleteUser
    {
        DateTime? DeleteDate { get; set; }
        Guid? DeletedUserId { get; set; }
        User? DeletedUser { get; set; }
    }
}
