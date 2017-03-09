using System.IO;
using System.Threading.Tasks;

namespace zavit.Domain.Profiles.ProfileImages
{
    public interface IProfileImageCreator
    {
        Task<string> Create(Stream image);
    }
}