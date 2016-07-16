using System.Web.Http;
using Microsoft.Owin;
using Owin;
using zavit.Infrastructure.Ioc;
using zavit.Web.Api.IocConfiguration.Installers;

[assembly: OwinStartup("ApiStartup", typeof(zavit.Web.Api.ApiStartup))]
namespace zavit.Web.Api
{
    public class ApiStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = Container.Instance;
            container.Install(new WebApiInstaller());

            var config = new HttpConfiguration();
            WebApiConfig.Register(config, container);
            app.UseWebApi(config);

            OAuthConfig.Register(app, container);
        }
    }
}