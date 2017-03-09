
using System.Threading.Tasks;

namespace zavit.Domain.Profiles.Registration
{
    public interface IProfileCreator
    {
        Task<Profile> CreateProfile(IProfileRegistration accountProfileRegistration);
    }
}