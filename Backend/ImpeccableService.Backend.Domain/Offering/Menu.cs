using System.Collections.Generic;

namespace ImpeccableService.Backend.Domain.Offering
{
    public class Menu
    {
        public Menu(string id, List<MenuSection> sections)
        {
            Id = id;
            Sections = sections;
        }

        public string Id { get; }
        
        public List<MenuSection> Sections { get; }
    }
}