using System;
using System.Collections.Generic;
using Cedar.Core.Configuration;
using Cedar.Core.EntLib.Data;
using Microsoft.Practices.Unity.Utility;

namespace Cedar.Framwork.AuditTrail.Redis
{
    /// <summary>
    /// </summary>
    [ConfigurationElement(typeof (RedisAuditLogProviderData))]
    public class RedisAuditLogProvider : AuditLogProviderBase
    {
        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="applicationName"></param>
        /// <param name="redisDatabaseWrapper"></param>
        public RedisAuditLogProvider(string name, string applicationName, RedisDatabaseWrapper redisDatabaseWrapper)
            : base(name)
        {
            Guard.ArgumentNotNullOrEmpty(applicationName, "applicationName");
            RedisDatabaseWrapper = redisDatabaseWrapper;
            ApplicationName = applicationName;
        }

        /// <summary>
        /// </summary>
        public RedisDatabaseWrapper RedisDatabaseWrapper { get; private set; }

        /// <summary>
        ///     Gets the name of the application.
        /// </summary>
        /// <value>
        ///     The name of the application.
        /// </value>
        public string ApplicationName { get; private set; }

        /// <summary>
        ///     Gets the log detail.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="logID">The log ID.</param>
        /// <returns>
        ///     The <see cref="T:System.Data.DataSet" /> contains the given table specific data change.
        /// </returns>
        public override List<dynamic> GetLogDetail(string tableName, string logID)
        {
            //RedisDatabaseWrapper
            return new List<dynamic>();
        }

        /// <summary>
        ///     Archives the audit log entries.
        /// </summary>
        /// <param name="from">The time after which the audit log entries is archived.</param>
        /// <param name="till">The time before which the audit log entries is archived.</param>
        /// <exception cref="T:System.NotImplementedException"></exception>
        public override void Archive(DateTime from, DateTime till)
        {
            //todo
        }
    }
}