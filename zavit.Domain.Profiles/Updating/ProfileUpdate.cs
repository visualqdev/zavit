using zavit.Domain.Profiles;

namespace zavit.Domain.Accounts.Updating
{
    public class ProfileUpdate
    {
        public string DisplayName { get; set; }
        public Gender Gender { get; set; }
        public string About { get; set; }
        public string Email { get; set; }
    }
}