using zavit.Domain.Messaging.MessageThreads;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.Authorization.ResourcesAuthorization.Messaging
{
    public class MessageThreadAccessAuthorization : IResourceAuthorization
    {
        readonly IUserContext _userContext;
        readonly IMessageThreadRepository _messageThreadRepository;

        public MessageThreadAccessAuthorization(IUserContext userContext, IMessageThreadRepository messageThreadRepository)
        {
            _userContext = userContext;
            _messageThreadRepository = messageThreadRepository;
        }

        public bool AuthorizeAccess(ResourceAuthorizeAttribute resourceAttribute, IActionContext actionContext)
        {
            if (!_userContext.IsAuthenticated)
                return false;

            var threadAccessAttribute = resourceAttribute as MessageThreadAccessAuthorizeAttribute;

            if (threadAccessAttribute == null)
                return false;

            var messageThreadIdParameter = actionContext.GetActionParameter(threadAccessAttribute.ParameterName);
            var messageThreadId = (int?)messageThreadIdParameter;

            return messageThreadId.HasValue && _messageThreadRepository.CanUserAccessThread(_userContext.Account.Id, messageThreadId);
        }
    }
}