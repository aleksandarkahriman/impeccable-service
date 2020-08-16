using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImpeccableService.Backend.Database.Offering.Model
{
    [Table("menu")]
    internal class MenuEntity
    {
        [Key]
        public string Id { get; set; }
        
        public string VenueId { get; set; }
    }
}