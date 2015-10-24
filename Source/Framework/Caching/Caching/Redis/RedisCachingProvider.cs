using System;
using Cedar.Core.Configuration;
using Cedar.Core.EntLib.Data;
using Newtonsoft.Json;

namespace Cedar.Framwork.Caching.Redis
{
    /// <summary>
    /// </summary>
    [ConfigurationElement(typeof (RedisCachingData))]
    public class RedisCachingProvider : CachingProviderBase
    {
        /// <summary>
        /// </summary>
        /// <param name="isenable"></param>
        /// <param name="expireSpan"></param>
        /// <param name="redisDatabaseWrapper"></param>
        public RedisCachingProvider(bool isenable, TimeSpan expireSpan, RedisDatabaseWrapper redisDatabaseWrapper)
            : base(isenable)
        {
            RedisDatabaseWrapper = redisDatabaseWrapper;
            ExpireSpan = expireSpan;
        }

        public RedisDatabaseWrapper RedisDatabaseWrapper { get; }

        private TimeSpan ExpireSpan { get; }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirationTime"></param>
        protected override void AddCore(string key, object value, TimeSpan expirationTime)
        {
            if (RedisDatabaseWrapper.KeyExists(key)) return;

            RedisDatabaseWrapper.StringSet(key, JsonConvert.SerializeObject(value));
            RedisDatabaseWrapper.KeyExpire(key,
                expirationTime == new TimeSpan(0, 0, 0) ? ExpireSpan : expirationTime);
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected override object GetCore(string key)
        {
            var keyExists = RedisDatabaseWrapper.KeyExists(key);
            if (!keyExists) return null;

            var data = RedisDatabaseWrapper.StringGet(key);
            var dedata = JsonConvert.DeserializeObject(data);
            return dedata;
        }

        /// <summary>
        /// </summary>
        protected override void ClearCore()
        {
            throw new NotImplementedException();
        }
    }
}