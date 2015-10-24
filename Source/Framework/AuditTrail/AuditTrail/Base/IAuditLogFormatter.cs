namespace Cedar.Framwork.AuditTrail
{
    /// <summary>
    ///     All of the audit log formatters used to forma the log data object as a literal text must implement this interface.
    /// </summary>
    public interface IAuditLogFormatter
    {
        /// <summary>
        ///     Formats the specified log dataas a literal text.
        /// </summary>
        /// <param name="logData">The log data object.</param>
        /// <returns>The formatted literal text.</returns>
        string Format(object logData);
    }
}