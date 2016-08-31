using Castle.Facilities.TypedFactory;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
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
            Kernel.Resolver.AddSubResolver(new CollectionResolver(Kernel));

            Register(
                Component.For<IKernel>().Instance(Kernel),
                Component.For<IWindsorContainer>().Instance(this)
            );

            Install(
                new PlacesInstaller(),
                new VenuesInstaller(),
                new InfrastructurePlacesInstaller(),
                new InfrastructureCoreInstaller(),
                new NhibernateWebInstaller(),
                new AccountsInstaller(),
                new InfrastructureAccountsInstaller(),
                new ClientsInstaller(),
                new InfrastructureClientsInstaller(),
                new InfrastructureExternalAccounts());
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
