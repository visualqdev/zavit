using zavit.Domain.Messaging.Messages;
using zavit.Domain.Messaging.MessageThreads;
using zavit.Web.Api.DtoFactories.Messaging.MessageThreads;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;
using zavit.Web.Api.DtoServices.Messaging.MessageThreads.NewMessages;
using zavit.Web.Api.DtoServices.Messaging.MessageThreads.NewMessageThreads;

namespace zavit.Web.Api.DtoServices.Messaging.MessageThreads
{
    public class MessageThreadDtoService : IMessageThreadDtoService
    {
        readonly INewMessageThreadRequestProvider _newMessageThreadRequestProvider;
        readonly IMessageThreadService _messageThreadService;
        readonly INewMessageRequestProvider _newMessageRequestProvider;
        readonly IMessageService _messageService;
        readonly INewMessageThreadDtoFactory _newMessageThreadDtoFactory;
        readonly IMessageThreadDtoFactory _messageThreadDtoFactory;

        public MessageThreadDtoService(INewMessageThreadRequestProvider newMessageThreadRequestProvider, IMessageThreadService messageThreadService, INewMessageRequestProvider newMessageRequestProvider, IMessageService messageService, INewMessageThreadDtoFactory newMessageThreadDtoFactory, IMessageThreadDtoFactory messageThreadDtoFactory)
        {
            _newMessageThreadRequestProvider = newMessageThreadRequestProvider;
            _messageThreadService = messageThreadService;
            _newMessageRequestProvider = newMessageRequestProvider;
            _messageService = messageService;
            _newMessageThreadDtoFactory = newMessageThreadDtoFactory;
            _messageThreadDtoFactory = messageThreadDtoFactory;
        }

        public NewMessageThreadDto SendMessageOnNewThread(NewMessageThreadDto newMessageThreadDto)
        {
            var newMessageThreadRequest = _newMessageThreadRequestProvider.Provide(newMessageThreadDto.Thread);
            var messageThread = _messageThreadService.CreateNewThread(newMessageThreadRequest);

            var newMessageRequest = _newMessageRequestProvider.Provide(newMessageThreadDto.Message);
            var message = _messageService.SendMessageOnThread(newMessageRequest, messageThread);

            return _newMessageThreadDtoFactory.CreateItem(messageThread, message);
        }

        public MessageThreadDto GetMessageThread(int threadId)
        {
            var messageThread = _messageThreadService.GetMessageThread(threadId);
            return _messageThreadDtoFactory.CreateItem(messageThread);
        }
    }
}