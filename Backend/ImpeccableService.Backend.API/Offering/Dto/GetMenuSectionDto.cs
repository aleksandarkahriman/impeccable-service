using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImpeccableService.Backend.API.Offering.Dto
{
    public class GetMenuSectionDto
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public List<GetMenuItemDto> Items { get; set; }
    }
}