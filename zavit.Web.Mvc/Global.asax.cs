using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using zavit.Domain.Shared;
using zavit.Infrastructure.Ioc;
using zavit.Infrastructure.Logging;
using zavit.Web.Api;
using zavit.Web.Mvc.IocConfiguration;

namespace zavit.Web.Mvc
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            var raisedException = Server.GetLastError();
            var loggerFactory = Container.Instance.Resolve<ILoggerFactory>();
            loggerFactory.GetLogger(nameof(Global)).Error(raisedException);
        }
    }
}