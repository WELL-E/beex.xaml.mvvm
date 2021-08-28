// Copyright (c) well-e.
// Licensed under the MIT License.

namespace Beex.Xaml.Mvvm
{
    public interface IViewContainer
    {
        /// <summary>
        /// clear services
        /// </summary>
        void Clear();

        /// <summary>
        /// Registers an implementation
        /// </summary>
        /// <param name="service">Object to register</param>
        /// <returns></returns>
        void RegisterService(object service);

        /// <summary>
        /// Unregisters an implementation
        /// </summary>
        /// <param name="service">Object to remove</param>
        /// <returns></returns>
        void UnregisterService(object service);

        /// <summary>
        /// Returns an implementation of the specified interfaces
        /// </summary>
        /// <typeparam name="T">Interface type</typeparam>
        /// <returns>Object implementing the interface</returns>
        T GetService<T>() where T : class;
    }
}
