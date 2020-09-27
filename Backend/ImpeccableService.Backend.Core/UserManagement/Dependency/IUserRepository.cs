﻿using System.Threading.Tasks;
using ImpeccableService.Backend.Core.UserManagement.Model;
using ImpeccableService.Backend.Domain.UserManagement;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Core.UserManagement.Dependency
{
    public interface IUserRepository
    {
        Task<ResultWithData<bool>> UserWithEmailExists(string email);

        Task<ResultWithData<User>> Create(User user);

        Task<ResultWithData<User>> Read(string email, string passwordHash);

        Task<Result> Create(Authentication authentication);

        Task<ResultWithData<User>> ReadByRefreshToken(string refreshToken);

        Task<ResultWithData<User>> Read(string id);
    }
}
