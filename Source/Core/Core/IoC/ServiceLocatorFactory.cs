#region

using System;
using System.Collections.Generic;
using System.Reflection;
using Cedar.Core.Configuration;
using Cedar.Core.IoC.Configuration;

#endregion

namespace Cedar.Core.IoC
{
    /// <summary>
    ///     This is the factory to create or get service locator.
    /// </summary>
    public static class ServiceLocatorFactory
    {
        private static IServiceLocator serviceLocator;
        private static readonly Dictionary<string, IServiceLocator> serviceLocators;
        private static readonly object syncHelper;
        private static List<Assembly> assemblies;
        private static ReflectedServiceLocatorConfigurator reflectedServiceLocatorConfigurator;

        /// <summary>
        /// </summary>
        static ServiceLocatorFactory()
        {
            syncHelper = new object();
            serviceLocators = new Dictionary<string, IServiceLocator>();
            Configurators = new List<IServiceLocatorConfigurator>();
            assemblies = InitializeResolvedAssemblies();
            reflectedServiceLocatorConfigurator =
                new ReflectedServiceLocatorConfigurator(new DefaultAssemblyResolver(assemblies));
        }

        /// <summary>
        ///     Gets the configurators.
        /// </summary>
        /// <value>
        ///     The configurators.
        /// </value>
        public static IList<IServiceLocatorConfigurator> Configurators { get; }

        /// <summary>
        ///     Gets the service locator.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The service locator.</returns>
        public static IServiceLocator GetServiceLocator(string name = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                if (ServiceLocatorFactory.serviceLocator != null)
                {
                    return ServiceLocatorFactory.serviceLocator;
                }
                ServiceLocationSettings configurationSection;
                IServiceLocator serviceLocator;
                //通过配置项获取默认ServiceLocator
                if (ConfigManager.TryGetConfigurationSection(out configurationSection))
                {
                    serviceLocator = configurationSection.GetServiceLocator(null);
                    if (serviceLocator != null)
                    {
                        ServiceLocatorFactory.serviceLocator = ConfigureServiceLocator(serviceLocator);
                        return ServiceLocatorFactory.serviceLocator;
                    }
                }

                //当没有默认配置项时,通过反射serviceLocator方式来获取实例
                serviceLocator = reflectedServiceLocatorConfigurator.CreateInstance<IServiceLocator>();
                if (serviceLocator == null)
                {
                    throw new InvalidOperationException("Resources.ExceptionDefaultServiceLocatorNotExists");
                }
                return ServiceLocatorFactory.serviceLocator = ConfigureServiceLocator(serviceLocator);
            }
            else
            {
                IServiceLocator serviceLocator;
                if (serviceLocators.TryGetValue(name, out serviceLocator))
                {
                    return serviceLocator;
                }

                IServiceLocator result;
                lock (syncHelper)
                {
                    if (serviceLocators.TryGetValue(name, out serviceLocator))
                    {
                        result = serviceLocator;
                    }
                    else
                    {
                        var configurationSection = ConfigManager.GetConfigurationSection<ServiceLocationSettings>();
                        serviceLocator = ConfigureServiceLocator(configurationSection.GetServiceLocator(name));
                        serviceLocators[name] = serviceLocator;
                        result = serviceLocator;
                    }
                }
                return result;
            }
        }

        /// <summary>
        ///     Adds the resovled assemblies.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        public static void AddResovledAssemblies(params Assembly[] assemblies)
        {
            Reset();
            assemblies = (assemblies ?? new Assembly[0]);
            var array = assemblies;
            for (var i = 0; i < array.Length; i++)
            {
                var item = array[i];
                if (!ServiceLocatorFactory.assemblies.Contains(item))
                {
                    ServiceLocatorFactory.assemblies.Add(item);
                }
            }
        }

        /// <summary>
        ///     Clear all cached service locators.
        /// </summary>
        public static void Reset()
        {
            lock (syncHelper)
            {
                serviceLocator = null;
                serviceLocators.Clear();
                Configurators.Clear();
                assemblies = InitializeResolvedAssemblies();
                reflectedServiceLocatorConfigurator =
                    new ReflectedServiceLocatorConfigurator(new DefaultAssemblyResolver(assemblies));
            }
        }

        internal static IServiceLocator ConfigureServiceLocator(IServiceLocator serviceLocator)
        {
            foreach (var current in Configurators)
            {
                current.Configure(serviceLocator);
            }
            foreach (var current2 in Settings.SectionNames)
            {
                ServiceLocatableSettings serviceLocatableSettings;
                if (ConfigManager.TryGetConfigurationSection(current2, out serviceLocatableSettings))
                {
                    serviceLocatableSettings.Configure(serviceLocator);
                }
            }
            reflectedServiceLocatorConfigurator.Configure(serviceLocator);
            return serviceLocator;
        }

        private static bool TryParseAssemblyName(string displayName, out AssemblyName assemblyName)
        {
            assemblyName = null;
            bool result;
            try
            {
                assemblyName = new AssemblyName(displayName);
                result = (null != assemblyName);
            }
            catch
            {
                result = false;
            }
            return result;
        }

        private static List<Assembly> InitializeResolvedAssemblies()
        {
            var list = new List<Assembly>();
            //获取默认系统的Assembly
            foreach (var current in Assemblies.GetAssemblies())
            {
                list.Add(current);
            }

            ServiceLocationSettings serviceLocationSettings;
            if (ConfigManager.TryGetConfigurationSection(out serviceLocationSettings) &&
                serviceLocationSettings.ResolvedAssemblies != null)
            {
                foreach (
                    AssemblyConfigurationElement assemblyConfigurationElement in
                        serviceLocationSettings.ResolvedAssemblies)
                {
                    AssemblyName assemblyRef;
                    if (TryParseAssemblyName(assemblyConfigurationElement.Assembly, out assemblyRef))
                    {
                        var assembly = Assembly.Load(assemblyRef);
                        if (null != assembly && !list.Contains(assembly))
                        {
                            list.Add(assembly);
                        }
                    }
                    else
                    {
                        var assembly2 = Assembly.LoadFile(assemblyConfigurationElement.Assembly);
                        if (null != assembly2 && !list.Contains(assembly2))
                        {
                            list.Add(assembly2);
                        }
                    }
                }
            }
            return list;
        }
    }
}