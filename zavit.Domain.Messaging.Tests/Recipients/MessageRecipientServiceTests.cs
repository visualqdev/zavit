using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging.Recipients;
using zavit.Domain.Shared.ResultCollections;

namespace zavit.Domain.Messaging.Tests.Recipients 
{
    [Subject("MessageRecipientService")]
    public class MessageRecipientServiceTests : TestOf<MessageRecipientService>
    {
        class When_suggesting_recipients
        {
            Because of = () => _result = Subject.SuggestRecipients(SearchTerm, Skip, Take, _account);

            It should_return_recipient_for_the_suggested_term = () => _result.ShouldEqual(_accountResultCollection);

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();
                _account.Id = 123;

                _accountResultCollection = NewInstanceOf<IResultCollection<Account>>();

                Injected<IAccountRepository>()
                    .Stub(a => a.Search(SearchTerm, Skip, Take, _account.Id))
                    .Return(_accountResultCollection);
            };

            static IResultCollection<Account> _result;
            static IResultCollection<Account> _accountResultCollection;
            static Account _account;
            const string SearchTerm = "Test term";
            const int Skip = 2;
            const int Take = 5;
        }
    }
}

