namespace ImpeccableService.Client.Domain.UserManagement
{
    public class User
    {
        public User(string id, string email)
        {
            Id = id;
            Email = email;
        }

        public string Id { get; }

        public string Email { get; }
    }
}
