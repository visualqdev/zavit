using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Owin;
using Owin;
using zavit.Infrastructure.Ioc;
using zavit.Infrastructure.Logging;
using zavit.Web.Api;
using zavit.Web.Mvc.IocConfiguration;
using zavit.Web.Mvc.IocConfiguration.Installers;

[assembly: OwinStartup("MvcStartup", typeof(zavit.Web.Mvc.MvcStartup))]
namespace zavit.Web.Mvc
{
    public class MvcStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = Container.Instance;
            container.Install(new WebMvcInstaller());
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            LoggingConfig.Configure(container);

            var apiStartup = container.Resolve<ApiStartup>();
            apiStartup.Configuration(app);
            container.Release(apiStartup);

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            SignalRConfig.ConfigureSignalr(app);
        }
    }
}