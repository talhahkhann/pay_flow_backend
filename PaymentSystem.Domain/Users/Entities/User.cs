using PaymentSystem.Domain.Event;
using PaymentSystem.Domain.Users.Events;

namespace PaymentSystem.Domain.Users
{
    public class User
    {
        private readonly List<IDomainEvent> _domainevents = new();
        public Guid Id { get; private set; }
        public string FullName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public bool EmailConfirmed { get; private set; }
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainevents.AsReadOnly();

        private User() { } //EF
        public User(Guid id, string fullName, string email, bool emailconfirmed)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            EmailConfirmed = emailconfirmed;
        }
        public void ConfirmEmail()
        {
        if (EmailConfirmed)
            return; // Already confirmed

         EmailConfirmed = true;
        _domainevents.Add(new UserEmailConfirmedEvent(Id, Email));
        }
    }
}
