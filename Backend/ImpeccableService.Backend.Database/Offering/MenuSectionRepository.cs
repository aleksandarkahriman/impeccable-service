using System.Threading.Tasks;
using AutoMapper;
using ImpeccableService.Backend.Core.Offering.Dependency;
using ImpeccableService.Backend.Database.Offering.Model;
using ImpeccableService.Backend.Domain.Offering;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Database.Offering
{
    internal class MenuSectionRepository : IMenuSectionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public MenuSectionRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public async Task<ResultWithData<MenuSection>> CreateForMenu(MenuSection menuSection, string menuId)
        {
            var menuSectionEntity = _mapper.Map<MenuSectionEntity>(menuSection);
            menuSectionEntity.MenuId = menuId;
            await _dbContext.MenuSections.AddAsync(menuSectionEntity);
            await _dbContext.SaveChangesAsync();
            return new ResultWithData<MenuSection>(menuSection);
        }
    }
}