using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ImpeccableService.Backend.Domain.Utility;
using Newtonsoft.Json;

namespace ImpeccableService.Backend.Database.UserManagement.Model
{
    [Table("user")]
    internal class UserEntity
    {
        [Key]
        public string Id { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Role { get; set; }

        public string ProfileImageSerialized { get; set; }

        [NotMapped]
        public Image ProfileImage
        {
            get => JsonConvert.DeserializeObject<Image>(ProfileImageSerialized);
            set => ProfileImageSerialized = JsonConvert.SerializeObject(value);
        }

        public List<SessionEntity> Sessions { get; set; }
    }
}
