using GamesWebProject.Models.Abstract;
using GamesWebProject.Services.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace GamesWebProject.Services
{
    public class SelfModelFactory
    {
        private readonly UrlHelper _urlHelper;

        public SelfModelFactory(HttpRequestMessage requestMessage)
        {
            _urlHelper = new UrlHelper(requestMessage);
        }

        public TValue Create<TValue>(SelfModelFactoryConfig config, Func<TValue> modelGetter) where TValue : BaseApiModel
        {
            var model = modelGetter();

            model._self = _urlHelper.Link(config.RouteName, new Dictionary<string, object> { { config.IdParameterName, config.IdValue } });

            return model;
        }
    }
}