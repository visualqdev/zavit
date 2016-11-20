using System.Collections.Generic;
using System.Linq;
using zavit.Domain.Messaging.Messages;
using zavit.Domain.Messaging.MessageThreads;
using zavit.Web.Api.DtoFactories.Messaging.MessageThreads;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;
using zavit.Web.Api.DtoServices.Messaging.MessageThreads.NewMessages;
using zavit.Web.Api.DtoServices.Messaging.MessageThreads.NewMessageThreads;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.DtoServices.Messaging.MessageThreads
{
    public class MessageThreadDtoService : IMessageThreadDtoService
    {
        readonly INewMessageThreadRequestProvider _newMessageThreadRequestProvider;
        readonly IMessageThreadService _messageThreadService;
        readonly INewMessageRequestProvider _newMessageRequestProvider;
        readonly IMessageService _messageService;
        readonly INewMessageThreadDtoFactory _newMessageThreadDtoFactory;
        readonly IInboxThreadDtoFactory _inboxThreadDtoFactory;
        readonly IUserContext _userContext;
        readonly IInboxThreadDetailsDtoFactory _inboxThreadDetailsDtoFactory;

        public MessageThreadDtoService(INewMessageThreadRequestProvider newMessageThreadRequestProvider, IMessageThreadService messageThreadService, INewMessageRequestProvider newMessageRequestProvider, IMessageService messageService, INewMessageThreadDtoFactory newMessageThreadDtoFactory, IUserContext userContext, IInboxThreadDtoFactory inboxThreadDtoFactory, IInboxThreadDetailsDtoFactory inboxThreadDetailsDtoFactory)
        {
            _newMessageThreadRequestProvider = newMessageThreadRequestProvider;
            _messageThreadService = messageThreadService;
            _newMessageRequestProvider = newMessageRequestProvider;
            _messageService = messageService;
            _newMessageThreadDtoFactory = newMessageThreadDtoFactory;
            _userContext = userContext;
            _inboxThreadDtoFactory = inboxThreadDtoFactory;
            _inboxThreadDetailsDtoFactory = inboxThreadDetailsDtoFactory;
        }

        public NewMessageThreadDto SendMessageOnNewThread(NewMessageThreadDto newMessageThreadDto)
        {
            var newMessageThreadRequest = _newMessageThreadRequestProvider.Provide(newMessageThreadDto.Thread);
            var messageThread = _messageThreadService.CreateNewThread(newMessageThreadRequest);

            var newMessageRequest = _newMessageRequestProvider.Provide(newMessageThreadDto.Message);
            var message = _messageService.SendMessageOnThread(newMessageRequest, messageThread);

            return _newMessageThreadDtoFactory.CreateItem(messageThread, message);
        }

        public InboxThreadDetailsDto GetMessageThread(int threadId, int messagesTake)
        {
            var messageThread = _messageThreadService.GetMessageThread(threadId);

            var account = _userContext.Account;
            var messageResultsCollection = _messageService.GetMessages(threadId, null, messagesTake, account);
            return _inboxThreadDetailsDtoFactory.CreateItem(messageThread, messageResultsCollection);
        }

        public IEnumerable<InboxThreadDto> GetMessageThreads()
        {
            var messageInbox = _messageThreadService.GetMessageInbox(_userContext.Account);
            return messageInbox.Threads.Select(t => _inboxThreadDtoFactory.CreateItem(t, messageInbox));
        }
    }
}