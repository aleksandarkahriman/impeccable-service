using System.ComponentModel.DataAnnotations;

namespace ImpeccableService.Backend.API.Offering.Dto
{
    public class PostVenueDto
    {
        [Required]
        public string Name { get; set; }
    }
}