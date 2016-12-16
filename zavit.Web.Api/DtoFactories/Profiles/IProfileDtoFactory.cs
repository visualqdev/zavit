using zavit.Domain.Profiles;
using zavit.Web.Api.Dtos.Profiles;

namespace zavit.Web.Api.DtoFactories.Profiles
{
    public interface IProfileDtoFactory
    {
        ProfileDto CreateItem(Profile profile);
    }
}