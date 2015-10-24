#region

using System;
using System.Configuration;

#endregion

namespace Cedar.Core.Configuration
{
    /// <summary>
    ///     以Name值作为键值的配置元素的元素集合
    /// </summary>
    /// <typeparam name="T">NamedConfigurationElement</typeparam>
    public class NamedElementCollection<T> : ConfigurationElementCollection where T : NamedConfigurationElement
    {
        /// <summary>
        ///     根据泛型NamedConfigurationElement的T创建实例
        /// </summary>
        /// <returns>NamedConfigurationElement</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return Activator.CreateInstance<T>();
        }

        /// <summary>
        ///     获取元素NamedConfigurationElement键值
        /// </summary>
        /// <param name="element">NamedConfigurationElement</param>
        /// <returns>NamedConfigurationElement.Name</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((NamedConfigurationElement) element).Name;
        }

        /// <summary>
        ///     通过Index获取NamedConfigurationElement
        /// </summary>
        /// <param name="index"></param>
        /// <returns>NamedConfigurationElement</returns>
        public T Get(int index)
        {
            return (T) BaseGet(index);
        }

        /// <summary>
        ///     通过name获取NamedConfigurationElement
        /// </summary>
        /// <param name="name"></param>
        /// <returns>NamedConfigurationElement</returns>
        public T Get(string name)
        {
            return (T) BaseGet(name);
        }
    }
}