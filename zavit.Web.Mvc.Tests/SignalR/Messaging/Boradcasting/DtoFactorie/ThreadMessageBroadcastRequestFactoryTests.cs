using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging;
using zavit.Domain.Messaging.Messages;
using zavit.Web.Api.DtoFactories.Messaging.Messages;
using zavit.Web.Api.Dtos.Messaging.Messages;
using zavit.Web.Mvc.SignalR.ConnectionIds;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.DtoFactories;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.Dtos;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.GroupIds;

namespace zavit.Web.Mvc.Tests.SignalR.Messaging.Boradcasting.DtoFactorie 
{
    [Subject("ThreadMessageBroadcastRequestFactory")]
    public class ThreadMessageBroadcastRequestFactoryTests : TestOf<ThreadMessageBroadcastRequestFactory>
    {
        class When_creatingg_broadcast_request_dto
        {
            Because of = () => _result = Subject.CreateItem(_message);

            It should_set_the_dto_to_be_the_new_message_dto = () => _result.Dto.ShouldEqual(_messageDto);

            It should_set_group_to_ids_made_from_recipient_ids = 
                () => _result.GroupIds.ShouldContainOnly(GroupId, OtherGroupId);

            It should_set_the_excluded_ids_to_contain_current_connection_id =
                () => _result.ConnectionIdsToExclude.ShouldContainOnly(ConnectionId);

            Establish context = () =>
            {
                _recipient = NewInstanceOf<Account>();
                _recipient.Id = 123;

                _otherRecipient = NewInstanceOf<Account>();
                _otherRecipient.Id = 234;

                _message = NewInstanceOf<Message>();
                _message.MessageThread = NewInstanceOf<MessageThread>();
                _message.MessageThread.Id = 777;
                _message.Stub(m => m.GetRecipients()).Return(new[] {_recipient, _otherRecipient});

                _messageDto = NewInstanceOf<MessageDto>();
                Injected<IMessageDtoFactory>().Stub(f => f.CreateItem(_message)).Return(_messageDto);

                Injected<IConnectionIdProvider>().Stub(p => p.GetConnectionId()).Return(ConnectionId);

                Injected<IThreadGroupIdProvider>()
                    .Stub(p => p.Provide(_message.MessageThread.Id, _recipient.Id))
                    .Return(GroupId);

                Injected<IThreadGroupIdProvider>()
                    .Stub(p => p.Provide(_message.MessageThread.Id, _otherRecipient.Id))
                    .Return(OtherGroupId);
            };

            static BroadcastRequest<MessageDto> _result;
            static Message _message;
            static MessageDto _messageDto;
            static Account _recipient;
            static Account _otherRecipient;
            const string ConnectionId = "currentConnectionId";
            const string GroupId = "groupId";
            const string OtherGroupId = "otherGroupId";
        }
    }
}

