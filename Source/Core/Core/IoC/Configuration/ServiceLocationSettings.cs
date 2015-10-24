#region

using System;
using System.Configuration;
using System.Linq;
using Cedar.Core.Configuration;
using Cedar.Core.Properties;

#endregion

namespace Cedar.Core.IoC.Configuration
{
    /// <summary>
    ///     The configuration setting for "cedar.serviceLocation".
    /// </summary>
    [ConfigurationSectionName("cedar.serviceLocation")]
    public class ServiceLocationSettings : ConfigurationSection
    {
        private const string DefaultServiceLocatorProperty = "defaultServiceLocator";
        private const string ServiceLocatorsProperty = "serviceLocators";
        private const string ResolvedAssembliesProperty = "resolvedAssemblies";

        /// <summary>
        ///     Gets the default service locator.
        /// </summary>
        /// <value>
        ///     The default service locator.
        /// </value>
        [ConfigurationProperty(DefaultServiceLocatorProperty, IsRequired = false, DefaultValue = "")]
        public string DefaultServiceLocator
        {
            get { return (string) base[DefaultServiceLocatorProperty]; }
            set { base[DefaultServiceLocatorProperty] = value; }
        }

        /// <summary>
        ///     Gets the service locators.
        /// </summary>
        /// <value>
        ///     The service locators.
        /// </value>
        [ConfigurationProperty(ServiceLocatorsProperty, IsRequired = true)]
        public NameTypeConfigurationElementCollection<ServiceLocatorDataBase> ServiceLocators
        {
            get
            {
                return (NameTypeConfigurationElementCollection<ServiceLocatorDataBase>) base[ServiceLocatorsProperty];
            }
            set { base[ServiceLocatorsProperty] = value; }
        }

        /// <summary>
        ///     Gets or sets the resolved assemblies.
        /// </summary>
        /// <value>
        ///     The resolved assemblies.
        /// </value>
        [ConfigurationProperty(ResolvedAssembliesProperty, IsRequired = false)]
        public ConfigurationElementCollection<AssemblyConfigurationElement> ResolvedAssemblies
        {
            get
            {
                return (ConfigurationElementCollection<AssemblyConfigurationElement>) base[ResolvedAssembliesProperty];
            }
            set { base[ResolvedAssembliesProperty] = value; }
        }

        /// <summary>
        ///     Gets the service locator.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The service locator.</returns>
        public IServiceLocator GetServiceLocator(string name = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                if (!string.IsNullOrEmpty(DefaultServiceLocator))
                {
                    return ServiceLocators.GetConfigurationElement(DefaultServiceLocator).CreateServiceLocator();
                }
                return null;
            }

            if (
                ServiceLocators.Cast<NameTypeConfigurationElement>()
                    .Any((NameTypeConfigurationElement element) => element.Name == name))
            {
                return ServiceLocators.GetConfigurationElement(name).CreateServiceLocator();
            }
            throw new ConfigurationErrorsException(Resources.ExceptionServiceLocatorNotExists.Format(new object[]
            {
                name
            }));
        }
    }
}