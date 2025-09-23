namespace Domain.Entities.Abstraction
{
    public interface IHasCreateUser
    {
        DateTime CreateDate { get; set; }
        Guid CreateUserId { get; set; }
        User CreateUser { get; set; }
    }
}
