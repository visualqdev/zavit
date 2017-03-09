using System;
using System.IO;
using System.Threading.Tasks;

namespace zavit.Domain.Profiles.ProfileImages
{
    public interface IProfileImageStorage
    {
        Task SaveImage(string imageName, Stream image);
        string ImageUrl(string imageName);
    }
}