using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.Unity.Utility;

namespace Cedar.Core.EntLib.IoC
{
    public class FixedAutoInterceptorPolicy : AutoInterceptorPolicy
    {
        /// <summary>
        /// </summary>
        /// <param name="interceptor"></param>
        public FixedAutoInterceptorPolicy(IInstanceInterceptor interceptor)
        {
            Guard.ArgumentNotNull(interceptor, "interceptor");
            Interceptor = interceptor;
        }

        /// <summary>
        ///     Gets the <see cref="T:Microsoft.Practices.Unity.InterceptionExtension.IInstanceInterceptor" />.
        /// </summary>
        public override IInstanceInterceptor Interceptor { get; }
    }
}