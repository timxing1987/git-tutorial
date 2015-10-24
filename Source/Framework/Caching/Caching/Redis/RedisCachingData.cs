using System;
using System.Configuration;
using Cedar.Core.Configuration;
using Cedar.Core.EntLib.Data;
using Cedar.Framwork.Caching.Configuration;

namespace Cedar.Framwork.Caching.Redis
{
    public class RedisCachingData : CachingData
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
        ///     Gets the provider creation expression.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>The delegate to create <see cref="T:Cedar.Core.AuditTrail.RedisAuditLogListener" />.</returns>
        public override Func<CachingProviderBase> GetProviderCreator(ServiceLocatableSettings settings)
        {
            var time = TimeSpan.FromSeconds(ExpirationTime);
            return
                () =>
                    new RedisCachingProvider(Enabled, time, new RedisDatabaseWrapper(Server, Database, Password, Port));
        }
    }
}