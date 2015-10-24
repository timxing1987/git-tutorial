using System.Configuration;
using Cedar.Core.Configuration;

namespace Cedar.Framwork.Caching.Configuration
{
    public class CachingData : ProviderDataBase<CachingProviderBase>
    {
        private const string EnabledProperty = "enabled";
        private const string CacheManagerProperty = "cacheManager";
        private const string PriorityProperty = "priority";
        private const string ExpirationModeProperty = "expirationMode";
        private const string ExpiredTimeProperty = "expirationTime";
        private const string ExpiredTimeFormatProperty = "expirationTimeFormat";
        private const string DepedentFileNameProperty = "depedentFile";
        private const string PrefixPropertyName = "prefix";

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="T:Cedar.Framwork.Caching.Configuration.CachingData" /> is
        ///     enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("enabled", IsRequired = false, DefaultValue = false)]
        public bool Enabled
        {
            get { return (bool) base["enabled"]; }
            set { base["enabled"] = value; }
        }

        /// <summary>
        ///     Gets or sets the name of cache manager.
        /// </summary>
        /// <value>
        ///     The name of cache manager.
        /// </value>
        [ConfigurationProperty("cacheManager", IsRequired = false, DefaultValue = "")]
        public string CacheManager
        {
            get { return (string) base["cacheManager"]; }
            set { base["cacheManager"] = value; }
        }

        /// <summary>
        ///     Gets or sets the prefix of cache item key.
        /// </summary>
        /// <value>
        ///     The prefix of cache item key.
        /// </value>
        [ConfigurationProperty("prefix", IsRequired = false, DefaultValue = "")]
        public string Prefix
        {
            get { return (string) base["prefix"]; }
            set { base["prefix"] = value; }
        }

        /// <summary>
        ///     Gets or sets the expired time for the two expiration modes: Absolute and Sliding.
        /// </summary>
        /// <value>
        ///     The expired time for the two expiration modes: Absolute and Sliding.
        /// </value>
        [ConfigurationProperty("expiration", IsRequired = false, DefaultValue = "6000")]
        public int ExpirationTime
        {
            get { return (int) base["expiration"]; }
            set { base["expiration"] = value; }
        }

        /// <summary>
        ///     Gets or sets the expired time format for the expiration mode "TimeFormat".
        /// </summary>
        /// <value>
        ///     The expired time format for the expiration mode "TimeFormat".
        /// </value>
        [ConfigurationProperty("expirationTimeFormat", IsRequired = false, DefaultValue = "")]
        public string ExpirationTimeFormat
        {
            get { return (string) base["expirationTimeFormat"]; }
            set { base["expirationTimeFormat"] = value; }
        }
    }
}