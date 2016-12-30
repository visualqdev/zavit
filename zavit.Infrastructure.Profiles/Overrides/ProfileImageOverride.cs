using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using zavit.Domain.Profiles;

namespace zavit.Infrastructure.Profiles.Overrides
{
    public class ProfileImageOverride : IAutoMappingOverride<ProfileImage>
    {
        public void Override(AutoMapping<ProfileImage> mapping)
        {
            mapping.Map(x => x.ImageFile).Column("ImageFile").CustomSqlType("VARBINARY(MAX)").Length(160000);
        }
    }
}