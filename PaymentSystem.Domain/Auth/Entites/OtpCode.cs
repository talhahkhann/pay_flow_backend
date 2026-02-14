namespace PaymentSystem.Domain.Auth;

public class OtpCode : BaseAuditableEntity
{
    public Guid UserId { get; private set; }
    public string Code { get; private set; } = default!;
    public DateTime ExpiresAt { get; private set; }
    public bool IsUsed { get; private set; }
    public int FailedAttempts { get; private set; }
    public int MaxAttempts { get; private set; } = 5;

    private OtpCode() { }

    public OtpCode(Guid userId, string code, int expiryMinutes)
    {
        UserId = userId;
        Code = code;
        ExpiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes);
        IsUsed = false;
    }
    public void RegisterFailedAttempt()
    {
        FailedAttempts++;
    }
    public void MarkAsUsed()
    {
        IsUsed = true;
    }

    public bool IsExpired() => DateTime.UtcNow > ExpiresAt;
    public bool IsLocked() => FailedAttempts >= MaxAttempts;
}
