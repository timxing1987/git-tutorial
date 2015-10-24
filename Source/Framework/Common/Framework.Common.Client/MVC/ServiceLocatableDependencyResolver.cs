#region

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Cedar.Core.IoC;
using Microsoft.Practices.Unity.Utility;

#endregion

namespace Cedar.Framework.Common.Client.MVC
{
    /// <summary>
    ///     A custom DependencyResolver which uses ServiceLocator to acticvate service.
    /// </summary>
    public class ServiceLocatableDependencyResolver : IDependencyResolver
    {
        /// <summary>
        /// </summary>
        /// <param name="serviceLocatorName"></param>
        public ServiceLocatableDependencyResolver(string serviceLocatorName = "")
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
        ///     Gets the service.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>The activated service instance.</returns>
        public object GetService(Type serviceType)
        {
            Guard.ArgumentNotNull(serviceType, "serviceType");
            object result;
            try
            {
                result = ServiceLocator.GetService(serviceType, null);
            }
            catch (ResolutionException)
            {
                result = null;
            }
            return result;
        }

        /// <summary>
        ///     Gets the services.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>The activated service instances.</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            Guard.ArgumentNotNull(serviceType, "serviceType");
            IEnumerable<object> result;
            try
            {
                result = ServiceLocator.GetAllServices(serviceType);
            }
            catch (ResolutionException)
            {
                result = null;
            }
            return result;
        }
    }
}