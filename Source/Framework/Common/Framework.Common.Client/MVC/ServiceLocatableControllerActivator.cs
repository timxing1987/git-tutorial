#region

using System;
using System.Web.Mvc;
using System.Web.Routing;
using Cedar.Core.IoC;
using Microsoft.Practices.Unity.Utility;

#endregion

namespace Cedar.Framework.Common.Client.MVC
{
    /// <summary>
    ///     A custom controller activator, which uses specified service locator for controller activation.
    /// </summary>
    public class ServiceLocatableControllerActivator : IControllerActivator
    {
        /// <summary>
        /// </summary>
        /// <param name="serviceLocatorName"></param>
        public ServiceLocatableControllerActivator(string serviceLocatorName = "")
        {
            if (string.IsNullOrEmpty(serviceLocatorName))
            {
                ServiceLocator = ServiceLocatorFactory.GetServiceLocator(null);
                return;
            }
            ServiceLocator = ServiceLocatorFactory.GetServiceLocator(serviceLocatorName);
        }

        /// <summary>
        ///     Gets the service locator.
        /// </summary>
        public IServiceLocator ServiceLocator { get; }

        /// <summary>
        ///     Creates the specified request context.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <param name="controllerType">Type of the controller.</param>
        /// <returns>The activated controller instance.</returns>
        public IController Create(RequestContext requestContext, Type controllerType)
        {
            Guard.ArgumentNotNull(requestContext, "requestContext");
            Guard.ArgumentNotNull(controllerType, "controllerType");
            return ServiceLocator.GetService(controllerType, null) as IController;
        }
    }
}