using System;
using System.Collections.Generic;
using ImpeccableService.Backend.Database;
using ImpeccableService.Backend.Database.Offering.Model;

namespace ImpeccableService.Backend.API.Test.Environment.Data
{
    public static class OfferingGenerator
    {
        internal static void AddTestOfferings(this ApplicationDbContext context)
        {
            var menuEntity = new MenuEntity { Id = Guid.NewGuid().ToString(), VenueId = "4ccb" };
            
            var breakfastItems = new List<MenuItemEntity>
            {
                new MenuItemEntity { Id = Guid.NewGuid().ToString(), Name = "Omelette" }
            };

            menuEntity.Sections = new List<MenuSectionEntity>
            {
                new MenuSectionEntity { Id = Guid.NewGuid().ToString(), Name = "Breakfast", Items = breakfastItems },
                new MenuSectionEntity { Id = Guid.NewGuid().ToString(), Name = "Pizza" },
                new MenuSectionEntity { Id = Guid.NewGuid().ToString(), Name = "Steak" },
                new MenuSectionEntity { Id = Guid.NewGuid().ToString(), Name = "Desert" }
            };
            
            context.Menus.Add(menuEntity);
        }
    }
}