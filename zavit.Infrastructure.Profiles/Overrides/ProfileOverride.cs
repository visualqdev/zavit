using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using zavit.Domain.Profiles;

namespace zavit.Infrastructure.Profiles.Overrides
{
    public class ProfileOverride : IAutoMappingOverride<Profile>
    {
        public void Override(AutoMapping<Profile> mapping)
        {
            mapping.References(x => x.ProfileImage).Cascade.All();
        }
    }
}