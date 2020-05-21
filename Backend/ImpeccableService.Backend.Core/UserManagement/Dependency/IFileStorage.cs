using ImpeccableService.Backend.Domain.Utility;

namespace ImpeccableService.Backend.Core.UserManagement.Dependency
{
    public interface IFileStorage
    {
        string Sign<T>(T file) where T : File;
    }
}
