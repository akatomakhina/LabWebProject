using GamesProject.Logic.Common.Services;
using GamesProject.Logic.Services;
using SimpleInjector;
using SimpleInjector.Packaging;
using Castle.DynamicProxy;
using SportsValidationIntercepter = GamesProject.Logic.Validator.ValidatorInterceptor;
using GamesProject.Logic.Logging;
using GamesProject.Logic.Services.PageParsers;
using FluentValidation;
using GamesProject.Logic.Common.Models;
using GamesProject.Logic.Validator;

namespace GamesProject.Logic
{
    public class InjectorPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            RegisterSportsNewsServices(container);
        }

        private void RegisterSportsNewsServices(Container container)
        {
            container.Register(() =>
                new ProxyGenerator().CreateInterfaceProxyWithTargetInterface<IGameService>(
                    container.GetInstance<GameService>(),
                    container.GetInstance<SportsValidationIntercepter>(),
                    container.GetInstance<NLogInterceptor>()
                    ));
            container.Register(() =>
                    new ProxyGenerator().CreateInterfaceProxyWithTargetInterface<IChannelService>(
                        container.GetInstance<ChannelService>(),
                        container.GetInstance<SportsValidationIntercepter>(),
                        container.GetInstance<NLogInterceptor>()
                        ));

            container.Register<IChannelHelper, ChannelHelper>();
            container.Register<IUserService, UserService>();
            container.Register<IFavoriteGamesService, FavoriteGamesService>();
            container.Register<IParseService, ParseService>();
            container.Register<AbstractValidator<LinkRequest>, LinkRequestValidator>();
        }
    }
}
