namespace ImpeccableService.Domain.UserManagement
{
    public class EmailRegistration
    {
        public EmailRegistration(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }

        public string Password { get; }
    }
}
