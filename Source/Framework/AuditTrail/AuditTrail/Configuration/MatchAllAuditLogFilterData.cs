using System;
using Cedar.Core.Configuration;

namespace Cedar.Framwork.AuditTrail.Configuration
{
    /// <summary>
    ///     The <see cref="T:Cedar.Framwork.AuditTrail.MatchAllAuditLogFilter" /> based configuration element.
    /// </summary>
    public class MatchAllAuditLogFilterData : AuditLogFilterDataBase
    {
        /// <summary>
        ///     Gets the provider creation expression.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>The delegate to create <see cref="T:Cedar.Framwork.AuditTrail.MatchAllAuditLogFilter" />.</returns>
        public override Func<IAuditLogFilter> GetProviderCreator(ServiceLocatableSettings settings)
        {
            return () => new MatchAllAuditLogFilter(null);
        }
    }
}