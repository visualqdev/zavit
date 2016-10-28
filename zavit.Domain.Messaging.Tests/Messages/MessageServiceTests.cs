using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging.MessageReads;
using zavit.Domain.Messaging.Messages;
using zavit.Domain.Messaging.MessageThreads;
using zavit.Domain.Shared.ResultCollections;

namespace zavit.Domain.Messaging.Tests.Messages 
{
    [Subject("MessageService")]
    public class MessageServiceTests : TestOf<MessageService>
    {
        class When_sending_a_message_on_a_thread
        {
            Because of = () => _result = Subject.SendMessageOnThread(_newMessageRequest, _messageThread);

            It should_return_a_new_message = () => _result.ShouldEqual(_message);

            It should_add_message_to_the_thread = () => _message.AssertWasCalled(m => m.AddToThread(_messageThread));

            It should_save_the_new_message = () => Injected<IMessageRepository>().AssertWasCalled(r => r.Save(_message));

            Establish context = () =>
            {
                _newMessageRequest = NewInstanceOf<NewMessageRequest>();
                _messageThread = NewInstanceOf<MessageThread>();

                _message = NewInstanceOf<Message>();
                Injected<INewMessageProvider>().Stub(p => p.Provide(_newMessageRequest)).Return(_message);
            };

            static MessageThread _messageThread;
            static Message _result;
            static NewMessageRequest _newMessageRequest;
            static Message _message;
        }

        class When_sending_a_message_on_a_thread_specified_by_thread_id
        {
            Because of = () => _result = Subject.SendMessageOnThread(_newMessageRequest, MessageThreadId);

            It should_return_a_new_message = () => _result.ShouldEqual(_message);

            It should_add_message_to_the_thread = () => _message.AssertWasCalled(m => m.AddToThread(_messageThread));

            It should_save_the_new_message = () => Injected<IMessageRepository>().AssertWasCalled(r => r.Save(_message));

            Establish context = () =>
            {
                _newMessageRequest = NewInstanceOf<NewMessageRequest>();
                _messageThread = NewInstanceOf<MessageThread>();
                Injected<IMessageThreadRepository>()
                    .Stub(r => r.GetMessageThread(MessageThreadId))
                    .Return(_messageThread);

                _message = NewInstanceOf<Message>();
                Injected<INewMessageProvider>().Stub(p => p.Provide(_newMessageRequest)).Return(_message);
            };
            
            static Message _result;
            static NewMessageRequest _newMessageRequest;
            static Message _message;
            static MessageThread _messageThread;
            const int MessageThreadId = 123;
        }

        class When_getting_messages
        {
            Because of = () => _result = Subject.GetMessages(MessageThreadId, OlderThanMessageId, Take, _account);

            It should_return_messages_result_collection_from_message_repository = () => _result.ShouldEqual(_messageResultCollection);

            It should_tell_the_message_read_service_that_user_has_accessed_messages_on_a_thread =
                () => Injected<IMessageReadService>().AssertWasCalled(s => s.MessagesRead(MessageThreadId, _account));

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();
                _account.Id = 44;

                _messageResultCollection = NewInstanceOf<IResultCollection<MessageInfo>>();
                Injected<IMessageRepository>()
                    .Stub(r => r.GetMessages(MessageThreadId, OlderThanMessageId, Take))
                    .Return(_messageResultCollection);
            };

            static Account _account;
            static readonly int? OlderThanMessageId = 123;
            static IResultCollection<MessageInfo> _result;
            static IResultCollection<MessageInfo> _messageResultCollection;
            const int MessageThreadId = 456;
            const int Take = 2;
        }
    }
}

