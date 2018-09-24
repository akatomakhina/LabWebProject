using GamesProject.DataAccess.Common.Models;
using GamesProject.DataAccess.Common.Repositories;
using GamesProject.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace GamesProject.DataAccess.Repositories
{
    class ChannelDbRepository : IChannelRepository
    {
        private GamesProjectContext _context;
        private bool isDisposed = false;

        public ChannelDbRepository(GamesProjectContext context)
        {
            _context = context;
        }

        public async Task<DbChannel> AddChannelAsync(DbChannel channel)
        {
            if (ReferenceEquals(channel, null))
            {
                throw new ArgumentNullException(nameof(channel));
            }

            var addedItem = _context.Channels.Add(channel);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return addedItem;
        }

        public async Task<DbChannel> DeleteChannelAsync(DbChannel channel)
        {
            if (ReferenceEquals(channel, null))
            {
                throw new ArgumentNullException(nameof(channel));
            }

            var deletedItem = _context.Channels.Remove(channel);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return deletedItem;
        }

        public async Task<IEnumerable<DbChannel>> GetChannelAsync()
        {
            return await _context.Channels.ToListAsync().ConfigureAwait(false);
        }

        public async Task<DbChannel> GetChannelByIdAsync(int id)
        {
            return await _context.Channels.SingleOrDefaultAsync(f => f.Id == id).ConfigureAwait(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    // If need dispose something
                }

                isDisposed = true;
                _context?.Dispose();
                _context = null;
            }
        }        

        ~ChannelDbRepository()
        {
            Dispose(false);
        }
    }
}
