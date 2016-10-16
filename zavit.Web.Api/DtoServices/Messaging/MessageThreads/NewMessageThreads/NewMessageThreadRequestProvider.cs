using System.Collections.Generic;
using System.Linq;
using zavit.Domain.Messaging.MessageThreads;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.DtoServices.Messaging.MessageThreads.NewMessageThreads
{
    public class NewMessageThreadRequestProvider : INewMessageThreadRequestProvider
    {
        readonly IUserContext _userContext;

        public NewMessageThreadRequestProvider(IUserContext userContext)
        {
            _userContext = userContext;
        }

        public NewMessageThreadRequest Provide(MessageThreadDto messageThreadDto)
        {
            var participants = new List<int> { _userContext.Account.Id };
            participants.AddRange(messageThreadDto.Participants.Where(p => p.Id != _userContext.Account.Id).Select(p => p.Id));
            
            return new NewMessageThreadRequest
            {
                ParticipantIds = participants
            };
        }
    }
}