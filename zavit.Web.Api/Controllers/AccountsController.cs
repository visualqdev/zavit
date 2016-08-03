using System.Web.Http;
using zavit.Domain.Accounts;
using zavit.Web.Api.Dtos.Accounts;

namespace zavit.Web.Api.Controllers
{
    public class AccountsController : ApiController
    {
        readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public IHttpActionResult Post(AccountRegistrationDto accountDto)
        {
            var registrationResult = _accountService.Register(accountDto);

            if (registrationResult.Success) return Ok();

            return BadRequest(registrationResult.ErrorMessage);
        }
    }
}