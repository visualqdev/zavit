using Machine.Specifications;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging.MessageThreads;

namespace zavit.Domain.Messaging.Tests.MessageThreads 
{
    [Subject("MessageThreadTitleBuilder")]
    public class MessageThreadTitleBuilderTests : TestOf<MessageThreadTitleBuilder>
    {
        Because of = () => _result = Subject.BuildTitle(_messageThread, RequestedByAccountId);

        It should_return_the_concat_display_names_of_all_participants_except_the_account_that_the_onbox_belongs_to =
            () => _result.ShouldEqual("User 1, User 2");

        Establish context = () =>
        {
            var ownerAccount = NewInstanceOf<Account>();
            ownerAccount.Id = RequestedByAccountId;
            ownerAccount.DisplayName = "Owner";

            var participantAccount = NewInstanceOf<Account>();
            participantAccount.Id = 234;
            participantAccount.DisplayName = "User 1";

            var otherParticipantAccount = NewInstanceOf<Account>();
            otherParticipantAccount.Id = 345;
            otherParticipantAccount.DisplayName = "User 2";

            _messageThread = NewInstanceOf<MessageThread>();
            _messageThread.Participants = new[] { ownerAccount, participantAccount, otherParticipantAccount };
        };

        static MessageThread _messageThread;
        const int RequestedByAccountId = 123;
        static string _result;
    }
}

