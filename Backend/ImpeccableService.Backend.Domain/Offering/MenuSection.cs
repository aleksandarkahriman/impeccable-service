using System.Collections.Generic;

namespace ImpeccableService.Backend.Domain.Offering
{
    public class MenuSection
    {
        public MenuSection(string id, string name, List<MenuItem> items)
        {
            Id = id;
            Name = name;
            Items = items;
        }

        public string Id { get; }
        
        public string Name { get; }
        
        public List<MenuItem> Items { get; }
    }
}