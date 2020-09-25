namespace ImpeccableService.Backend.Domain.Offering
{
    public class MenuItem
    {
        public MenuItem(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; }
        
        public string Name { get; }
    }
}