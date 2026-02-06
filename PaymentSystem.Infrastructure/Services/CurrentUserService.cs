using Microsoft.AspNetCore.Http;
using PaymentSystem.Application.Common.Interfaces;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId =>
        Guid.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirst("uid")?.Value, out var id)
            ? id
            : null;

    public string? Email =>
        _httpContextAccessor.HttpContext?.User?.Identity?.Name;
}
