using NHibernate;
using NHibernate.SqlCommand;
using zavit.Domain.Profiles;

namespace zavit.Infrastructure.Profiles
{
    public class ProfileImageRepository : IProfileImageRepository
    {
        readonly ISession _session;

        public ProfileImageRepository(ISession session)
        {
            _session = session;
        }

        public ProfileImage Get(int accountId)
        {
            return _session.QueryOver<Profile>()
                .Where(p => p.Account.Id == accountId)
                .Select(p => p.ProfileImage)
                .SingleOrDefault<ProfileImage>();
        }
    }
}