using System.Linq;
using zavit.Domain.Accounts;
using zavit.Domain.Shared.ResultCollections;
using zavit.Web.Api.Dtos.MessageRecipients;

namespace zavit.Web.Api.DtoFactories.MessageRecipients
{
    public class MessageRecipientCollectionDtoFactory : IMessageRecipientCollectionDtoFactory
    {
        readonly IMessageRecipientDtoFactory _messageRecipientDtoFactory;

        public MessageRecipientCollectionDtoFactory(IMessageRecipientDtoFactory messageRecipientDtoFactory)
        {
            _messageRecipientDtoFactory = messageRecipientDtoFactory;
        }

        public MessageRecipientCollectionDto CreateItem(IResultCollection<Account> recipientResultCollection)
        {
            var recipientDtos = recipientResultCollection.Results.Select(r => _messageRecipientDtoFactory.CreateItem(r));

            return new MessageRecipientCollectionDto
            {
                HasMoreResults = recipientResultCollection.HasMoreResults,
                Recipients = recipientDtos
            };
        }
    }
}