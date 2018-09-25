using EntertainmentPortal.Web.Models.Sports;
using EntertainmentPortal.Web.Services.Sports.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace EntertainmentPortal.Web.Services.Sports
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