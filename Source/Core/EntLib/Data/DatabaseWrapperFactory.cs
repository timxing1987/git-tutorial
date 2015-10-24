using System;
using System.Configuration;
using System.Data.Common;
using System.Data.OracleClient;
using System.Data.SqlClient;
using Cedar.Core.Data;
using Cedar.Core.EntLib.Properties;
using Cedar.Core.IoC;
using Cedar.Core.SettingSource;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.Unity.Utility;
using MySql.Data.MySqlClient;

namespace Cedar.Core.EntLib.Data
{
    [MapTo(typeof(IDatabaseFactory), 0, Lifetime = Lifetime.Singleton)]
    public class DatabaseWrapperFactory : IDatabaseFactory
    {
        private static readonly DbProviderMapping defaultGenericMapping;
        private static readonly DbProviderMapping defaultOracleMapping;
        private static readonly DbProviderMapping defaultSqlMapping;
        private static readonly DbProviderMapping defaultMySqlMapping;

        static DatabaseWrapperFactory()
        {
            defaultSqlMapping = new DbProviderMapping("System.Data.SqlClient", typeof(SqlDatabase));
            defaultMySqlMapping = new DbProviderMapping("MySql.Data.MySqlClient", typeof(MySqlClientFactory));
            defaultGenericMapping = new DbProviderMapping("generic", typeof(GenericDatabase));
        }

        /// <summary>
        /// Gets the database.
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
            ConnectionStringSettings connectionStringSettings = connectionStringsSection.ConnectionStrings[databaseName];
            ValidateConnectionStringSettings(databaseName, connectionStringSettings);
            DatabaseSettings databaseSettings;
            if (!ConfigManager.TryGetConfigurationSection("dataConfiguration", out databaseSettings))
            {
                databaseSettings = new DatabaseSettings();
            }

            if (DbProviderFactories.GetFactoryClasses().Rows.Find(connectionStringSettings.ProviderName) == null)
            {
                throw new ConfigurationErrorsException(Resources.ExceptionNoProviderDefinedForConnectionString.Format(new object[]
				{
					databaseName
				}), connectionStringSettings.ElementInformation.Source, connectionStringSettings.ElementInformation.LineNumber);
            }

            DatabaseData databaseData = GetDatabaseData(connectionStringSettings, databaseSettings);
            Microsoft.Practices.EnterpriseLibrary.Data.Database database = databaseData.BuildDatabase();
            return new DatabaseWrapper(() => database, databaseName);
        }

        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <returns>The <see cref="T:Cedar.Core.Data.Database" />.</returns>
        public Database GetDatabase()
        {
            var configurationSection = ConfigManager.GetConfigurationSection<DatabaseSettings>("dataConfiguration");
            string defaultDatabase = configurationSection.DefaultDatabase;
            if (string.IsNullOrEmpty(defaultDatabase))
            {
                throw new ConfigurationErrorsException(Resources.ExceptionDefaultDatabaseNotExists);
            }
            return GetDatabase(configurationSection.DefaultDatabase);
        }

        private static void ValidateConnectionStringSettings(string name, ConnectionStringSettings connectionStringSettings)
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
                throw new ConfigurationErrorsException(Resources.ExceptionNoProviderDefinedForConnectionString.Format(new object[]
				{
					name
				}), connectionStringSettings.ElementInformation.Source, connectionStringSettings.ElementInformation.LineNumber);
            }
        }

        private static DbProviderMapping GetDefaultMapping(string dbProviderName)
        {
            if ("System.Data.SqlClient".Equals(dbProviderName))
            {
                return defaultSqlMapping;
            }
           
            if ("MySql.Data.MySqlClient".Equals(dbProviderName))
            {
                return defaultMySqlMapping;
            }

            DbProviderFactory factory = DbProviderFactories.GetFactory(dbProviderName);
            if (SqlClientFactory.Instance == factory)
            {
                return defaultSqlMapping;
            }
            if (OracleClientFactory.Instance == factory)
            {
                return defaultOracleMapping;
            }
            if (MySqlClientFactory.Instance == factory)
            {
                return defaultMySqlMapping;
            }

            return null;
        }

        private static DbProviderMapping GetGenericMapping()
        {
            return defaultGenericMapping;
        }

        private DatabaseData GetDatabaseData(ConnectionStringSettings connectionString, DatabaseSettings databaseSettings)
        {
            return CreateDatabaseData(GetAttribute(GetProviderMapping(connectionString.ProviderName, databaseSettings).DatabaseType).ConfigurationType, connectionString);
        }

        private static DatabaseData CreateDatabaseData(Type configurationElementType, ConnectionStringSettings settings)
        {
            object obj;
            try
            {
                Func<string, ConfigurationSection> func = (string sectionName) => SettingSourceFactory.GetSettingSource(null).GetConfigurationSection(sectionName);
                obj = Activator.CreateInstance(configurationElementType, settings, func);
            }
            catch (MissingMethodException innerException)
            {
                throw new InvalidOperationException(Resources.ExceptionDatabaseDataTypeDoesNotHaveRequiredConstructor.Format(configurationElementType), innerException);
            }
            DatabaseData result;
            try
            {
                result = (DatabaseData)obj;
            }
            catch (InvalidCastException innerException2)
            {
                throw new InvalidOperationException(Resources.ExceptionDatabaseDataTypeDoesNotInheritFromDatabaseData.Format(configurationElementType), innerException2);
            }
            return result;
        }

        private static DbProviderMapping GetProviderMapping(string dbProviderName, DatabaseSettings databaseSettings)
        {
            if (databaseSettings != null)
            {
                DbProviderMapping dbProviderMapping = databaseSettings.ProviderMappings.Get(dbProviderName);
                if (dbProviderMapping != null)
                {
                    return dbProviderMapping;
                }
            }
            return GetDefaultMapping(dbProviderName) ?? GetGenericMapping();
        }

        private static ConfigurationElementTypeAttribute GetAttribute(Type databaseType)
        {
            var configurationElementTypeAttribute = (ConfigurationElementTypeAttribute)Attribute.GetCustomAttribute(databaseType, typeof(ConfigurationElementTypeAttribute), false);
            if (configurationElementTypeAttribute == null)
            {
                throw new InvalidOperationException(Resources.ExceptionNoConfigurationElementTypeAttribute.Format(new object[]
				{
					databaseType.Name
				}));
            }
            return configurationElementTypeAttribute;
        }
    }
}
