namespace ImpeccableService.Backend.API.UserManagement.Dto
{
    public class AuthenticationCredentialsDto
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public string LogoutToken { get; set; }
    }
}
