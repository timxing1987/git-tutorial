#region

using System.Configuration;

#endregion

namespace Cedar.Core.Configuration
{
    /// <summary>
    ///     以Name为键值命名的配置元素
    /// </summary>
    public class NamedConfigurationElement : ConfigurationElement
    {
        /// <summary>
        ///     The name property name
        /// </summary>
        private const string NamePropertyName = "name";

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string) base["name"]; }
            set { base["name"] = value; }
        }
    }
}