using zavit.Domain.Profiles;

namespace zavit.Web.Api.DtoServices.Profiles
{
    public interface IProfileImageUrlBuilder
    {
        string Build(Profile profile);
    }
}