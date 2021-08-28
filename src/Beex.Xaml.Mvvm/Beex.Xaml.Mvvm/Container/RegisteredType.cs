using System;

namespace Beex.Xaml.Mvvm
{

    // RegisteredType is supposed to be a short lived object tying an item to its container
    // and allowing users to mark it as a singleton or per-scope item
    class RegisteredType : IRegisteredType
    {
        private readonly Type _itemType;
        private readonly Action<Func<ILifetime, object>> _registerFactory;
        private readonly Func<ILifetime, object> _factory;

        public RegisteredType(Type itemType, Action<Func<ILifetime, object>> registerFactory, Func<ILifetime, object> factory)
        {
            _itemType = itemType;
            _registerFactory = registerFactory;
            _factory = factory;

            registerFactory(_factory);
        }

        public void AsSingleton()
            => _registerFactory(lifetime => lifetime.GetServiceAsSingleton(_itemType, _factory));

        public void PerScope()
            => _registerFactory(lifetime => lifetime.GetServicePerScope(_itemType, _factory));
    }
}
