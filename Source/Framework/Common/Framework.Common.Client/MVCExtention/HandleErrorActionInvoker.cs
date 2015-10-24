#region

using System.Web.Mvc;

#endregion

namespace Cedar.Framework.Common.Client.MVCExtention
{
    /// <summary>
    /// </summary>
    public class HandleErrorActionInvoker : ControllerActionInvoker
    {
        /// <summary>
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        public virtual ActionResult InvokeActionMethod(ControllerContext controllerContext,
            ActionDescriptor actionDescriptor)
        {
            var parameterValues = GetParameterValues(controllerContext, actionDescriptor);
            return base.InvokeActionMethod(controllerContext, actionDescriptor, parameterValues);
        }
    }
}