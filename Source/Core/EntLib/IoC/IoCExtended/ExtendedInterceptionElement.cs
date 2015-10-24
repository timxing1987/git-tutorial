#region

using System;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;

#endregion

namespace Cedar.Core.EntLib.IoC.IoCExtended
{
    public class ExtendedInterceptionElement : ContainerConfiguringElement
    {
        [ConfigurationProperty("interceptor", IsRequired = false, DefaultValue = "")]
        public string Interceptor
        {
            get { return (string) this["interceptor"]; }
            set
            {
                base["interceptor"] = value;
            }
        }

        protected override void ConfigureContainer(IUnityContainer container)
        {
            var interception = new ExtendedInterception();
            if (!string.IsNullOrEmpty(Interceptor))
            {
                var type = Type.GetType(Interceptor);
                if (null == type)
                {
                    throw new ConfigurationErrorsException(string.Format("The {0} is not a valid Interceptor.",
                        Interceptor));
                }

                if (!typeof (IInterceptor).IsAssignableFrom(type))
                {
                    throw new ConfigurationErrorsException(string.Format("The {0} is not a valid Interceptor.",
                        Interceptor));
                }
                interception.Interceptor = (IInterceptor) Activator.CreateInstance(type);
            }

            container.AddExtension(interception);
        }
    }
}