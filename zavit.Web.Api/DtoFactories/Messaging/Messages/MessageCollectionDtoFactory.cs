using System.Linq;
using zavit.Domain.Messaging.Messages;
using zavit.Domain.Shared.ResultCollections;
using zavit.Web.Api.Dtos.Messaging.Messages;

namespace zavit.Web.Api.DtoFactories.Messaging.Messages
{
    public class MessageCollectionDtoFactory : IMessageCollectionDtoFactory
    {
        readonly IMessageDtoFactory _messageDtoFactory;

        public MessageCollectionDtoFactory(IMessageDtoFactory messageDtoFactory)
        {
            _messageDtoFactory = messageDtoFactory;
        }

        public MessagesCollectionDto CreateItem(IResultCollection<Message> messageCollection)
        {
            var messageDtos = messageCollection.Results.Select(r => _messageDtoFactory.CreateItem(r));
            return new MessagesCollectionDto
            {
                Take = messageCollection.Take,
                HasMoreResults = messageCollection.HasMoreResults,
                Messages = messageDtos
            };
        }
    }
}