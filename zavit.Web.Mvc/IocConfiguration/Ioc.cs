using Castle.Facilities.TypedFactory;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using zavit.Infrastructure.Ioc.DomainInstallers;
using zavit.Web.Api.IocConfiguration.Installers;

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

            Container.Install(
                new WebApiInstaller(),
                new PlacesInstaller()
                );
            return Container;
        }
    }
}