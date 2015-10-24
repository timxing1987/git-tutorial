#region

using System;
using System.Collections.Generic;
using Cedar.Core.IoC;

#endregion

namespace Cedar.Core.EntLib.IoC
{
    /// <summary>
    ///     All of concrete service locator classes should be derived from this abstract class.
    /// </summary>
    public abstract class ServiceLocatorBase : IServiceLocator
    {
        /// <summary>
        ///     Gets the service based on the specified registered type and optional registration name.
        /// </summary>
        /// <param name="registeredType">The service type registered.</param>
        /// <param name="name">The service type registration name.</param>
        /// <returns>
        ///     The service instance activated.
        /// </returns>
        public abstract object GetService(Type registeredType, string name = null);

        /// <summary>
        ///     Gets all services based on the specified registered type.
        /// </summary>
        /// <param name="registeredType">The service type registered.</param>
        /// <returns>
        ///     The list of acticated service instances.
        /// </returns>
        public abstract IEnumerable<object> GetAllServices(Type registeredType);

        /// <summary>
        ///     Gets all keys (service type registrtion name) for the specified registered type.
        /// </summary>
        /// <param name="registeredType">The service type registered.</param>
        /// <returns>
        ///     The key list.
        /// </returns>
        public abstract IEnumerable<string> GetAllKeys(Type registeredType);

        /// <summary>
        ///     Determines whether the specified registered type is registered.
        /// </summary>
        /// <param name="registeredType">The service type registered.</param>
        /// <returns>
        ///     <c>true</c> if the specified registered type is registered; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsRegistered(Type registeredType);

        /// <summary>
        ///     Determines whether the specified registered type is registered.
        /// </summary>
        /// <param name="registeredType">The service type registered.</param>
        /// <param name="mappedToType">The concrete type to which the registered type is mapped.</param>
        /// <returns>
        ///     <c>true</c> if the specified registered type is registered; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsRegistered(Type registeredType, Type mappedToType);

        /// <summary>
        ///     Determines whether the specified registered type is registered.
        /// </summary>
        /// <typeparam name="T">The service type registered.</typeparam>
        /// <returns>
        ///     <c>true</c> if the specified registered type is registered; otherwise, <c>false</c>.
        /// </returns>
        public bool IsRegistered<T>()
        {
            return IsRegistered(typeof (T));
        }

        /// <summary>
        ///     Perform service type registration.
        /// </summary>
        /// <param name="registeredType">The service type registered.</param>
        /// <param name="mappedToType">The concrete type to which the registered type is mapped.</param>
        /// <param name="name">The service type registration name.</param>
        /// <param name="isDefault">if set to <c>true</c> [is default].</param>
        /// <param name="lifetime">The lifetime.</param>
        public abstract void Register(Type registeredType, Type mappedToType, string name = null, bool isDefault = false,
            Lifetime lifetime = Lifetime.Transient);

        /// <summary>
        ///     Perform service type registration.
        /// </summary>
        /// <typeparam name="T">The service type registered.</typeparam>
        /// <param name="creator">The delegate to create the service instance.</param>
        /// <param name="name">The service type registration name.</param>
        /// <param name="isDefault">A bool value indicating whether it is a default service registration.</param>
        /// <param name="lifetime">The lifetime.</param>
        public abstract void Register<T>(Func<T> creator, string name = null, bool isDefault = false,
            Lifetime lifetime = Lifetime.Transient);

        /// <summary>
        ///     Gets the service based on the specified registered type and optional registration name.
        /// </summary>
        /// <typeparam name="T">The service type registered.</typeparam>
        /// <param name="name">The service type registration name.</param>
        /// <returns>
        ///     The service instance activated.
        /// </returns>
        public T GetService<T>(string name = null)
        {
            var service = GetService(typeof (T), name);
            if (service == null)
            {
                return default(T);
            }
            return (T) service;
        }

        /// <summary>
        ///     Gets all services based on the specified registered type.
        /// </summary>
        /// <typeparam name="T">The service type registered.</typeparam>
        /// <returns>
        ///     The list of activated service instance.
        /// </returns>
        public IEnumerable<T> GetAllServices<T>()
        {
            foreach (var current in GetAllServices(typeof (T)))
            {
                if (current != null)
                {
                    yield return (T) current;
                }
            }
        }

        /// <summary>
        ///     Perform service type registration.
        /// </summary>
        /// <typeparam name="TFrom">The service type registered.</typeparam>
        /// <typeparam name="TTo">The concrete type to which the registered type is mapped.</typeparam>
        /// <param name="name">The service type registration name.</param>
        /// <param name="isDefault">if set to <c>true</c> [is default].</param>
        /// <param name="lifetime">The lifetime.</param>
        public void Register<TFrom, TTo>(string name = null, bool isDefault = false,
            Lifetime lifetime = Lifetime.Transient)
        {
            Register(typeof (TFrom), typeof (TTo), name, isDefault, lifetime);
        }

        /// <summary>
        ///     Gets all keys.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        ///     The key list.
        /// </returns>
        /// Gets all keys (service type registrtion name) for the specified registered type.
        public IEnumerable<string> GetAllKeys<T>()
        {
            return GetAllKeys(typeof (T));
        }

        /// <summary>
        ///     Determines whether the specified registered type is registered.
        /// </summary>
        /// <typeparam name="TFrom">The service type registered.</typeparam>
        /// <typeparam name="TTo">The concrete type to which the registered type is mapped.</typeparam>
        /// <returns>
        ///     <c>true</c> if the specified registered type is registered; otherwise, <c>false</c>.
        /// </returns>
        public bool IsRegistered<TFrom, TTo>()
        {
            return IsRegistered(typeof (TFrom), typeof (TTo));
        }
    }
}