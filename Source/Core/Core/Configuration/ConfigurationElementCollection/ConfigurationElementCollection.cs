#region

using System;
using System.Configuration;

#endregion

namespace Cedar.Core.Configuration
{
    /// <summary>
    ///     默认的元素集合,以Key值作为键值的配置元素的元素集合
    /// </summary>
    /// <typeparam name="T">类型为KeyedConfigurationElement</typeparam>
    public class ConfigurationElementCollection<T> : ConfigurationElementCollection where T : KeyedConfigurationElement
    {
        /// <summary>
        ///     根据泛型KeyedConfigurationElement的T创建实例
        /// </summary>
        /// <returns>KeyedConfigurationElement</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return Activator.CreateInstance<T>();
        }

        /// <summary>
        ///     获取元素KeyedConfigurationElement键值
        /// </summary>
        /// <param name="element">KeyedConfigurationElement</param>
        /// <returns>KeyedConfigurationElement.Key</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((KeyedConfigurationElement) element).key;
        }
    }
}