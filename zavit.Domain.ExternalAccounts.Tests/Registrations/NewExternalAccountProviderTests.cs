using Machine.Specifications;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.ExternalAccounts.Registrations;

namespace zavit.Domain.ExternalAccounts.Tests.Registrations 
{
    [Subject("NewExternalAccountProvider")]
    public class NewExternalAccountProviderTests : TestOf<NewExternalAccountProvider>
    {
        class When_providing_an_external_account
        {
            Because of = () => _result = Subject.Provide(_account, LoginProvider, ProviderKey);

            It should_set_the_account = () => _result.Account.ShouldEqual(_account);

            It should_set_the_login_provider = () => _result.LoginProvider.ShouldEqual(LoginProvider);

            It should_set_the_provider_key = () => _result.ProviderKey.ShouldEqual(ProviderKey);

            Establish context = () => {};

            static ExternalAccount _result;
            static Account _account;
            const string ProviderKey = "Provider key";
            const string LoginProvider = "Login provider";
        }
    }
}

