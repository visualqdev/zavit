using Castle.Facilities.TypedFactory;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using zavit.Infrastructure.Core;
using zavit.Infrastructure.Logging.Ioc;

namespace zavit.Infrastructure.Ioc
{
    public sealed class Container : WindsorContainer, IContainer
    {
        static volatile Container _instance;
        static readonly object SyncRoot = new object();

        Container()
        {
            AddFacility<TypedFactoryFacility>();
            AddFacility<LoggerFacility>();
            Kernel.Resolver.AddSubResolver(new CollectionResolver(Kernel));

            Register(
                Component.For<IKernel>().Instance(Kernel),
                Component.For<IWindsorContainer>().Instance(this)
            );

            Install(FromAssembly.This());
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
