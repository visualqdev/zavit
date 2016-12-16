using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Profiles.Registration;

namespace zavit.Domain.Profiles.Tests.Registration 
{
    [Subject("ProfileAccountRegistrationObserver")]
    public class ProfileAccountRegistrationObserverTests : TestOf<ProfileAccountRegistrationObserver>
    {
        class When_notified_that_account_has_been_registered
        {
            Because of = () => Subject.AccountRegsitered(_account);

            It should_save_new_profile_for_the_account = () => Injected<IProfileRepository>().AssertWasCalled(r => r.Save(_profile));

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();

                _profile = NewInstanceOf<Profile>();
                Injected<IProfileCreator>().Stub(c => c.CreateProfile(_account)).Return(_profile);
            };

            static Account _account;
            static Profile _profile;
        }
    }
}

