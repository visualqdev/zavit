using System;
using System.Web;
using zavit.Infrastructure.Ioc;
using zavit.Infrastructure.Logging;

namespace zavit.Web.Mvc
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {}

        protected void Application_BeginRequest(object sender, EventArgs e)
        {}
        
        protected void Application_Error(object sender, EventArgs e)
        {
            var raisedException = Server.GetLastError();
            var loggerFactory = Container.Instance.Resolve<ILoggerFactory>();
            loggerFactory.GetLogger(nameof(Global)).Error(raisedException);
        }
    }
}