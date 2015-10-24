using Cedar.Core.IoC;

namespace Cedar.Core.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public static class LoggerFactories
    {
        /// <summary>
        /// Gets the ILogger.
        /// </summary>
        /// <returns>The <see cref="T:Cedar.Core.Logging.ILogger" />.</returns>
        public static ILogger CreateLogger()
        {
            IServiceLocator serviceLocator = ServiceLocatorFactory.GetServiceLocator(null);
            ILoggerFactory service = serviceLocator.GetService<ILoggerFactory>(null);
            return service.Create();
        }
    }
}
