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

        public void Save(Profile profile)
        {
            _session.Save(profile);
            _session.Flush();
        }
    }
}
