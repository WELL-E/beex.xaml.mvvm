using System;
using System.Collections.Concurrent;

namespace Beex.Xaml.Mvvm
{
    // ObjectCache provides common caching logic for lifetimes
    abstract class ObjectCache
    {
        // Instance cache
        private readonly ConcurrentDictionary<Type, object> _instanceCache = new ConcurrentDictionary<Type, object>();

        // Get from cache or create and cache object
        protected object GetCached(Type type, Func<ILifetime, object> factory, ILifetime lifetime)
            => _instanceCache.GetOrAdd(type, _ => factory(lifetime));

        public void Dispose()
        {
            foreach (var obj in _instanceCache.Values)
                (obj as IDisposable)?.Dispose();
        }
    }
}
