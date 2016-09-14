using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using zavit.Domain.Venues;

namespace zavit.Infrastructure.Venues.Overrides
{
    public class VenueOverride : IAutoMappingOverride<Venue>
    {
        public void Override(AutoMapping<Venue> mapping)
        {
            mapping.HasManyToMany(x => x.Activities);
        }
    }
}