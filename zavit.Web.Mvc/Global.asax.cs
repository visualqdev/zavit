using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using zavit.Web.Api;
using zavit.Web.Mvc.IocConfiguration;

namespace zavit.Web.Mvc
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();

            var container = Ioc.Configure();

            GlobalConfiguration.Configure(config => WebApiConfig.Register(config, container));
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}