using ImpeccableService.Backend.Domain.Utility;

namespace ImpeccableService.Backend.Domain.UserManagement
{
    public class User
    {
        public User(string email, string passwordHash, Image profileImage)
            : this(0, email, passwordHash, UserRole.Consumer, profileImage)
        {
        }

        public User(int id, string email, string passwordHash, string role, Image profileImage)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            ProfileImage = profileImage;
        }

        public int Id { get; }

        public string Email { get; }

        public string PasswordHash { get; }

        public string Role { get; }

        public Image ProfileImage { get; set; }
    }
}
