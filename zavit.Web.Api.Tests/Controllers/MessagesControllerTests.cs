using System;
using System.Web.Http;
using System.Web.Http.Results;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Messaging.MessageReads;
using zavit.Web.Api.Controllers;
using zavit.Web.Api.Dtos.Messaging.Messages;
using zavit.Web.Api.DtoServices.Messaging.Messages;

namespace zavit.Web.Api.Tests.Controllers 
{
    [Subject("MessagesController")]
    public class MessagesControllerTests : TestOf<MessagesController>
    {
        class When_posting_a_new_message
        {
            Because of = () => _result = Subject.Post(MessageThreadId, _messageDto);

            It should_return_http_result_specifying_the_default_route =
               () => ((CreatedAtRouteNegotiatedContentResult<MessageDto>)_result).RouteName.ShouldEqual(MessagesController.GetMessagesRoute);

            It should_return_http_result_with_a_newly_created_message_thread_dto =
                () => ((CreatedAtRouteNegotiatedContentResult<MessageDto>)_result).Content.ShouldEqual(_createdMessageDto);

            It should_return_http_result_specifying_the_created_venue_id_as_a_route_value =
                () => int.Parse(((CreatedAtRouteNegotiatedContentResult<MessageDto>)_result).RouteValues["messageThreadId"].ToString()).ShouldEqual(MessageThreadId);

            Establish context = () =>
            {
                _messageDto = NewInstanceOf<MessageDto>();

                _createdMessageDto = NewInstanceOf<MessageDto>();
                Injected<IMessageDtoService>()
                    .Stub(s => s.SendMessage(MessageThreadId, _messageDto))
                    .Return(_createdMessageDto);
            };

            static IHttpActionResult _result;
            static MessageDto _messageDto;
            static MessageDto _createdMessageDto;
            const int MessageThreadId = 123;
        }

        class When_getting_the_messages_on_a_thread_older_than_message_id
        {
            Because of = () => _result = Subject.Get(MessageThreadId, OlderThanMessageId, Take);

            It should_return_messages_collection_dto = () => _result.ShouldEqual(_messagesCollectionDto);

            Establish context = () =>
            {
                _messagesCollectionDto = NewInstanceOf<MessagesCollectionDto>();

                Injected<IMessageDtoService>()
                    .Stub(s => s.GetMessages(MessageThreadId, OlderThanMessageId, Take))
                    .Return(_messagesCollectionDto);
            };

            const int Take = 2;
            const int MessageThreadId = 123;
            static readonly int? OlderThanMessageId = 456;
            static MessagesCollectionDto _result;
            static MessagesCollectionDto _messagesCollectionDto;
        }

        class When_posting_message_status
        {
            Because of = () => Subject.Post(MessageStamp, _messageStatus);

            It should_tell_the_message_read_service_that_the_message_has_been_read = 
                () => Injected<IMessageDtoService>().AssertWasCalled(s => s.ConfirmMessageRead(MessageStamp));

            Establish context = () =>
            {
                _messageStatus = NewInstanceOf<MessageStatusDto>();
            };

            static readonly Guid MessageStamp = Guid.NewGuid();
            static MessageStatusDto _messageStatus;
        }
    }
}

