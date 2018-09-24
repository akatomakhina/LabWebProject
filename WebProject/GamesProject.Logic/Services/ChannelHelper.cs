using AutoMapper;
using CodeHollow.FeedReader;
using GamesProject.Logic.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesProject.Logic.Services
{
    public class ChannelHelper : IChannelHelper
    {
        public async Task<Channel> GetChannelWithGamesAsync(string source)
        {
            try
            {
                var rawChannel = await FeedReader.ReadAsync(source).ConfigureAwait(false);
                var channel = Mapper.Map<Channel>(rawChannel);
                channel.LinkRSS = source;
                return channel;
            }
            catch (Exception e)
            {
                throw new UriFormatException($"Incorrect data source. {source}", e);
            }
        }

        public async Task<IEnumerable<Game>> GetGamesAsync(string source)
        {
            try
            {
                var channel = await FeedReader.ReadAsync(source).ConfigureAwait(false);
                channel.Link = source;
                return channel.Items.Select(item => Mapper.Map<Game>(item));
            }
            catch (Exception e)
            {
                throw new UriFormatException($"Incorrect data source. {source}", e);
            }
        }
    }
}
