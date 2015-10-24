using System;
using System.Collections.Generic;
using System.Linq;
using Cedar.Core;

namespace Cedar.Framwork.AuditTrail
{
    /// <summary>
    ///     This static class is used to store the type based AuditLogFormatter list.
    /// </summary>
    public static class AuditLogFormatters
    {
        /// <summary>
        ///     Gets the type based AuditLogFormatter list.
        /// </summary>
        /// <value>
        ///     The type based AuditLogFormatter list.
        /// </value>
        public static IDictionary<Type, IAuditLogFormatter> Formatters { get; private set; }

        /// <summary>
        ///     Gets the formatter.
        /// </summary>
        /// <param name="logData">The log data.</param>
        /// <returns>The AuditLogFormatter used to format the given log data.</returns>
        public static IAuditLogFormatter GetFormatter(object logData)
        {
            if (logData == null)
            {
                return new DefaultAuditLogFormatter();
            }
            IAuditLogFormatter result;
            if (Formatters.TryGetValue(logData.GetType(), out result))
            {
                return result;
            }
            var auditLogFormatterAttribute =
                AttributeAccessor.GetAttributes<AuditLogFormatterAttribute>(logData.GetType(), true).FirstOrDefault();
            if (auditLogFormatterAttribute != null)
            {
                return (IAuditLogFormatter) Activator.CreateInstance(auditLogFormatterAttribute.FormatterType);
            }
            return new DefaultAuditLogFormatter();
        }
    }
}