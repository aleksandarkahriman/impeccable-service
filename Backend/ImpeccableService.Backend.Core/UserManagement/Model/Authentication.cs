using ImpeccableService.Domain.UserManagement;

namespace ImpeccableService.Backend.Core.UserManagement.Model
{
    public class Authentication
    {
        public Authentication(User user, SecurityCredentials securityCredentials)
        {
            User = user;
            SecurityCredentials = securityCredentials;
        }

        public User User { get; }

        public SecurityCredentials SecurityCredentials { get; }
    }
}
