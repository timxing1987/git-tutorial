using System.Threading;
using Cedar.Core.ApplicationContexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrameworkTest.ApplicationContextTest
{
    [TestClass]
    public class ApplicationContextTestUnitTest
    {
        [TestMethod]
        public void ApplicationContextTestMethod_IsNotNull()
        {
            var applicationContext1 = ApplicationContext.Current;
            Assert.IsNotNull(applicationContext1);
        }

        [TestMethod]
        public void ApplicationContextTestMethod()
        {
            var applicationContext1 = ApplicationContext.Current;
            applicationContext1.UserId = "1";

            ApplicationContext applicationContext2 = null;
            var tread = new Thread(() =>
            {
                applicationContext2 = ApplicationContext.Current;
                applicationContext2.UserId = "2";
                Assert.AreEqual(applicationContext2.UserId, "2");
            });
            tread.Start();

            var tread2 = new Thread(() =>
            {
                applicationContext1.UserId = "2";
                Assert.AreEqual(applicationContext1.UserId, "2");
            });
            Assert.AreEqual(applicationContext1.UserId, "1");
            tread2.Start();
            Assert.AreEqual(applicationContext1.UserId, "1");
        }
    }
}