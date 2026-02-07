namespace PaymentSystem.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
         Guid? UserId { get; }
         string? Email { get; }
         IList<string> Roles { get; }
         bool IsAuthenticated { get; }
    }
}
