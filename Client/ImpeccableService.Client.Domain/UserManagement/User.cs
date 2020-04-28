namespace ImpeccableService.Client.Domain.UserManagement
{
    public class User
    {
        public User(int id, string email)
        {
            Id = id;
            Email = email;
        }

        public int Id { get; }

        public string Email { get; }
    }
}
