using System;

namespace Beex.Xaml.Mvvm
{
    /// <summary>
    /// Represents a scope in which per-scope objects are instantiated a single time
    /// </summary>
    public interface IScope : IDisposable, IServiceProvider
    {
    }

}
