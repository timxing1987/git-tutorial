#region

using System;
using System.Configuration;
using System.IO;
using Cedar.Core.Configuration;
using Cedar.Core.EntLib.Properties;
using Cedar.Core.EntLib.SettingSource.Configuration;
using Cedar.Core.IoC;
using Cedar.Core.SettingSource;
using Microsoft.Practices.Unity.Utility;

#endregion

namespace Cedar.Core.EntLib.SettingSource
{
    /// <summary>
    ///     A simple file based Setting Source.
    /// </summary>
    [ConfigurationElement(typeof (SimpleFileSettingSourceData)), MapTo(typeof (ISettingSource), 0)]
    public class SimpleFileSettingSource : ISettingSource
    {
        /// <summary>
        /// </summary>
        public SimpleFileSettingSource()
            : this(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="configurationFilePath"></param>
        public SimpleFileSettingSource(string configurationFilePath)
        {
            Guard.ArgumentNotNullOrEmpty(configurationFilePath, "configurationFilePath");
            var text = Path.IsPathRooted(configurationFilePath)
                ? configurationFilePath
                : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configurationFilePath);
            if (!File.Exists(text))
            {
                throw new FileNotFoundException(Resources.ExceptionConfigurationLoadFileNotFound.Format(new object[]
                {
                    configurationFilePath
                }));
            }
            ConfigurationFilePath = text;
            var fileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = configurationFilePath
            };
            Configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        }

        /// <summary>
        ///     Gets the configuration.
        /// </summary>
        /// <value>
        ///     The configuration.
        /// </value>
        public System.Configuration.Configuration Configuration { get; }

        /// <summary>
        ///     Gets the configuration file path.
        /// </summary>
        /// <value>
        ///     The configuration file path.
        /// </value>
        public string ConfigurationFilePath { get; private set; }

        /// <summary>
        ///     Gets the configuration section based on specified section name.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns>
        ///     The target configuration section.
        /// </returns>
        public ConfigurationSection GetConfigurationSection(string sectionName)
        {
            Guard.ArgumentNotNullOrEmpty(sectionName, "sectionName");
            return Configuration.GetSection(sectionName);
        }

        /// <summary>
        ///     Sets as current settings source.
        /// </summary>
        public void SetAsCurrentSettingsSource()
        {
        }
    }
}