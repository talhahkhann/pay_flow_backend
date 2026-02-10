namespace PaymentSystem.Domain.Users
{
    public class User
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public bool EmailConfirmed { get; private set; }

        private User() { } //EF
        public User(Guid id ,string fullName,string email,bool emailconfirmed)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            EmailConfirmed = emailconfirmed;
        }
    }
}
