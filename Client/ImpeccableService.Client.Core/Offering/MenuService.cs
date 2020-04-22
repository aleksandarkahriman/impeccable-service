using System.Threading.Tasks;
using ImpeccableService.Client.Core.Offering.Dependency;
using ImpeccableService.Domain.Offering;
using Utility.Application.ResultContract;

namespace ImpeccableService.Client.Core.Offering
{
    public class MenuService
    {
        private readonly IMenuRemoteRepository _menuRemoteRepository;

        public MenuService(IMenuRemoteRepository menuRemoteRepository)
        {
            _menuRemoteRepository = menuRemoteRepository;
        }

        public Task<ResultWithData<Menu>> GetMenuById(string id) => _menuRemoteRepository.GetMenuById(id);
    }
}
