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

            menuEntity.Sections =new List<SectionEntity>
            {
                new SectionEntity { Id = Guid.NewGuid().ToString(), Menu = menuEntity, MenuId = menuEntity.Id, Name = "Breakfast" }
            };
            
            context.Menus.Add(menuEntity);
        }
    }
}