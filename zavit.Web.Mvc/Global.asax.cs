using System;
using System.Configuration;
using System.Web;
using zavit.Infrastructure.Ioc;
using zavit.Infrastructure.Logging;

namespace zavit.Web.Mvc
{
    public class Global : HttpApplication
    {
        static bool _useHttps;

        protected void Application_Start(object sender, EventArgs e)
        {
            bool.TryParse(ConfigurationManager.AppSettings["Application.UseHttps"], out _useHttps);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (!Context.Request.IsSecureConnection && _useHttps && !Context.Response.IsRequestBeingRedirected)
            {
                Response.Redirect(Context.Request.Url.ToString().Replace("http:", "https:"));
            }
        }
        
        protected void Application_Error(Object sender, EventArgs e)
        {
            var raisedException = Server.GetLastError();
            var loggerFactory = Container.Instance.Resolve<ILoggerFactory>();
            loggerFactory.GetLogger(nameof(Global)).Error(raisedException);
        }
    }
}