using zavit.Domain.Accounts;
using zavit.Domain.Profiles;
using zavit.Domain.Profiles.Registration;
using zavit.Web.Api.Dtos.Accounts;

namespace zavit.Web.Api.DtoServices.Accounts
{
    public class AccountProfileRegistrationFactory : IAccountProfileRegistrationFactory
    {
        public IAccountProfileRegistration CreateItem(AccountRegistrationDto accountRegistrationDto)
        {
            return new AccountProfileRegistration
            {
                AccountType = AccountType.Internal,
                Gender = Gender.NotSpecified,
                DisplayName = accountRegistrationDto.DisplayName,
                Username = accountRegistrationDto.Username,
                Email = accountRegistrationDto.Username,
                Password = accountRegistrationDto.Password
            };
        }
    }
}