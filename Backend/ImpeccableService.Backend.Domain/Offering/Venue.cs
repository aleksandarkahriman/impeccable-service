namespace ImpeccableService.Backend.Domain.Offering
{
    public class Venue
    {
        public Venue(string id, string name, string companyId)
        {
            Id = id;
            Name = name;
            CompanyId = companyId;
        }

        public string Id { get; }
        
        public string Name { get; }
        
        public string CompanyId { get; }
    }
}