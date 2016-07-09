using System.Web.Http;
using Castle.Windsor;
using zavit.Web.Api.IocConfiguration.DependencyResolving;

namespace zavit.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config, IWindsorContainer container)
        {
            config.DependencyResolver = new WindsorDependencyResolver(container);
            
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(CommonRoutes.Default, "api/{controller}/{id}", new {id = RouteParameter.Optional});
        }
    }
}