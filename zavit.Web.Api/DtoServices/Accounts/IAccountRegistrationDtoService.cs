using zavit.Domain.Accounts.Registrations;
using zavit.Web.Api.Dtos.Accounts;

namespace zavit.Web.Api.DtoServices.Accounts
{
    public interface IAccountRegistrationDtoService
    {
        AccountRegistrationResult Register(AccountRegistrationDto accountRegistrationDto);
    }
}