using zavit.Web.Api.Dtos.Profiles;

namespace zavit.Web.Api.DtoServices.Profiles
{
    public interface IProfileDtoService
    {
        ProfileDto Update(ProfileDto profiledDto);
        ProfileDto GetMyProfile();
    }
}