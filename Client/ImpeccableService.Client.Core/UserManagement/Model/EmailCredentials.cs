namespace ImpeccableService.Client.Core.UserManagement.Model
{
    public class EmailCredentials
    {
        public EmailCredentials(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }

        public string Password { get; }
    }
}
