using AutoMapper;
using GamesProject.DataAccess.Common.Models;
using GamesProject.DataAccess.Common.Repositories;
using GamesProject.Logic.Common.Exceptions;
using GamesProject.Logic.Common.Models;
using GamesProject.Logic.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesProject.Logic.Services
{
    class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<User> CreateUserAsync()
        {
            var newUser = new DbUser();

            var addedUser = await _userRepository.AddUserAsync(newUser).ConfigureAwait(false);
            return Mapper.Map<User>(addedUser);
        }

        public async Task DeleteUserAsync(int userId)
        {
            var dbUser = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);

            if (ReferenceEquals(dbUser, null))
            {
                throw new RequestedResourceNotFoundException($"User with id {userId}");
            }

            var deletedUser = await _userRepository.DeleteUserAsync(dbUser).ConfigureAwait(false);
            throw new NotImplementedException();
        }
    }
}
