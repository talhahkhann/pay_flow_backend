namespace PaymentSystem.Domain.Users
{
    public class User
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;

        private User() { } //EF
        public User(Guid id ,string fullName,string email)
        {
            Id = id;
            FullName = fullName;
            Email = email;
        }
    }
}
