// Copyright(c) Microsoft Corporation.All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Beex.Xaml.Mvvm
{
    /// <summary>
    /// Inversion of control container handles dependency injection for registered types
    /// </summary>
    public class ServiceContainer : IScope, IServiceContainer
    {
        // Map of registered types
        private readonly Dictionary<Type, Func<ILifetime, object>> _registeredTypes = new Dictionary<Type, Func<ILifetime, object>>();

        // Lifetime management
        private readonly ContainerLifetime _lifetime;

        /// <summary>
        /// Creates a new instance of IoC Container
        /// </summary>
        public ServiceContainer() => _lifetime = new ContainerLifetime(t => _registeredTypes[t]);

        /// <summary>
        /// Registers a factory function which will be called to resolve the specified interface
        /// </summary>
        /// <param name="interface">Interface to register</param>
        /// <param name="factory">Factory function</param>
        /// <returns></returns>
        public IRegisteredType Register(Type @interface, Func<object> factory)
        => RegisterType(@interface, _ => factory());

        /// <summary>
        /// Registers an implementation type for the specified interface
        /// </summary>
        /// <param name="interface">Interface to register</param>
        /// <param name="implementation">Implementing type</param>
        /// <returns></returns>
        public IRegisteredType Register(Type @interface, Type implementation)
        => RegisterType(@interface, FactoryFromType(implementation));

        private IRegisteredType RegisterType(Type itemType, Func<ILifetime, object> factory)
        => new RegisteredType(itemType, f => _registeredTypes[itemType] = f, factory);

        /// <summary>
        /// Registers an implementation type for the specified interface
        /// </summary>
        /// <typeparam name="T">Interface to register</typeparam>
        /// <param name="container">This container instance</param>
        /// <param name="type">Implementing type</param>
        /// <returns>IRegisteredType object</returns>
        public IRegisteredType Register<T>(Type type)
        {
            return Register(typeof(T), type);
        }

        /// <summary>
        /// Registers an implementation type for the specified interface
        /// </summary>
        /// <typeparam name="TInterface">Interface to register</typeparam>
        /// <typeparam name="TImplementation">Implementing type</typeparam>
        /// <param name="container">This container instance</param>
        /// <returns>IRegisteredType object</returns>
        public IRegisteredType Register<TInterface, TImplementation>()
        where TImplementation : TInterface
        {
            return Register(typeof(TInterface), typeof(TImplementation));
        }

        /// <summary>
        /// Registers a factory function which will be called to resolve the specified interface
        /// </summary>
        /// <typeparam name="T">Interface to register</typeparam>
        /// <param name="container">This container instance</param>
        /// <param name="factory">Factory method</param>
        /// <returns>IRegisteredType object</returns>
        public IRegisteredType Register<T>(Func<T> factory)
        {
            return Register(typeof(T), () => factory());
        }

        /// <summary>
        /// Registers a type
        /// </summary>
        /// <param name="container">This container instance</param>
        /// <typeparam name="T">Type to register</typeparam>
        /// <returns>IRegisteredType object</returns>
        public IRegisteredType Register<T>()
        {
            return Register(typeof(T), typeof(T));
        }

        /// <summary>
        /// Returns the object registered for the given type, if registered
        /// </summary>
        /// <param name="type">Type as registered with the container</param>
        /// <returns>Instance of the registered type, if registered; otherwise <see langword="null"/></returns>
        public object GetService(Type serviceType)
        {
             return Resolve(serviceType);
        }

        /// <summary>
        /// Returns an implementation of the specified interfaces
        /// </summary>
        /// <typeparam name="T">Interface type</typeparam>
        /// <param name="scope">This scope instance</param>
        /// <returns>Object implementing the interface</returns>
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        /// <summary>
        /// Returns the object registered for the given type, if registered
        /// </summary>
        /// <param name="type">Type as registered with the container</param>
        /// <returns>Instance of the registered type, if registered; otherwise <see langword="null"/></returns>
        public object Resolve(Type type)
        {
            Func<ILifetime, object> registeredType;

            if (!_registeredTypes.TryGetValue(type, out registeredType))
            {
                return null;
            }

            return registeredType(_lifetime);
        }

        /// <summary>
        /// Creates a new scope
        /// </summary>
        /// <returns>Scope object</returns>
        public IScope CreateScope() => new ScopeLifetime(_lifetime);

        /// <summary>
        /// Disposes any <see cref="IDisposable"/> objects owned by this container.
        /// </summary>
        public void Dispose() => _lifetime.Dispose();

        #region Container items
        // Compiles a lambda that calls the given type's first constructor resolving arguments
        private static Func<ILifetime, object> FactoryFromType(Type itemType)
        {
            // Get first constructor for the type
            var constructors = itemType.GetConstructors();
            if (constructors.Length == 0)
            {
                // If no public constructor found, search for an internal constructor
                constructors = itemType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
            }
            var constructor = constructors.First();

            // Compile constructor call as a lambda expression
            var arg = Expression.Parameter(typeof(ILifetime));
            return (Func<ILifetime, object>)Expression.Lambda(
            Expression.New(constructor, constructor.GetParameters().Select(
            param =>
            {
                var resolve = new Func<ILifetime, object>(lifetime => lifetime.GetService(param.ParameterType));
                return Expression.Convert(Expression.Call(Expression.Constant(resolve.Target), resolve.Method, arg), param.ParameterType);
            })), arg).Compile();
        }

        #endregion
    }

}