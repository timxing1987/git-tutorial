using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using StackExchange.Redis;

namespace Cedar.Core.EntLib.Data
{
    //[MapTo(typeof(RedisDatabaseWrapper), 0, Lifetime = Lifetime.Singleton)]
    public class RedisDatabaseWrapper : IDisposable
    {
        private static IConnectionMultiplexer _connectionMultiplexer;
        private readonly int database;

        public RedisDatabaseWrapper(string ip, int database, string password = null, int port = 6379)
        {
            var options = new ConfigurationOptions
            {
                EndPoints =
                {
                    new DnsEndPoint(ip, port)
                },
                KeepAlive = 180,
                Password = password,
                //DefaultVersion = new Version("2.8.5"),
                // Needed for cache clear
                AllowAdmin = true
            };
            this.database = database;
            _connectionMultiplexer = ConnectionMultiplexer.Connect(options);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool StringSet(string key, string value)
        {
            var db = _connectionMultiplexer.GetDatabase(database);
            return db.StringSet(key, value);
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool KeyExpire(string key, TimeSpan value)
        {
            var db = _connectionMultiplexer.GetDatabase(database);
            return db.KeyExpire(key, value);
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyExists(string key)
        {
            var db = _connectionMultiplexer.GetDatabase(database);
            return db.KeyExists(key);
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string StringGet(string key)
        {
            var db = _connectionMultiplexer.GetDatabase(database);
            return db.StringGet(key);
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyDelete(string key)
        {
            var db = _connectionMultiplexer.GetDatabase(database);
            return db.KeyDelete(key);
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashfield"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool HashSet(string key, string hashfield, string value)
        {
            var db = _connectionMultiplexer.GetDatabase(database);
            return db.HashSet(key, hashfield, value);
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> HashValues(string key)
        {
            var db = _connectionMultiplexer.GetDatabase(database);
            var results = db.HashValues(key);
            var list = results.Select(item => (string) item).ToList();
            return list;
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashfield"></param>
        /// <returns></returns>
        public string HashGet(string key, string hashfield)
        {
            var db = _connectionMultiplexer.GetDatabase(database);
            var results = db.HashGet(key, hashfield);
            return results;
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Dictionary<string, string> HashGetAll(string key)
        {
            var db = _connectionMultiplexer.GetDatabase(database);
            var results = db.HashGetAll(key);
            var dic = results.ToDictionary<HashEntry, string, string>(item => item.Name, item => item.Value);
            return dic;
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long HashLength(string key)
        {
            var db = _connectionMultiplexer.GetDatabase(database);
            var results = db.HashLength(key);
            return results;
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashfield"></param>
        /// <returns></returns>
        public bool HashExists(string key, string hashfield)
        {
            var db = _connectionMultiplexer.GetDatabase(database);
            var results = db.HashExists(key, hashfield);
            return results;
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashfield"></param>
        /// <returns></returns>
        public bool HashDelete(string key, string hashfield)
        {
            var db = _connectionMultiplexer.GetDatabase(database);
            var results = db.HashDelete(key, hashfield);
            return results;
        }
    }
}