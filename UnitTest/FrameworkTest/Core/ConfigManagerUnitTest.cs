using Cedar.Core;
using Cedar.Core.IoC.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrameworkTest.Core
{
    [TestClass]
    public class ConfigManagerUnitTest
    {
        [TestMethod]
        public void ConfigManagerTestMethod_Connectstrings()
        {
            var connectstrings = ConfigManager.ConnectionStrings;
            Assert.IsNotNull(connectstrings);
            Assert.IsTrue(connectstrings.Count > 0);
        }

        [TestMethod]
        public void ConfigManagerTestMethod_GetConfigurationSection()
        {
            ServiceLocationSettings section;
            var result = ConfigManager.TryGetConfigurationSection("cedar.serviceLocation", out section);
            Assert.IsTrue(result);
            Assert.IsNotNull(section);
            Assert.IsNotNull(section.DefaultServiceLocator);
            Assert.IsNotNull(section.ResolvedAssemblies);
            Assert.IsNotNull(section.ServiceLocators);
        }

        [TestMethod]
        public void ConfigManagerTestMethod_GetConfigurationSectionDefault()
        {
            ServiceLocationSettings section;
            var result = ConfigManager.TryGetConfigurationSection(out section);
            Assert.IsTrue(result);
            Assert.IsNotNull(section);
            Assert.IsNotNull(section.DefaultServiceLocator);
            Assert.IsNotNull(section.ResolvedAssemblies);
            Assert.IsNotNull(section.ServiceLocators);
        }
    }
}