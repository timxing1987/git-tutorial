#region

using System;
using System.Configuration;
using System.Linq;
using Cedar.Core.Configuration;
using Cedar.Core.Properties;
using Cedar.Core.SettingSource;
using Microsoft.Practices.Unity.Utility;

#endregion

namespace Cedar.Core
{
    /// <summary>
    ///     The static facade class to manangement configuration.
    /// </summary>
    public static class ConfigManager
    {
        /// <summary>
        ///     Gets the connection strings settings.
        /// </summary>
        /// <value>The connection strings settings.</value>
        public static ConnectionStringSettingsCollection ConnectionStrings
            => GetConfigurationSection<ConnectionStringsSection>("connectionStrings").ConnectionStrings;

        /// <summary>
        ///     Tries the get configuration section.
        /// </summary>
        /// <typeparam name="TConfigSection">The type of the config section.</typeparam>
        /// <param name="configSectionName">Name of the config section.</param>
        /// <param name="configurationSection">The configuration section.</param>
        /// <returns>
        ///     A <see cref="T:System.Boolean" /> value indicating whether to successfully get the
        ///     <see cref="T:System.Configuration.ConfigurationSection" />.
        /// </returns>
        public static bool TryGetConfigurationSection<TConfigSection>(string configSectionName,
            out TConfigSection configurationSection) where TConfigSection : ConfigurationSection
        {
            Guard.ArgumentNotNullOrEmpty(configSectionName, "configSectionName");
            configurationSection =
                (SettingSourceFactory.GetSettingSource().GetConfigurationSection(configSectionName) as TConfigSection);
            return configurationSection != null;
        }

        /// <summary>
        ///     Tries the get configuration section.
        /// </summary>
        /// <typeparam name="TConfigSection">The type of the config section.</typeparam>
        /// <param name="configurationSection">The configuration section.</param>
        /// <returns>
        ///     A <see cref="T:System.Boolean" /> value indicating whether to successfully get the
        ///     <see cref="T:System.Configuration.ConfigurationSection" />.
        /// </returns>
        public static bool TryGetConfigurationSection<TConfigSection>(out TConfigSection configurationSection)
            where TConfigSection : ConfigurationSection
        {
            var configurationSectionNameAttribute =
                AttributeAccessor.GetAttributes<ConfigurationSectionNameAttribute>(typeof (TConfigSection), false)
                    .FirstOrDefault();
            if (configurationSectionNameAttribute == null)
            {
                throw new InvalidOperationException(
                    Resources.ExceptionConfigurationSectionNameAttributeNotExists.Format(new object[]
                    {
                        typeof (ConfigurationSectionNameAttribute).Name
                    }));
            }
            return TryGetConfigurationSection(configurationSectionNameAttribute.SectionName, out configurationSection);
        }

        /// <summary>
        ///     Gets the configuration section.
        /// </summary>
        /// <typeparam name="TConfigSection">The type of the config section.</typeparam>
        /// <param name="configSectionName">Name of the config section.</param>
        /// <returns>The located <see cref="T:System.Configuration.ConfigurationSection" />.</returns>
        public static TConfigSection GetConfigurationSection<TConfigSection>(string configSectionName)
            where TConfigSection : ConfigurationSection
        {
            Guard.ArgumentNotNullOrEmpty(configSectionName, "configSectionName");
            var tConfigSection =
                SettingSourceFactory.GetSettingSource().GetConfigurationSection(configSectionName) as TConfigSection;
            if (tConfigSection == null)
            {
                throw new ConfigurationErrorsException(Resources.ExceptionMissingConfigSection.Format(new object[]
                {
                    configSectionName
                }));
            }
            return tConfigSection;
            //update by Yam, for future, the tConfigSection should have default handler.
            //TConfigSection tConfigSection2 = tConfigSection;
            //if (tConfigSection2 == null)
            //{
            //    throw new InvalidCastException(Resources.ExceptionNotSpeciifedConfigSection.Format(new object[]
            //    {
            //        configSectionName,
            //        typeof (TConfigSection).FullName
            //    }));
            //}
            //return tConfigSection2;
            //end update
        }

        /// <summary>
        ///     Gets the configuration section.
        /// </summary>
        /// <typeparam name="TConfigSection">The type of the config section.</typeparam>
        /// <returns>The located <see cref="T:System.Configuration.ConfigurationSection" />.</returns>
        public static TConfigSection GetConfigurationSection<TConfigSection>()
            where TConfigSection : ConfigurationSection
        {
            var configurationSectionNameAttribute =
                AttributeAccessor.GetAttributes<ConfigurationSectionNameAttribute>(typeof (TConfigSection), false)
                    .FirstOrDefault();
            if (configurationSectionNameAttribute == null)
            {
                throw new InvalidOperationException(
                    Resources.ExceptionConfigurationSectionNameAttributeNotExists.Format(new object[]
                    {
                        typeof (ConfigurationSectionNameAttribute).Name
                    }));
            }
            return GetConfigurationSection<TConfigSection>(configurationSectionNameAttribute.SectionName);
        }
    }
}