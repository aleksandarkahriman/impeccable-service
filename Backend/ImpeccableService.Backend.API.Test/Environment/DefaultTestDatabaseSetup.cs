using AutoMapper;
using ImpeccableService.Backend.API.Test.Environment.Data;
using ImpeccableService.Backend.Database;

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
            
            _context.AddTestUsers(_mapper);
            _context.AddTestOfferings();

            _context.SaveChanges();
        }
    }
}
