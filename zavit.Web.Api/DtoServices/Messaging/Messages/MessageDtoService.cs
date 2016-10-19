using System;
using zavit.Domain.Messaging.Messages;
using zavit.Web.Api.DtoFactories.Messaging.Messages;
using zavit.Web.Api.Dtos.Messaging.Messages;
using zavit.Web.Api.DtoServices.Messaging.MessageThreads.NewMessages;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.DtoServices.Messaging.Messages
{
    public class MessageDtoService : IMessageDtoService
    {
        readonly INewMessageRequestProvider _newMessageRequestProvider;
        readonly IMessageService _messageService;
        readonly IMessageDtoFactory _messageDtoFactory;
        readonly IUserContext _userContext;
        readonly IMessageCollectionDtoFactory _messaCollectionDtoFactory;

        public MessageDtoService(INewMessageRequestProvider newMessageRequestProvider, IMessageService messageService, IMessageDtoFactory messageDtoFactory, IUserContext userContext, IMessageCollectionDtoFactory messaCollectionDtoFactory)
        {
            _newMessageRequestProvider = newMessageRequestProvider;
            _messageService = messageService;
            _messageDtoFactory = messageDtoFactory;
            _userContext = userContext;
            _messaCollectionDtoFactory = messaCollectionDtoFactory;
        }

        public MessageDto SendMessage(int messageThreadId, MessageDto messageDto)
        {
            var newMessageRequest = _newMessageRequestProvider.Provide(messageDto);
            var message = _messageService.SendMessageOnThread(newMessageRequest, messageThreadId);
            return _messageDtoFactory.CreateItem(message);
        }

        public MessagesCollectionDto GetMessages(int messageThreadId, int? olderThanMessageId, int take)
        {
            var account = _userContext.Account;
            var messageCollection = _messageService.GetMessages(messageThreadId, olderThanMessageId, take, account);

            return _messaCollectionDtoFactory.CreateItem(messageCollection);
        }
    }
}