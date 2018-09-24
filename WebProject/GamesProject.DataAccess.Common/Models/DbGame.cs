using System;
using System.Collections.Generic;

namespace GamesProject.DataAccess.Common.Models
{
    public class DbGame
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? PubDate { get; set; }

        public string Link { get; set; }

        public string Enclosure { get; set; }

        public string Guid { get; set; }

        public int ChannelId { get; set; }

        public virtual DbChannel Channel { get; set; }

        public virtual ICollection<DbFavoriteGames> FavoriteGames { get; set; }
    }
}
