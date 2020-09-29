using System.Collections.Generic;

namespace ImpeccableService.Backend.Domain.Offering
{
    public class Menu
    {
        public Menu(string id, string companyId, List<MenuSection> sections)
        {
            Id = id;
            Sections = sections;
            CompanyId = companyId;
        }

        public string Id { get; }
        
        public string CompanyId { get; }
        
        public List<MenuSection> Sections { get; }
    }
}