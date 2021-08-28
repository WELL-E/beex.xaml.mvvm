using System;

namespace Beex.Xaml.Mvvm
{
    // Per-scope lifetime management
    class ScopeLifetime : ObjectCache, ILifetime
    {
        // Singletons come from parent container's lifetime
        private readonly ContainerLifetime _parentLifetime;

        public ScopeLifetime(ContainerLifetime parentContainer) => _parentLifetime = parentContainer;

        public object GetService(Type type) => _parentLifetime.GetFactory(type)(this);

        // Singleton resolution is delegated to parent lifetime
        public object GetServiceAsSingleton(Type type, Func<ILifetime, object> factory)
            => _parentLifetime.GetServiceAsSingleton(type, factory);

        // Per-scope objects get cached
        public object GetServicePerScope(Type type, Func<ILifetime, object> factory)
            => GetCached(type, factory, this);
    }
}
