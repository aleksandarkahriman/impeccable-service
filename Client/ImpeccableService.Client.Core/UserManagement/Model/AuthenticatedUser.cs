using ImpeccableService.Client.Domain.UserManagement;

namespace ImpeccableService.Client.Core.UserManagement.Model
{
    public class AuthenticatedUser : User
    {
        public AuthenticatedUser(string id, string email, SecurityCredentials securityCredentials) : base(id, email)
        {
            SecurityCredentials = securityCredentials;
        }

        public SecurityCredentials SecurityCredentials { get; }
    }
}
