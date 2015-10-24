#region

using System;
using System.Collections.Generic;
using System.Linq;
using Cedar.Core.Configuration;
using Cedar.Core.EntLib.IoC.Configuration;
using Cedar.Core.IoC;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.Utility;

#endregion

namespace Cedar.Core.EntLib.IoC
{
    /// <summary>
    ///     The unity based service locator.
    /// </summary>
    [ConfigurationElement(typeof (UnityContainerServiceLocatorData)), MapTo(typeof (IServiceLocator), 0)]
    public class UnityContainerServiceLocator : ServiceLocatorBase
    {
        /// <summary>
        /// </summary>
        public UnityContainerServiceLocator()
        {
            UnityContainer = new UnityContainer();
            UnityContainer.AddNewExtension<DefaultInjectionExtension>();
            UnityConfigurationSection unityConfigurationSection;
            if (ConfigManager.TryGetConfigurationSection("unity", out unityConfigurationSection))
            {
                if (unityConfigurationSection.Containers.Any((ContainerElement c) => string.IsNullOrEmpty(c.Name)))
                {
                    unityConfigurationSection.Configure(UnityContainer);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="unityContainer"></param>
        public UnityContainerServiceLocator(IUnityContainer unityContainer)
        {
            Guard.ArgumentNotNull(unityContainer, "unityContainer");
            unityContainer.AddExtension(new DefaultInjectionExtension());
            UnityContainer = unityContainer;
        }

        /// <summary>
        ///     Gets the unity container.
        /// </summary>
        /// <value>
        ///     The unity container.
        /// </value>
        public IUnityContainer UnityContainer { get; }

        /// <summary>
        ///     Gets the service based on the specified registered type and optional registration name.
        /// </summary>
        /// <param name="registeredType">The service type registered.</param>
        /// <param name="name">The service type registration name.</param>
        /// <returns>
        ///     The service instance activated.
        /// </returns>
        public override object GetService(Type registeredType, string name = null)
        {
            Guard.ArgumentNotNull(registeredType, "registeredType");
            object result;
            try
            {
                string text;
                if (string.IsNullOrEmpty(name) &&
                    UnityContainer.Configure<DefaultInjectionExtension>().TryGetDefaultName(registeredType, out text))
                {
                    name = text;
                }
                result = (string.IsNullOrWhiteSpace(name)
                    ? UnityContainer.Resolve(registeredType)
                    : UnityContainer.Resolve(registeredType, name));
            }
            catch (ResolutionFailedException ex)
            {
                throw new ResolutionException(ex.Message, ex);
            }
            return result;
        }

        /// <summary>
        ///     Gets all services based on the specified registered type.
        /// </summary>
        /// <param name="registeredType">The service type registered.</param>
        /// <returns>
        ///     The list of acticated service instances.
        /// </returns>
        public override IEnumerable<object> GetAllServices(Type registeredType)
        {
            Guard.ArgumentNotNull(registeredType, "registeredType");
            IEnumerable<object> result;
            try
            {
                result = UnityContainer.ResolveAll(registeredType);
            }
            catch (ResolutionFailedException ex)
            {
                throw new ResolutionException(ex.Message, ex);
            }
            return result;
        }

        /// <summary>
        ///     Gets all keys (service type registrtion name) for the specified registered type.
        /// </summary>
        /// <param name="registeredType">The service type registered.</param>
        /// <returns>
        ///     The key list.
        /// </returns>
        public override IEnumerable<string> GetAllKeys(Type registeredType)
        {
            Guard.ArgumentNotNull(registeredType, "registeredType");
            return (
                from registration in UnityContainer.Registrations
                select registration.Name).ToArray();
        }

        /// <summary>
        ///     Determines whether the specified registered type is registered.
        /// </summary>
        /// <param name="registeredType">The service type registered.</param>
        /// <returns>
        ///     <c>true</c> if the specified registered type is registered; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsRegistered(Type registeredType)
        {
            Guard.ArgumentNotNull(registeredType, "registeredType");
            return
                UnityContainer.Registrations.Any(
                    (ContainerRegistration registration) => registration.RegisteredType == registeredType);
        }

        /// <summary>
        ///     Determines whether the specified registered type is registered.
        /// </summary>
        /// <param name="registeredType">The service type registered.</param>
        /// <param name="mappedToType">The concrete type to which the registered type is mapped.</param>
        /// <returns>
        ///     <c>true</c> if the specified registered type is registered; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsRegistered(Type registeredType, Type mappedToType)
        {
            Guard.ArgumentNotNull(registeredType, "registeredType");
            Guard.ArgumentNotNull(mappedToType, "mappedToType");
            return
                UnityContainer.Registrations.Any(
                    (ContainerRegistration registration) =>
                        registration.RegisteredType == registeredType && registration.MappedToType == mappedToType);
        }

        /// <summary>
        ///     Perform service type registration.
        /// </summary>
        /// <param name="registeredType">The service type registered.</param>
        /// <param name="mappedToType">The concrete type to which the registered type is mapped.</param>
        /// <param name="name">The service type registration name.</param>
        /// <param name="isDefault">if set to <c>true</c> [is default].</param>
        /// <param name="lifetime">The lifetime.</param>
        public override void Register(Type registeredType, Type mappedToType, string name = null, bool isDefault = false,
            Lifetime lifetime = Lifetime.Transient)
        {
            Guard.ArgumentNotNull(registeredType, "registeredType");
            Guard.ArgumentNotNull(mappedToType, "mappedToType");
            var lifetimeManager = (lifetime == Lifetime.Singleton)
                ? (LifetimeManager) new ContainerControlledLifetimeManager()
                : new TransientLifetimeManager();
            if (isDefault)
            {
                UnityContainer.Configure<DefaultInjectionExtension>().Register(registeredType, name);
            }
            UnityContainer.RegisterType(registeredType, mappedToType, name, lifetimeManager);
        }

        /// <summary>
        ///     Registers the specified new expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="creator">The new expression.</param>
        /// <param name="name">The name.</param>
        /// <param name="isDefault">if set to <c>true</c> [is default].</param>
        /// <param name="lifetime">The lifetime.</param>
        public override void Register<T>(Func<T> creator, string name = null, bool isDefault = false,
            Lifetime lifetime = Lifetime.Transient)
        {
            Guard.ArgumentNotNull(creator, "creator");
            if (isDefault)
            {
                UnityContainer.Configure<DefaultInjectionExtension>().Register<T>(name);
            }
            var lifetimeManager = (lifetime == Lifetime.Singleton)
                ? (LifetimeManager) new ContainerControlledLifetimeManager()
                : new TransientLifetimeManager();
            UnityContainer.RegisterType(typeof (T), name, lifetimeManager,
                new InjectionFactory((IUnityContainer container) => creator()));
        }
    }
}