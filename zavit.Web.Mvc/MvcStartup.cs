using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup("MvcStartup", typeof(zavit.Web.Mvc.MvcStartup))]
namespace zavit.Web.Mvc
{
    public class MvcStartup
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}