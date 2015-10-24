using System.Configuration;
using System.Linq;
using Cedar.Core.EntLib.Properties;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Cedar.Core.EntLib.IoC.Configuration
{
    /// <summary>
    ///     The <see cref="T:Microsoft.Practices.Unity.Configuration.ContainerConfiguringElement" /> for automatic interception
    ///     extension.
    /// </summary>
    public class AutoInterceptionElement : ContainerConfiguringElement
    {
        private const string intrceptorProperty = "interceptor";

        /// <summary>
        ///     Gets or sets the interceptor.
        /// </summary>
        /// <value>
        ///     The interceptor.
        /// </value>
        [ConfigurationProperty("interceptor", IsRequired = false)]
        public AutoInterceptorElement Interceptor
        {
            get { return (AutoInterceptorElement) base["interceptor"]; }
            set { base["interceptor"] = value; }
        }

        /// <summary>
        ///     Configures the container.
        /// </summary>
        /// <param name="container">The container.</param>
        protected override void ConfigureContainer(IUnityContainer container)
        {
            if (Interceptor == null)
            {
                return;
            }

            var interceptorType = TypeResolver.ResolveType(Interceptor.TypeName);
            if (!typeof (IInstanceInterceptor).IsAssignableFrom(interceptorType))
            {
                throw new ConfigurationErrorsException(Resources.ExceptionOnlyInstanceInterceptorBeSupported);
            }

            var builderName = interceptorType.AssemblyQualifiedName;
            var source =
                Interceptor.Injection.SelectMany(
                    (InjectionMemberElement element) =>
                        element.GetInjectionMembers(container, typeof (IInstanceInterceptor), interceptorType,
                            builderName));
            container.RegisterType(typeof (IInstanceInterceptor), interceptorType, builderName,
                new ContainerControlledLifetimeManager(), source.ToArray());

            var buildKey = new NamedTypeBuildKey(typeof (IInstanceInterceptor), builderName);
            var instance =
                new ResolvedAutoInterceptorPolicy(
                    (NamedTypeBuildKey key) => container.Resolve<IInstanceInterceptor>(key.Name), buildKey);
            container.RegisterInstance(typeof (AutoInterceptorPolicy),
                typeof (AutoInterceptorPolicy).AssemblyQualifiedName, instance, new ContainerControlledLifetimeManager());
        }
    }
}