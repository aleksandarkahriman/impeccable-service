namespace ImpeccableService.Client.Domain.UserManagement
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
