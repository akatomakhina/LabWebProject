using GamesProject.DataAccess.Common.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesProject.DataAccess.ModelConfigurations
{
    public class FavoriteGamesDbModelConfig : EntityTypeConfiguration<DbFavoriteGames>
    {
        public FavoriteGamesDbModelConfig()
        {
            ToTable("FavoriteGames");
            HasKey(k => k.Id);
            Property(p => p.LastModified).IsOptional();
            HasRequired(feed => feed.News).WithMany(f => f.FavoriteNews)
                .HasForeignKey(userFeed => userFeed.NewsId);
            HasRequired(user => user.User).WithMany(f => f.FavoriteNews)
                .HasForeignKey(userFeed => userFeed.UserId);
        }
    }
}
