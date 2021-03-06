﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NHibernate;
using zavit.Infrastructure.Orm;

namespace zavit.Infrastructure.Ioc.Infrastructure
{
    public class NhibernateWebInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<SessionFactoryProvider>().LifestyleSingleton(),
                Component.For<ISessionFactory>().UsingFactoryMethod(kernel => kernel.Resolve<SessionFactoryProvider>().Provide()).LifestyleSingleton(),
                Component.For<ISession>().UsingFactoryMethod(kernel => kernel.Resolve<ISessionFactory>().OpenSession()).LifestylePerWebRequest());
        }
    }
}