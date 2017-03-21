using System.Threading.Tasks;
using zavit.Domain.Accounts.Registrations;
using zavit.Web.Api.Dtos.Accounts;

namespace zavit.Web.Api.DtoServices.Accounts
{
    public interface IAccountRegistrationDtoService
    {
        Task<AccountRegistrationResult> Register(AccountRegistrationDto accountRegistrationDto);
    }
}