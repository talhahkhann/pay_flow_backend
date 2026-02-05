using PaymentSystem.Domain.Payments;

public class Payment : BaseAuditableEntity
{
    public Guid UserId { get; private set; }
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }
    public PaymentStatus Status { get; private set; }
    public string TransactionReference { get; private set; }

    private Payment() { } // For EF Core

    public Payment(Guid userId, decimal amount, string currency)
    {
        UserId = userId;
        Amount = amount;
        Currency = currency;
        Status = PaymentStatus.Pending;
        TransactionReference = $"TXN-{DateTime.UtcNow.Ticks}";

        AddDomainEvent(new PaymentCreatedEvent(Id, userId, amount));
    }

    public void MarkAsProcessing()
    {
        if (Status != PaymentStatus.Pending)
            throw new InvalidOperationException("Invalid state transition");

        Status = PaymentStatus.Processing;
    }

    public void MarkAsCompleted()
    {
        if (Status != PaymentStatus.Processing)
            throw new InvalidOperationException("Cannot complete payment in current state");

        Status = PaymentStatus.Completed;
        AddDomainEvent(new PaymentCompletedEvent(Id, Amount));
    }

    public void MarkAsFailed()
    {
        if (Status == PaymentStatus.Completed)
            throw new InvalidOperationException("Cannot fail a completed payment");

        Status = PaymentStatus.Failed;
    }
}
