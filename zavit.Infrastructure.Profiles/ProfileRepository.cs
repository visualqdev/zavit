using NHibernate;
using zavit.Domain.Profiles;

namespace zavit.Infrastructure.Profiles
{
    public class ProfileRepository : IProfileRepository
    {
        readonly ISession _session;

        public ProfileRepository(ISession session)
        {
            _session = session;
        }

        public void Update(Profile profile)
        {
            _session.Update(profile);
            _session.Flush();
        }

        public Profile GetForAccount(int accountId)
        {
            var profile = _session.QueryOver<Profile>()
                .Where(p => p.Account.Id == accountId)
                .SingleOrDefault();

            return profile;
        }

        public void Save(Profile profile)
        {
            _session.Save(profile);
            _session.Flush();
        }
    }
}
