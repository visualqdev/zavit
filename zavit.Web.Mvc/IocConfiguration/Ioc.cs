using Castle.Facilities.TypedFactory;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using zavit.Infrastructure.Ioc.DomainInstallers;
using zavit.Infrastructure.Ioc.Infrastructure;
using zavit.Web.Api.IocConfiguration.Installers;
using zavit.Web.Mvc.IocConfiguration.Installers;

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
                new PlacesInstaller(),
                new VenuesInstaller(),
                new WebMvcInstaller(),
                new InfrastructurePlacesInstaller(),
                new InfrastructureCoreInstaller(),
                new NhibernateWebInstaller()
            );
            return Container;
        }
    }
}