using zavit.Domain.Messaging;
using zavit.Domain.Messaging.MessageReads;
using zavit.Domain.Messaging.MessageThreads;
using zavit.Domain.Shared.ResultCollections;
using zavit.Web.Api.DtoFactories.Messaging.Messages;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.DtoFactories.Messaging.MessageThreads
{
    public class InboxThreadDetailsDtoFactory : IInboxThreadDetailsDtoFactory
    {
        readonly IMessageThreadTitleBuilder _messageThreadTitleBuilder;
        readonly IUserContext _userContext;
        readonly IMessageCollectionDtoFactory _messageCollectionDtoFactory;

        public InboxThreadDetailsDtoFactory(IMessageThreadTitleBuilder messageThreadTitleBuilder, IUserContext userContext, IMessageCollectionDtoFactory messageCollectionDtoFactory)
        {
            _messageThreadTitleBuilder = messageThreadTitleBuilder;
            _userContext = userContext;
            _messageCollectionDtoFactory = messageCollectionDtoFactory;
        }

        public InboxThreadDetailsDto CreateItem(MessageThread messageThread, IResultCollection<MessageInfo> messageResultsCollection)
        {
            var account = _userContext.Account;
            var messageCollectionDto = _messageCollectionDtoFactory.CreateItem(messageResultsCollection);

            return new InboxThreadDetailsDto
            {
                ThreadTitle = _messageThreadTitleBuilder.BuildTitle(messageThread, account.Id),
                ThreadId = messageThread.Id,
                MessagesCollection = messageCollectionDto
            };
        }
    }
}