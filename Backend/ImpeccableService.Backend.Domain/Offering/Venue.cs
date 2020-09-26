namespace ImpeccableService.Backend.Domain.Offering
{
    public class Venue
    {
        public Venue(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; }
        
        public string Name { get; }
    }
}