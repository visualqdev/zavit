using zavit.Web.Api.Dtos.Messaging.MessageThreads;

namespace zavit.Web.Api.DtoServices.Messaging.MessageThreads
{
    public interface IMessageThreadDtoService
    {
        NewMessageThreadDto SendMessageOnNewThread(NewMessageThreadDto newMessageThreadDto);
        MessageThreadDto GetMessageThread(int threadId);
    }
}