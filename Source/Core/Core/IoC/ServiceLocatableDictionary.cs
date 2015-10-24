using System.Collections.Generic;

namespace Cedar.Core.IoC
{
    /// <summary>
    ///     A dictionary of object can be activated by service locator.
    /// </summary>
    /// <typeparam name="T">The type of element's value.</typeparam>
    public class ServiceLocatableDictionary<T>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Cedar.Core.IoC.ServiceLocatableDictionary`1" /> class.
        /// </summary>
        /// <param name="serviceLocatorName">Name of the service locator.</param>
        public ServiceLocatableDictionary(string serviceLocatorName = null)
        {
            ServiceLocator = ServiceLocatorFactory.GetServiceLocator(serviceLocatorName);
        }

        /// <summary>
        ///     Gets the service locator.
        /// </summary>
        /// <value>
        ///     The service locator.
        /// </value>
        public IServiceLocator ServiceLocator { get; }

        /// <summary>
        ///     Gets the keys.
        /// </summary>
        /// <value>
        ///     The keys.
        /// </value>
        public IEnumerable<string> Keys
        {
            get { return ServiceLocator.GetAllKeys<T>(); }
        }

        /// <summary>
        ///     Gets the values.
        /// </summary>
        /// <value>
        ///     The values.
        /// </value>
        public IEnumerable<T> Values
        {
            get { return ServiceLocator.GetAllServices<T>(); }
        }

        /// <summary>
        ///     Gets the object with the specified key.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        /// <param name="key">The key.</param>
        /// <returns>The value.</returns>
        public T this[string key]
        {
            get { return ServiceLocator.GetService<T>(key); }
        }
    }
}