using System;
using System.Diagnostics;
using Cedar.Core.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrameworkTest.Logging
{
    [TestClass]
    public class LoggingUnitTest
    {
        [TestMethod]
        public void LoggingInfoTestMethod()
        {
            var modle = new
            {
                id = Guid.NewGuid().ToString(),
                name = Guid.NewGuid().ToString(),
                des = Guid.NewGuid().ToString()
            };
            LoggerFactories.CreateLogger().Write(modle, TraceEventType.Information);
            Assert.IsTrue(true);
        }
    }
}
