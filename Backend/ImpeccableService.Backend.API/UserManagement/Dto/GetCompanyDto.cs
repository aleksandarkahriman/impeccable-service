using System.ComponentModel.DataAnnotations;

namespace ImpeccableService.Backend.API.UserManagement.Dto
{
    public class GetCompanyDto
    {
        [Required]
        public string Name { get; set; }
    }
}