using Cedar.Core.Configuration;
using Cedar.Core.EntLib.Data;
using Microsoft.Practices.Unity.Utility;
using Newtonsoft.Json;

namespace Cedar.Framwork.AuditTrail.Redis
{
    /// <summary>
    ///     This audit log listener which writes the log entries into database.
    /// </summary>
    [ConfigurationElement(typeof (RedisAuditLogListenerData))]
    public class RedisAuditLogListener : AuditLogListenerBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Cedar.Core.AuditTrail.RedisAuditLogListener" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="filterName">Name of the filter.</param>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="applicationName">Name of the application.</param>
        public RedisAuditLogListener(string name, string filterName, RedisDatabaseWrapper redisDatabaseWrapper,
            string applicationName) : base(name, filterName)
        {
            Guard.ArgumentNotNullOrEmpty(applicationName, "applicationName");
            RedisDatabaseWrapper = redisDatabaseWrapper;
            ApplicationName = applicationName;
        }

        /// <summary>
        /// </summary>
        public RedisDatabaseWrapper RedisDatabaseWrapper { get; }

        /// <summary>
        ///     Gets the name of the application.
        /// </summary>
        /// <value>
        ///     The name of the application.
        /// </value>
        public string ApplicationName { get; private set; }

        /// <summary>
        ///     Writes the specified audit log entry.
        /// </summary>
        /// <param name="logEntry">The audit log entry.</param>
        protected override void WriteCore(AuditLogEntry logEntry)
        {
            Guard.ArgumentNotNull(logEntry, "logEntry");
            RedisDatabaseWrapper.StringSet($"{logEntry.FunctionName}:{logEntry.TransactionId}",
                JsonConvert.SerializeObject(logEntry));
        }
    }
}