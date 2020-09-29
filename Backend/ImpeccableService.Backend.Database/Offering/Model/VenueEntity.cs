using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ImpeccableService.Backend.Database.UserManagement.Model;

namespace ImpeccableService.Backend.Database.Offering.Model
{
    [Table("venue")]
    internal class VenueEntity
    {
        [Key]
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        [Required]
        public string CompanyId { get; set; }
        
        public CompanyEntity Company { get; set; }
    }
}