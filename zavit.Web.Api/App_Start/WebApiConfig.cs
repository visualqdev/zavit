using System.Web.Http;
using System.Web.Http.Filters;
using Castle.Windsor;
using zavit.Web.Api.Authorization.AccessAuthorization;
using zavit.Web.Api.Authorization.ClaimsIdentities;
using zavit.Web.Api.IocConfiguration.DependencyResolving;

namespace zavit.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config, IWindsorContainer container)
        {
            config.DependencyResolver = new WindsorDependencyResolver(container);
            config.Filters.AddRange(new IActionFilter[] {
                container.Resolve<ClaimsIdentityFilter>(),
                container.Resolve<AccessAuthorizationFilter>()
                });

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(CommonRoutes.Default, "api/{controller}/{id}", new {id = RouteParameter.Optional});
        }
    }
}