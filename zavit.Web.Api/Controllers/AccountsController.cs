using System.Threading.Tasks;
using System.Web.Http;
using zavit.Web.Api.Dtos.Accounts;
using zavit.Web.Api.DtoServices.Accounts;

namespace zavit.Web.Api.Controllers
{
    public class AccountsController : ApiController
    {
        readonly IAccountRegistrationDtoService _accountRegistrationDtoService;

        public AccountsController(IAccountRegistrationDtoService accountRegistrationDtoService)
        {
            _accountRegistrationDtoService = accountRegistrationDtoService;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(AccountRegistrationDto accountDto)
        {
            var registrationResult = await _accountRegistrationDtoService.Register(accountDto);

            if (registrationResult.Success) return Ok();

            return BadRequest(registrationResult.ErrorMessage);
        }
    }
}