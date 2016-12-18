using zavit.Domain.Profiles;

namespace zavit.Web.Api.Dtos.Profiles
{
    public class ProfileDto
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string About { get; set; }
    }
}