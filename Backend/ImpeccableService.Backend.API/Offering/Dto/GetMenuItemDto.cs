using System.ComponentModel.DataAnnotations;
using ImpeccableService.Backend.API.Utility.Dto;

namespace ImpeccableService.Backend.API.Offering.Dto
{
    public class GetMenuItemDto
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public ImageDto Thumbnail { get; set; }
    }
}