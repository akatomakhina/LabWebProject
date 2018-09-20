using GamesProject.Logic.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamesProject.Logic.Common.Services
{
    public interface IChannelService
    {
        Task<Channel> GetOrCreateChannelAsync(LinkRequest createRequest);

        Task<IEnumerable<Channel>> GetChannels();

        Task RemoveChannelAsync(int channelId);
    }
}
