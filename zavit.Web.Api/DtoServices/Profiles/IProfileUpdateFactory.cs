using zavit.Domain.Accounts.Updating;
using zavit.Web.Api.Dtos.Profiles;

namespace zavit.Web.Api.DtoServices.Profiles
{
    public interface IProfileUpdateFactory
    {
        ProfileUpdate CreateItem(ProfileDto profileDto);
    }
}