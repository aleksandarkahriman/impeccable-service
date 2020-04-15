namespace ImpeccableService.Domain.UserManagement
{
    public class SecurityCredentials
    {
        public SecurityCredentials(string accessToken, string refreshToken, string logoutToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            LogoutToken = logoutToken;
        }

        public string AccessToken { get; }

        public string RefreshToken { get; }

        public string LogoutToken { get; }
    }
}