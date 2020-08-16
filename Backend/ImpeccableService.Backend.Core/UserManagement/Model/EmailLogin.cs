namespace ImpeccableService.Backend.Core.UserManagement.Model
{
    public class EmailLogin
    {
        public EmailLogin(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }

        public string Password { get; }
    }
}
