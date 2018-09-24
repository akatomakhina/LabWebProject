using GamesProject.Logic.Common.Models;
using System.Threading.Tasks;

namespace GamesProject.Logic.Services.PageParsers
{
    public interface IParseService
    {
        Task<Game> LoadDetailAsync(Game news);
    }
}
