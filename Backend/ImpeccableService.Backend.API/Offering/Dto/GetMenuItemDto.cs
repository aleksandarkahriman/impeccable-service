using System.ComponentModel.DataAnnotations;

namespace ImpeccableService.Backend.API.Offering.Dto
{
    public class GetMenuItemDto
    {
        [Required]
        public string Name { get; set; }
    }
}