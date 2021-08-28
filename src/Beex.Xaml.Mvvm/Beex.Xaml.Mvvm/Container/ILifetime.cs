using System;

namespace Beex.Xaml.Mvvm
{
    // ILifetime management adds resolution strategies to an IScope
    interface ILifetime : IScope
    {
        object GetServiceAsSingleton(Type type, Func<ILifetime, object> factory);

        object GetServicePerScope(Type type, Func<ILifetime, object> factory);
    }
}
