using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImpeccableService.Backend.API.Offering.Dto
{
    public class GetMenuDto
    {
        [Required]
        public string Id { get; set; }
        
        [Required]
        public List<GetMenuSectionDto> Sections { get; set; } 
    }
}