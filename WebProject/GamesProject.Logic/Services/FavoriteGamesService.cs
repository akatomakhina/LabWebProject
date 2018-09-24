using AutoMapper;
using GamesProject.DataAccess.Common.Models;
using GamesProject.DataAccess.Common.Repositories;
using GamesProject.Logic.Common.Exceptions;
using GamesProject.Logic.Common.Models;
using GamesProject.Logic.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesProject.Logic.Services
{
    public class FavoriteGamesService : IFavoriteGamesService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFavoriteGamesRepository _favoriteGamesRepository;
        private readonly IGameRepository _gameRepository;

        public FavoriteGamesService(IUserRepository userRepository, IFavoriteGamesRepository favoriteGamesRepository, IGameRepository gameRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _favoriteGamesRepository = favoriteGamesRepository ?? throw new ArgumentNullException(nameof(favoriteGamesRepository));
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        }

        public async Task<FavoriteGames> AddFavoriteGameAsync(int userId, int gameId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);

            if (ReferenceEquals(user, null))
            {
                throw new RequestedResourceNotFoundException($"User with id {userId}");
            }

            var game = await _gameRepository.GetGameByIdAsync(gameId).ConfigureAwait(false);

            if (ReferenceEquals(game, null))
            {
                throw new RequestedResourceNotFoundException($"Game with id {gameId}");
            }

            var favoritesGame = await _favoriteGamesRepository.GetFavoriteGamesForUserAsync(userId).ConfigureAwait(false);
            var savedChannelItem = favoritesGame.SingleOrDefault(item => item.UserId == userId && item.GameId == gameId);

            if (!ReferenceEquals(savedChannelItem, null))
            {
                throw new RequestedResourceHasConflictException
                    ($"The game with id:{gameId} has already been added to the user with id:{userId}.");
            }

            var favoriteGame = new DbFavoriteGames()
            {
                LastModified = DateTime.Now,
                User = user,
                Games = game
            };

            var addedItem = await _favoriteGamesRepository.AddFavoriteGamesAsync(favoriteGame).ConfigureAwait(false);
            return Mapper.Map<FavoriteGames>(addedItem);
        }

        public async Task<IEnumerable<FavoriteGames>> GetFavoriteGamesForUserAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);

            if (ReferenceEquals(user, null))
            {
                throw new RequestedResourceNotFoundException($"User with id {userId}");
            }

            var favoritesGame = await _favoriteGamesRepository.GetFavoriteGamesForUserAsync(userId).ConfigureAwait(false);
            return favoritesGame.Select(item => Mapper.Map<FavoriteGames>(item));
        }

        public async Task<Game> GetGameFromFavoriteGames(int favoriteGamesItemId)
        {
            var favoriteGame = await _favoriteGamesRepository.GetFavoriteGamesByIdAsync(favoriteGamesItemId).ConfigureAwait(false);
            if (ReferenceEquals(favoriteGame, null))
            {
                throw new RequestedResourceNotFoundException($"Favorite game with id {favoriteGame}");
            }

            var game = await _gameRepository.GetGameByIdAsync(favoriteGame.GameId).ConfigureAwait(false);
            if (ReferenceEquals(game, null))
            {
                throw new RequestedResourceNotFoundException($"Game with id {game}");
            }

            var gameWithDetailInfo = Mapper.Map<Game>(game);

            return gameWithDetailInfo;
        }

        public async Task RemoveFavoriteGameAsync(int favoriteGamesItemId)
        {
            var itemForRemove = await _favoriteGamesRepository.GetFavoriteGamesByIdAsync(favoriteGamesItemId).ConfigureAwait(false);

            if (ReferenceEquals(itemForRemove, null))
            {
                throw new RequestedResourceNotFoundException($"Favorite news with id {favoriteGamesItemId}");
            }

            await _favoriteGamesRepository.RemoveFavoriteGamesAsync(itemForRemove).ConfigureAwait(false);
        }

        private async Task<User> GetUserByIdAsync(int userFeedId)
        {
            var userFeed = await _userRepository.GetUserByIdAsync(userFeedId).ConfigureAwait(false);

            if (ReferenceEquals(userFeed, null))
            {
                throw new RequestedResourceNotFoundException($"Feed with id {userFeedId}");
            }

            return Mapper.Map<User>(userFeed);
        }
    }
}
