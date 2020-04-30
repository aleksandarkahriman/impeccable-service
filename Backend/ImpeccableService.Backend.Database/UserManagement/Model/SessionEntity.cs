using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ImpeccableService.Backend.Core.UserManagement.Model;

namespace ImpeccableService.Backend.Database.UserManagement.Model
{
    [Table("session")]
    internal class SessionEntity
    {
        public SessionEntity()
        {
        }

        public SessionEntity(Authentication authentication)
        {
            Id = Guid.NewGuid().ToString();
            UserId = authentication.User.Id;
            AccessToken = authentication.SecurityCredentials.AccessToken;
            RefreshToken = authentication.SecurityCredentials.RefreshToken;
            LogoutToken = authentication.SecurityCredentials.LogoutToken;
        }

        [Key]
        public string Id { get; set; }

        public int UserId { get; set; }

        public UserEntity User { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public string LogoutToken { get; set; }
    }
}
