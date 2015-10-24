#region

using System;
using System.Web;

#endregion

namespace Cedar.Framework.Common.Client
{
    /// <summary>
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// </summary>
        public static string keyOfErrorMessage = Guid.NewGuid().ToString();

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="errorMessage"></param>
        public static void SetErrorMessage(this HttpContext context, string errorMessage)
        {
            context.Items[keyOfErrorMessage] = errorMessage;
        }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetErrorMessage(this HttpContext context)
        {
            return context.Items[keyOfErrorMessage] as string;
        }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public static void ClearErrorMessage(this HttpContext context)
        {
            if (context.Items.Contains(keyOfErrorMessage))
            {
                context.Items.Remove(keyOfErrorMessage);
            }
        }
    }
}