using System.ComponentModel.DataAnnotations;
using ImpeccableService.Backend.API.Utility.Dto;

namespace ImpeccableService.Backend.API.UserManagement.Dto
{
    public class GetUserProfileDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public ImageDto ProfileImage { get; set; }
    }
}
