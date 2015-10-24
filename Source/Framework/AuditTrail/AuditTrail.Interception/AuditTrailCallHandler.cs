using System;
using Cedar.AuditTrail.Interception.Configuration;
using Cedar.Framwork.AuditTrail;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;
using Newtonsoft.Json;

//using Cedar.Framwork.AuditTrail.BusinessEntity;
//using Cedar.Framwork.AuditTrail.Interface;

namespace Cedar.AuditTrail.Interception
{
    [ConfigurationElementType(typeof (AuditTrailCallHandlerData))]
    public class AuditTrailCallHandler : ICallHandler
    {
        /// <summary>
        ///     Create a new AuditTrailCallHandler
        /// </summary>
        /// <param name="functionName">The name of the function to audit.</param>
        /// <param name="order">The order in which the handler will be executed.</param>
        public AuditTrailCallHandler(string functionName, int order)
        {
            FunctionName = functionName;
            Order = order;
        }

        /// <summary>
        ///     Create a new AuditTrailCallHandler
        /// </summary>
        /// <param name="functionName">The name of the function to audit.</param>
        public AuditTrailCallHandler(string functionName)
            : this(functionName, 0)
        {
            //auditservice = ServiceLocatorFactory.GetServiceLocator().GetService<IAuditTrailManagementService>();
        }

        //private IAuditTrailManagementService auditservice;

        /// <summary>
        ///     Gets or sets the name of the function.
        /// </summary>
        /// <value>The name of the function.</value>
        public string FunctionName { get; }

        /// <summary>
        ///     Order in which the handler will be executed.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        ///     Invoking the Audit Trail related operation.
        /// </summary>
        /// <param name="input">Method Invocation Message.</param>
        /// <param name="getNext">
        ///     A GetNextHandlerDelegate object delegating the invocation to the next CallHandler or Target
        ///     instance.
        /// </param>
        /// <returns>The return message of the method invocation.</returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            IMethodReturn result;
            try
            {
                var auditLogger = AuditLogger.CreateAuditLogger(FunctionName);
                var methodReturn = getNext()(input, getNext);
                if (methodReturn != null)
                {
                    //var data = new AuditLogModel()
                    //{
                    //    ID = Guid.NewGuid().ToString(),
                    //    AuditName = FunctionName,
                    //    AuditType = input.MethodBase.Name,
                    //    Arguments = input.Arguments,
                    //    LogDateTime = DateTime.Now,
                    //    Result = methodReturn.ReturnValue,
                    //    Target = input.Target,
                    //    TransactionID = "TransactionID"
                    //};
                    //try
                    //{
                    //    auditservice.InsertAuditLog(data);
                    //}
                    //catch (Exception e)
                    //{
                    //    //todo add log
                    //}
                    if (methodReturn.Exception == null)
                    {
                        auditLogger.Write(input.MethodBase.Name, string.Empty,
                            JsonConvert.SerializeObject(input.Arguments),
                            JsonConvert.SerializeObject(methodReturn.ReturnValue));
                        auditLogger.Flush();
                    }
                }
                result = methodReturn;
            }
            catch (Exception ex)
            {
                result = input.CreateExceptionMethodReturn(ex);
            }
            return result;
        }
    }
}