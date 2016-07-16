using Machine.Specifications;
using Rhino.Mspec.Contrib;

namespace zavit.Domain.Accounts.Tests 
{
    [Subject("AccountServiceTests")]
    public class AccountServiceTests : TestOf<AccountServiceTests>
    {
        class When_registering_a_new_account_but_an_account_with_a_same_username_already_exists
        {
            Because of = () => ;

            It should_ = () => ;

            Establish context = () => {};
        }
    }
}

