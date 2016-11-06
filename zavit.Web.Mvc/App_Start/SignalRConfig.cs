using Owin;

namespace zavit.Web.Mvc
{
    public class SignalRConfig
    {
        public static void ConfigureSignalr(IAppBuilder app)
        {
            //bool useServiceBus;

            //if (bool.TryParse(ConfigurationManager.AppSettings["signalr:UseServiceBus"], out useServiceBus) && useServiceBus)
            //{
            //    var connectionString = ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"];
            //    GlobalHost.DependencyResolver.UseServiceBus(connectionString, "signalr");
            //}

            app.MapSignalR();
        }
    }
}