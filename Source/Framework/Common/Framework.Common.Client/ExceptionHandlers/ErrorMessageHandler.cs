#region

using System;
using System.Configuration;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;

#endregion

namespace Cedar.Framework.Common.Client.ExceptionHandlers
{
    /// <summary>
    /// </summary>
    [ConfigurationElementType(typeof (ErrorMessageHandlerData))]
    public class ErrorMessageHandler : IExceptionHandler
    {
        /// <summary>
        /// </summary>
        /// <param name="errorMessage"></param>
        public ErrorMessageHandler(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// </summary>
        public string ErrorMessage { get; }

        /// <summary>
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="handlingInstanceId"></param>
        /// <returns></returns>
        public Exception HandleException(Exception exception, Guid handlingInstanceId)
        {
            if (null != HttpContext.Current)
            {
                HttpContext.Current.SetErrorMessage(ErrorMessage);
            }
            return exception;
        }
    }

    /// <summary>
    /// </summary>
    public class ErrorMessageHandlerData : ExceptionHandlerData
    {
        /// <summary>
        /// </summary>
        [ConfigurationProperty("errorMessage", IsRequired = true)]
        public string ErrorMessage
        {
            get { return (string) this["errorMessage"]; }
            set { this["errorMessage"] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="namePrefix"></param>
        /// <returns></returns>
        //public override IEnumerable<TypeRegistration> GetRegistrations(string namePrefix)
        //{
        //    yield return new TypeRegistration<IExceptionHandler>(() => new ErrorMessageHandler(this.ErrorMessage))
        //    {
        //        Name = this.BuildName(namePrefix),
        //        Lifetime = TypeRegistrationLifetime.Transient
        //    };
        //}
    }
}