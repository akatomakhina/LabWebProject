namespace GamesProject.DataAccess.Common.Models
{
    public class DbFavoriteGames
    {
        public int Id { get; set; }

        //public DateTime? LastModified { get; set; }

        public int UserId { get; set; }

        public virtual DbUser User { get; set; }

        public int GameId { get; set; }

        public virtual DbGame Games { get; set; }
    }
}
