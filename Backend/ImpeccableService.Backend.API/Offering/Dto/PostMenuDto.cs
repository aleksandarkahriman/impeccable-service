using System.ComponentModel.DataAnnotations;

namespace ImpeccableService.Backend.API.Offering.Dto
{
    public class PostMenuDto
    {
        [Required]
        public string Name { get; set; }
    }
}