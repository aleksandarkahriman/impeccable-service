using ImpeccableService.Backend.Domain.Utility;

namespace ImpeccableService.Backend.Domain.Offering
{
    public class MenuItem
    {
        public MenuItem(string id, string name, Image thumbnail)
        {
            Id = id;
            Name = name;
            Thumbnail = thumbnail;
        }

        public string Id { get; }
        
        public string Name { get; }
        
        public Image Thumbnail { get; }
    }
}