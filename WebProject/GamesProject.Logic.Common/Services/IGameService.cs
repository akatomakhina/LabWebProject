using GamesProject.Logic.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamesProject.Logic.Common.Services
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetGamesFromFeedAsync(int channelId);

        Task<Game> GetGameFromIdAsync(int channelId, int gameId);

        Task<IEnumerable<Game>> GetGamesByTitleAsync(string title);

        Task RemoveGameAsync(int channelId, int gameId);
    }
}
