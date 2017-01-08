using System.Web.Http;
using System.Web.Http.Results;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts.Registrations;
using zavit.Web.Api.Controllers;
using zavit.Web.Api.Dtos.Accounts;
using zavit.Web.Api.DtoServices.Accounts;

namespace zavit.Web.Api.Tests.Controllers 
{
    [Subject("AccountsController")]
    public class AccountsControllerTests : TestOf<AccountsController>
    {
        class When_registering_a_new_account_successfully
        {
            Because of = () => _result = Subject.Post(_accountDto);

            It should_return_an_ok_result = () => _result.ShouldBeAssignableTo<OkResult>();

            Establish context = () =>
            {
                _accountDto = NewInstanceOf<AccountRegistrationDto>();

                var registrationResult = NewInstanceOf<AccountRegistrationResult>();
                registrationResult.Success = true;
                Injected<IAccountRegistrationDtoService>().Stub(s => s.Register(_accountDto)).Return(registrationResult);
            };

            static AccountRegistrationDto _accountDto;
            static IHttpActionResult _result;
        }

        class When_registering_a_new_account_unsuccessfully
        {
            Because of = () => _result = Subject.Post(_accountDto);

            It should_return_an_bad_request_result = () => _result.ShouldBeAssignableTo<BadRequestErrorMessageResult>();

            It should_add_an_error_message_to_the_response =
                () => ((BadRequestErrorMessageResult) _result).Message.ShouldEqual(_registrationResult.ErrorMessage);

            Establish context = () =>
            {
                _accountDto = NewInstanceOf<AccountRegistrationDto>();

                _registrationResult = NewInstanceOf<AccountRegistrationResult>();
                _registrationResult.Success = false;
                _registrationResult.ErrorMessage = "Test Error message";
                Injected<IAccountRegistrationDtoService>().Stub(s => s.Register(_accountDto)).Return(_registrationResult);
            };

            static AccountRegistrationDto _accountDto;
            static IHttpActionResult _result;
            static AccountRegistrationResult _registrationResult;
        }
    }
}

