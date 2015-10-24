#region

using System;
using System.Web.Mvc;

#endregion

namespace Cedar.Framework.Common.Client.ExceptionHandlers
{
    /// <summary>
    /// </summary>
    public class ExtendedHandleErrorInfo : HandleErrorInfo
    {
        /// <summary>
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="errorMessage"></param>
        public ExtendedHandleErrorInfo(Exception exception, string controllerName, string actionName,
            string errorMessage)
            : base(exception, controllerName, actionName)
        {
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// </summary>
        public string ErrorMessage { get; private set; }
    }
}