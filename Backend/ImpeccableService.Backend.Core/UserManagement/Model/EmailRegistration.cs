namespace ImpeccableService.Backend.Core.UserManagement.Model
{
    public class EmailRegistration
    {
        public EmailRegistration(string email, string password, string role)
        {
            Email = email;
            Password = password;
            Role = role;
        }

        public string Email { get; }

        public string Password { get; }
        
        public string Role { get; }
    }
}
