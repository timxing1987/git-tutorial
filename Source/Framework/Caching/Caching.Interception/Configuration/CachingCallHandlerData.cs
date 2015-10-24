using System;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.Unity;

namespace Cedar.Framwork.Caching.Interception.Configuration
{
    public class CachingCallHandlerData : CallHandlerData
    {
        private const string ExpirationTimePropertyName = "expirationTime";

        /// <summary>
        ///     Create a new <see cref="T:Cedar.Framwork.Caching.Interception.Configuration.CachingCallHandlerData" /> instance.
        /// </summary>
        public CachingCallHandlerData()
        {
            ExpirationTime = CachingCallHandler.DefaultExpirationTime;
        }

        /// <summary>
        ///     Create a new <see cref="T:Cedar.Framwork.Caching.Interception.Configuration.CachingCallHandlerData" /> instance
        ///     with the given name.
        /// </summary>
        /// <param name="handlerName">Name of handler to store in config file.</param>
        public CachingCallHandlerData(string handlerName) : base(handlerName, typeof (CachingCallHandler))
        {
            ExpirationTime = CachingCallHandler.DefaultExpirationTime;
        }

        /// <summary>
        ///     Create a new <see cref="T:Cedar.Framwork.Caching.Interception.Configuration.CachingCallHandlerData" /> instance
        ///     with the given name.
        /// </summary>
        /// <param name="handlerName">Name of handler to store in config file.</param>
        /// <param name="handlerOrder">Order of handler to store in config file.</param>
        public CachingCallHandlerData(string handlerName, int handlerOrder)
            : base(handlerName, typeof (CachingCallHandler), handlerOrder)
        {
            ExpirationTime = CachingCallHandler.DefaultExpirationTime;
        }

        /// <summary>
        ///     Expiration time
        /// </summary>
        /// <value>The "expirationTime" attribute</value>
        [ConfigurationProperty("expiration")]
        public TimeSpan ExpirationTime
        {
            get { return (TimeSpan) base["expiration"]; }
            set { base["expiration"] = value; }
        }

        /// <summary>
        ///     Configures an <see cref="T:Microsoft.Practices.Unity.IUnityContainer" /> to resolve the represented call handler by
        ///     using the specified name.
        /// </summary>
        /// <param name="container">The container to configure.</param>
        /// <param name="registrationName">The name of the registration.</param>
        protected void DoConfigureContainer(IUnityContainer container, string registrationName)
        {
            container.RegisterType<UnityContainer>(registrationName, new InjectionConstructor(ExpirationTime),
                new InjectionProperty("Order", Order));
        }
    }
}