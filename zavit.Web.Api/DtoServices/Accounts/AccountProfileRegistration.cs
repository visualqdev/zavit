using zavit.Domain.Accounts;
using zavit.Domain.Profiles;
using zavit.Domain.Profiles.Registration;

namespace zavit.Web.Api.DtoServices.Accounts
{
    public class AccountProfileRegistration : IAccountProfileRegistration
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public AccountType AccountType { get; set; }
        public Gender Gender { get; set; }
    }
}