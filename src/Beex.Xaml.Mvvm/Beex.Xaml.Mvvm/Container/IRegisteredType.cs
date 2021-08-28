namespace Beex.Xaml.Mvvm
{
    /// <summary>
    /// IRegisteredType is return by Container.Register and allows further configuration for the registration
    /// </summary>
    public interface IRegisteredType
    {
        /// <summary>
        /// Make registered type a singleton
        /// </summary>
        void AsSingleton();

        /// <summary>
        /// Make registered type a per-scope type (single instance within a Scope)
        /// </summary>
        void PerScope();
    }
}
