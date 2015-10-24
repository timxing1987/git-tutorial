#region

using System;
using Microsoft.Practices.Unity.Utility;

#endregion

namespace Cedar.Core.Configuration
{
    /// <summary>
    ///     配置元素属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigurationElementAttribute : Attribute
    {
        /// <summary>
        ///     初始化一个新的实例配置元素类型
        /// </summary>
        /// <param name="configurationElementType">配置元素类型</param>
        public ConfigurationElementAttribute(Type configurationElementType)
        {
            Guard.ArgumentNotNull(configurationElementType, "configurationElementType");
            ConfigurationElementType = configurationElementType;
        }

        /// <summary>
        ///     获取配元素类型
        /// </summary>
        public Type ConfigurationElementType { get; private set; }
    }
}