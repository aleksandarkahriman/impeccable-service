using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImpeccableService.Backend.Database.Offering.Model
{
    [Table("venue")]
    internal class VenueEntity
    {
        [Key]
        public string Id { get; set; }
        
        public string Name { get; set; }
    }
}