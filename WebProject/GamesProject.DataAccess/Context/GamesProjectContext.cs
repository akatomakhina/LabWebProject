using GamesProject.DataAccess.Common.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.IO;
using System.Configuration;
using System.Reflection;
using GamesProject.DataAccess.ModelConfigurations;

namespace GamesProject.DataAccess.Context
{
    [DbConfigurationType(typeof(GamesProjectContextConfiguration))]
    public class GamesProjectContext : DbContext
    {
        private const string _connectionName = "GamesProjectContext";
        private const string _configFileName = "GamesProject.DataAccess.dll.config";
        private static readonly string _connectionString;

        static GamesProjectContext()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string fullCfgFileName = $@"{Path.GetDirectoryName(path)}\{_configFileName}";

            var configMap = new ExeConfigurationFileMap() { ExeConfigFilename = fullCfgFileName };
            var config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            foreach (ConnectionStringSettings connectionString in config.ConnectionStrings.ConnectionStrings)
            {
                if (connectionString.Name.Equals(_connectionName, StringComparison.OrdinalIgnoreCase))
                {
                    _connectionString = connectionString.ConnectionString;
                    break;
                }
            }

            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new ConfigurationErrorsException($"Cannot find connection string with name {_connectionName}");
            }
        }

        public GamesProjectContext() : base(_connectionString)
        {
        }

        public IDbSet<DbChannel> Channels { get; set; }

        public IDbSet<DbFavoriteGames> FavoriteGames { get; set; }

        public IDbSet<DbUser> Users { get; set; }

        public IDbSet<DbGame> Games { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GameDbModelConfig());
            modelBuilder.Configurations.Add(new ChannelDbModelConfig());
            modelBuilder.Configurations.Add(new FavoriteGamesDbModelConfig());
            modelBuilder.Configurations.Add(new UserDbModelConfig());
            base.OnModelCreating(modelBuilder);
        }
    }

    public class GamesProjectContextConfiguration : DbConfiguration
    {
        public GamesProjectContextConfiguration()
        {
            this.SetProviderServices(SqlProviderServices.ProviderInvariantName, SqlProviderServices.Instance);
        }
    }
}
