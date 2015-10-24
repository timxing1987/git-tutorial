using System;
using Cedar.Core.ApplicationContexts;
using Cedar.Core.IoC;
using FrameworkTest.TestService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrameworkTest.Caching
{
    /// <summary>
    ///     Summary description for CachingUnitTest
    /// </summary>
    [TestClass]
    public class CachingUnitTest
    {
        private static ITestService iTestService;
        private static IServiceLocator iServiceLocate;

        public CachingUnitTest()
        {
            iServiceLocate = ServiceLocatorFactory.GetServiceLocator();
            iTestService = iServiceLocate.GetService<ITestService>();
        }

        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestMethod()
        {
            ApplicationContext.Current.UserId = Guid.NewGuid().ToString();
            ApplicationContext.Current.TransactionId = Guid.NewGuid().ToString();
            ApplicationContext.Current.UserName = Guid.NewGuid().ToString();
            //CachingSettings at = ConfigManager.GetConfigurationSection<CachingSettings>();
            //at.Configure(iServiceLocate);
            var result = iTestService.SayHelloCaching(1, new {id = "1", name = "name"});
            Assert.IsNotNull(result);
            var result2 = iTestService.SayHelloCaching(1, new {id = "1", name = "name"});
            Assert.AreEqual(result, result2);
            //Assert.AreEqual(result, "SayHelloResults");
        }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion
    }
}