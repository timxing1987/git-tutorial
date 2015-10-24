using Cedar.Core.Configuration;
using Cedar.Framwork.AuditTrail.Configuration;
using Microsoft.Practices.Unity.Utility;

namespace Cedar.Framwork.AuditTrail
{
    /// <summary>
    ///     This audit log filter match all <see cref="T:Cedar.Framwork.AuditTrail.AuditLogEntry" />.
    /// </summary>
    [ConfigurationElement(typeof (MatchAllAuditLogFilterData))]
    public class MatchAllAuditLogFilter : AuditLogFilterBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Cedar.Framwork.AuditTrail.MatchAllAuditLogFilter" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public MatchAllAuditLogFilter(string name = null) : base(string.IsNullOrEmpty(null) ? "MatchAll" : name)
        {
        }

        /// <summary>
        ///     Matches the specified log entry.
        /// </summary>
        /// <param name="logEntry">The log entry.</param>
        /// <returns>
        ///     A <see cref="T:System.Boolean" /> value indicating whether to match the specified log entry.
        /// </returns>
        public override bool Match(AuditLogEntry logEntry)
        {
            Guard.ArgumentNotNull(logEntry, "logEntry");
            return true;
        }
    }
}