using zavit.Domain.Messaging.Messages;
using zavit.Web.Api.Dtos.Messaging.Messages;

namespace zavit.Web.Api.DtoFactories.Messaging.Messages
{
    public class MessageDtoFactory : IMessageDtoFactory
    {
        public MessageDto CreateItem(Message message)
        {
            return new MessageDto
            {
                Id = message.Id,
                Body = message.Body,
                SentOn = message.SentOn
            };
        }
    }
}