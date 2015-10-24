#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Cedar.Core.IoC;
using Microsoft.Practices.Unity.Utility;

#endregion

namespace Cedar.Framework.Common.Client.WebAPI
{
    /// <summary>
    ///     A custom DependencyResolver which uses ServiceLocator to acticvate service.
    /// </summary>
    public class ServiceLocatableDependencyResolver : IDependencyResolver, IDependencyScope, IDisposable
    {
        private readonly List<IDisposable> disposableServices = new List<IDisposable>();

        /// <summary>
        ///     Initializes a new instance of the ServiceLocatableDependencyResolver class.
        /// </summary>
        /// <param name="serviceLocatorName">Name of the service locator.</param>
        public ServiceLocatableDependencyResolver(string serviceLocatorName = null)
        {
            ServiceLocator = ServiceLocatorFactory.GetServiceLocator(serviceLocatorName);
        }

        /// <summary>
        ///     Initializes a new instance of the ServiceLocatableDependencyResolver class.
        /// </summary>
        /// <param name="serviceLocator">The service locator.</param>
        public ServiceLocatableDependencyResolver(IServiceLocator serviceLocator)
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
        ///     Begins the scope.
        /// </summary>
        /// <returns>The created DependencyScope.</returns>
        public IDependencyScope BeginScope()
        {
            return new ServiceLocatableDependencyResolver(ServiceLocator);
        }

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
                var service = ServiceLocator.GetService(serviceType, null);
                AddDisposableService(service);
                result = service;
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
                var list = new List<object>();
                foreach (var current in ServiceLocator.GetAllServices(serviceType))
                {
                    AddDisposableService(current);
                    list.Add(current);
                }
                result = list;
            }
            catch (ResolutionException)
            {
                result = Enumerable.Empty<object>();
            }
            return result;
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            foreach (var current in disposableServices)
            {
                current.Dispose();
            }
        }

        private void AddDisposableService(object servie)
        {
            var disposable = servie as IDisposable;
            if (disposable != null && !disposableServices.Contains(disposable))
            {
                disposableServices.Add(disposable);
            }
        }
    }
}