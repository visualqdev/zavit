using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace zavit.Infrastructure.Orm.Conventions
{
    public class PrimaryKeyConvention : IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            instance.Column(instance.EntityType.Name + "Id");
        }
    }
}