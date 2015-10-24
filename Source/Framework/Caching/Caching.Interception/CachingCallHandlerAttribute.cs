using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Cedar.Framwork.Caching.Interception
{
    /// <summary>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class CachingCallHandlerAttribute : HandlerAttribute
    {
        private readonly TimeSpan expirationTime;

        /// <summary>
        ///     Creates a <see cref="T:Cedar.Framwork.Caching.Interception.CachingCallHandlerAttribute" /> using the default
        ///     expiration time of 5 minutes.
        /// </summary>
        public CachingCallHandlerAttribute()
        {
            //this.expirationTime = CachingCallHandler.DefaultExpirationTime;
        }

        /// <summary>
        ///     Creates a <see cref="T:Cedar.Framwork.Caching.Interception.CachingCallHandlerAttribute" /> using the given
        ///     expiration time.
        /// </summary>
        /// <param name="hours">Hours until expiration.</param>
        /// <param name="minutes">Minutes until expiration.</param>
        /// <param name="seconds">Seconds until expiration.</param>
        public CachingCallHandlerAttribute(int hours, int minutes, int seconds)
        {
            expirationTime = new TimeSpan(hours, minutes, seconds);
        }

        /// <summary>
        ///     Derived classes implement this method. When called, it
        ///     creates a new call handler as specified in the attribute
        ///     configuration.
        /// </summary>
        /// <returns>A new call handler object.</returns>
        public override ICallHandler CreateHandler(IUnityContainer ignored)
        {
            return new CachingCallHandler(expirationTime, Order);
        }
    }
}