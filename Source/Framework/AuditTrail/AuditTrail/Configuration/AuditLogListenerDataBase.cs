using System.Configuration;
using Cedar.Core.Configuration;

namespace Cedar.Framwork.AuditTrail.Configuration
{
    /// <summary>
    ///     This is base class of all concrete audit log listener classes.
    /// </summary>
    public class AuditLogListenerDataBase : ProviderDataBase<AuditLogListenerBase>
    {
        private const string FilterProperty = "filter";

        /// <summary>
        ///     Gets or sets the configuration name of audit log filter.
        /// </summary>
        /// <value>
        ///     The configuration name of audit log filter.
        /// </value>
        [ConfigurationProperty("filter", IsRequired = false, DefaultValue = "")]
        public string Filter
        {
            get { return (string) base["filter"]; }
            set { base["filter"] = value; }
        }
    }
}