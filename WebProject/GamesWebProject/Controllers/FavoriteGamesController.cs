using GamesProject.Logic.Common.Services;
using GamesWebProject.CustomFilters;
using GamesWebProject.Services;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GamesWebProject.Controllers
{
    [RoutePrefix("api/favoriteGames")]
    [GameExceptionFilter]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FavoriteGamesController : ApiController
    {
        private readonly IFavoriteGamesService _favoriteGamesService;
        private const int PageSize = 7;
        private const string FavoriteGamesGetRouteName = "FavoriteGames";
        private const string SingleSelfGamesGetRoute = "SelfGamesFromChannel";
        private const string PageGetRouteName = "page";
        private const string SportsUserIdParameterName = "userId";
        private const string GameIdParameterName = "favoriteGameItemId";
        private readonly ILogger _logger;

        private readonly Lazy<PagedModelFactory> _pagedModelFactoryLazy;
        private readonly Lazy<SelfModelFactory> _selfModelFactoryLazy;

        private PagedModelFactory PagedModelFactory => _pagedModelFactoryLazy.Value;
        private SelfModelFactory SelfModelFactory => _selfModelFactoryLazy.Value;

        public FavoriteGamesController(IFavoriteGamesService favoriteGamesService, ILogger logger)
        {
            _favoriteGamesService = favoriteGamesService ?? throw new ArgumentNullException(nameof(favoriteGamesService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _pagedModelFactoryLazy = new Lazy<PagedModelFactory>(() => new PagedModelFactory(Request));
            _selfModelFactoryLazy = new Lazy<SelfModelFactory>(() => new SelfModelFactory(Request));
        }
    }
}