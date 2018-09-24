using GamesProject.Logic.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamesProject.Logic.Services
{
    public interface IChannelHelper
    {
        Task<Channel> GetChannelWithGamesAsync(string source);

        Task<IEnumerable<Game>> GetGamesAsync(string source);
    }
}
