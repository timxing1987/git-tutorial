using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity.Utility;

namespace Cedar.Framwork.AuditTrail
{
    /// <summary>
    ///     This is base class of all concrete audit log provider classes used to retrieve and archive audit log entries.
    /// </summary>
    public abstract class AuditLogProviderBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Cedar.Framwork.AuditTrail.AuditLogProviderBase" /> class.
        /// </summary>
        /// <param name="name">The provider name.</param>
        public AuditLogProviderBase(string name)
        {
            Guard.ArgumentNotNullOrEmpty(name, "name");
            Name = name;
        }

        /// <summary>
        ///     Gets the provider name.
        /// </summary>
        /// <value>
        ///     The provider name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="logID"></param>
        /// <returns></returns>
        public abstract List<dynamic> GetLogDetail(string tableName, string logID);

        /// <summary>
        ///     Archives the audit log entries.
        /// </summary>
        /// <param name="from">The time after which the audit log entries is archived.</param>
        /// <param name="till">The time before which the audit log entries is archived.</param>
        public abstract void Archive(DateTime from, DateTime till);
    }
}