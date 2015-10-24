using Cedar.Core.SettingSource;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrameworkTest.Core
{
    [TestClass]
    public class SettingSourceUnitTest
    {
        [TestMethod]
        public void SettingSourceFactoryTestMethod_IsNotNull()
        {
            var setting = SettingSourceFactory.GetSettingSource();
            Assert.IsNotNull(setting);
        }

        [TestMethod]
        public void SettingSourceFactoryTestMethod_ByName()
        {
            var setting2 = SettingSourceFactory.GetSettingSource("SimpleFileSettingSource");
            Assert.IsNotNull(setting2);
        }
    }
}