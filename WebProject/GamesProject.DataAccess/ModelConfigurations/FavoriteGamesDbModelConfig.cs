using GamesProject.DataAccess.Common.Models;
using System.Data.Entity.ModelConfiguration;

namespace GamesProject.DataAccess.ModelConfigurations
{
    public class FavoriteGamesDbModelConfig : EntityTypeConfiguration<DbFavoriteGames>
    {
        public FavoriteGamesDbModelConfig()
        {
            ToTable("FavoriteGames");
            HasKey(k => k.Id);
            Property(p => p.LastModified).IsOptional();
            HasRequired(user => user.User).WithMany(g => g.FavoriteGames)
                .HasForeignKey(favoriteGames=> favoriteGames.UserId);            
            HasRequired(game => game.Games).WithMany(g => g.FavoriteGames)
                .HasForeignKey(favoriteGames => favoriteGames.GameId);            
        }
    }
}
