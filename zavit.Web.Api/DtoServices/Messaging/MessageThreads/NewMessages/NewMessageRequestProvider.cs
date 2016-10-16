using zavit.Domain.Messaging.Messages;
using zavit.Web.Api.Dtos.Messaging.Messages;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.DtoServices.Messaging.MessageThreads.NewMessages
{
    public class NewMessageRequestProvider : INewMessageRequestProvider
    {
        readonly IUserContext _userContex;

        public NewMessageRequestProvider(IUserContext userContex)
        {
            _userContex = userContex;
        }

        public NewMessageRequest Provide(MessageDto messageDto)
        {
            return new NewMessageRequest
            {
                Sender = _userContex.Account,
                Body = messageDto.Body
            };
        }
    }
}