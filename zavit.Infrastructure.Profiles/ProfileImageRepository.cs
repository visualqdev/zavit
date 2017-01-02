using NHibernate;
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

        public ProfileImage GetByAccountId(int accountId)
        {
            return _session.QueryOver<Profile>()
                .Where(p => p.Account.Id == accountId)
                .Select(p => p.ProfileImage)
                .SingleOrDefault<ProfileImage>();
        }

        public ProfileImage Get(int profileImageId)
        {
            return _session.QueryOver<ProfileImage>()
                .Where(p => p.Id == profileImageId)
                .SingleOrDefault<ProfileImage>();
        }
    }
}