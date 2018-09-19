using GamesProject.DataAccess.Common.Repositories;
using GamesProject.DataAccess.Repositories;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace GamesProject.DataAccess
{
    class InjectorPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IChannelRepository, ChannelDbRepository>();
            container.Register<IFavoriteGamesRepository, FavoriteGamesDbRepository>();
            container.Register<IUserRepository, UserDbRepository>();
            container.Register<IGameRepository, GameDbRepository>();
        }
    }
}
