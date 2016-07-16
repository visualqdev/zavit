using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup("ApiStartup", typeof(zavit.Web.Api.ApiStartup))]
namespace zavit.Web.Api
{
    public class ApiStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config, null);
            app.UseWebApi(config);
        }
    }
}