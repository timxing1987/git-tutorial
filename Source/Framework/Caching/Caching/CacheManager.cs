using System;
using Cedar.Core.IoC;
using Cedar.Framwork.Caching;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;

namespace Cedar.AuditTrail.Interception
{
    public class CacheManager : IDisposable
    {
        private static readonly object syncHelper = new object();
        private static CachingProviderBase provider;
        private bool disposed;

        private CacheManager()
        {
            Providers = new ServiceLocatableDictionary<CachingProviderBase>(null);
        }

        /// <summary>
        ///     Gets the providers.
        /// </summary>
        /// <value>
        ///     The providers.
        /// </value>
        public static ServiceLocatableDictionary<CachingProviderBase> Providers { get; private set; }

        /// <summary>
        ///     Gets the provider.
        /// </summary>
        /// <value>
        ///     The provider.
        /// </value>
        public static CachingProviderBase Provider
        {
            get
            {
                if (provider != null)
                {
                    return provider;
                }
                CachingProviderBase result;
                lock (syncHelper)
                {
                    if (provider != null)
                    {
                        result = provider;
                    }
                    else
                    {
                        result =
                            (provider =
                                ServiceLocatorFactory.GetServiceLocator(null).GetService<CachingProviderBase>(null));
                    }
                }
                return result;
            }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            EnsureNotDisposed();
            disposed = true;
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(string key)
        {
            return Provider.Get(key);
        }

        /// <summary>
        /// </summary>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public static CacheManager CreateCacheManager(string functionName)
        {
            Guard.ArgumentNotNullOrEmpty(functionName, "functionName");
            return new CacheManager();
        }

        private void EnsureNotDisposed()
        {
            if (disposed)
            {
                throw new InvalidOperationException("ExceptionLoggerIsDisposed");
            }
        }
    }
}