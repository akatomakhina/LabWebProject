using GamesProject.Logic.Common.Models;
using GamesProject.Logic.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesProject.Logic.Services
{
    class GameService : IGameService
    {
        public Task<Game> GetGameFromIdAsync(int channelId, int gameId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetGamesByTitleAsync(string title)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetGamesFromFeedAsync(int channelId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveGameAsync(int channelId, int gameId)
        {
            throw new NotImplementedException();
        }
    }
}
