﻿using System.Threading.Tasks;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Accounts.Registrations;
using zavit.Web.Api.Dtos.Accounts;
using zavit.Web.Api.DtoServices.Accounts;

namespace zavit.Web.Api.Tests.DtoServices.Accounts 
{
    [Subject("AccountRegistrationDtoService")]
    public class AccountRegistrationDtoServiceTests : TestOf<AccountRegistrationDtoService>
    {
        class When_registering_account_using_registration_dto
        {
            Because of = () => _result = Subject.Register(_accountRegistrationDto).Result;

            It should_return_the_result_of_account_profile_registration = () => _result.ShouldEqual(_accountRegistrationResult);

            Establish context = () =>
            {
                _accountRegistrationDto = NewInstanceOf<AccountRegistrationDto>();

                var accountProfileRegistration = NewInstanceOf<IAccountRegistration>();
                Injected<IAccountProfileRegistrationFactory>()
                    .Stub(f => f.CreateItem(_accountRegistrationDto))
                    .Return(accountProfileRegistration);

                _accountRegistrationResult = NewInstanceOf<AccountRegistrationResult>();
                Injected<IAccountService>()
                    .Stub(s => s.Register(accountProfileRegistration))
                    .Return(Task.FromResult(_accountRegistrationResult));
            };

            static AccountRegistrationDto _accountRegistrationDto;
            static AccountRegistrationResult _result;
            static AccountRegistrationResult _accountRegistrationResult;
        }
    }
}

