using System.Web.Mvc;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Web.Mvc.Controllers;

namespace zavit.Web.Mvc.Tests.Controllers 
{
    [Subject("AccountVerificationController")]
    public class AccountVerificationControllerTests : TestOf<AccountVerificationController>
    {
        class When_verifying_an_account_and_the_provided_verification_code_is_valid
        {
            Because of = () => _result = Subject.Verify(Id);

            It should_return_a_default_view = () => _result.ViewName.ShouldEqual("Index");

            It should_set_model_to_verification_success_message = () => _result.Model.ShouldEqual(AccountVerificationController.SuccessMessage);

            Establish context = () =>
            {
                Injected<IAccountService>().Stub(a => a.VerifyAccount(Id)).Return(true);
            };

            static ViewResult _result;
            const string Id = "Verification Code";
        }

        class When_verifying_an_account_and_the_provided_verification_code_is_not_valid
        {
            Because of = () => _result = Subject.Verify(Id);

            It should_return_a_default_view = () => _result.ViewName.ShouldEqual("Index");

            It should_set_model_to_verification_failure_message = () => _result.Model.ShouldEqual(AccountVerificationController.FailureMessage);

            Establish context = () =>
            {
                Injected<IAccountService>().Stub(a => a.VerifyAccount(Id)).Return(false);
            };

            static ViewResult _result;
            const string Id = "Verification Code";
        }
    }
}

