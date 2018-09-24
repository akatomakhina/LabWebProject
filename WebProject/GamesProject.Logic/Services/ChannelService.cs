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
    public class ChannelService : IChannelService
    {
        private readonly IChannelRepository _channelRepository;
        private readonly IChannelHelper _channelHelper;

        public ChannelService(IChannelRepository channelRepository, IChannelHelper channelHelper)
        {
            _channelRepository = channelRepository ?? throw new ArgumentNullException(nameof(channelRepository));
            _channelHelper = channelHelper ?? throw new ArgumentNullException(nameof(channelHelper));
        }

        public async Task<IEnumerable<Channel>> GetChannels()
        {
            var dbChannels = await _channelRepository.GetChannelAsync().ConfigureAwait(false);
            return dbChannels.Select(feed => Mapper.Map<Channel>(feed));
        }

        public async Task<Channel> GetOrCreateChannelAsync(LinkRequest createRequest)
        {
            if (ReferenceEquals(createRequest, null))
            {
                throw new ArgumentNullException(nameof(createRequest));
            }

            if (!Uri.TryCreate(createRequest.Link, UriKind.Absolute, out Uri uri))
            {
                throw new UriFormatException($"Incorrect data source. {createRequest}");
            }

            var channel = await _channelHelper.GetChannelWithGamesAsync(createRequest.Link).ConfigureAwait(false);

            return await GetOrCreateChannelAsync(channel).ConfigureAwait(false);
        }

        public async Task RemoveChannelAsync(int channelId)
        {
            var existedChannel = await _channelRepository.GetChannelByIdAsync(channelId).ConfigureAwait(false);

            if (ReferenceEquals(existedChannel, null))
            {
                throw new RequestedResourceNotFoundException($"Channel with id {channelId}");
            }

            var deletedItem = await _channelRepository.DeleteChannelAsync(existedChannel).ConfigureAwait(false);
        }

        private async Task<Channel> GetOrCreateChannelAsync(Channel channel)
        {
            var dbChannels = await _channelRepository.GetChannelAsync().ConfigureAwait(false);
            var dbItem = dbChannels.SingleOrDefault(dbChannel => dbChannel.Link == channel.Link);
            if (ReferenceEquals(dbItem, null))
            {
                return await _channelRepository.AddChannelAsync(Mapper.Map<DbChannel>(channel))
                    .ContinueWith(task => Mapper.Map<Channel>(task.Result)).ConfigureAwait(false);
            }
            else
            {
                return Mapper.Map<Channel>(dbItem);
            }
        }
    }
}
