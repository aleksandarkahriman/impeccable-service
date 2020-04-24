using System.ComponentModel.DataAnnotations;

namespace ImpeccableService.Backend.API.UserManagement.Dto
{
    public class EmailRegistrationDto
    {
        public EmailRegistrationDto()
        {
        }

        public EmailRegistrationDto(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        public string Password { get; set; }
    }
}
