using System;
using System.Collections;
using System.ComponentModel;
using System.Text;

namespace Cedar.Framwork.AuditTrail
{
    /// <summary>
    ///     The audit log formatter which is by default used.
    /// </summary>
    public class DefaultAuditLogFormatter : IAuditLogFormatter
    {
        /// <summary>
        ///     Formats the specified log dataas a literal text.
        /// </summary>
        /// <param name="logData">The log data object.</param>
        /// <returns>
        ///     The formatted literal text.
        /// </returns>
        public string Format(object logData)
        {
            if (logData == null)
            {
                return string.Empty;
            }
            if (logData is string)
            {
                return logData.ToString();
            }
            var stringBuilder = new StringBuilder();
            var enumerable = logData as IEnumerable;
            if (enumerable != null)
            {
                foreach (var current in enumerable)
                {
                    stringBuilder.AppendLine(AuditLogFormatters.GetFormatter(current).Format(current));
                }
                return stringBuilder.ToString().Trim();
            }
            if (logData is string || typeof (ValueType).IsAssignableFrom(logData.GetType()))
            {
                return logData.ToString().Trim();
            }
            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(logData))
            {
                var value = propertyDescriptor.GetValue(logData);
                stringBuilder.AppendLine(string.Format("{0}: {1}", propertyDescriptor.Name, value));
            }
            return stringBuilder.ToString().Trim();
        }
    }
}