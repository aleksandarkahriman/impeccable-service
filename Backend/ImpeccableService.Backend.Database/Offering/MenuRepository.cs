using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ImpeccableService.Backend.Core.Offering.Dependency;
using ImpeccableService.Backend.Domain.Offering;
using Microsoft.EntityFrameworkCore;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Database.Offering
{
    internal class MenuRepository : IMenuRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public MenuRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public async Task<ResultWithData<Menu>> ReadByVenueId(string venueId)
        {
            var menuEntity = await _dbContext.Menus
                .Where(menu => menu.VenueId == venueId)
                .Include(menu => menu.Sections)
                .FirstOrDefaultAsync();

            return menuEntity != null
                ? new ResultWithData<Menu>(_mapper.Map<Menu>(menuEntity))
                : new ResultWithData<Menu>(new KeyNotFoundException($"Menu for venue {venueId} not found."));
        }
    }
}