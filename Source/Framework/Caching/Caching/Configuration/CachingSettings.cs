using System.Configuration;
using Cedar.Core.Configuration;
using Cedar.Core.IoC;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;

namespace Cedar.Framwork.Caching.Configuration
{
    /// <summary>
    /// </summary>
    [ConfigurationSectionName("cedar.caching")]
    public class CachingSettings : ServiceLocatableSettings
    {
        /// <summary>
        ///     Gets or sets the default provider name.
        /// </summary>
        /// <value>
        ///     The default provider name.
        /// </value>
        [ConfigurationProperty("defaultProvider", IsRequired = true)]
        public string DefaultProvider
        {
            get { return (string) base["defaultProvider"]; }
            set { base["defaultProvider"] = value; }
        }

        /// <summary>
        ///     Gets or sets the audit log providers.
        /// </summary>
        /// <value>
        ///     The audit log providers.
        /// </value>
        [ConfigurationProperty("cachingProviders", IsRequired = true)]
        public NameTypeConfigurationElementCollection<CachingData> CachingProviders
        {
            get { return (NameTypeConfigurationElementCollection<CachingData>) base["cachingProviders"]; }
            set { base["cachingProviders"] = value; }
        }

        /// <summary>
        ///     Configures the specified service locator.
        /// </summary>
        /// <param name="serviceLocator">The service locator.</param>
        public override void Configure(IServiceLocator serviceLocator)
        {
            Guard.ArgumentNotNull(serviceLocator, "serviceLocator");
            foreach (CachingData cachingData in CachingProviders)
            {
                var providerCreator = cachingData.GetProviderCreator(this);
                if (providerCreator != null)
                {
                    serviceLocator.Register(providerCreator, cachingData.Name,
                        cachingData.Name == DefaultProvider, cachingData.Lifetime);
                }
            }
        }
    }
}