using System.ComponentModel.DataAnnotations;

namespace ImpeccableService.Backend.API.Offering.Dto
{
    public class GetVenueDto
    {
        [Required]
        public string Id { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}