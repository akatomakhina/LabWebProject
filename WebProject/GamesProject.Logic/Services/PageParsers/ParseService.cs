using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GamesProject.Logic.Common.Models;
using GamesProject.Logic.Services.PageParsers.Parser;

namespace GamesProject.Logic.Services.PageParsers
{
    public class ParseService : IParseService
    {
        private readonly List<IGamePageParser> _pageParsers;

        public ParseService()
        {
            _pageParsers = new List<IGamePageParser>()
            {
                new StopGamePageParser()
            };
        }

        public async Task<Game> LoadDetailAsync(Game news)
        {
            if (ReferenceEquals(news, null))
            {
                throw new ArgumentNullException(nameof(news));
            }

            if (!TryCreateUri(news.Link))
            {
                throw new UriFormatException();
            }

            var uri = new Uri(news.Link);
            var htmlTask = LoadPageAsync(uri);
            var parser = ChoosePageParser(uri);
            var html = await htmlTask;
            if (ReferenceEquals(parser, null) || (ReferenceEquals(html, null)))
            {
                return news;
            }

            news.Description = parser.Parse(html);
            return news;
        }

        protected virtual async Task<string> LoadPageAsync(Uri uri)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(uri);

                string source = null;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    source = await response.Content.ReadAsStringAsync();
                }

                return source;
            }
        }

        protected virtual IGamePageParser ChoosePageParser(Uri uri)
        {
            return _pageParsers.FirstOrDefault(parser => parser.Host == uri.Host);
        }

        private bool TryCreateUri(string uri)
        {
            return Uri.TryCreate(uri, UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
