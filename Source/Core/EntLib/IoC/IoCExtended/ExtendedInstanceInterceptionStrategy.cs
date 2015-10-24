#region

using System;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

#endregion

namespace Cedar.Core.EntLib.IoC.IoCExtended
{
    public class ExtendedInstanceInterceptionStrategy : InstanceInterceptionStrategy
    {
        public ExtendedInstanceInterceptionStrategy(ExtendedInterception interception)
        {
            if (null == interception)
            {
                throw new ArgumentNullException("interception");
            }

            Interception = interception;
        }

        public ExtendedInterception Interception { get; private set; }

        private static IInstanceInterceptionPolicy FindInterceptorPolicy(IBuilderContext context)
        {
            Type buildKey = context.BuildKey.Type;
            Type type = context.OriginalBuildKey.Type;
            IInstanceInterceptionPolicy policy =
                context.Policies.Get<IInstanceInterceptionPolicy>(context.BuildKey, false) ??
                context.Policies.Get<IInstanceInterceptionPolicy>(buildKey, false);
            if (policy != null)
            {
                return policy;
            }
            policy = context.Policies.Get<IInstanceInterceptionPolicy>(context.OriginalBuildKey, false) ??
                     context.Policies.Get<IInstanceInterceptionPolicy>(type, false);
            return policy;
        }

        public override void PreBuildUp(IBuilderContext context)
        {
            if (context.BuildKey.Type == typeof (IUnityContainer))
            {
                return;
            }

            IInstanceInterceptionPolicy policy = FindInterceptorPolicy(context);
            if (null != policy)
            {
                if (policy.GetInterceptor(context).CanIntercept(context.BuildKey.Type))
                {
                    Interception.SetDefaultInterceptorFor(context.BuildKey.Type, policy.GetInterceptor(context));
                }
            }
            else
            {
                if (Interception.Interceptor.CanIntercept(context.BuildKey.Type) &&
                    Interception.Interceptor is IInstanceInterceptor)
                {
                    Interception.SetDefaultInterceptorFor(context.BuildKey.Type,
                        (IInstanceInterceptor) Interception.Interceptor);
                }
            }
            base.PreBuildUp(context);
        }
    }
}