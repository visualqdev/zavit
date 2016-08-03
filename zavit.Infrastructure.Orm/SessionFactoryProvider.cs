using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using zavit.Domain.Shared;
using zavit.Infrastructure.Orm.Conventions;

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
            var mappingAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("zavit.Domain.")).ToArray();

            var entityInterface = typeof (IEntity<>);

            var sessionfactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(_databaseSettings.ConnectionString))
                .Mappings(m => m.AutoMappings.Add(AutoMap
                    .Assemblies(mappingAssemblies)
                    .Where(entity => 
                    {
                        var entityInterfaces = entity.GetInterfaces();
                        var isEntity = entityInterfaces.Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == entityInterface);
                        return isEntity;
                    })
                    .Conventions.AddFromAssemblyOf<PrimaryKeyConvention>()))
                .ExposeConfiguration(config => new SchemaUpdate(config).Execute(true, true))
                .BuildSessionFactory();

            return sessionfactory;
        }
    }
}
