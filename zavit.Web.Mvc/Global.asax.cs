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
            if (_useHttps)
            {
                switch (Request.Url.Scheme)
                {
                    case "https":
                        Response.AddHeader("Strict-Transport-Security", "max-age=300");
                        break;
                    case "http":
                        var path = "https://" + Request.Url.Host + Request.Url.PathAndQuery;
                        Response.Status = "301 Moved Permanently";
                        Response.AddHeader("Location", path);
                        break;
                }
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