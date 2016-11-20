using zavit.Domain.Messaging.MessageReads;
using zavit.Domain.Messaging.Messages;
using zavit.Web.Api.DtoFactories.Messaging.MessageThreadParticipants;
using zavit.Web.Api.Dtos.Messaging.Messages;

namespace zavit.Web.Api.DtoFactories.Messaging.Messages
{
    public class MessageDtoFactory : IMessageDtoFactory
    {
        readonly IThreadParticipantDtoFactory _threadParticipantDtoFactory;

        public MessageDtoFactory(IThreadParticipantDtoFactory threadParticipantDtoFactory)
        {
            _threadParticipantDtoFactory = threadParticipantDtoFactory;
        }

        public MessageDto CreateItem(MessageInfo messageInfo)
        {
            var messageDto = CreateItem(messageInfo.Message);
            messageDto.Status = messageInfo.Status.ToString();
            return messageDto;
        }

        public MessageDto CreateItem(Message message)
        {
            return new MessageDto
            {
                Id = message.Id,
                Body = message.Body,
                SentOn = message.SentOn,
                Sender = _threadParticipantDtoFactory.CreateItem(message.Sender),
                Status = MessageStatus.Sent.ToString(),
                Stamp = message.Stamp,
                ThreadId = message.MessageThread.Id
            };
        }
    }
}