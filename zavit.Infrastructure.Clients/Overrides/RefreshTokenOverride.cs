using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using zavit.Domain.Clients.Tokens;

namespace zavit.Infrastructure.Clients.Overrides
{
    public class RefreshTokenOverride : IAutoMappingOverride<RefreshToken>
    {
        public void Override(AutoMapping<RefreshToken> mapping)
        {
            mapping.References(x => x.Client).Cascade.None();
        }
    }
}