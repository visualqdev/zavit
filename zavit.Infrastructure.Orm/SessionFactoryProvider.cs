using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using zavit.Infrastructure.Orm.Entities;

namespace zavit.Infrastructure.Orm
{
    public class SessionFactoryProvider
    {
        readonly IDatabaseSettings _databaseSettings;

        public SessionFactoryProvider(IDatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        public ISessionFactory Provide()
        {
            var sessionfactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(_databaseSettings.ConnectionString))
                .Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<TestEntity>().Where(a => a.Namespace.EndsWith("Entities"))))
                .BuildSessionFactory();

            return sessionfactory;
        }
    }
}
