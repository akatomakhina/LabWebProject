using GamesProject.DataAccess.Common.Models;
using System.Data.Entity.ModelConfiguration;

namespace GamesProject.DataAccess.ModelConfigurations
{
    public class GameDbModelConfig : EntityTypeConfiguration<DbGame>
    {
        public GameDbModelConfig()
        {
            ToTable("Games");
            HasKey(k => k.Id);
            Property(p => p.Title).IsRequired().IsUnicode().IsVariableLength();            
            Property(p => p.Description).IsRequired().IsUnicode().IsVariableLength();
            Property(p => p.PubDate).IsOptional();
            Property(p => p.Link).IsRequired().IsUnicode().IsVariableLength();
            Property(p => p.Enclosure).IsOptional().IsUnicode().IsVariableLength();
            Property(p => p.Guid).IsOptional().IsUnicode().IsVariableLength();

            HasRequired(c => c.Channel).WithMany(g => g.Games)
                .HasForeignKey(c => c.ChannelId);
            HasMany(games => games.FavoriteGames);
        }
    }
}
