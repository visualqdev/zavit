using System.Web.Mvc;
using zavit.Domain.Accounts;

namespace zavit.Web.Mvc.Controllers
{
    public class AccountVerificationController : Controller
    {
        readonly IAccountService _accountService;
        public const string SuccessMessage = "Thank you for verifying your account.";
        public const string FailureMessage = "Sorry, we couldn't use this verification link. The verification link is invalid or expired.";

        public AccountVerificationController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public ViewResult Verify(string id)
        {
            var verificatioSuccessful = _accountService.VerifyAccount(id);
            if (verificatioSuccessful)
                return View("Index", (object)SuccessMessage);

            return View("Index", (object)FailureMessage);
        }
    }
}