using System.Threading.Tasks;

namespace ImpeccableService.Backend.Core.UserManagement.Dependency
{
    public interface ISecurityEnvironmentVariables
    {
        string PasswordHashSalt();

        Task<string> SecurityCredentialsSecret();
    }
}
