using System.Configuration;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;

namespace Cedar.Core.EntLib.IoC.Configuration
{
    /// <summary>
    ///     The interceptor based configuration element.
    /// </summary>
    public class AutoInterceptorElement : DeserializableConfigurationElement
    {
        private const string typeProperty = "type";

        /// <summary>
        ///     Gets the injection.
        /// </summary>
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public InjectionMemberElementCollection Injection
        {
            get { return (InjectionMemberElementCollection) base[""]; }
        }

        /// <summary>
        ///     Gets or sets the name of the type.
        /// </summary>
        /// <value>
        ///     The name of the type.
        /// </value>
        [ConfigurationProperty("type", IsRequired = true)]
        public string TypeName
        {
            get { return (string) base["type"]; }
            set { base["type"] = value; }
        }
    }
}