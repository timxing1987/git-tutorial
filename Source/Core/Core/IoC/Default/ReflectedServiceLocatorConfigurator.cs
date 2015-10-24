#region

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity.Utility;

#endregion

namespace Cedar.Core.IoC
{
    /// <summary>
    ///     反射serviceLocator配置者
    /// </summary>
    internal class ReflectedServiceLocatorConfigurator : IServiceLocatorConfigurator
    {
        private readonly object syncHelper = new object();
        private Dictionary<MapToAttribute, Type> mapToAttributes;

        /// <summary>
        ///     构造函数，获取可Resolver的集合
        /// </summary>
        /// <param name="assemblyResolver"></param>
        public ReflectedServiceLocatorConfigurator(IAssemblyResolver assemblyResolver)
        {
            Guard.ArgumentNotNull(assemblyResolver, "assemblyResolver");
            AssemblyResolver = assemblyResolver;
        }

        /// <summary>
        /// </summary>
        public IAssemblyResolver AssemblyResolver { get; }

        /// <summary>
        ///     对serviceLocator进行配置服务
        /// </summary>
        /// <param name="serviceLocator"></param>
        public void Configure(IServiceLocator serviceLocator)
        {
            foreach (var current in
                from attribute in GetMapToAttributes().Keys
                group attribute by attribute.RegisteredType)
            {
                if (!serviceLocator.IsRegistered(current.Key))
                {
                    var mapToAttribute = current.OrderByDescending((MapToAttribute a) => a.Quality).First();
                    serviceLocator.Register(mapToAttribute.RegisteredType, mapToAttributes[mapToAttribute], null, true,
                        mapToAttribute.Lifetime);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public Dictionary<MapToAttribute, Type> GetMapToAttributes()
        {
            if (mapToAttributes != null)
            {
                return mapToAttributes;
            }
            lock (syncHelper)
            {
                if (mapToAttributes != null)
                {
                    return mapToAttributes;
                }
                mapToAttributes = new Dictionary<MapToAttribute, Type>();
                foreach (var current in AssemblyResolver.GetAssemblies())
                {
                    var types = current.GetTypes();
                    for (var i = 0; i < types.Length; i++)
                    {
                        var type = types[i];
                        foreach (var current2 in AttributeAccessor.GetAttributes<MapToAttribute>(type, false)
                            )
                        {
                            mapToAttributes.Add(current2, type);
                        }
                    }
                }
            }
            return mapToAttributes;
        }

        /// <summary>
        ///     通过属性映射创建实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public T CreateInstance<T>(params object[] args)
        {
            var mapToAttribute = (
                from attribute in GetMapToAttributes().Keys
                where attribute.RegisteredType == typeof (T)
                orderby attribute.Quality descending
                select attribute).FirstOrDefault();
            if (mapToAttribute != null)
            {
                return (T) Activator.CreateInstance(mapToAttributes[mapToAttribute], args);
            }
            return default(T);
        }
    }
}