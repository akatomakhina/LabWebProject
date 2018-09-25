using GamesWebProject.Models;
using GamesWebProject.Services.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace GamesWebProject.Services
{
    public class PagedModelFactory
    {
        private readonly UrlHelper _urlHelper;

        public PagedModelFactory(HttpRequestMessage requestMessage)
        {
            _urlHelper = new UrlHelper(requestMessage);
        }

        public PagedModel<TValue> Create<TValue>(PagedModelFactoryConfig config, List<TValue> pageValues)
        {
            if (config.PageSize < pageValues.Count)
            {
                throw new Exception("Wrong page values number.");
            }

            var instance = Activator.CreateInstance<PagedModel<TValue>>();
            instance.TotalCount = config.TotalCount;

            var pagesNumber = (int)Math.Ceiling((double)config.TotalCount / config.PageSize);
            instance.TotalPage = pagesNumber;

            if(config.TotalCount <= config.PageSize)
            {
                instance.PrevPage = null;
                instance.NextPage = null;
            }

            if (config.TotalCount > config.PageSize && config.CurrentPage > 0)
            {
                instance.PrevPage = _urlHelper.Link(config.RouteName, new { page = config.CurrentPage - 1 });
            }

            if (config.TotalCount > config.PageSize && config.CurrentPage < pagesNumber - 1)
            {
                instance.NextPage = _urlHelper.Link(config.RouteName, new { page = config.CurrentPage + 1 });
            }

            instance.Values = pageValues;

            return instance;
        }
    }
}