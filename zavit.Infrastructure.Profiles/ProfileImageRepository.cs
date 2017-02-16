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
        
        public ProfileImage Get(int profileImageId)
        {
            return _session.QueryOver<ProfileImage>()
                .Where(p => p.Id == profileImageId)
                .SingleOrDefault<ProfileImage>();
        }

        public void RemoveImage(ProfileImage profileImage)
        {
            _session.Delete(profileImage);
            _session.Flush();
        }
    }
}