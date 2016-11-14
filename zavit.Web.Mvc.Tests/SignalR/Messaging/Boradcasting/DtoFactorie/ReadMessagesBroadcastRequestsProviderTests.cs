using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging.Messages;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.DtoFactories;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.Dtos;

namespace zavit.Web.Mvc.Tests.SignalR.Messaging.Boradcasting.DtoFactorie 
{
    [Subject("ReadMessagesBroadcastRequestsProvider")]
    public class ReadMessagesBroadcastRequestsProviderTests : TestOf<ReadMessagesBroadcastRequestsProvider>
    {
        class When_providing_messages_read_broadcast_requests
        {
            Because of = () => _result = Subject.Provide(_completelyReadMessages, MessageThreadId);

            It should_return_the_broadcast_requests_for_all_senders = 
                () => _result.ShouldContainOnly(_broadcastRequest, _otherSenderBroadcastRequest);

            Establish context = () =>
            {
                var message = NewInstanceOf<Message>();
                message.Sender = NewInstanceOf<Account>();
                message.Sender.Id = 20;
                var otherMessage = NewInstanceOf<Message>();
                otherMessage.Sender = message.Sender;

                var messageFromOtherSender = NewInstanceOf<Message>();
                messageFromOtherSender.Sender = NewInstanceOf<Account>();
                messageFromOtherSender.Sender.Id = 21;
                var otherMessageFromOtherSender = NewInstanceOf<Message>();
                otherMessageFromOtherSender.Sender = messageFromOtherSender.Sender;

                _completelyReadMessages = new List<Message> { message, messageFromOtherSender, otherMessage, otherMessageFromOtherSender };

                _broadcastRequest = NewInstanceOf<BroadcastRequest<ReadMessagesDto>>();
                Injected<IReadMessagesBroadcastDtoFactory>()
                    .Stub(f => f.CreateItem(new[] {message, otherMessage}, MessageThreadId, message.Sender.Id))
                    .Return(_broadcastRequest);

                _otherSenderBroadcastRequest = NewInstanceOf<BroadcastRequest<ReadMessagesDto>>();
                Injected<IReadMessagesBroadcastDtoFactory>()
                    .Stub(f => f.CreateItem(new[] { messageFromOtherSender, otherMessageFromOtherSender }, MessageThreadId, messageFromOtherSender.Sender.Id))
                    .Return(_otherSenderBroadcastRequest);
            };

            const int MessageThreadId = 123;
            static IList<Message> _completelyReadMessages;
            static IEnumerable<BroadcastRequest<ReadMessagesDto>> _result;
            static BroadcastRequest<ReadMessagesDto> _otherSenderBroadcastRequest;
            static BroadcastRequest<ReadMessagesDto> _broadcastRequest;
        }
    }
}

