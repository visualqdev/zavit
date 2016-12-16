using Machine.Specifications;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Profiles.Registration;

namespace zavit.Domain.Profiles.Tests.Registration 
{
    [Subject("ProfileCreator")]
    public class ProfileCreatorTests : TestOf<ProfileCreator>
    {
        class When_creating_profile_from_account
        {
            Because of = () => _result = Subject.CreateProfile(_account);

            It should_set_the_account_to_the_provided_account = () => _result.Account.ShouldEqual(_account);

            It should_set_the_gender_to_value_not_specified = () => _result.Gender.ShouldEqual(Gender.NotSpecified);

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();
            };

            static Account _account;
            static Profile _result;
        }
    }
}

