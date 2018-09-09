using GamesProject.DataAccess.Common.Models;
using System.Threading.Tasks;

namespace GamesProject.DataAccess.Common.Repositories
{
    public interface IChannelRepository
    {
        Task<DbChannel> AddChannelAsync(DbChannel channel);

        Task<DbChannel> DeleteChannelAsync(int id);

        Task<DbChannel> GetChannelByIdAsync(int id);
    }
}
