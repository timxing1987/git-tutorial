using System;
using Cedar.Core.ApplicationContexts;
using Cedar.Core.IoC;
using FrameworkTest.TestService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrameworkTest.AuditTrail
{
    [TestClass]
    public class AuditTrailUnitTest
    {
        //private static IAuditTrailManagementService iAuditTrailManagementService;
        private static ITestService iTestService;
        private static IServiceLocator iServiceLocate;

        public AuditTrailUnitTest()
        {
            //iAuditTrailManagementService = ServiceLocatorFactory.GetServiceLocator().GetService<IAuditTrailManagementService>();
            iServiceLocate = ServiceLocatorFactory.GetServiceLocator();
            iTestService = iServiceLocate.GetService<ITestService>();
        }

        [TestMethod]
        public void AuditTrailCallHandlerTestMethod()
        {
            ApplicationContext.Current.UserId = Guid.NewGuid().ToString();
            ApplicationContext.Current.TransactionId = Guid.NewGuid().ToString();
            ApplicationContext.Current.UserName = Guid.NewGuid().ToString();
            //AuditTrailSettings at = ConfigManager.GetConfigurationSection<AuditTrailSettings>();
            //at.Configure(iServiceLocate);
            var result = iTestService.SayHello(new {id = "1", name = "name"});
            Assert.IsFalse(string.IsNullOrEmpty(result));
        }
    }
}