namespace ImpeccableService.Domain.UserManagement
{
    public class User
    {
        public User(string email, string passwordHash)
            : this(0, email, passwordHash)
        {
        }

        public User(int id, string email, string passwordHash)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
        }

        public int Id { get; }

        public string Email { get; }

        public string PasswordHash { get; }
    }
}
