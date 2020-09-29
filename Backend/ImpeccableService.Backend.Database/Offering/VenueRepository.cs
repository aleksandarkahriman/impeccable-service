using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ImpeccableService.Backend.Core.Offering.Dependency;
using ImpeccableService.Backend.Database.Offering.Model;
using ImpeccableService.Backend.Domain.Offering;
using Microsoft.EntityFrameworkCore;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Database.Offering
{
    internal class VenueRepository : IVenueRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public VenueRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ResultWithData<List<Venue>>> Read()
        {
            var venueEntities = await _dbContext.Venues.ToListAsync();
            return new ResultWithData<List<Venue>>(_mapper.Map<List<Venue>>(venueEntities));
        }
        
        public async Task<ResultWithData<Venue>> Read(string id)
        {
            var venueEntity = await _dbContext.Venues
                .Where(venue => venue.Id == id)
                .Include(venue => venue.Company)
                .FirstOrDefaultAsync();

            return venueEntity != null
                ? new ResultWithData<Venue>(_mapper.Map<Venue>(venueEntity))
                : new ResultWithData<Venue>(new KeyNotFoundException($"Venue {id} not found."));
        }

        public async Task<ResultWithData<Venue>> Create(Venue venue)
        {
            var venueEntity = _mapper.Map<VenueEntity>(venue);
            await _dbContext.Venues.AddAsync(venueEntity);
            await _dbContext.SaveChangesAsync();
            return new ResultWithData<Venue>(venue);
        }
    }
}