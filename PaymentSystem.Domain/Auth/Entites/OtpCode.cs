namespace PaymentSystem.Domain.Auth;

public class OtpCode : BaseAuditableEntity
{
    public Guid UserId { get; private set; }
    public string Code { get; private set; } = default!;
    public DateTime ExpiresAt { get; private set; }
    public bool IsUsed { get; private set; }

    private OtpCode() { }

    public OtpCode(Guid userId, string code, int expiryMinutes)
    {
        UserId = userId;
        Code = code;
        ExpiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes);
        IsUsed = false;
    }

    public void MarkAsUsed()
    {
        IsUsed = true;
    }

    public bool IsExpired() => DateTime.UtcNow > ExpiresAt;
}
