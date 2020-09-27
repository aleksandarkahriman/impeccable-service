using System.ComponentModel.DataAnnotations;
using ImpeccableService.Backend.API.UserManagement.Dto.Validation;

namespace ImpeccableService.Backend.API.UserManagement.Dto
{
    public class EmailRegistrationDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        public string Password { get; set; }
        
        [Required]
        [ValidRole]
        public string Role { get; set; }
    }
}
