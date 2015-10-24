using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.Unity.Utility;

namespace Cedar.Core.EntLib.IoC
{
    /// <summary>
    ///     This <see cref="T:Microsoft.Practices.ObjectBuilder2.BuilderStrategy" /> is used to automatically register
    ///     interceptor.
    /// </summary>
    public class AutoInterceptionStrategy : BuilderStrategy
    {
        /// <summary>
        ///     Pres the build up.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.Practices.ObjectBuilder2.IBuilderContext" />.</param>
        public override void PreBuildUp(IBuilderContext context)
        {
            Guard.ArgumentNotNull(context, "context");
            if (!CanIntercept(context))
            {
                return;
            }
            var policy = FindInterceptionPolicy<IInstanceInterceptionPolicy>(context, true);

            if (policy == null)
            {
                AutoInterceptorPolicy autoInterceptorPolicy =
                    context.NewBuildUp<FixedAutoInterceptorPolicy>(typeof (AutoInterceptorPolicy).AssemblyQualifiedName);
                if (autoInterceptorPolicy == null)
                {
                    return;
                }

                var interceptor = autoInterceptorPolicy.Interceptor;
                if (interceptor == null)
                {
                    return;
                }
                if (!interceptor.CanIntercept(context.BuildKey.Type))
                {
                    return;
                }
                //context.Policies.Set(new FixedInstanceInterceptionPolicy(interceptor), context.BuildKey);
                context.Policies.Set<IInstanceInterceptionPolicy>(new FixedInstanceInterceptionPolicy(interceptor),
                    context.BuildKey.Type);
                context.Policies.Clear<IInterceptionBehaviorsPolicy>(context.BuildKey);
            }
            if (FindInterceptionPolicy<IInterceptionBehaviorsPolicy>(context, true) == null)
            {
                var interceptionBehavior = new InterceptionBehavior<PolicyInjectionBehavior>();
                interceptionBehavior.AddPolicies(context.OriginalBuildKey.Type, context.BuildKey.Type,
                    context.BuildKey.Name, context.Policies);
            }
        }

        /// <summary>
        ///     Determines whether this instance can intercept the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        ///     <c>true</c> if this instance can intercept the specified context; otherwise, <c>false</c>.
        /// </returns>
        protected bool CanIntercept(IBuilderContext context)
        {
            Guard.ArgumentNotNull(context, "context");
            return !context.BuildKey.Type.IsTypeOfUnity() && !context.BuildKey.Type.IsTypeOfEntLib() &&
                   !typeof (AutoInterceptorPolicy).IsAssignableFrom(context.BuildKey.Type);
        }

        /// <summary>
        ///     Get current instance's InterceptionPolicy
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="probeOriginalKey"></param>
        /// <returns></returns>
        private static T FindInterceptionPolicy<T>(IBuilderContext context, bool probeOriginalKey)
            where T : class, IBuilderPolicy
        {
            var type = context.BuildKey.Type;
            T arg_34_0;
            if ((arg_34_0 = context.Policies.Get<T>(context.BuildKey, false)) == null)
            {
                arg_34_0 = context.Policies.Get<T>(type, false);
            }
            var t = arg_34_0;
            if (t != null)
            {
                return t;
            }
            if (!probeOriginalKey)
            {
                return default(T);
            }

            //try get original InterceptionPolicy
            var type2 = context.OriginalBuildKey.Type;
            T arg_80_0;
            if ((arg_80_0 = context.Policies.Get<T>(context.OriginalBuildKey, false)) == null)
            {
                arg_80_0 = context.Policies.Get<T>(type2, false);
            }
            return arg_80_0;
        }
    }
}