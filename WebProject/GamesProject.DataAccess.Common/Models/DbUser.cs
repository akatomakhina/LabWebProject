using System.Collections.Generic;

namespace GamesProject.DataAccess.Common.Models
{
    public class DbUser
    {
        public int Id { get; set; }

        public virtual ICollection<DbFavoriteGames> FavoriteGames { get; set; }
    }
}
