using zavit.Domain.Accounts.Registrations;
using zavit.Domain.Profiles;
using zavit.Web.Api.Dtos.Accounts;

namespace zavit.Web.Api.DtoServices.Accounts
{
    public class AccountRegistrationDtoService : IAccountRegistrationDtoService
    {
        readonly IAccountProfileRegistrationFactory _accountProfileRegistrationFactory;
        readonly IProfileService _profileService;

        public AccountRegistrationDtoService(IAccountProfileRegistrationFactory accountProfileRegistrationFactory, IProfileService profileService)
        {
            _accountProfileRegistrationFactory = accountProfileRegistrationFactory;
            _profileService = profileService;
        }

        public AccountRegistrationResult Register(AccountRegistrationDto accountRegistrationDto)
        {
            var accountProfileRegistration = _accountProfileRegistrationFactory.CreateItem(accountRegistrationDto);

            var accountRegistrationResult = _profileService.Register(accountProfileRegistration);
            return accountRegistrationResult;
        }
    }
}