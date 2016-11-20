namespace zavit.Web.Api.Authorization.ResourcesAuthorization.Messaging
{
    public class MessageThreadAccessAuthorizeAttribute : ResourceAuthorizeAttribute
    {
        public virtual string ParameterName { get; }

        public MessageThreadAccessAuthorizeAttribute() : this("messageThreadId") { }

        public MessageThreadAccessAuthorizeAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }
    }
}