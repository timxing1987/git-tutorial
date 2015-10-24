#region

using System;

#endregion

namespace Cedar.Framework.Common.Client.ExceptionHandlers
{
    /// <summary>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class HandleErrorActionAttribute : Attribute
    {
        /// <summary>
        /// </summary>
        /// <param name="handleErrorAction"></param>
        public HandleErrorActionAttribute(string handleErrorAction = "")
        {
            HandleErrorAction = handleErrorAction;
        }

        /// <summary>
        /// </summary>
        public string HandleErrorAction { get; private set; }
    }
}