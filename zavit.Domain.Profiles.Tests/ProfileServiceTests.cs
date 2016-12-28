using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Accounts.Registrations;
using zavit.Domain.Profiles.Registration;
using zavit.Domain.Profiles.Updating;

namespace zavit.Domain.Profiles.Tests 
{
    [Subject("ProfileService")]
    public class ProfileServiceTests : TestOf<ProfileService>
    {
        class When_updating_successfully_updating_profile
        {
            Because of = () => _result = Subject.Update(_profileUpdate);

            It should_save_the_updated_profile = () => Injected<IProfileRepository>().AssertWasCalled(r => r.Update(_profile));

            It should_return_the_updated_profile = () => _result.ShouldEqual(_profile);

            Establish context = () =>
            {
                _profileUpdate = NewInstanceOf<ProfileUpdate>();
                _profileUpdate.Account = NewInstanceOf<Account>();
                _profileUpdate.Account.Id = 132;

                _profile = NewInstanceOf<Profile>();
                Injected<IProfileRepository>().Stub(r => r.GetForAccount(_profileUpdate.Account.Id)).Return(_profile);

                var updaters = (List<IProfileUpdater>) Injected<IEnumerable<IProfileUpdater>>();
                updaters.Add(NewInstanceOf<IProfileUpdater>());
                _profile.Stub(p => p.AcceptUpdate(_profileUpdate, updaters)).Return(true);
            };

            static ProfileUpdate _profileUpdate;
            static Profile _result;
            static Profile _profile;
        }

        class When_updating_profile_but_the_profile_does_not_accept_the_update
        {
            Because of = () => _result = Subject.Update(_profileUpdate);

            It should_not_try_to_save_the_profile = () => Injected<IProfileRepository>().AssertWasNotCalled(r => r.Update(_profile));

            It should_return_the_original_profile = () => _result.ShouldEqual(_profile);

            Establish context = () =>
            {
                _profileUpdate = NewInstanceOf<ProfileUpdate>();
                _profileUpdate.Account = NewInstanceOf<Account>();
                _profileUpdate.Account.Id = 132;

                _profile = NewInstanceOf<Profile>();
                Injected<IProfileRepository>().Stub(r => r.GetForAccount(_profileUpdate.Account.Id)).Return(_profile);

                var updaters = (List<IProfileUpdater>)Injected<IEnumerable<IProfileUpdater>>();
                updaters.Add(NewInstanceOf<IProfileUpdater>());
                _profile.Stub(p => p.AcceptUpdate(_profileUpdate, updaters)).Return(false);
            };

            static ProfileUpdate _profileUpdate;
            static Profile _result;
            static Profile _profile;
        }

        class When_registering_account_profile_successfully
        {
            Because of = () => _result = Subject.Register(_accountProfileRegistration);

            It should_return_the_successful_account_registration_result = () => _result.ShouldEqual(_accountRegistrationResult);

            It should_save_the_newly_created_profile_for_the_newly_registered_account =
                () => Injected<IProfileRepository>().AssertWasCalled(r => r.Save(_profile));

            Establish context = () =>
            {
                _accountProfileRegistration = NewInstanceOf<IAccountProfileRegistration>();
                
                _accountRegistrationResult = new AccountRegistrationResult
                {
                    Success = true,
                    Account = NewInstanceOf<Account>()
                };

                Injected<IAccountService>()
                    .Stub(s => s.Register(_accountProfileRegistration))
                    .Return(_accountRegistrationResult);

                _profile = NewInstanceOf<Profile>();
                Injected<IProfileCreator>()
                    .Stub(c => c.CreateProfile(_accountRegistrationResult.Account, _accountProfileRegistration))
                    .Return(_profile);
            };

            static IAccountProfileRegistration _accountProfileRegistration;
            static AccountRegistrationResult _accountRegistrationResult;
            static AccountRegistrationResult _result;
            static Profile _profile;
        }

        class When_registering_account_and_account_cannot_be_registered
        {
            Because of = () => _result = Subject.Register(_accountProfileRegistration);

            It should_return_the_unsuccessful_account_registration_result = () => _result.ShouldEqual(_accountRegistrationResult);

            It should_not_try_to_create_a_profile = () => Injected<IProfileCreator>().AssertWasNotCalled(r => r.CreateProfile(Arg<Account>.Is.Anything, Arg<IAccountProfileRegistration>.Is.Anything));

            Establish context = () =>
            {
                _accountProfileRegistration = NewInstanceOf<IAccountProfileRegistration>();

                _accountRegistrationResult = new AccountRegistrationResult
                {
                    Success = false
                };

                Injected<IAccountService>()
                    .Stub(s => s.Register(_accountProfileRegistration))
                    .Return(_accountRegistrationResult);
            };

            static IAccountProfileRegistration _accountProfileRegistration;
            static AccountRegistrationResult _accountRegistrationResult;
            static AccountRegistrationResult _result;
        }
    }
}

