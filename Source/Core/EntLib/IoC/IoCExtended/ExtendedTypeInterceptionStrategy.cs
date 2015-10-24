#region

using System;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.InterceptionExtension;

#endregion

namespace Cedar.Core.EntLib.IoC.IoCExtended
{
    public class ExtendedTypeInterceptionStrategy : TypeInterceptionStrategy
    {
        public ExtendedTypeInterceptionStrategy(ExtendedInterception interception)
        {
            if (null == interception)
            {
                throw new ArgumentNullException("interception");
            }

            Interception = interception;
        }

        public ExtendedInterception Interception { get; private set; }

        private static ITypeInterceptionPolicy GetInterceptionPolicy(IBuilderContext context)
        {
            var policy = context.Policies.Get<ITypeInterceptionPolicy>(context.BuildKey, false);
            if (policy == null)
            {
                policy = context.Policies.Get<ITypeInterceptionPolicy>(context.BuildKey.Type, false);
            }
            return policy;
        }

        public override void PreBuildUp(IBuilderContext context)
        {
            var policy = GetInterceptionPolicy(context);
            if (null != policy)
            {
                if (policy.GetInterceptor(context).CanIntercept(context.BuildKey.Type))
                {
                    Interception.SetInterceptorFor(context.BuildKey.Type, policy.GetInterceptor(context));
                }
            }
            else
            {
                if (Interception.Interceptor.CanIntercept(context.BuildKey.Type) &&
                    Interception.Interceptor is ITypeInterceptor)
                {
                    Interception.SetDefaultInterceptorFor(context.BuildKey.Type,
                        (ITypeInterceptor) Interception.Interceptor);
                }
            }
            base.PreBuildUp(context);
        }
    }
}