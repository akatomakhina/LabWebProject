using GamesProject.DataAccess.Common.Models;
using System.Data.Entity.ModelConfiguration;

namespace GamesProject.DataAccess.ModelConfigurations
{
    class ChannelDbModelConfig : EntityTypeConfiguration<DbChannel>
    {
        public ChannelDbModelConfig()
        {
            ToTable("Channels");
            HasKey(k => k.Id);
            Property(p => p.Title).IsRequired().IsUnicode().IsVariableLength();
            Property(p => p.Link).IsRequired().IsUnicode().IsVariableLength();
            Property(p => p.LastModified).IsOptional();

            HasMany(g => g.Games);
        } 
    }
}
