// Copyright (c) well-e.
// Licensed under the MIT License.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Beex.Xaml.Mvvm
{
    public class ViewContainer : IViewContainer
    {
        private readonly List<object> _services = new List<object>();

        /// <summary>
        /// clear services
        /// </summary>
        public void Clear()
        {
            List<object> obj = _services;
            lock (obj)
            {
                _services.Clear();
            }
        }

        /// <summary>
        /// Returns an implementation of the specified interfaces
        /// </summary>
        /// <typeparam name="T">Interface type</typeparam>
        /// <returns>Object implementing the interface</returns>
        public T GetService<T>() where T : class
        {
            var theType = typeof(T);
            if (!theType.IsInterface)
            {
                throw new ArgumentException("服务接口类型无效");
            }

            List<object> obj = _services;
            lock (obj)
            {
                return (T)FindService(theType);
            }
        }

        /// <summary>
        /// Registers an implementation
        /// </summary>
        /// <param name="service">Object to register</param>
        /// <returns></returns>
        public void RegisterService(object service)
        {
            List<object> obj = _services;
            lock (obj)
            {
                _services.Add(service);
            }
        }

        /// <summary>
        /// Unregisters an implementation
        /// </summary>
        /// <param name="service">Object to remove</param>
        /// <returns></returns>
        public void UnregisterService(object service)
        {
            List<object> obj = _services;
            lock (obj)
            {
                _services.Remove(service);
            }
        }

        /// <summary>
        /// Returns an implementation of the specified interfaces
        /// </summary>
        /// <param name="type">Interface type</param>
        /// <returns>Object implementing the interface</returns>
        private object FindService(Type type)
        {
            foreach (var item in _services)
            {
                if (type.IsAssignableFrom(item.GetType()))
                {
                    return item;
                }
            }

            return null;
        }

    }
}