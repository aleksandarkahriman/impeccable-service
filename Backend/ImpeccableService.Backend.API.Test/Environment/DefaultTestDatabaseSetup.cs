using System;
using AutoMapper;
using ImpeccableService.Backend.Database;
using ImpeccableService.Backend.Database.Offering.Model;
using ImpeccableService.Backend.Database.UserManagement.Model;

namespace ImpeccableService.Backend.API.Test.Environment
{
    internal class DefaultTestDatabaseSetup : ITestDatabaseSetup
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DefaultTestDatabaseSetup(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Initialize()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var validTestUser = TestUserRegistry.ValidTestUser();
            _context.Users.Add(_mapper.Map<UserEntity>(validTestUser));

            var menuEntity = new MenuEntity { Id = Guid.NewGuid().ToString(), VenueId = "4ccb" };
            _context.Menus.Add(menuEntity);

            _context.SaveChanges();
        }
    }
}
