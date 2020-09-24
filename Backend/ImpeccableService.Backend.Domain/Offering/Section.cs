namespace ImpeccableService.Backend.Domain.Offering
{
    public class Section
    {
        public Section(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; }
        
        public string Name { get; }
    }
}