using Microsoft.Practices.Unity.Utility;

namespace Cedar.Framwork.AuditTrail
{
    /// <summary>
    ///     This is base class of all concrete audit log fiter classes.
    /// </summary>
    public abstract class AuditLogFilterBase : IAuditLogFilter
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Cedar.Framwork.AuditTrail.MatchAllAuditLogFilter" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public AuditLogFilterBase(string name)
        {
            Guard.ArgumentNotNullOrEmpty(name, "name");
            Name = name;
        }

        /// <summary>
        ///     Gets the name of audit log filter.
        /// </summary>
        /// <value>
        ///     The name of audit log filter.
        /// </value>
        public string Name { get; }

        /// <summary>
        ///     Matches the specified log entry.
        /// </summary>
        /// <param name="logEntry">The log entry.</param>
        /// <returns>
        ///     A <see cref="T:System.Boolean" /> value indicating whether to match the specified log entry.
        /// </returns>
        public abstract bool Match(AuditLogEntry logEntry);
    }
}