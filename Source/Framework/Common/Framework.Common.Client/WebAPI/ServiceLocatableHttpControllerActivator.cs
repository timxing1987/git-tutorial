#region

using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Cedar.Core.IoC;
using Microsoft.Practices.Unity.Utility;

#endregion

namespace Cedar.Framework.Common.Client.WebAPI
{
    /// <summary>
    ///     A custom HttpControllerActivator which uses ServiceLocator to activate HttpController.
    /// </summary>
    public class ServiceLocatableHttpControllerActivator : IHttpControllerActivator
    {
        /// <summary>
        ///     Initializes a new instance of the ServiceLocatableHttpControllerActivator /> class.
        /// </summary>
        /// <param name="serviceLocatorName">Name of the service locator.</param>
        public ServiceLocatableHttpControllerActivator(string serviceLocatorName = null)
            : this(ServiceLocatorFactory.GetServiceLocator(serviceLocatorName))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the ServiceLocatableHttpControllerActivator /> class.
        /// </summary>
        /// <param name="serviceLocator">The service locator.</param>
        public ServiceLocatableHttpControllerActivator(IServiceLocator serviceLocator)
        {
            Guard.ArgumentNotNull(serviceLocator, "serviceLocator");
            ServiceLocator = serviceLocator;
        }

        /// <summary>
        ///     Gets the service locator.
        /// </summary>
        /// <value>
        ///     The service locator.
        /// </value>
        public IServiceLocator ServiceLocator { get; }

        /// <summary>
        ///     Creates the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="controllerDescriptor">The controller descriptor.</param>
        /// <param name="controllerType">Type of the controller.</param>
        /// <returns>The activated HttpController.</returns>
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            return ServiceLocator.GetService(controllerType, null) as IHttpController;
        }
    }
}