namespace Cedar.Core.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// Creates a new logger.
        /// </summary>
        ILogger Create();
    }
}
