#region

using System.Configuration;

#endregion

namespace Cedar.Core.Configuration
{
    /// <summary>
    ///     微软默认的配置元素,以Key值作为键值的配置元素,Key值规则可重写
    /// </summary>
    public abstract class KeyedConfigurationElement : ConfigurationElement
    {
        /// <summary>
        ///     Gets the key.
        /// </summary>
        /// <value>
        ///     The key.
        /// </value>
        public abstract object key { get; }
    }
}