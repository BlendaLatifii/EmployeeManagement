namespace Domain.Interface.Security
{
    public interface IAuthorizationManager
    {
        Guid? GetUserId();
        Guid? GetDepartmentId();
    }
}
