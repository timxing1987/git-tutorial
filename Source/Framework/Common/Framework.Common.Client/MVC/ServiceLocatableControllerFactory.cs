#region

using System;
using System.Web.Mvc;
using System.Web.Routing;
using Cedar.Core.IoC;

#endregion

namespace Cedar.Framework.Common.Client.MVC
{
    /// <summary>
    /// </summary>
    public class ServiceLocatableControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        /// </summary>
        /// <param name="serviceLocatorName"></param>
        public ServiceLocatableControllerFactory(string serviceLocatorName = "")
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
        ///     Retrieves the controller instance for the specified request context and controller type.
        /// </summary>
        /// <param name="requestContext">The context of the HTTP request, which includes the HTTP context and route data.</param>
        /// <param name="controllerType">The type of the controller.</param>
        /// <returns>
        ///     The controller instance.
        /// </returns>
        /// <exception cref="T:System.Web.HttpException">
        ///     <paramref name="controllerType" /> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="controllerType" /> cannot be assigned.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        ///     An instance of <paramref name="controllerType" /> cannot be
        ///     created.
        /// </exception>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (null == controllerType)
            {
                return null;
            }
            return (IController) ServiceLocator.GetService(controllerType, null);
        }
    }
}