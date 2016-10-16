using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
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
    }
}

