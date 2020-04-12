namespace ImpeccableService.Domain.UserManagement
{
    public class User
    {
        public User(int id, SecurityCredentials securityCredentials)
        {
            Id = id;
            SecurityCredentials = securityCredentials;
        }

        public int Id { get; }

        public SecurityCredentials SecurityCredentials { get; }
    }
}
