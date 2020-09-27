using System;
using System.Collections.Generic;
using ImpeccableService.Backend.Database;
using ImpeccableService.Backend.Database.Offering.Model;
using ImpeccableService.Backend.Domain.Utility;
using Newtonsoft.Json;

namespace ImpeccableService.Backend.API.Test.Environment.Data
{
    public static class OfferingGenerator
    {
        internal static void AddTestOfferings(this ApplicationDbContext context)
        {
            var venueEntity = new VenueEntity { Id = "4ccb", Name = "Corso"};

            context.Venues.Add(venueEntity);
            
            var menuEntity = new MenuEntity { Id = Guid.NewGuid().ToString(), VenueId = "4ccb" };

            var englishBreakfastId = Guid.NewGuid().ToString();
            var breakfastItems = new List<MenuItemEntity>
            {
                new MenuItemEntity { 
                    Id = englishBreakfastId,
                    Name = "English breakfast",
                    ThumbnailImageSerialized = JsonConvert.SerializeObject(new Image($"Offering/{menuEntity.VenueId}/{menuEntity.Id}/{englishBreakfastId}")) }
            };

            menuEntity.Sections = new List<MenuSectionEntity>
            {
                new MenuSectionEntity { Id = Guid.NewGuid().ToString(), Name = "Breakfast", Items = breakfastItems }
            };
            
            context.Menus.Add(menuEntity);
        }
    }
}