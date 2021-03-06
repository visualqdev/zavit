﻿using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace zavit.Web.Api.Authorization.ClaimsIdentities
{
    public class ClaimsIdentityFilter : IActionFilter
    {
        readonly IClaimsIdentityProviderFactory _claimsIdentityProviderFactory;

        public bool AllowMultiple => true;

        public ClaimsIdentityFilter(IClaimsIdentityProviderFactory claimsIdentityProviderFactory)
        {
            _claimsIdentityProviderFactory = claimsIdentityProviderFactory;
        }

        public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var identity = actionContext.RequestContext.Principal?.Identity as ClaimsIdentity;

            var claimsProvider = _claimsIdentityProviderFactory.Create();
            claimsProvider.SetIdentity(identity);
            _claimsIdentityProviderFactory.Release(claimsProvider);

            return continuation();
        }
    }
}