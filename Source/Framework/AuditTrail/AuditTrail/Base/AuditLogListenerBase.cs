using Cedar.Core.IoC;
using Microsoft.Practices.Unity.Utility;

namespace Cedar.Framwork.AuditTrail
{
    /// <summary>
    ///     This class is base class of all concrete audit log listener to listen the log writing request and write log entry.
    /// </summary>
    public abstract class AuditLogListenerBase
    {
        private IAuditLogFilter filter;
        private readonly string filterName;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Cedar.Framwork.AuditTrail.AuditLogListenerBase" /> class.
        /// </summary>
        /// <param name="name">The listern name.</param>
        /// <param name="filterName">The configuration name of audit log filter.</param>
        public AuditLogListenerBase(string name, string filterName)
        {
            Guard.ArgumentNotNullOrEmpty(name, "name");
            Name = name;
            this.filterName = filterName;
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets the filter which determines whether to write the given audit log entry.
        /// </summary>
        /// <value>
        ///     The filter which determines whether to write the given audit log entry.
        /// </value>
        public IAuditLogFilter Filter
        {
            get
            {
                if (filter != null)
                {
                    return filter;
                }
                if (string.IsNullOrEmpty(filterName))
                {
                    return new MatchAllAuditLogFilter(null);
                }
                filter = ServiceLocatorFactory.GetServiceLocator(null).GetService<IAuditLogFilter>(filterName);
                return filter;
            }
            internal set { filter = value; }
        }

        /// <summary>
        ///     Writes the specified audit log entry.
        /// </summary>
        /// <param name="logEntry">The audit log entry.</param>
        public void Write(AuditLogEntry logEntry)
        {
            Guard.ArgumentNotNull(logEntry, "logEntry");
            if (Filter.Match(logEntry))
            {
                WriteCore(logEntry);
            }
        }

        /// <summary>
        ///     Writes the specified audit log entry.
        /// </summary>
        /// <param name="logEntry">The audit log entry.</param>
        protected abstract void WriteCore(AuditLogEntry logEntry);

        /// <summary>
        ///     Gets the audit log formatter.
        /// </summary>
        /// <param name="logEntryItem">The log entry item.</param>
        /// <returns>
        ///     The AuditLogFormatter to format the given <see cref="T:Cedar.Framwork.AuditTrail.AuditLogEntryItem" />'s log
        ///     data.
        /// </returns>
        protected virtual IAuditLogFormatter GetAuditLogFormatter(AuditLogEntryItem logEntryItem)
        {
            Guard.ArgumentNotNull(logEntryItem, "logEntryItem");
            return AuditLogFormatters.GetFormatter(logEntryItem.LogData);
        }
    }
}