using System;
using System.Threading.Tasks;
using ImpeccableService.Client.Domain.Offering;
using Utility.Application.ResultContract;

namespace ImpeccableService.Client.Core.Offering.Dependency.Placeholder
{
    internal class VenueRemoteRepositoryPlaceholder : IVenueRemoteRepository
    {
        public Task<ResultWithData<Venue>> GetVenueById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
