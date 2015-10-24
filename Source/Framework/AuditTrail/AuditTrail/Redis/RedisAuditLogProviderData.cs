using System;
using System.Configuration;
using Cedar.Core.Configuration;
using Cedar.Core.EntLib.Data;
using Cedar.Framwork.AuditTrail.Configuration;

namespace Cedar.Framwork.AuditTrail.Redis
{
    /// <summary>
    /// </summary>
    public class RedisAuditLogProviderData : AuditLogProviderDataBase
    {
        /// <summary>
        /// </summary>
        [ConfigurationProperty("server", IsRequired = true)]
        public string Server
        {
            get { return (string) base["server"]; }
            set { base["server"] = value; }
        }

        /// <summary>
        /// </summary>
        [ConfigurationProperty("port", IsRequired = false, DefaultValue = 6379)]
        public int Port
        {
            get { return (int) base["port"]; }
            set { base["port"] = value; }
        }

        /// <summary>
        /// </summary>
        [ConfigurationProperty("password", IsRequired = false, DefaultValue = "")]
        public string Password
        {
            get { return (string) base["password"]; }
            set { base["password"] = value; }
        }

        /// <summary>
        /// </summary>
        [ConfigurationProperty("database", IsRequired = false, DefaultValue = 1)]
        public int Database
        {
            get { return (int) base["database"]; }
            set { base["database"] = value; }
        }

        /// <summary>
        ///     Gets or sets the name of the application.
        /// </summary>
        /// <value>
        ///     The name of the application.
        /// </value>
        [ConfigurationProperty("applicationName", IsRequired = true)]
        public string ApplicationName
        {
            get { return (string) base["applicationName"]; }
            set { base["applicationName"] = value; }
        }

        /// <summary>
        ///     Gets the provider creation expression.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>The delegate to create <see cref="T:Cedar.Core.AuditTrail.DbAuditLogProvider" />.</returns>
        public override Func<AuditLogProviderBase> GetProviderCreator(ServiceLocatableSettings settings)
        {
            return
                () =>
                    new RedisAuditLogProvider(Name, ApplicationName,
                        new RedisDatabaseWrapper(Server, Database, Password, Port));
        }
    }
}