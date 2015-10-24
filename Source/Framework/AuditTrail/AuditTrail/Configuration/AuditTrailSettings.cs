using System.Configuration;
using Cedar.Core.Configuration;
using Cedar.Core.IoC;
using Microsoft.Practices.Unity.Utility;

namespace Cedar.Framwork.AuditTrail.Configuration
{
    /// <summary>
    /// </summary>
    [ConfigurationSectionName("cedar.auditTrail")]
    public class AuditTrailSettings : ServiceLocatableSettings
    {
        private const string DefaultAuditLogProviderProperty = "defaultProvider";
        private const string AuditLogProvidersProperty = "auditLogProviders";
        private const string AuditLogListenersProperty = "auditLogListeners";
        private const string AuditLogFiltersProperty = "auditLogFilters";

        /// <summary>
        ///     Gets or sets the default provider name.
        /// </summary>
        /// <value>
        ///     The default provider name.
        /// </value>
        [ConfigurationProperty(DefaultAuditLogProviderProperty, IsRequired = true)]
        public string DefaultProvider
        {
            get { return (string) base[DefaultAuditLogProviderProperty]; }
            set { base[DefaultAuditLogProviderProperty] = value; }
        }

        /// <summary>
        ///     Gets or sets the audit log providers.
        /// </summary>
        /// <value>
        ///     The audit log providers.
        /// </value>
        [ConfigurationProperty(AuditLogProvidersProperty, IsRequired = true)]
        public NameTypeConfigurationElementCollection<AuditLogProviderDataBase> AuditLogProviders
        {
            get
            {
                return (NameTypeConfigurationElementCollection<AuditLogProviderDataBase>) base[AuditLogProvidersProperty];
            }
            set { base[AuditLogProvidersProperty] = value; }
        }

        /// <summary>
        ///     Gets or sets the audit log listeners.
        /// </summary>
        /// <value>
        ///     The audit log listeners.
        /// </value>
        [ConfigurationProperty(AuditLogListenersProperty, IsRequired = true)]
        public NameTypeConfigurationElementCollection<AuditLogListenerDataBase> AuditLogListeners
        {
            get
            {
                return (NameTypeConfigurationElementCollection<AuditLogListenerDataBase>) base[AuditLogListenersProperty];
            }
            set { base[AuditLogListenersProperty] = value; }
        }

        /// <summary>
        ///     Gets or sets the audit log filters.
        /// </summary>
        /// <value>
        ///     The audit log filters.
        /// </value>
        [ConfigurationProperty(AuditLogFiltersProperty, IsRequired = false)]
        public NameTypeConfigurationElementCollection<AuditLogFilterDataBase> AuditLogFilters
        {
            get
            {
                return (NameTypeConfigurationElementCollection<AuditLogFilterDataBase>) base[AuditLogFiltersProperty];
            }
            set { base[AuditLogFiltersProperty] = value; }
        }

        /// <summary>
        ///     Configures the specified service locator.
        /// </summary>
        /// <param name="serviceLocator">The service locator.</param>
        public override void Configure(IServiceLocator serviceLocator)
        {
            Guard.ArgumentNotNull(serviceLocator, "serviceLocator");
            foreach (AuditLogProviderDataBase auditLogProviderDataBase in AuditLogProviders)
            {
                var providerCreator = auditLogProviderDataBase.GetProviderCreator(this);
                if (providerCreator != null)
                {
                    serviceLocator.Register(providerCreator, auditLogProviderDataBase.Name,
                        auditLogProviderDataBase.Name == DefaultProvider, auditLogProviderDataBase.Lifetime);
                }
            }

            foreach (AuditLogListenerDataBase auditLogListenerDataBase in AuditLogListeners)
            {
                var providerCreator2 = auditLogListenerDataBase.GetProviderCreator(this);
                if (providerCreator2 != null)
                {
                    serviceLocator.Register(providerCreator2, auditLogListenerDataBase.Name, false,
                        auditLogListenerDataBase.Lifetime);
                }
            }

            foreach (AuditLogFilterDataBase auditLogFilterDataBase in AuditLogFilters)
            {
                var providerCreator3 = auditLogFilterDataBase.GetProviderCreator(this);
                if (providerCreator3 != null)
                {
                    serviceLocator.Register(providerCreator3, auditLogFilterDataBase.Name, false,
                        auditLogFilterDataBase.Lifetime);
                }
            }
        }
    }
}