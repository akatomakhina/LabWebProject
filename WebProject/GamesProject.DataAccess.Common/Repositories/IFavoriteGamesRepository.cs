using GamesProject.DataAccess.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamesProject.DataAccess.Common.Repositories
{
    public interface IFavoriteGamesRepository
    {
        Task<DbFavoriteGames> AddFavoriteGamesAsync(DbFavoriteGames news);

        Task<IEnumerable<DbFavoriteGames>> GetAllFavoriteGamesAsync();

        Task<DbFavoriteGames> GetFavoriteGamesByIdAsync(int id);

        Task<IEnumerable<DbFavoriteGames>> GetFavoriteGamesForUserAsync(int userId);

        //Task<DbFavoriteGames> UpdateFavoriteGamesAsync(DbFavoriteGames news);

        Task<DbFavoriteGames> RemoveFavoriteGamesAsync(DbFavoriteGames news);
    }
}
