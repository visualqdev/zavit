using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging;
using zavit.Domain.Messaging.MessageReads;
using zavit.Domain.Messaging.MessageThreads;
using zavit.Domain.Shared.ResultCollections;
using zavit.Web.Api.DtoFactories.Messaging.Messages;
using zavit.Web.Api.DtoFactories.Messaging.MessageThreads;
using zavit.Web.Api.Dtos.Messaging.Messages;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.Tests.DtoFactories.Messaging.MessageThreads 
{
    [Subject("InboxThreadDetailsDtoFactory")]
    public class InboxThreadDetailsDtoFactoryTests : TestOf<InboxThreadDetailsDtoFactory>
    {
        class When_creating_inbox_thread_details_dto
        {
            Because of = () => _result = Subject.CreateItem(_messageThread, _messageResultsCollection);

            It should_set_the_thread_title_to_be_the_title_built_by_message_thread_title_builder = 
                () => _result.ThreadTitle.ShouldEqual(ThreadTitle);

            It should_set_the_thread_id_to_be_the_same_as_message_thread_id =
                () => _result.ThreadId.ShouldEqual(_messageThread.Id);

            It should_set_the_messages_to_be_messages_collection_dto_created_from_message_results =
                () => _result.MessagesCollection.ShouldEqual(_messagesCollectionDto);

            Establish context = () =>
            {
                _messageResultsCollection = NewInstanceOf<IResultCollection<MessageInfo>>();
                _messageThread = NewInstanceOf<MessageThread>();
                _messageThread.Id = 234;

                var account = NewInstanceOf<Account>();
                account.Id = 123;
                Injected<IUserContext>().Stub(c => c.Account).Return(account);

                Injected<IMessageThreadTitleBuilder>()
                    .Stub(b => b.BuildTitle(_messageThread, account.Id))
                    .Return(ThreadTitle);

                _messagesCollectionDto = NewInstanceOf<MessagesCollectionDto>();
                Injected<IMessageCollectionDtoFactory>()
                    .Stub(f => f.CreateItem(_messageResultsCollection))
                    .Return(_messagesCollectionDto);
            };

            static IResultCollection<MessageInfo> _messageResultsCollection;
            static MessageThread _messageThread;
            static InboxThreadDetailsDto _result;
            static MessagesCollectionDto _messagesCollectionDto;
            const string ThreadTitle = "Test title";
        }
    }
}

