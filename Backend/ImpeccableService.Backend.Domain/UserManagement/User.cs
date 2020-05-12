namespace ImpeccableService.Backend.Domain.UserManagement
{
    public class User
    {
        public User(string email, string passwordHash)
            : this(0, email, passwordHash, UserRole.Consumer)
        {
        }

        public User(int id, string email, string passwordHash, string role)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
        }

        public int Id { get; }

        public string Email { get; }

        public string PasswordHash { get; }

        public string Role { get; }
    }
}
