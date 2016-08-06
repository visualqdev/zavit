using System;
using System.Linq;
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
            var overrideAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("zavit.Infrastructure."));

            var entityInterface = typeof (IEntity<>);

            var fluentConfiguration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(_databaseSettings.ConnectionString))
                .Mappings(m => m.AutoMappings.Add(() =>
                {
                    var autoPersistanceModel = AutoMap.Assemblies(mappingAssemblies)
                        .Where(entity =>
                        {
                            var entityInterfaces = entity.GetInterfaces();
                            var isEntity = entityInterfaces.Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == entityInterface);
                            return isEntity;
                        });
                    autoPersistanceModel.Conventions.AddFromAssemblyOf<PrimaryKeyConvention>();

                    foreach (var overrideAssembly in overrideAssemblies)
                    {
                        autoPersistanceModel.UseOverridesFromAssembly(overrideAssembly);
                    }

                    return autoPersistanceModel;
                }))
                .ExposeConfiguration(config => new SchemaUpdate(config).Execute(true, true));

            var sessionfactory = fluentConfiguration.BuildSessionFactory();

            return sessionfactory;
        }
    }
}
