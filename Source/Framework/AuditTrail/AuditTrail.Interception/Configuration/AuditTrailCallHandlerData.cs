using System;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.Unity;

namespace Cedar.AuditTrail.Interception.Configuration
{
    /// <summary>
    ///     AuditTrail CallhandlerData containing the configuration definition for CallHandler.
    /// </summary>
    public class AuditTrailCallHandlerData : CallHandlerData
    {
        /// <summary>
        ///     The name of Function Name configuration property name.
        /// </summary>
        public const string FunctionNamePropertyName = "functionName";

        /// <summary>
        ///     The name of Application Version configuration property name.
        /// </summary>
        public const string ApplicationVersionPropertyName = "applicationVersion";

        /// <summary>
        ///     Create a new AuditTrailCallHandlerData object.
        /// </summary>
        public AuditTrailCallHandlerData()
        {
        }

        /// <summary>
        ///     Create a new AuditTrailCallHandlerData object.
        /// </summary>
        /// <param name="name">Name of handler entry.</param>
        /// <param name="type">Type of handler to create.</param>
        /// <param name="order">The order of the handler.</param>
        public AuditTrailCallHandlerData(string name, Type type, int order)
            : base(name, type, order)
        {
        }

        /// <summary>
        ///     Create a new AuditTrailCallHandlerData object.
        /// </summary>
        /// <param name="name">Name of handler entry.</param>
        /// <param name="type">Type of handler to create.</param>
        /// <param name="order">The order of the handler.</param>
        /// <param name="functionName">The name of function to audit.</param>
        public AuditTrailCallHandlerData(string name, Type type, int order, string functionName)
            : base(name, type, order)
        {
            FunctionName = functionName;
        }

        /// <summary>
        ///     The name the fucntion representing the current operation.
        /// </summary>
        [ConfigurationProperty("functionName", IsRequired = false, DefaultValue = "")]
        public string FunctionName
        {
            get { return (string)base["functionName"]; }
            set { base["functionName"] = value; }
        }

        /// <summary>
        /// </summary>
        /// <param name="container"></param>
        /// <param name="registrationName"></param>
        protected void DoConfigureContainer(IUnityContainer container, string registrationName)
        {
            container.RegisterType<UnityContainer>(registrationName, new InjectionConstructor(FunctionName),
                new InjectionProperty("Order", Order));
        }
    }
}