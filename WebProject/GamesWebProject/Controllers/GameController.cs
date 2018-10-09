using EntertainmentPortal.Web.Models.Sports;
using GamesProject.Logic.Common.Models;
using GamesProject.Logic.Common.Services;
using GamesWebProject.CustomFilters;
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
    [RoutePrefix("api/channel")]
    [GameExceptionFilter]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GameController : ApiController
    {
        private const int PageSize = 7;
        private const string SingleSelfGamesGetRoute = "SelfGames";
        private const string PageGetRouteName = "page";
        private const string GamesGetRouteName = "gamesNews";
        private const string GamesIdParameterName = "gameId";
        private readonly IGameService _gameService;
        private readonly ILogger _logger;

        private readonly Lazy<PagedModelFactory> _pagedModelFactoryLazy;
        private readonly Lazy<SelfModelFactory> _selfModelFactoryLazy;

        private PagedModelFactory PagedModelFactory => _pagedModelFactoryLazy.Value;
        private SelfModelFactory SelfModelFactory => _selfModelFactoryLazy.Value;

        public GameController(IGameService gameService, ILogger logger)
        {
            _gameService = gameService ?? throw new ArgumentNullException(nameof(gameService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _pagedModelFactoryLazy = new Lazy<PagedModelFactory>(() => new PagedModelFactory(Request));
            _selfModelFactoryLazy = new Lazy<SelfModelFactory>(() => new SelfModelFactory(Request));
        }

        [HttpGet]
        [Route("{channelId:int}/games/page", Name = GamesGetRouteName)]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a game.", Type = typeof(IEnumerable<Game>))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Channel not found.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Server error.")]
        public async Task<IHttpActionResult> GetGamesFromChannelPagination([FromUri] int channelId, int page = 0)
        {
            var channel = await _gameService.GetGamesFromFeedAsync(channelId).ConfigureAwait(false);
            int count = channel.Count();

            var gameRoute = channel.Skip(page * PageSize).Take(PageSize)
                .Select(n => SelfModelFactory.Create(new SelfModelFactoryConfig
                {
                    IdParameterName = GamesIdParameterName,
                    IdValue = n.Id,
                    RouteName = SingleSelfGamesGetRoute
                },
                () => new GameApiModel(n)));

            var pagedModel = PagedModelFactory.Create(new PagedModelFactoryConfig
            {
                CurrentPage = page,
                PageSize = PageSize,
                RouteName = GamesGetRouteName,
                TotalCount = count
            },
            gameRoute.ToList());

            return Ok(pagedModel);
        }

        [HttpGet]
        [Route("{channelId:int}/games/{gameId:int}", Name = SingleSelfGamesGetRoute)]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a game.", Type = typeof(Game))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Channel not found.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Server error.")]
        public async Task<IHttpActionResult> GetGameById([FromUri] int channelId, [FromUri] int gameId)
        {
            var game = await _gameService.GetGameFromIdAsync(channelId, gameId).ConfigureAwait(false);
            return Ok(game);
        }


        [HttpGet]
        [Route("{channelId:int}/games")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a game.", Type = typeof(IEnumerable<Game>))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Channel not found.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Server error.")]
        public async Task<IHttpActionResult> GetGameFromFeed([FromUri] int gameId)
        {
            var game = await _gameService.GetGamesFromFeedAsync(gameId).ConfigureAwait(false);
            return Ok(game);
        }


        [HttpDelete]
        [Route("{channelId:int}/games/{gameId:int}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Deletes an existed game.")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Channel not found.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Server error.")]
        public async Task<IHttpActionResult> DeleteGame([FromUri] int channelId, [FromUri] int gameId)
        {
            await _gameService.RemoveGameAsync(channelId, gameId).ConfigureAwait(false);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
        }


        [HttpGet]
        [Route("title/{title}")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Search games by title.", Type = typeof(IEnumerable<Game>))]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public async Task<IHttpActionResult> SearchGameByTitle(string title)
        {
            var game = await _gameService.GetGamesByTitleAsync(title);
            return Ok(game);
        }
    }
}