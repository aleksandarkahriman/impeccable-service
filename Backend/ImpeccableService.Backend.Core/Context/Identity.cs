namespace ImpeccableService.Backend.Core.Context
{
    public class Identity
    {
        public Identity(int id, string email)
        {
            Id = id;
            Email = email;
        }

        public int Id { get; }

        public string Email { get; }
    }
}
