using System;
using System.Diagnostics;
using Cedar.Core.Logging;
using log4net;

[assembly: log4net.Config.DOMConfigurator(ConfigFile = "ConfigFiles/log4net.config", Watch = true)]

namespace Cedar.Core.EntLib.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public class LoggerWrapper : ILogger
    {
        public static readonly ILog LogError;
        public static readonly ILog LogInfo;
        public static readonly ILog LogWarm;

        static LoggerWrapper()
        {
            LogError = LogManager.GetLogger("ERROR_loging");
            LogInfo = LogManager.GetLogger("INFO_loging");
            LogWarm = LogManager.GetLogger("WARN_loging");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="severity"></param>
        /// <param name="exception"></param>
        public void Write(object message, TraceEventType severity, Exception exception = null)
        {
            GenerateLog(severity).Invoke(message, exception);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="severity"></param>
        /// <returns></returns>
        private Action<object, Exception> GenerateLog(TraceEventType severity)
        {
            switch (severity)
            {
                case TraceEventType.Error:
                    return LogError.Error;
                case TraceEventType.Information:
                    return LogInfo.Info;
                case TraceEventType.Warning:
                    return LogWarm.Warn;
                case TraceEventType.Critical:
                    break;
                case TraceEventType.Verbose:
                    break;
                case TraceEventType.Start:
                    break;
                case TraceEventType.Stop:
                    break;
                case TraceEventType.Suspend:
                    break;
                case TraceEventType.Resume:
                    break;
                case TraceEventType.Transfer:
                    break;
                default:
                    return LogInfo.Info;
            }
            return LogInfo.Info;
        }
    }
}