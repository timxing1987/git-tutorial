using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.Unity.ObjectBuilder;
using Microsoft.Practices.Unity.Utility;

namespace Cedar.Core.EntLib.IoC
{
    /// <summary>
    ///     A <see cref="T:Microsoft.Practices.Unity.UnityContainerExtension" /> for automatic interception.
    /// </summary>
    public class AutoInterception : UnityContainerExtension
    {
        /// <summary>
        ///     Registers the default interceptor.
        /// </summary>
        /// <param name="interceptor">The interceptor.</param>
        /// <returns></returns>
        public IUnityContainer RegisterDefaultInterceptor(IInstanceInterceptor interceptor)
        {
            Guard.ArgumentNotNull(interceptor, "interceptor");
            var instance = new FixedAutoInterceptorPolicy(interceptor);
            Container.RegisterInstance(typeof (AutoInterceptorPolicy),
                typeof (AutoInterceptorPolicy).AssemblyQualifiedName, instance, new ContainerControlledLifetimeManager());
            return Container;
        }

        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        protected override void Initialize()
        {
            Context.Strategies.AddNew<AutoInterceptionStrategy>(UnityBuildStage.Setup);
            var transparentProxyInterceptor = new TransparentProxyInterceptor();
            Context.Container.RegisterInstance(typeof (IInstanceInterceptor).AssemblyQualifiedName,
                transparentProxyInterceptor);
            Context.Container.RegisterInstance(typeof (AutoInterceptorPolicy).AssemblyQualifiedName,
                new FixedAutoInterceptorPolicy(transparentProxyInterceptor));
        }
    }
}