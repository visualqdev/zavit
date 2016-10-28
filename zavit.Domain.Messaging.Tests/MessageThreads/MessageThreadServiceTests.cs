using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging.MessageThreads;

namespace zavit.Domain.Messaging.Tests.MessageThreads 
{
    [Subject("MessageThreadService")]
    public class MessageThreadServiceTests : TestOf<MessageThreadService>
    {
        class When_creating_a_new_message_thread
        {
            Because of = () => _result = Subject.CreateNewThread(_newMessageThreadRequest);

            It should_return_the_new_message_thread = () => _result.ShouldEqual(_messageThread);

            Establish context = () =>
            {
                _newMessageThreadRequest = NewInstanceOf<NewMessageThreadRequest>();

                _messageThread = NewInstanceOf<MessageThread>();
                Injected<INewMessageThreadProvider>()
                    .Stub(p => p.Provide(_newMessageThreadRequest))
                    .Return(_messageThread);

                Injected<IMessageThreadRepository>().Stub(r => r.Save(_messageThread));
            };

            static NewMessageThreadRequest _newMessageThreadRequest;
            static MessageThread _result;
            static MessageThread _messageThread;
        }

        class When_getting_a_message_thread
        {
            Because of = () => _result = Subject.GetMessageThread(ThreadId);

            It should_return_a_message_thread_from_the_repository = () => _result.ShouldEqual(_messageThread);

            Establish context = () =>
            {
                _messageThread = NewInstanceOf<MessageThread>();
                Injected<IMessageThreadRepository>().Stub(r => r.GetMessageThread(ThreadId)).Return(_messageThread);
            };

            static int ThreadId;
            static MessageThread _result;
            static MessageThread _messageThread;
        }

        class When_getting_message_threads
        {
            Because of = () => _result = Subject.GetMessageInbox(_account);

            It should_return_the_message_inbox_from_repository_for_the_specified_user = 
                () => _result.ShouldEqual(_messageInbox);

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();
                _account.Id = 456;

                _messageInbox = NewInstanceOf<IMessageInbox>();
                Injected<IMessageThreadRepository>()
                    .Stub(r => r.GetInbox(_account.Id))
                    .Return(_messageInbox);
            };

            static IMessageInbox _result;
            static Account _account;
            static IMessageInbox _messageInbox;
        }
    }
}

