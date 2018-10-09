using EntertainmentPortal.Web.Models.Sports;
using GamesProject.Logic.Common.Models;
using GamesProject.Logic.Common.Services;
using GamesWebProject.CustomFilters;
using GamesWebProject.Models;
using GamesWebProject.Services;
using GamesWebProject.Services.Models;
using NLog;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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

        [HttpPost]
        [Route("{userId:int}/{gameId:int}")]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Add game for user to favorite", Type = typeof(FavoriteGames))]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> AddFavoriteGame([FromUri] int userId, int gameId)
        {
            var gamesItem = await _favoriteGamesService.AddFavoriteGameAsync(userId, gameId).ConfigureAwait(false);
            return Ok(gamesItem);
        }


        [HttpGet]
        [Route("{userId:int}")]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Get user-channel items to User", Type = typeof(IEnumerable<FavoriteGames>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Syntax error.")]
        [SwaggerResponse(HttpStatusCode.Conflict, Description = "Conflicting request to the resource.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Server error.")]
        public async Task<IHttpActionResult> GetFavoriteGamesForUser([FromUri] int userId)
        {
            var gamesItems = await _favoriteGamesService.GetFavoriteGamesForUserAsync(userId).ConfigureAwait(false);
            return Ok(gamesItems);
        }


        [HttpGet]
        [Route("{userId}/games/" + PageGetRouteName, Name = FavoriteGamesGetRouteName)]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Get user-channel items to User", Type = typeof(IEnumerable<FavoriteGames>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Syntax error.")]
        [SwaggerResponse(HttpStatusCode.Conflict, Description = "Conflicting request to the resource.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Server error.")]
        public async Task<IHttpActionResult> GetFavoriteGamesForUserPagination([FromUri] int userId, int page = 0)
        {
            var channelsEnumerable = await _favoriteGamesService.GetFavoriteGamesForUserAsync(userId).ConfigureAwait(false);
            int count = channelsEnumerable.Count();

            var channels = channelsEnumerable.Skip(page * PageSize).Take(PageSize)
                .Select(ch => SelfModelFactory.Create(new SelfModelFactoryConfig
                {
                    IdParameterName = GameIdParameterName,
                    IdValue = ch.GameId,
                    RouteName = SingleSelfGamesGetRoute
                },
                () => new FavoriteGamesApiModel(ch)));

            var pagedModel = PagedModelFactory.Create(new PagedModelFactoryConfig
            {
                CurrentPage = page,
                PageSize = PageSize,
                RouteName = FavoriteGamesGetRouteName,
                TotalCount = count
            },
            channels.ToList());

            return Ok(pagedModel);
        }


        [HttpGet]
        [Route("news/{favoriteGameItemId:int}", Name = SingleSelfGamesGetRoute)]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a game.", Type = typeof(Game))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Favorite news not found.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Server error.")]
        public async Task<IHttpActionResult> GetGamesByIdFromFavoriteGamesSelf([FromUri] int favoriteGamesItemId)
        {
            var games = await _favoriteGamesService.GetGameFromFavoriteGames(favoriteGamesItemId).ConfigureAwait(false);

            var newsModel = SelfModelFactory.Create(new SelfModelFactoryConfig
            {
                IdParameterName = GameIdParameterName,
                IdValue = favoriteGamesItemId,
                RouteName = SingleSelfGamesGetRoute
            },
            () => new GameApiModel(games));

            return Ok(newsModel);
        }


        [HttpDelete]
        [Route("{itemId:int}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Deletes an existed favorite game.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> DeleteGame([FromUri] int itemId)
        {
            await _favoriteGamesService.RemoveFavoriteGameAsync(itemId).ConfigureAwait(false);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
        }
    }
}