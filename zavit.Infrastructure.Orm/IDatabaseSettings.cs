using FluentNHibernate.Cfg.Db;

namespace zavit.Infrastructure.Orm
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; }
    }
}