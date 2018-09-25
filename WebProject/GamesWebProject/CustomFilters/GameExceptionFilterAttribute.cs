using GamesProject.Logic.Common.Exceptions;
using NLog;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace GamesWebProject.CustomFilters
{
    public class GameExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private ILogger _logger;

        public GameExceptionFilterAttribute()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is RequestedResourceNotFoundException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            else if (context.Exception is RequestedResourceHasConflictException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Conflict);
            }
            else if (context.Exception is UriFormatException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            else if (context.Exception is GamesServiceException exception)
            {
                switch (exception.ErrorCode)
                {
                    case ErrorType.ValidationException:
                        _logger.Warn(exception);
                        context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                        break;
                    case ErrorType.BadRequest:
                        _logger.Warn(exception);
                        context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                        break;
                    default:
                        _logger.Error(exception);
                        context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                        break;
                }
            }
            else if (context.Exception is Exception)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}