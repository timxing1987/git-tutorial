using System;

namespace Cedar.Framwork.AuditTrail
{
    /// <summary>
    ///     This class represent a single item of an audit log entry.
    /// </summary>
    public class AuditLogEntryItem
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Cedar.Framwork.AuditTrail.AuditLogEntryItem" /> class.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="logId">The log id.</param>
        /// <param name="subId">The sub id.</param>
        public AuditLogEntryItem(string operation = null, string logId = null, int subId = 1)
        {
            Operation = operation;
            LogId = (string.IsNullOrEmpty(logId) ? Guid.NewGuid().ToString() : logId);
            SubId = subId;
        }

        /// <summary>
        ///     Gets the log id.
        /// </summary>
        /// <value>
        ///     The log id.
        /// </value>
        public string LogId { get; private set; }

        /// <summary>
        ///     Gets the sub id.
        /// </summary>
        /// <value>
        ///     The sub id.
        /// </value>
        public int SubId { get; private set; }

        /// <summary>
        ///     Gets or sets the name of the table.
        /// </summary>
        /// <value>
        ///     The name of the table.
        /// </value>
        public string TableName { get; set; }

        /// <summary>
        ///     Gets or sets the operation.
        /// </summary>
        /// <value>
        ///     The operation.
        /// </value>
        public string Operation { get; set; }

        /// <summary>
        ///     Gets or sets the log data.
        /// </summary>
        /// <value>
        ///     The log data.
        /// </value>
        public object LogData { get; set; }
    }
}