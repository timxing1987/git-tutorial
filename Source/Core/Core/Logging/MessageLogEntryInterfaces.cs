using System.Diagnostics;

namespace Cedar.Core.Logging
{
    /// <summary>
	/// The logging interface for MessageEntry 
	/// </summary>
	public interface IMessageLogEntry
    {
        /// <summary>
        /// Gets the type of the trace event.
        /// </summary>
        /// <value>
        /// The type of the trace event.
        /// </value>
        TraceEventType TraceEventType
        {
            get;
        }
    }

    /// <summary>
	/// The logging interface for MessageEntry0
	/// </summary>
	public interface IMessageLogEntry0 : IMessageLogEntry
    {
        /// <summary>
        /// Formats this instance.
        /// </summary>
        /// <returns></returns>
        string Format();
    }

    /// <summary>
	/// The logging interface for MessageEntry1
	/// </summary>
	public interface IMessageLogEntry1 : IMessageLogEntry
    {
        /// <summary>
        /// Formats the specified argument.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns></returns>
        string Format(object arg);
    }

    /// <summary>
	/// The logging interface for MessageEntry2
	/// </summary>
	public interface IMessageLogEntry2 : IMessageLogEntry
    {
        /// <summary>
        /// Formats the specified arg1.
        /// </summary>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        /// <returns></returns>
        string Format(object arg1, object arg2);
    }

    /// <summary>
	/// The logging interface for MessageEntry3
	/// </summary>
	public interface IMessageLogEntry3 : IMessageLogEntry
    {
        /// <summary>
        /// Formats the specified arg1.
        /// </summary>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        /// <param name="arg3">The arg3.</param>
        /// <returns></returns>
        string Format(object arg1, object arg2, object arg3);
    }

    /// <summary>
	/// The logging interface for MessageEntryN
	/// </summary>
	public interface IMessageLogEntryN : IMessageLogEntry
    {
        /// <summary>
        /// Formats the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        string Format(params object[] args);
    }
}
