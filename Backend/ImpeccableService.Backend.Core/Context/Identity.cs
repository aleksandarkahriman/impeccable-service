namespace ImpeccableService.Backend.Core.Context
{
    public class Identity
    {
        public Identity(string id, string email)
        {
            Id = id;
            Email = email;
        }

        public string Id { get; }

        public string Email { get; }
    }
}
