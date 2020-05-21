using ImpeccableService.Backend.Domain.Utility;

namespace ImpeccableService.Backend.Core.UserManagement.Model
{
    public class DefaultUserProfileImage : Image
    {
        private const string DefaultUserImagePath = "Default/ProfileImage.png";

        public DefaultUserProfileImage() : base(DefaultUserImagePath)
        {
        }
    }
}
