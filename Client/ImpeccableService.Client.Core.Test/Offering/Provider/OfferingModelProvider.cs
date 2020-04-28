using System.Collections.Generic;
using ImpeccableService.Client.Domain.Offering;

namespace ImpeccableService.Client.Core.Test.Offering.Provider
{
    public class OfferingModelProvider
    {
        public static Venue ConstructTestVenue()
        {
            var menus = new List<MenuPreview>
            {
                new MenuPreview("morningMenuId"),
                new MenuPreview("eveningMenuId")
            };

            return new Venue("venueId", menus);
        }

        public static Menu ConstructTestMenu()
        {
            return new Menu("eveningMenuId");
        }
    }
}
