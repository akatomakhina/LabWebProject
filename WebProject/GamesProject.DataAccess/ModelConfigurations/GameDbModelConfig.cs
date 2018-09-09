using GamesProject.DataAccess.Common.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesProject.DataAccess.ModelConfigurations
{
    public class GameDbModelConfig : EntityTypeConfiguration<DbGame>
    {
    }
}
