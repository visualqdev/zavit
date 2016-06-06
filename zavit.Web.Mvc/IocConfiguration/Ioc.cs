using Castle.Facilities.TypedFactory;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace zavit.Web.Mvc.IocConfiguration
{
    public static class Ioc
    {
        public static WindsorContainer Container;

        public static WindsorContainer Configure()
        {
            Container = new WindsorContainer();
            Container.AddFacility<TypedFactoryFacility>();

            Container.Register(
                Component.For<IKernel>().Instance(Container.Kernel),
                Component.For<IWindsorContainer>().Instance(Container)
            );

            Container.Install(FromAssembly.InThisApplication());

            return Container;
        }
    }
}