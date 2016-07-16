using Castle.Facilities.TypedFactory;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using zavit.Infrastructure.Ioc.DomainInstallers;
using zavit.Infrastructure.Ioc.Infrastructure;

namespace zavit.Infrastructure.Ioc
{
    public sealed class Container : WindsorContainer
    {
        static volatile Container _instance;
        static readonly object SyncRoot = new object();

        Container()
        {
            AddFacility<TypedFactoryFacility>();

            Register(
                Component.For<IKernel>().Instance(Kernel),
                Component.For<IWindsorContainer>().Instance(this)
            );

            Install(
                new PlacesInstaller(),
                new VenuesInstaller(),
                new InfrastructurePlacesInstaller(),
                new InfrastructureCoreInstaller(),
                new NhibernateWebInstaller());
        }

        public static Container Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new Container();
                    }
                }

                return _instance;
            }
        }
    }
}
