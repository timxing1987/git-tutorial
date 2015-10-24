using System.Configuration;
using Cedar.Core.Configuration;
using Cedar.Core.IoC;

namespace Cedar.Core.ApplicationContexts.Configuration
{
    /// <summary>
    ///     Define the application context setting.
    /// </summary>
    [ConfigurationSectionName("cedar.applicationContexts")]
    public class ApplicationContextSettings : ServiceLocatableSettings
    {
        private const string ContextLocatorsProperty = "contextLocators";
        private const string DefaultContextLocatorNameProperty = "defaultContextLocator";
        private const string ContextAttachBehaviorProperty = "contextAttachBehavior";

        /// <summary>
        ///     Gets or sets the context attach behavior.
        /// </summary>
        /// <value>The context attach behavior.</value>
        [ConfigurationProperty(ContextAttachBehaviorProperty, IsRequired = false,
            DefaultValue = ContextAttachBehavior.Clear)]
        public ContextAttachBehavior ContextAttachBehavior
        {
            get { return (ContextAttachBehavior) base[ContextAttachBehaviorProperty]; }
            set { base[ContextAttachBehaviorProperty] = value; }
        }

        /// <summary>
        ///     Gets the collection of defined ContextLocator objects.
        /// </summary>
        /// <value>
        ///     The collection of defined ContextLocator objects.
        /// </value>
        [ConfigurationProperty(ContextLocatorsProperty, IsRequired = true)]
        public NameTypeConfigurationElementCollection<ContextLocatorDataBase> ContextLocators
        {
            get
            {
                return (NameTypeConfigurationElementCollection<ContextLocatorDataBase>) base[ContextLocatorsProperty];
            }
            set { base[ContextLocatorsProperty] = value; }
        }

        /// <summary>
        ///     Gets or sets the default service locator.
        /// </summary>
        /// <value>The default service locator.</value>
        [ConfigurationProperty(DefaultContextLocatorNameProperty, IsRequired = true)]
        public string DefaultContextLocator
        {
            get { return base[DefaultContextLocatorNameProperty] as string; }
            set { base[DefaultContextLocatorNameProperty] = value; }
        }

        /// <summary>
        ///     Configures the specified service locator.
        /// </summary>
        /// <param name="serviceLocator">The service locator.</param>
        public override void Configure(IServiceLocator serviceLocator)
        {
            foreach (ContextLocatorDataBase contextLocatorDataBase in ContextLocators)
            {
                var providerCreator = contextLocatorDataBase.GetProviderCreator(this);
                if (providerCreator != null)
                {
                    serviceLocator.Register(providerCreator, contextLocatorDataBase.Name,
                        contextLocatorDataBase.Name == DefaultContextLocator, contextLocatorDataBase.Lifetime);
                }
            }
        }
    }
}