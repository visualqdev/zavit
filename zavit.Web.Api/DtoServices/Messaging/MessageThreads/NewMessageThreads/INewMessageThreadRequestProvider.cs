using zavit.Domain.Messaging.MessageThreads;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;

namespace zavit.Web.Api.DtoServices.Messaging.MessageThreads.NewMessageThreads
{
    public interface INewMessageThreadRequestProvider
    {
        NewMessageThreadRequest Provide(MessageThreadDto messageThreadDto);
    }
}