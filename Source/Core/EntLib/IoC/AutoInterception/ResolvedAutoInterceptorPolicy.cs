using System;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.Unity.Utility;

namespace Cedar.Core.EntLib.IoC
{
    public class ResolvedAutoInterceptorPolicy : AutoInterceptorPolicy
    {
        /// <summary>
        ///     A delegate to resove the <see cref="T:Microsoft.Practices.Unity.InterceptionExtension.IInstanceInterceptor" />
        ///     based on the given <see cref="T:Microsoft.Practices.ObjectBuilder2.NamedTypeBuildKey" />.
        /// </summary>
        public Func<NamedTypeBuildKey, IInstanceInterceptor> InterceptorResolver;

        /// <summary>
        /// </summary>
        /// <param name="interceptorResolver"></param>
        /// <param name="buildKey"></param>
        public ResolvedAutoInterceptorPolicy(Func<NamedTypeBuildKey, IInstanceInterceptor> interceptorResolver,
            NamedTypeBuildKey buildKey)
        {
            Guard.ArgumentNotNull(interceptorResolver, "interceptorResolver");
            Guard.ArgumentNotNull(buildKey, "buildKey");
            BuildKey = buildKey;
            InterceptorResolver = interceptorResolver;
        }

        /// <summary>
        ///     Gets the build key.
        /// </summary>
        public NamedTypeBuildKey BuildKey { get; }

        /// <summary>
        ///     Gets the <see cref="T:Microsoft.Practices.Unity.InterceptionExtension.IInstanceInterceptor" />.
        /// </summary>
        public override IInstanceInterceptor Interceptor
        {
            get { return InterceptorResolver(BuildKey); }
        }
    }
}