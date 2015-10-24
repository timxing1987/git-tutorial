#region

using System;

#endregion

namespace Cedar.Framework.Common.Client.ExceptionHandlers
{
    /// <summary>
    /// </summary>
    public class ExceptionDetail
    {
        /// <summary>
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="errorMessage"></param>
        public ExceptionDetail(Exception exception, string errorMessage = null)
        {
            HelpLink = exception.HelpLink;
            Message = string.IsNullOrEmpty(errorMessage) ? exception.Message : errorMessage;
            StackTrace = exception.StackTrace;
            Type = exception.GetType().ToString();
            if (exception.InnerException != null)
            {
                InnerException = new ExceptionDetail(exception.InnerException);
            }
        }

        /// <summary>
        /// </summary>
        public string HelpLink { get; set; }

        /// <summary>
        /// </summary>
        public ExceptionDetail InnerException { get; set; }

        /// <summary>
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// </summary>
        public string Type { get; set; }
    }
}