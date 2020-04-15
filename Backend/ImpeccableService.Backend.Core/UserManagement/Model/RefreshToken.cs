namespace ImpeccableService.Backend.Core.UserManagement.Model
{
    public class RefreshToken
    {
        public RefreshToken(string token)
        {
            Token = token;
        }

        public string Token { get; }
    }
}
