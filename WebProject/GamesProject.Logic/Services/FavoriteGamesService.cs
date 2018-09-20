using GamesProject.Logic.Common.Models;
using GamesProject.Logic.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesProject.Logic.Services
{
    class FavoriteGamesService : IFavoriteGamesService
    {
        public Task<FavoriteGames> AddFavoriteGameAsync(int userId, int newsId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FavoriteGames>> GetFavoriteGamesForUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Game> GetGameFromFavoriteGames(int favoriteGamesItemId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFavoriteGameAsync(int favoriteGamesItemId)
        {
            throw new NotImplementedException();
        }
    }
}
