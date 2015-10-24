using System;
using System.Configuration;
using Cedar.Core.Data;
using Cedar.Core.EntLib.Properties;
using Cedar.Core.IoC;
using Cedar.Core.SettingSource;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using Microsoft.Practices.Unity.Utility;
using MySql.Data.Entity;

namespace Cedar.Core.EntLib.Data
{
    [MapTo(typeof (IDatabaseFactory), 0, Lifetime = Lifetime.Singleton)]
    public class DatabaseWrapperFactory : IDatabaseFactory
    {
        /// <summary>
        ///     Gets the database.
        /// </summary>
        /// <param name="databaseName">Name of the connection string.</param>
        /// <returns>The <see cref="T:Cedar.Core.Data.Database" />.</returns>
        public Database GetDatabase(string databaseName)
        {
            Guard.ArgumentNotNullOrEmpty(databaseName, "databaseName");
            ConnectionStringsSection connectionStringsSection;
            if (!ConfigManager.TryGetConfigurationSection("connectionStrings", out connectionStringsSection))
            {
                throw new ConfigurationErrorsException(Resources.ExceptionNoConnectionStringSection);
            }
            var connectionStringSettings = connectionStringsSection.ConnectionStrings[databaseName];

            var factory = new MySqlConnectionFactory();
            return new MySqlDatabaseWrapper(() => factory, databaseName, connectionStringSettings);
        }

        /// <summary>
        ///     Gets the database.
        /// </summary>
        /// <returns>The <see cref="T:Cedar.Core.Data.Database" />.</returns>
        public Database GetDatabase()
        {
            var configurationSection = ConfigManager.GetConfigurationSection<DatabaseSettings>("dataConfiguration");
            var defaultDatabase = configurationSection.DefaultDatabase;
            if (string.IsNullOrEmpty(defaultDatabase))
            {
                throw new ConfigurationErrorsException(Resources.ExceptionDefaultDatabaseNotExists);
            }
            return GetDatabase(configurationSection.DefaultDatabase);
        }

        private static void ValidateConnectionStringSettings(string name,
            ConnectionStringSettings connectionStringSettings)
        {
            if (connectionStringSettings == null)
            {
                throw new ConfigurationErrorsException(Resources.ExceptionNoDatabaseDefined.Format(new object[]
                {
                    name
                }));
            }
            if (string.IsNullOrEmpty(connectionStringSettings.ProviderName))
            {
                throw new ConfigurationErrorsException(
                    Resources.ExceptionNoProviderDefinedForConnectionString.Format(new object[]
                    {
                        name
                    }), connectionStringSettings.ElementInformation.Source,
                    connectionStringSettings.ElementInformation.LineNumber);
            }
        }

        private static DbProviderMapping GetDefaultMapping(string dbProviderName)
        {
            //if ("MySql.Data.MySqlClient".Equals(dbProviderName))
            //{
            //    return defaultMySqlMapping;
            //}

            //DbProviderFactory factory = DbProviderFactories.GetFactory(dbProviderName);
            //if (MySqlClientFactory.Instance == factory)
            //{
            //    return defaultMySqlMapping;
            //}

            return null;
        }

        private DatabaseData GetDatabaseData(ConnectionStringSettings connectionString,
            DatabaseSettings databaseSettings)
        {
            return
                CreateDatabaseData(
                    GetAttribute(GetProviderMapping(connectionString.ProviderName, databaseSettings).DatabaseType)
                        .ConfigurationType, connectionString);
        }

        private static DatabaseData CreateDatabaseData(Type configurationElementType, ConnectionStringSettings settings)
        {
            object obj;
            try
            {
                Func<string, ConfigurationSection> func =
                    (string sectionName) =>
                        SettingSourceFactory.GetSettingSource(null).GetConfigurationSection(sectionName);
                obj = Activator.CreateInstance(configurationElementType, settings, func);
            }
            catch (MissingMethodException innerException)
            {
                throw new InvalidOperationException(
                    Resources.ExceptionDatabaseDataTypeDoesNotHaveRequiredConstructor.Format(configurationElementType),
                    innerException);
            }

            DatabaseData result;
            try
            {
                result = (DatabaseData) obj;
            }
            catch (InvalidCastException innerException2)
            {
                throw new InvalidOperationException(
                    Resources.ExceptionDatabaseDataTypeDoesNotInheritFromDatabaseData.Format(configurationElementType),
                    innerException2);
            }
            return result;
        }

        private static DbProviderMapping GetProviderMapping(string dbProviderName, DatabaseSettings databaseSettings)
        {
            if (databaseSettings != null)
            {
                var dbProviderMapping = databaseSettings.ProviderMappings.Get(dbProviderName);
                if (dbProviderMapping != null)
                {
                    return dbProviderMapping;
                }
            }
            return GetDefaultMapping(dbProviderName);
        }

        private static ConfigurationElementTypeAttribute GetAttribute(Type databaseType)
        {
            var configurationElementTypeAttribute =
                (ConfigurationElementTypeAttribute)
                    Attribute.GetCustomAttribute(databaseType, typeof (ConfigurationElementTypeAttribute), false);
            if (configurationElementTypeAttribute == null)
            {
                throw new InvalidOperationException(
                    Resources.ExceptionNoConfigurationElementTypeAttribute.Format(new object[]
                    {
                        databaseType.Name
                    }));
            }
            return configurationElementTypeAttribute;
        }
    }
}