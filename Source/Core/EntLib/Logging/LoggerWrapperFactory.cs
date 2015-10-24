using Cedar.Core.IoC;
using Cedar.Core.Logging;

namespace Cedar.Core.EntLib.Logging
{
    /// <summary>
    /// 
    /// </summary>
    [MapTo(typeof(ILoggerFactory), 0, Lifetime = Lifetime.Singleton)]
    public class LoggerWrapperFactory : ILoggerFactory
    {
        public ILogger Create()
        {
            return new LoggerWrapper();
        }
    }
}
