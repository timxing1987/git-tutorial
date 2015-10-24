#region

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.Unity.ObjectBuilder;

#endregion

namespace Cedar.Core.EntLib.IoC.IoCExtended
{
    public class ExtendedInterception : Interception
    {
        public ExtendedInterception()
        {
            Interceptor = new TransparentProxyInterceptor();
        }

        public IInterceptor Interceptor { get; internal set; }

        protected override void Initialize()
        {
            Context.Strategies.Add(new ExtendedInstanceInterceptionStrategy(this), UnityBuildStage.Setup);
            Context.Strategies.Add(new ExtendedTypeInterceptionStrategy(this), UnityBuildStage.PreCreation);
            Context.Container.RegisterInstance<InjectionPolicy>(typeof (AttributeDrivenPolicy).AssemblyQualifiedName,
                new AttributeDrivenPolicy());
        }
    }
}