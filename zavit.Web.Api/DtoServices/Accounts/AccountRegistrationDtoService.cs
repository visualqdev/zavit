using System.Threading.Tasks;
using zavit.Domain.Accounts;
using zavit.Domain.Accounts.Registrations;
using zavit.Web.Api.Dtos.Accounts;

namespace zavit.Web.Api.DtoServices.Accounts
{
    public class AccountRegistrationDtoService : IAccountRegistrationDtoService
    {
        readonly IAccountProfileRegistrationFactory _accountProfileRegistrationFactory;
        readonly IAccountService _accountService;

        public AccountRegistrationDtoService(IAccountProfileRegistrationFactory accountProfileRegistrationFactory, IAccountService accountService)
        {
            _accountProfileRegistrationFactory = accountProfileRegistrationFactory;
            _accountService = accountService;
        }

        public async Task<AccountRegistrationResult> Register(AccountRegistrationDto accountRegistrationDto)
        {
            var accountProfileRegistration = _accountProfileRegistrationFactory.CreateItem(accountRegistrationDto);

            var accountRegistrationResult = await _accountService.Register(accountProfileRegistration);
            return accountRegistrationResult;
        }
    }
}