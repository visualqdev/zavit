using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Messaging.Messages;

namespace zavit.Domain.Messaging.Tests.Messages 
{
    [Subject("MessageService")]
    public class MessageServiceTests : TestOf<MessageService>
    {
        class When_sending_a_message_on_a_thread
        {
            Because of = () => _result = Subject.SendMessageOnThread(_newMessageRequest, _messageThread);

            It should_return_a_new_message = () => _result.ShouldEqual(_message);

            It should_save_the_new_message = () => Injected<IMessageRepository>().AssertWasCalled(r => r.Save(_message));

            Establish context = () =>
            {
                _newMessageRequest = NewInstanceOf<NewMessageRequest>();
                _messageThread = NewInstanceOf<MessageThread>();

                _message = NewInstanceOf<Message>();
                Injected<INewMessageProvider>().Stub(p => p.Provide(_newMessageRequest, _messageThread)).Return(_message);
            };

            static MessageThread _messageThread;
            static Message _result;
            static NewMessageRequest _newMessageRequest;
            static Message _message;
        }
    }
}

