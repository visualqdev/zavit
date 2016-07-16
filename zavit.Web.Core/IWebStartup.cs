using Owin;

namespace zavit.Web.Core
{
    public interface IWebStartup
    {
        void Configuration(IAppBuilder app);
    }
}