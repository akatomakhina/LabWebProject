using AutoMapper;
using GamesProject.Logic.Common.Models;
using GamesProject.Logic.Common.Services;
using GamesWebProject.CustomFilters;
using GamesWebProject.Models;
using NLog;
using Swashbuckle.Swagger.Annotations;
using System;
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
    public class ChannelController : ApiController
    {
        private readonly IChannelService _channelService;
        private readonly ILogger _logger;

        public ChannelController(IChannelService channelService, ILogger logger)
        {
            _channelService = channelService ?? throw new ArgumentNullException(nameof(channelService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Creates or update channel.", Type = typeof(int))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Link is invalid.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Server error.")]
        public async Task<IHttpActionResult> GetOrCreate([FromBody] LinkRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var channel = await _channelService.GetOrCreateChannelAsync(createRequest).ConfigureAwait(false);
            var location = string.Format("api/channel/{0}", channel.Id);
            return Created(location, channel.Id);
        }


        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Get all channels.", Type = typeof(int))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GetChannels()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var channels = await _channelService.GetChannels().ConfigureAwait(false);
            var channelsModel = channels.Select(c => Mapper.Map<ChannelModel>(channels));
            return Ok(channelsModel);
        }


        [HttpDelete]
        [Route("{id:int}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Deletes an existing channel.")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Channel not found.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Server error.")]
        public async Task<IHttpActionResult> DeleteChannel([FromUri] int channelId)
        {
            await _channelService.RemoveChannelAsync(channelId).ConfigureAwait(false);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
        }
    }
}