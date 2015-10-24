using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.Unity.Utility;

namespace Cedar.AuditTrail.Interception
{
    /// <summary>
    ///     AuditTrail HanlderAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuditTrailCallHandlerAttribute : HandlerAttribute
    {
        /// <summary>
        ///     Create a new AuditTrailCallHandlerAttribute object.
        /// </summary>
        /// <param name="functionName">Logged function name.</param>
        public AuditTrailCallHandlerAttribute(string functionName)
        {
            Guard.ArgumentNotNullOrEmpty(functionName, "functionName");
            FunctionName = functionName;
        }

        /// <summary>
        ///     Gets or sets the name of the function.
        /// </summary>
        /// <value>The name of the function.</value>
        public string FunctionName { get; }

        /// <summary>
        ///     creates a new AuditTrailCallHandler as specified in the attribute configuration.
        /// </summary>
        /// <param name="container">
        ///     The <see cref="T:Microsoft.Practices.Unity.IUnityContainer" /> to use when creating handlers,
        ///     if necessary.
        /// </param>
        /// <returns>A new AuditTrailCallHandler object.</returns>
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new AuditTrailCallHandler(FunctionName) {Order = Order};
        }
    }
}