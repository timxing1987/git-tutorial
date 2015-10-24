#region

using System.Configuration;
using Cedar.Core.IoC;

#endregion

namespace Cedar.Core.Configuration
{
    /// <summary>
    ///     All of concrete <see cref="T:System.Configuration.ConfigurationSection" /> classes in which the service locaor can
    ///     be specified should be derived from this class.
    /// </summary>
    public abstract class ServiceLocatableSettings : ConfigurationSection, IServiceLocatorConfigurator
    {
        /// <summary>
        ///     Configures the specified service locator.
        /// </summary>
        /// <param name="serviceLocator">The service locator.</param>
        public virtual void Configure(IServiceLocator serviceLocator)
        {
        }
    }
}