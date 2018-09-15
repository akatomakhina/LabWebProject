using GamesProject.DataAccess.Common.Models;
using System.Data.Entity.ModelConfiguration;

namespace GamesProject.DataAccess.ModelConfigurations
{
    public class UserDbModelConfig : EntityTypeConfiguration<DbUser>
    {
        public UserDbModelConfig()
        {
            ToTable("Users");
            HasKey(k => k.Id);

            HasMany(fg => fg.FavoriteGames);
        }        
    }
}
