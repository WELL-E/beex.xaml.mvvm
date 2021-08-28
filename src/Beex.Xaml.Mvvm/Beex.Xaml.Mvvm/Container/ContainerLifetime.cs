using System;

namespace Beex.Xaml.Mvvm
{
    // Container lifetime management
    class ContainerLifetime : ObjectCache, ILifetime
    {
        // Retrieves the factory functino from the given type, provided by owning container
        public Func<Type, Func<ILifetime, object>> GetFactory { get; private set; }

        public ContainerLifetime(Func<Type, Func<ILifetime, object>> getFactory) => GetFactory = getFactory;

        public object GetService(Type type) => GetFactory(type)(this);

        // Singletons get cached per container
        public object GetServiceAsSingleton(Type type, Func<ILifetime, object> factory)
            => GetCached(type, factory, this);

        // At container level, per-scope items are equivalent to singletons
        public object GetServicePerScope(Type type, Func<ILifetime, object> factory)
            => GetServiceAsSingleton(type, factory);
    }
}
