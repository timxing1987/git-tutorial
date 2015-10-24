#region

using System.Configuration;
using Cedar.Core.Configuration;

#endregion

namespace Cedar.Core.IoC.Configuration
{
    /// <summary>
    ///     The configuration element to configure assembly name or path.
    /// </summary>
    public class AssemblyConfigurationElement : KeyedConfigurationElement
    {
        private const string AssemblyProperty = "assembly";

        /// <summary>
        ///     Gets or sets the assembly.
        /// </summary>
        /// <value>
        ///     The assembly.
        /// </value>
        [ConfigurationProperty("assembly", IsRequired = true)]
        public string Assembly
        {
            get { return (string) base["assembly"]; }
            set { base["assembly"] = value; }
        }

        /// <summary>
        ///     Gets the key.
        /// </summary>
        /// <value>
        ///     The key.
        /// </value>
        public override object key
        {
            get { return Assembly; }
        }
    }
}