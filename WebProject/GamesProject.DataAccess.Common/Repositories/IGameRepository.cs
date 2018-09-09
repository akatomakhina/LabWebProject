using GamesProject.DataAccess.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamesProject.DataAccess.Common.Repositories
{
    public interface IGameRepository
    {
        Task<DbGame> AddGameAsync(DbGame game);

        Task<DbGame> DeleteGameAsync(DbGame game);

        Task<IEnumerable<DbGame>> GetAllGameAsync();

        Task<DbGame> UpdateGameAsync(DbGame game);

        Task<DbGame> GetGameByIdAsync(int id);
    }
}
