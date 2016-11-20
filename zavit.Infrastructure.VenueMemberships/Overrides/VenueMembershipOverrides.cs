using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using zavit.Domain.VenueMemberships;

namespace zavit.Infrastructure.VenueMemberships.Overrides
{
    public class VenueMembershipOverrides : IAutoMappingOverride<VenueMembership>
    {
        public void Override(AutoMapping<VenueMembership> mapping)
        {
            mapping.HasManyToMany(x => x.Activities);
        }
    }
}