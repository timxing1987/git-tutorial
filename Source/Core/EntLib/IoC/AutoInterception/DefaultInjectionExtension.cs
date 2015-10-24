#region

using System;
using System.Collections.Concurrent;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Utility;

#endregion

namespace Cedar.Core.EntLib.IoC
{
    internal class DefaultInjectionExtension : UnityContainerExtension
    {
        public DefaultInjectionExtension()
        {
            DefaultNames = new ConcurrentDictionary<Type, string>();
        }

        public ConcurrentDictionary<Type, string> DefaultNames { get; }

        public void Register(Type type, string name)
        {
            Guard.ArgumentNotNull(type, "type");
            DefaultNames[type] = name;
        }

        public void Register<T>(string name)
        {
            Register(typeof (T), name);
        }

        public bool TryGetDefaultName(Type type, out string name)
        {
            Guard.ArgumentNotNull(type, "type");
            return DefaultNames.TryGetValue(type, out name);
        }

        public bool TryGetDefaultName<T>(out string name)
        {
            return DefaultNames.TryGetValue(typeof (T), out name);
        }

        protected override void Initialize()
        {
            //ExtendedInterception interception = new ExtendedInterception();
            //interception.Interceptor = new TransparentProxyInterceptor();
            //this.Container.AddExtension(interception);
        }
    }
}