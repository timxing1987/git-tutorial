namespace Cedar.Framwork.AuditTrail
{
    /// <summary>
    ///     All of audit log filters used by audit log listeners to determine whether to write the given log entry.
    /// </summary>
    public interface IAuditLogFilter
    {
        /// <summary>
        ///     Gets the name of audit log filter.
        /// </summary>
        /// <value>
        ///     The name of audit log filter.
        /// </value>
        string Name { get; }

        /// <summary>
        ///     Matches the specified log entry.
        /// </summary>
        /// <param name="logEntry">The log entry.</param>
        /// <returns>A <see cref="T:System.Boolean" /> value indicating whether to match the specified log entry.</returns>
        bool Match(AuditLogEntry logEntry);
    }
}