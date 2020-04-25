using ImpeccableService.Domain.UserManagement;

namespace ImpeccableService.Client.Core.UserManagement.Model
{
    public class AuthenticatedUser : User
    {
        public AuthenticatedUser(int id, string email, SecurityCredentials securityCredentials) : base(id, email, string.Empty)
        {
            SecurityCredentials = securityCredentials;
        }

        public SecurityCredentials SecurityCredentials { get; }
    }
}
