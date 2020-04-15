using ImpeccableService.Domain.UserManagement;

namespace ImpeccableService.Client.Core.UserManagement.Model
{
    public class AuthenticatedUser : User
    {
        public AuthenticatedUser(int id, SecurityCredentials securityCredentials) : base(id)
        {
            SecurityCredentials = securityCredentials;
        }

        public SecurityCredentials SecurityCredentials { get; }
    }
}
