using System;
using System.Diagnostics;

namespace Cedar.Core.Logging
{
    /// <summary>
    /// Manages logging.
    /// </summary>
    /// <remarks>
    /// This is a facade for the different logging subsystems.
    /// It offers a simplified interface that follows Ioc patterns
    /// and a simplified priority/level/severity abstraction. 
    /// </remarks>
    public interface ILogger
    {
        /// <summary>
        /// Logs message.
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="severity">The message severity</param>
        /// <param name="exception">The message exception</param>
        void Write(object message, TraceEventType severity, Exception exception = null);
    }
}
