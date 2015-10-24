#region

using System;

#endregion

namespace Cedar.Framework.Common.Client.ExceptionHandlers
{
    /// <summary>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ExceptionPolicyAttribute : Attribute
    {
        /// <summary>
        /// </summary>
        /// <param name="exceptionPolicyName"></param>
        public ExceptionPolicyAttribute(string exceptionPolicyName)
        {
            ExceptionPolicyName = exceptionPolicyName;
        }

        /// <summary>
        /// </summary>
        public string ExceptionPolicyName { get; private set; }
    }
}