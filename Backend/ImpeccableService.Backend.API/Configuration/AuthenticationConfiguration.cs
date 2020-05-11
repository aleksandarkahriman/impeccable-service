namespace ImpeccableService.Backend.API.Configuration
{
    public class AuthenticationConfiguration
    {
        public string PasswordHashSalt { get; set; }

        public string Secret { get; set; }

        public string Issuer { get; set; }
    }
}
