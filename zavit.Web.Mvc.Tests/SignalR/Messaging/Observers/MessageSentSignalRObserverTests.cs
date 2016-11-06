using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Messaging.Messages;
using zavit.Web.Api.Dtos.Messaging.Messages;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.DtoFactories;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.Dtos;
using zavit.Web.Mvc.SignalR.Messaging.Observers;

namespace zavit.Web.Mvc.Tests.SignalR.Messaging.Observers 
{
    [Subject("MessageSentSignalRObserver")]
    public class MessageSentSignalRObserverTests : TestOf<MessageSentSignalRObserver>
    {
        class When_message_has_been_sent
        {
            Because of = () => Subject.MessageSent(_message);

            It should_broadcast_the_message_dto = () => Injected<IMessagingBroadcaster>().AssertWasCalled(b => b.ThreadMessageSent(_messageBroadcastRequest));

            Establish context = () =>
            {
                _message = NewInstanceOf<Message>();

                _messageBroadcastRequest = NewInstanceOf<BroadcastRequest<MessageDto>>();
                Injected<IThreadMessageBroadcastRequestFactory>().Stub(p => p.CreateItem(_message)).Return(_messageBroadcastRequest);
            };

            static Message _message;
            static BroadcastRequest<MessageDto> _messageBroadcastRequest;
        }
    }
}

