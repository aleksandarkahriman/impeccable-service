using System;
using System.Threading.Tasks;
using ImpeccableService.Domain.Offering;
using Utility.Application.ResultContract;

namespace ImpeccableService.Client.Core.Offering.Dependency.Placeholder
{
    internal class MenuRemoteRepositoryPlaceholder : IMenuRemoteRepository
    {
        public Task<ResultWithData<Menu>> GetMenuById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
