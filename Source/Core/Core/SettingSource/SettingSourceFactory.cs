#region

using System;
using System.Collections.Generic;
using System.Configuration;
using Cedar.Core.IoC;
using Cedar.Core.Properties;
using Cedar.Core.SettingSource.Configuration;
using Microsoft.Practices.Unity.Utility;

#endregion

namespace Cedar.Core.SettingSource
{
    /// <summary>
    ///     This is factory to create or get setting source.
    /// </summary>
    public static class SettingSourceFactory
    {
        private static ISettingSource settingSource;

        private static readonly Dictionary<string, ISettingSource> settingSources =
            new Dictionary<string, ISettingSource>();

        private static readonly object syncHelper = new object();

        /// <summary>
        ///     Gets the setting source.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The setting source.</returns>
        public static ISettingSource GetSettingSource(string name = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                if (settingSource != null)
                {
                    return settingSource;
                }
                //获取webconfig中的配置节
                var settingSourceSettings =
                    ConfigurationManager.GetSection("cedar.settingSource") as SettingSourceSettings;
                if (settingSourceSettings != null)
                {
                    return settingSource = settingSourceSettings.GetSettingSource(null);
                }

                //当没有配置节时,使用核心库中默认的初始化配置
                var assemblyResolver = new DefaultAssemblyResolver(Assemblies.GetAssemblies());
                var instance =
                    new ReflectedServiceLocatorConfigurator(assemblyResolver).CreateInstance<ISettingSource>();
                if (instance == null)
                {
                    throw new TypeLoadException(Resources.ExceptionCannotResolveTypeName.Format(new object[]
                    {
                        "SettingSource"
                    }));
                }

                return settingSource = instance;
            }
            ISettingSource getSettingSource;
            if (settingSources.TryGetValue(name, out getSettingSource))
            {
                return getSettingSource;
            }

            ISettingSource result;
            lock (syncHelper)
            {
                if (settingSources.TryGetValue(name, out getSettingSource))
                {
                    result = getSettingSource;
                }
                else
                {
                    var settingSourceSettings =
                        ConfigurationManager.GetSection("cedar.settingSource") as SettingSourceSettings;
                    if (settingSourceSettings == null)
                    {
                        throw new ConfigurationErrorsException(
                            Resources.ExceptionSettingSourceNotExists.Format(new object[]
                            {
                                "cedar.settingSource"
                            }));
                    }
                    getSettingSource = settingSourceSettings.GetSettingSource(name);
                    settingSources[name] = getSettingSource;
                    result = getSettingSource;
                }
            }
            return result;
        }

        /// <summary>
        ///     Changes the setting source.
        /// </summary>
        /// <param name="settingSource">The setting source.</param>
        public static void ChangeSettingSource(ISettingSource settingSource)
        {
            Guard.ArgumentNotNull(settingSource, "settingSource");
            if (SettingSourceFactory.settingSource != settingSource)
            {
                settingSource.SetAsCurrentSettingsSource();
                SettingSourceFactory.settingSource = settingSource;
                ServiceLocatorFactory.Reset();
            }
        }

        /// <summary>
        ///     Changes the setting source.
        /// </summary>
        /// <param name="name">The name.</param>
        public static void ChangeSettingSource(string name)
        {
            Guard.ArgumentNotNullOrEmpty(name, "name");
            var settingSourceSettings = ConfigurationManager.GetSection("cedar.settingSource") as SettingSourceSettings;
            if (settingSourceSettings == null)
            {
                throw new ConfigurationErrorsException(Resources.ExceptionSettingSourceNotExists.Format(new object[]
                {
                    "cedar.settingSource"
                }));
            }
            ChangeSettingSource(settingSourceSettings.GetSettingSource(name));
        }

        /// <summary>
        ///     Resets this default setting source to null.
        /// </summary>
        public static void Reset()
        {
            if (settingSource != null)
            {
                ServiceLocatorFactory.Reset();
            }
            settingSource = null;
        }
    }
}