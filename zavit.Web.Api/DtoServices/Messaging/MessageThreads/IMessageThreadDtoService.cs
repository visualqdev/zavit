using System.Collections.Generic;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;

namespace zavit.Web.Api.DtoServices.Messaging.MessageThreads
{
    public interface IMessageThreadDtoService
    {
        NewMessageThreadDto SendMessageOnNewThread(NewMessageThreadDto newMessageThreadDto);
        InboxThreadDetailsDto GetMessageThread(int threadId, int messagesTake);
        IEnumerable<InboxThreadDto> GetMessageThreads();
    }
}