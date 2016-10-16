using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace zavit.Web.Api.Authorization.ResourcesAuthorization
{
    public class ResourceAuthorizationFilter : IActionFilter
    {
        public bool AllowMultiple => true;
        readonly IResourceAuthorizationsProvider _resourceAuthorizationsProvider;

        public ResourceAuthorizationFilter(IResourceAuthorizationsProvider resourceAuthorizationsProvider)
        {
            _resourceAuthorizationsProvider = resourceAuthorizationsProvider;
        }

        public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var attributes = actionContext.ActionDescriptor.GetCustomAttributes<ResourceAuthorizeAttribute>();
            if (attributes.Count == 0)
            {
                return continuation();
            }

            var accessAttribute = attributes[0];
            var authorizations = _resourceAuthorizationsProvider.GetResourceAuthorizations().ToList();

            var actionContextWrapper = new ActionContextWrapper(actionContext);
            if (authorizations.Any(a => a.AuthorizeAccess(accessAttribute, actionContextWrapper)))
            {
                authorizations.ForEach(a => _resourceAuthorizationsProvider.Release(a));
                return continuation();
            }

            authorizations.ForEach(a => _resourceAuthorizationsProvider.Release(a));
            return Unauthorized(actionContext);
        }

        static Task<HttpResponseMessage> Unauthorized(HttpActionContext actionContext)
        {
            return Task.FromResult(actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized"));
        }
    }
}