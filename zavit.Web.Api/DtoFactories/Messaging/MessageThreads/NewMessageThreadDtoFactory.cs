using zavit.Domain.Messaging;
using zavit.Domain.Messaging.Messages;
using zavit.Web.Api.DtoFactories.Messaging.Messages;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;

namespace zavit.Web.Api.DtoFactories.Messaging.MessageThreads
{
    public class NewMessageThreadDtoFactory : INewMessageThreadDtoFactory
    {
        readonly IMessageThreadDtoFactory _messageThreadDtoFactory;
        readonly IMessageDtoFactory _messageDtoFactory;

        public NewMessageThreadDtoFactory(IMessageThreadDtoFactory messageThreadDtoFactory, IMessageDtoFactory messageDtoFactory)
        {
            _messageThreadDtoFactory = messageThreadDtoFactory;
            _messageDtoFactory = messageDtoFactory;
        }

        public NewMessageThreadDto CreateItem(MessageThread messageThread, Message message)
        {
            return new NewMessageThreadDto
            {
                Thread = _messageThreadDtoFactory.CreateItem(messageThread),
                Message = _messageDtoFactory.CreateItem(message)
            };
        }
    }
}