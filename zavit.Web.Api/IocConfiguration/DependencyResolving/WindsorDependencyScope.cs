using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace zavit.Web.Api.IocConfiguration.DependencyResolving
{
    public class WindsorDependencyScope : IDependencyScope
    {
        readonly List<object> _instances;
        readonly Action<object> _release;
        readonly IDependencyScope _scope;

        public WindsorDependencyScope(IDependencyScope scope, Action<object> release)
        {
            if (scope == null)
            {
                throw new ArgumentNullException(nameof(scope));
            }

            if (release == null)
            {
                throw new ArgumentNullException(nameof(release));
            }

            _scope = scope;
            _release = release;
            _instances = new List<object>();
        }

        public void Dispose()
        {
            foreach (object instance in _instances)
            {
                _release(instance);
            }

            _instances.Clear();
        }

        public object GetService(Type t)
        {
            var service = _scope.GetService(t);
            AddToScope(service);

            return service;
        }

        public IEnumerable<object> GetServices(Type t)
        {
            var services = _scope.GetServices(t);
            AddToScope(services);

            return services;
        }

        void AddToScope(params object[] services)
        {
            if (services.Any())
            {
                _instances.AddRange(services);
            }
        }
    }
}