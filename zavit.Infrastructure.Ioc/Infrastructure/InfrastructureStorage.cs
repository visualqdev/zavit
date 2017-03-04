using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Infrastructure.Storage;
using zavit.Infrastructure.Storage.Azure;

namespace zavit.Infrastructure.Ioc.Infrastructure
{
    public class InfrastructureStorage : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IFileStorage>().ImplementedBy<FileStorage>().LifestyleSingleton(),
                Component.For<IStorageQueue>().ImplementedBy<StorageQueue>().LifestyleSingleton(),
                Component.For<ITableStorage>().ImplementedBy<TableStorage>().LifestyleSingleton()
                );
        }
    }
}