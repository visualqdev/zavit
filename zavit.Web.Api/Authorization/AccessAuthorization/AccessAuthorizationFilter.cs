using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace zavit.Web.Api.Authorization.AccessAuthorization
{
    public class AccessAuthorizationFilter : IActionFilter
    {
        readonly IUserContextFactory _userContextFactory;

        public AccessAuthorizationFilter(IUserContextFactory userContextFactory)
        {
            _userContextFactory = userContextFactory;
        }

        public bool AllowMultiple => true;

        public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var authorizationAttributes = GetAuthorizationAttributes(actionContext);

            if (!AuthorizationIsRequired(authorizationAttributes))
            {
                return continuation();
            }

            var userContext = _userContextFactory.Create();
            var isAuthenticated = userContext.IsAuthenticated;
            _userContextFactory.Release(userContext);

            return isAuthenticated ? continuation() : Unauthorized(actionContext);
        }

        public IList<AccessAuthorizeAttribute> GetAuthorizationAttributes(HttpActionContext actionContext)
        {
            var actionAttributes = actionContext
                .ActionDescriptor
                .GetCustomAttributes<AccessAuthorizeAttribute>();

            if (actionAttributes.Count > 0)
                return actionAttributes;

            return actionContext
                .ControllerContext
                .ControllerDescriptor
                .GetCustomAttributes<AccessAuthorizeAttribute>();
        }

        public bool AuthorizationIsRequired(IList<AccessAuthorizeAttribute> authorizeAttributes)
        {
            return authorizeAttributes.Count > 0;
        }

        static Task<HttpResponseMessage> Unauthorized(HttpActionContext actionContext)
        {
            return Task.FromResult(actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized"));
        }
    }
}