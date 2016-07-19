using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using zavit.Infrastructure.Orm.Entities;

namespace zavit.Infrastructure.Orm.Overrides
{
    public class TestEntityMappingOverride : IAutoMappingOverride<TestEntity>
    {
        public void Override(AutoMapping<TestEntity> mapping)
        {
            mapping.Table("Profiles");
            mapping.Id(p => p.ProfileId);
        }
    }
}