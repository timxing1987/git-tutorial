#region

using System;
using System.Collections.Generic;

#endregion

namespace Cedar.Core.IoC
{
    /// <summary>
    ///     All of service locator classes must implement this interface.
    /// </summary>
    public interface IServiceLocator
    {
        /// <summary>
        ///     Gets the service based on the specified registered type and optional registration name.
        /// </summary>
        /// <param name="registeredType">The service type registered.</param>
        /// <param name="name">The service type registration name.</param>
        /// <returns>The service instance activated.</returns>
        object GetService(Type registeredType, string name = null);

        /// <summary>
        ///     Gets all services based on the specified registered type.
        /// </summary>
        /// <param name="registeredType">The service type registered.</param>
        /// <returns>The list of acticated service instances.</returns>
        IEnumerable<object> GetAllServices(Type registeredType);

        /// <summary>
        ///     Gets all keys (service type registrtion name) for the specified registered type.
        /// </summary>
        /// <param name="registeredType">The service type registered.</param>
        /// <returns>The key list.</returns>
        IEnumerable<string> GetAllKeys(Type registeredType);

        /// <summary>
        ///     Determines whether the specified registered type is registered.
        /// </summary>
        /// <param name="registeredType">The service type registered.</param>
        /// <returns>
        ///     <c>true</c> if the specified registered type is registered; otherwise, <c>false</c>.
        /// </returns>
        bool IsRegistered(Type registeredType);

        /// <summary>
        ///     Determines whether the specified registered type is registered.
        /// </summary>
        /// <param name="registeredType">The service type registered.</param>
        /// <param name="mappedToType">The concrete type to which the registered type is mapped.</param>
        /// <returns>
        ///     <c>true</c> if the specified registered type is registered; otherwise, <c>false</c>.
        /// </returns>
        bool IsRegistered(Type registeredType, Type mappedToType);

        /// <summary>
        ///     Perform service type registration.
        /// </summary>
        /// <param name="registeredType">The service type registered.</param>
        /// <param name="mappedToType">The concrete type to which the registered type is mapped.</param>
        /// <param name="name">The service type registration name.</param>
        /// <param name="isDefault">if set to <c>true</c> [is default].</param>
        /// <param name="lifetime">The lifetime.</param>
        void Register(Type registeredType, Type mappedToType, string name = null, bool isDefault = false,
            Lifetime lifetime = Lifetime.Transient);

        /// <summary>
        ///     Gets the service based on the specified registered type and optional registration name.
        /// </summary>
        /// <typeparam name="T">The service type registered.</typeparam>
        /// <param name="name">The service type registration name.</param>
        /// <returns>The service instance activated.</returns>
        T GetService<T>(string name = null);

        /// <summary>
        ///     Gets all services based on the specified registered type.
        /// </summary>
        /// <typeparam name="T">The service type registered.</typeparam>
        /// <returns>The list of activated service instance.</returns>
        IEnumerable<T> GetAllServices<T>();

        /// <summary>
        ///     Gets all keys.
        /// </summary>
        /// Gets all keys (service type registrtion name) for the specified registered type.
        /// <returns>The key list.</returns>
        IEnumerable<string> GetAllKeys<T>();

        /// <summary>
        ///     Determines whether this instance is registered.
        /// </summary>
        /// <typeparam name="T">The service type registered.</typeparam>
        /// <returns>
        ///     <c>true</c> if this instance is registered; otherwise, <c>false</c>.
        /// </returns>
        bool IsRegistered<T>();

        /// <summary>
        ///     Perform service type registration.
        /// </summary>
        /// <typeparam name="T">The service type registered.</typeparam>
        /// <param name="creator">The delegate to create the service instance.</param>
        /// <param name="name">The service type registration name.</param>
        /// <param name="isDefault">A bool value indicating whether it is a default service registration.</param>
        /// <param name="lifetime">The lifetime.</param>
        void Register<T>(Func<T> creator, string name = null, bool isDefault = false,
            Lifetime lifetime = Lifetime.Transient);

        /// <summary>
        ///     Perform service type registration.
        /// </summary>
        /// <typeparam name="TFrom">The service type registered.</typeparam>
        /// <typeparam name="TTo">The concrete type to which the registered type is mapped.</typeparam>
        /// <param name="name">The service type registration name.</param>
        /// <param name="isDefault">if set to <c>true</c> [is default].</param>
        /// <param name="lifetime">The lifetime.</param>
        void Register<TFrom, TTo>(string name = null, bool isDefault = false, Lifetime lifetime = Lifetime.Transient);
    }
}