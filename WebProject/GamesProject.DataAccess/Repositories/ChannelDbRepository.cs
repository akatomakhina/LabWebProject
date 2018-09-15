using GamesProject.DataAccess.Common.Models;
using GamesProject.DataAccess.Common.Repositories;
using GamesProject.DataAccess.Context;
using System;
using System.Threading.Tasks;

namespace GamesProject.DataAccess.Repositories
{
    class ChannelDbRepository : IChannelRepository
    {
        private GamesProjectContext _context;
        private bool isDisposed = false;

        public Task<DbChannel> AddChannelAsync(DbChannel channel)
        {
            throw new NotImplementedException();
        }

        public Task<DbChannel> DeleteChannelAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DbChannel> GetChannelByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
