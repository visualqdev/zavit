using zavit.Domain.Profiles;

namespace zavit.Web.Api.DtoServices.Profiles
{
    public class ProfileImageUrlBuilder : IProfileImageUrlBuilder
    {
        public string Build(Profile profile)
        {
            return profile.ProfileImage == null 
                ? null 
                : $"/api/profileimages/{profile.ProfileImage.Id}";
        }
    }
}