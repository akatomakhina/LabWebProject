using GamesProject.DataAccess.Common.Models;
using GamesProject.DataAccess.Common.Repositories;
using GamesProject.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace GamesProject.DataAccess.Repositories
{
    class GameDbRepository : IGameRepository
    {
        private GamesProjectContext _context;
        private bool isDisposed = false;

        public GameDbRepository(GamesProjectContext context)
        {
            _context = context;
        }

        public async Task<DbGame> AddGameAsync(DbGame game)
        {
            if(ReferenceEquals(game, null))
            {
                throw new ArgumentNullException(nameof(game));
            }

            var addedItem = _context.Games.Add(game);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return addedItem;
        }

        public async Task<DbGame> DeleteGameAsync(DbGame game)
        {
            if (ReferenceEquals(game, null))
            {
                throw new ArgumentNullException(nameof(game));
            }

            var deletedItem = _context.Games.Remove(game);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return deletedItem;
        }

        public async Task<IEnumerable<DbGame>> GetAllGameAsync()
        {
            return await _context.Games.ToListAsync().ConfigureAwait(false);
        }

        public async Task<DbGame> GetGameByIdAsync(int id)
        {
            return await _context.Games.SingleOrDefaultAsync(g => g.Id == id).ConfigureAwait(false);
        }

        public async Task<DbGame> UpdateGameAsync(DbGame game)
        {
            if (ReferenceEquals(game, null))
            {
                throw new ArgumentNullException(nameof(game));
            }

            var updateItem = await GetGameByIdAsync(game.Id).ConfigureAwait(false);

            _context.Entry(updateItem).CurrentValues.SetValues(game);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return updateItem;
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

        ~GameDbRepository()
        {
            Dispose(false);
        }
    }
}
