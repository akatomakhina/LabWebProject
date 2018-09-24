using GamesProject.DataAccess.Common.Models;
using GamesProject.DataAccess.Common.Repositories;
using GamesProject.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GamesProject.DataAccess.Repositories
{
    class FavoriteGamesDbRepository : IFavoriteGamesRepository
    {
        private GamesProjectContext _context;
        private bool isDisposed = false;

        public FavoriteGamesDbRepository(GamesProjectContext context)
        {
            _context = context;
        }

        public async Task<DbFavoriteGames> AddFavoriteGamesAsync(DbFavoriteGames news)
        {
            if(ReferenceEquals(news, null))
            {
                throw new ArgumentNullException(nameof(news));
            }

            var addedItem = _context.FavoriteGames.Add(news);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return addedItem;
        }

        public async Task<IEnumerable<DbFavoriteGames>> GetAllFavoriteGamesAsync()
        {
            return await _context.FavoriteGames.ToListAsync().ConfigureAwait(false);
        }

        public async Task<DbFavoriteGames> GetFavoriteGamesByIdAsync(int id)
        {
            return await _context.FavoriteGames.SingleOrDefaultAsync(f => f.Id == id).ConfigureAwait(false);
        }

        public async Task<DbFavoriteGames> RemoveFavoriteGamesAsync(DbFavoriteGames news)
        {
            if (ReferenceEquals(news, null))
            {
                throw new ArgumentNullException(nameof(news));
            }

            var deletedItem = _context.FavoriteGames.Remove(news);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return deletedItem;
        }

        public async Task<IEnumerable<DbFavoriteGames>> GetFavoriteGamesForUserAsync(int userId)
        {
            return await _context.FavoriteGames.Where(gameItem => gameItem.UserId == userId).ToListAsync();
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

        ~FavoriteGamesDbRepository()
        {
            Dispose(false);
        }
    }
}
