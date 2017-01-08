using zavit.Domain.Accounts;
using zavit.Domain.Accounts.Registrations;
using zavit.Domain.Profiles;

namespace zavit.Web.Api.DtoServices.Accounts
{
    public class AccountProfileRegistration : IAccountRegistration
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public AccountType AccountType { get; set; }
        public Gender Gender { get; set; }
        public byte[] ProfileImage => null;
    }
}