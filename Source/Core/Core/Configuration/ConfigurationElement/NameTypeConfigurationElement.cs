#region

using System;
using System.ComponentModel;
using System.Configuration;
using System.Xml;

#endregion

namespace Cedar.Core.Configuration
{
    /// <summary>
    ///     以Name和Type为键值对的配置元素
    /// </summary>
    public class NameTypeConfigurationElement : ConfigurationElement
    {
        private const string NameProperty = "name";
        internal const string TypeProperty = "type";

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string) base["name"]; }
            set { base["name"] = value; }
        }

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        [TypeConverter(typeof (AssemblyQualifiedTypeNameConfigurationConverter)),
         ConfigurationProperty("type", IsRequired = true)]
        public Type Type
        {
            get { return (Type) base["type"]; }
            set { base["type"] = value; }
        }

        /// <summary>
        ///     Deserializes the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        internal void Deserialize(XmlReader reader)
        {
            DeserializeElement(reader, false);
        }
    }
}