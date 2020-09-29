namespace ImpeccableService.Backend.Core.Context
{
    public class Identity
    {
        public Identity(string id, string email, string companyOwnership)
        {
            Id = id;
            Email = email;
            CompanyOwnership = companyOwnership;
        }

        public string Id { get; }

        public string Email { get; }
        
        public string CompanyOwnership { get; }
    }
}
