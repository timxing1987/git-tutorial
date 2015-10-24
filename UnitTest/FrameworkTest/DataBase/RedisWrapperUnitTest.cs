using System;
using System.Threading;
using Cedar.Core.EntLib.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrameworkTest.DataBase
{
    /// <summary>
    ///     Summary description for CachingUnitTest
    /// </summary>
    [TestClass]
    public class RedisWrapperUnitTest
    {
        private readonly RedisDatabaseWrapper _wrapper;
        private readonly string key = DateTime.Now.ToString("yyyyMMddHHmmss");

        public RedisWrapperUnitTest()
        {
            _wrapper = new RedisDatabaseWrapper("172.16.0.204", 4);
        }

        [TestMethod]
        public void HashSetTestMethod()
        {
            var hashSetresult = _wrapper.HashSet("hash", key, key);
            var hashGetresult = _wrapper.HashGet("hash", key);
            Assert.IsTrue(hashSetresult);
            Assert.IsNotNull(hashGetresult);
            Assert.AreEqual(hashGetresult, key);
        }


        [TestMethod]
        public void keyExpireTestMethod()
        {
            var timespan = new TimeSpan(0, 0, 2);
            var hashSetresult = _wrapper.StringSet(key, key);
            var hashGetresult = _wrapper.StringGet(key);
            var keyExpireresult = _wrapper.KeyExpire(key, timespan);
            Thread.Sleep(timespan);
            var newhashGetresult = _wrapper.StringGet(key);
            Assert.IsTrue(hashSetresult);
            Assert.IsNotNull(hashGetresult);
            Assert.AreEqual(hashGetresult, key);
            Assert.IsTrue(keyExpireresult);
            Assert.IsNull(newhashGetresult);
        }
    }
}