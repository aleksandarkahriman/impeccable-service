using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ImpeccableService.Backend.Core.UserManagement.Dependency;
using ImpeccableService.Backend.Core.UserManagement.Model;
using ImpeccableService.Backend.Database.UserManagement.Model;
using ImpeccableService.Domain.UserManagement;
using Microsoft.EntityFrameworkCore;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Database.UserManagement
{
    internal class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ResultWithData<bool>> UserWithEmailExists(string email)
        {
            var exists = await _dbContext.Users.AnyAsync(user => user.Email == email);
            return new ResultWithData<bool>(exists);
        }

        public async Task<ResultWithData<User>> Create(User user)
        {
            var userEntity = _mapper.Map<UserEntity>(user);

            await _dbContext.Users.AddAsync(userEntity);
            await _dbContext.SaveChangesAsync();

            return new ResultWithData<User>(_mapper.Map<User>(userEntity));
        }

        public async Task<ResultWithData<User>> Read(string email, string passwordHash)
        {
            var userEntity = await _dbContext.Users
                .FirstOrDefaultAsync(user => user.Email == email && user.PasswordHash == passwordHash);

            return userEntity != null
                ? new ResultWithData<User>(_mapper.Map<User>(userEntity))
                : new ResultWithData<User>(new KeyNotFoundException("User not found."));
        }

        public Task<Result> Create(Authentication authentication)
        {
            throw new System.NotImplementedException();
        }

        public Task<ResultWithData<User>> ReadByRefreshToken(string refreshToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
