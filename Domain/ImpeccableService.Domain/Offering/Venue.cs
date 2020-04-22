using System.Collections.Generic;

namespace ImpeccableService.Domain.Offering
{
    public class Venue
    {
        public Venue(string id, IList<MenuPreview> menus)
        {
            Id = id;
            Menus = menus ?? new List<MenuPreview>();
        }

        public string Id { get; }

        public IList<MenuPreview> Menus { get; }
    }
}
