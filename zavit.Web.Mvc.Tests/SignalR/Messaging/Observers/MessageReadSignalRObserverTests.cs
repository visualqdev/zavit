using System;
using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Messaging.Messages;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.DtoFactories;
using zavit.Web.Mvc.SignalR.Messaging.Observers;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.Dtos;

namespace zavit.Web.Mvc.Tests.SignalR.Messaging.Observers 
{
    [Subject("MessageReadSignalRObserver")]
    public class MessageReadSignalRObserverTests : TestOf<MessageReadSignalRObserver>
    {
        class When_messages_have_been_read
        {
            Because of = () => Subject.MessagesRead(_completelyReadMessages, MessageThreadId);

            It should_broadcast_all_of_the_read_message_broadcast_requests =
                () =>
                {
                    Injected<IMessagingBroadcaster>().AssertWasCalled(b => b.ThreadMessageRead(_readMessagesBroadcastDto));
                    Injected<IMessagingBroadcaster>().AssertWasCalled(b => b.ThreadMessageRead(_otherReadMessagesBroadcastDto));

                };

            Establish context = () =>
            {
                _completelyReadMessages = new List<Message> { NewInstanceOf<Message>() };

                _readMessagesBroadcastDto = NewInstanceOf<BroadcastRequest<ReadMessagesDto>>();
                _otherReadMessagesBroadcastDto = NewInstanceOf<BroadcastRequest<ReadMessagesDto>>();

                Injected<IReadMessagesBroadcastRequestsProvider>()
                    .Stub(f => f.Provide(_completelyReadMessages, MessageThreadId))
                    .Return(new[] { _readMessagesBroadcastDto, _otherReadMessagesBroadcastDto });
            };

            static IList<Message> _completelyReadMessages;
            static BroadcastRequest<ReadMessagesDto> _readMessagesBroadcastDto;
            static BroadcastRequest<ReadMessagesDto> _otherReadMessagesBroadcastDto;
            const int MessageThreadId = 123;
        }
    }
}

