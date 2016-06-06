using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.Windsor;

namespace zavit.Web.Api.IocConfiguration.DependencyResolving
{
    public class WindsorDependencyResolver : IDependencyResolver
    {
        readonly IWindsorContainer _container;

        public WindsorDependencyResolver(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            return new WindsorDependencyScope(this, _container.Release);
        }

        public void Dispose()
        {
        }

        public object GetService(Type t)
        {
            var ret = _container.Kernel.HasComponent(t) ? _container.Resolve(t) : null;
            return ret;
        }

        public IEnumerable<object> GetServices(Type t)
        {
            var ret = _container.ResolveAll(t).Cast<object>().ToArray();
            return ret;
        }
    }
}