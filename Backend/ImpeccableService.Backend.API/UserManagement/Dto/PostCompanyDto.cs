using System.ComponentModel.DataAnnotations;

namespace ImpeccableService.Backend.API.UserManagement.Dto
{
    public class PostCompanyDto
    {
        [Required]
        public string Name { get; set; }
    }
}