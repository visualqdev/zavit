using zavit.Domain.Accounts;

namespace zavit.Domain.Profiles.Updating
{
    public class ProfileUpdate
    {
        public Account Account { get; set; }
        public string DisplayName { get; set; }
        public Gender Gender { get; set; }
        public string About { get; set; }
        public string Email { get; set; }
    }
}