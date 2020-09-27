using System.ComponentModel.DataAnnotations;

namespace ImpeccableService.Backend.API.Offering.Dto
{
    public class PostMenuSectionDto
    {
        [Required]
        public string Name { get; set; }
    }
}