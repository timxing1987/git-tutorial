#region

using System;
using Microsoft.Practices.Unity.Utility;

#endregion

namespace Cedar.Core.Configuration
{
    /// <summary>
    ///     配置元素Section属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigurationSectionNameAttribute : Attribute
    {
        /// <summary>
        /// </summary>
        /// <param name="sectionName"></param>
        public ConfigurationSectionNameAttribute(string sectionName)
        {
            Guard.ArgumentNotNullOrEmpty(sectionName, "sectionName");
            SectionName = sectionName;
        }

        /// <summary>
        ///     Gets the name of the configuration section.
        /// </summary>
        /// <value>
        ///     The name of the configuration section.
        /// </value>
        public string SectionName { get; private set; }
    }
}