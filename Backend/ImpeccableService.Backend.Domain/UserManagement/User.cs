using ImpeccableService.Backend.Domain.Utility;

namespace ImpeccableService.Backend.Domain.UserManagement
{
    public class User
    {
        public User(string id, string email, string passwordHash, string role, Image profileImage)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            ProfileImage = profileImage;
        }

        public string Id { get; }

        public string Email { get; }

        public string PasswordHash { get; }

        public string Role { get; }

        public Image ProfileImage { get; set; }
    }
}
