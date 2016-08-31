using System.Configuration;
using zavit.Infrastructure.Orm;

namespace zavit.Web.Mvc.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        string _connectionString;
        public string ConnectionString => _connectionString ?? (_connectionString = ConfigurationManager.AppSettings["Database.ConnectionString"]);
    }
}