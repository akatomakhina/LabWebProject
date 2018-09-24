using GamesProject.DataAccess.Common.Repositories;
using GamesProject.Logic.Common.Models;
using GamesProject.Logic.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesProject.Logic.Services
{
    public class ChannelService : IChannelService
    {
        private readonly IChannelRepository _channelRepository;
        private readonly ChannelHelper _channelHelper;

        public ChannelService(IChannelRepository channelRepository, ChannelHelper channelHelper)
        {
            _channelRepository = channelRepository ?? throw new ArgumentNullException(nameof(channelRepository));
            _channelHelper = channelHelper ?? throw new ArgumentNullException(nameof(channelHelper));
        }

        public Task<IEnumerable<Channel>> GetChannels()
        {
            throw new NotImplementedException();
        }

        public Task<Channel> GetOrCreateChannelAsync(LinkRequest createRequest)
        {
            throw new NotImplementedException();
        }

        public Task RemoveChannelAsync(int channelId)
        {
            throw new NotImplementedException();
        }
    }
}
