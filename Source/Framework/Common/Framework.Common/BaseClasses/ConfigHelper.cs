using System.Configuration;

namespace Cedar.Framework.Common.BaseClasses
{
    /// <summary>
    /// </summary>
    public class ConfigHelper
    {
        /// <summary>
        ///     根据key获取value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSettings(string key)
        {
            return ConfigurationManager.AppSettings[key] ?? "";
        }
    }
}