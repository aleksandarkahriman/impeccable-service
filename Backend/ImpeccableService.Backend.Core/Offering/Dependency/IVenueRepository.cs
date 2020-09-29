using System.Collections.Generic;
using System.Threading.Tasks;
using ImpeccableService.Backend.Domain.Offering;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Core.Offering.Dependency
{
    public interface IVenueRepository
    {
        Task<ResultWithData<List<Venue>>> Read();

        Task<ResultWithData<Venue>> Read(string id);

        Task<ResultWithData<Venue>> Create(Venue venue);
    }
}