namespace Domain.Entities.Abstraction
{
    public interface IHasUpdateUser
    {
        DateTime? UpdateDate { get; set; }
        Guid? UpdateUserId { get; set; }
        User? UpdateUser { get; set; }
    }
}
