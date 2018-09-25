using AutoMapper;
using CodeHollow.FeedReader;
using GamesProject.DataAccess.Common.Models;
using GamesProject.Logic.Common.Models;
using System.Linq;

namespace GamesProject.Logic.Mappings
{
    public class GamesMappingProfile : Profile
    {
        public GamesMappingProfile()
        {
            CreateMap<DbChannel, Channel>();

            this.CreateMap<CodeHollow.FeedReader.Feed, Common.Models.Channel>()
                .ForMember(cf => cf.Games, lf => lf.MapFrom(p => p.Items.Select(i => Mapper.Map<FeedItem, Game>(i))))
                .ForMember(cf => cf.LastModified, lf => lf.MapFrom(p => p.LastUpdatedDate))
                .ForMember(cf => cf.Id, lf => lf.Ignore())
                .ForMember(cf => cf.LinkRSS, lf => lf.Ignore());

            this.CreateMap<FeedItem, Game>()
                .ForMember(sn => sn.Guid, fi => fi.MapFrom(p => p.Id))
                .ForMember(sn => sn.Enclosure, fi => fi.MapFrom(p => p.Content))
                .ForMember(sn => sn.Id, fi => fi.Ignore());
            //.ForMember(sn => sn.FeedId, fi => fi.Ignore());

            this.CreateMap<Game, DbGame>()
                .ForMember(sn => sn.Channel, dbsn => dbsn.Ignore())
                .ForMember(sn => sn.FavoriteGames, dbsn => dbsn.Ignore())
                .ForMember(sn => sn.ChannelId, dbsn => dbsn.Ignore());
        }
    }
}
