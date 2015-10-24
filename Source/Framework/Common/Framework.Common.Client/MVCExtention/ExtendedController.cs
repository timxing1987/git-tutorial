#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cedar.Framework.Common.Client.ExceptionHandlers;

#endregion

namespace Cedar.Framework.Common.Client.MVCExtention
{
    /// <summary>
    /// </summary>
    public class ExtendedController : Controller //, IServiceLocator
    {
        private static readonly Dictionary<Type, ControllerDescriptor> controllerDescriptors =
            new Dictionary<Type, ControllerDescriptor>();

        private static readonly object syncHelper = new object();
        private string _defaultLocalName = string.Empty;

        /// <summary>
        /// </summary>
        public ExtendedController()
        {
            HandleErrorActionInvoker = new HandleErrorActionInvoker();
        }

        /// <summary>
        /// </summary>
        /// <param name="applicationName"></param>
        public ExtendedController(string applicationName)
        {
            _defaultLocalName = applicationName;
            HandleErrorActionInvoker = new HandleErrorActionInvoker();
        }

        /// <summary>
        /// </summary>
        public ControllerDescriptor Descriptor
        {
            get
            {
                ControllerDescriptor descriptor;
                if (controllerDescriptors.TryGetValue(GetType(), out descriptor))
                {
                    return descriptor;
                }
                lock (syncHelper)
                {
                    if (controllerDescriptors.TryGetValue(GetType(), out descriptor))
                    {
                        return descriptor;
                    }
                    descriptor = new ReflectedControllerDescriptor(GetType());
                    controllerDescriptors.Add(GetType(), descriptor);
                    return descriptor;
                }
            }
        }

        /// <summary>
        /// </summary>
        public HandleErrorActionInvoker HandleErrorActionInvoker { get; private set; }

        /// <summary>
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            ////或者当前的ExceptionPolicy，如果不存在，则直接调用基类OnException方法
            //string exceptionPolicyName = this.GetExceptionPolicyName();
            //if (string.IsNullOrEmpty(exceptionPolicyName))
            //{
            //    base.OnException(filterContext);
            //    return;
            //}

            ////利用EntLib的EHAB进行异常处理，并获取错误消息和最后抛出的异常
            //filterContext.ExceptionHandled = true;
            //Exception exceptionToThrow;
            //string errorMessage;
            //try
            //{
            //    //todo 之后完善
            //    //ExceptionPolicy.HandleException(filterContext.Exception, exceptionPolicyName, out exceptionToThrow);
            //    //errorMessage = System.Web.HttpContext.Current.GetErrorMessage();
            //    errorMessage = filterContext.Exception.Message;
            //}
            //finally
            //{
            //    System.Web.HttpContext.Current.ClearErrorMessage();
            //}

            ////exceptionToThrow = exceptionToThrow ?? filterContext.Exception;
            //exceptionToThrow = filterContext.Exception;//todo

            //try
            //{
            //    //todo业务写日志
            //    //Utility.WriteErorrLog(exceptionToThrow);
            //}
            //catch
            //{
            //}

            ////对于Ajax请求，直接返回一个用于封装异常的JsonResult
            //if (Request.IsAjaxRequest())
            //{
            //    filterContext.Result = Json(new ExceptionDetail(exceptionToThrow, errorMessage));
            //    return;
            //}

            ////如果设置了匹配的HandleErrorAction，则调用之；
            ////否则将Error View呈现出来
            //string handleErrorAction = this.GetHandleErrorActionName();
            //string controllerName = ControllerContext.RouteData.GetRequiredString("controller");
            //string actionName = ControllerContext.RouteData.GetRequiredString("action");
            //errorMessage = string.IsNullOrEmpty(errorMessage) ? exceptionToThrow.Message : errorMessage;
            //ActionDescriptor actionDescriptor = null;
            //if (!string.IsNullOrEmpty(handleErrorAction))
            //{
            //    actionDescriptor = Descriptor.FindAction(ControllerContext, handleErrorAction);
            //}
            //if (actionDescriptor == null)
            //{
            //    filterContext.Result = View("Error", new ExtendedHandleErrorInfo(exceptionToThrow, controllerName, actionName, errorMessage));
            //}
            //else
            //{
            //    ModelState.AddModelError("", errorMessage);
            //    filterContext.Result = this.HandleErrorActionInvoker.InvokeActionMethod(ControllerContext, actionDescriptor);
            //}
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public string GetExceptionPolicyName()
        {
            var actionName = ControllerContext.RouteData.GetRequiredString("action");
            var actionDescriptor = Descriptor.FindAction(ControllerContext, actionName);
            if (null == actionDescriptor)
            {
                return string.Empty;
            }
            var exceptionPolicyAttribute = actionDescriptor.GetCustomAttributes(true)
                .OfType<ExceptionPolicyAttribute>()
                .FirstOrDefault()
                                           ??
                                           Descriptor.GetCustomAttributes(true)
                                               .OfType<ExceptionPolicyAttribute>()
                                               .FirstOrDefault()
                                           ?? new ExceptionPolicyAttribute("");
            return exceptionPolicyAttribute.ExceptionPolicyName;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public string GetHandleErrorActionName()
        {
            var actionName = ControllerContext.RouteData.GetRequiredString("action");
            var actionDescriptor = Descriptor.FindAction(ControllerContext, actionName);
            if (null == actionDescriptor)
            {
                return string.Empty;
            }
            var handleErrorActionAttribute = actionDescriptor.GetCustomAttributes(true)
                .OfType<HandleErrorActionAttribute>()
                .FirstOrDefault()
                                             ??
                                             Descriptor.GetCustomAttributes(true)
                                                 .OfType<HandleErrorActionAttribute>()
                                                 .FirstOrDefault()
                                             ?? new HandleErrorActionAttribute("");
            return handleErrorActionAttribute.HandleErrorAction;
        }

        #region IServiceLocator

        //public IEnumerable<TService> GetAllInstances<TService>()
        //{
        //    return ServiceLocatorFactory.GetServiceLocator(_defaultLocalName).GetAllInstances<TService>();
        //}

        //public IEnumerable<object> GetAllInstances(Type serviceType)
        //{
        //    return ServiceLocatorFactory.GetServiceLocator(_defaultLocalName).GetAllInstances(serviceType);
        //}

        //public TService GetInstance<TService>(string key)
        //{
        //    return ServiceLocatorFactory.GetServiceLocator(_defaultLocalName).GetInstance<TService>(key);
        //}

        //public TService GetInstance<TService>()
        //{
        //    return ServiceLocatorFactory.GetServiceLocator(_defaultLocalName).GetInstance<TService>();
        //}

        //public object GetInstance(Type serviceType, string key)
        //{
        //    return ServiceLocatorFactory.GetServiceLocator(_defaultLocalName).GetInstance(serviceType, key);
        //}

        //public object GetInstance(Type serviceType)
        //{
        //    return ServiceLocatorFactory.GetServiceLocator(_defaultLocalName).GetInstance(serviceType);
        //}

        //public object GetService(Type serviceType)
        //{
        //    return ServiceLocatorFactory.GetServiceLocator(_defaultLocalName).GetService(serviceType);
        //}

        #endregion
    }

    //public static class Utility
    //{
    //    public static void WriteErorrLog(Exception ex)
    //    {
    //        if (ex == null) return;
    //        var rootPath = ConfigurationManager.AppSettings["ClientErrorLog"];

    //        var dt = DateTime.Now;

    //        var filePath = rootPath + dt.Year.ToString().PadLeft(4, '0') + dt.Month.ToString().PadLeft(2, '0') +
    //                       dt.Day.ToString().PadLeft(2, '0') + "\\";
    //        if (!Directory.Exists(filePath))
    //        {
    //            Directory.CreateDirectory(filePath);
    //        }
    //        var sbr = new StringBuilder();
    //        sbr.Append(filePath);

    //        sbr.Append(dt.Year.ToString().PadLeft(4, '0'));
    //        sbr.Append(dt.Month.ToString().PadLeft(2, '0'));
    //        sbr.Append(dt.Day.ToString().PadLeft(2, '0'));
    //        sbr.Append(dt.Hour.ToString().PadLeft(2, '0'));
    //        sbr.Append(dt.Minute.ToString().PadLeft(2, '0'));
    //        sbr.Append(dt.Second.ToString().PadLeft(2, '0'));
    //        sbr.Append(dt.Millisecond.ToString().PadLeft(3, '0'));
    //        sbr.Append(".xml");
    //        filePath = sbr.ToString();

    //        using (var sw = new StreamWriter(filePath, true, Encoding.UTF8))
    //        {
    //            sw.WriteLine(dt);
    //            sw.WriteLine(ex.Message);
    //            sw.WriteLine("异常信息：" + ex);
    //            sw.WriteLine("异常堆栈：" + ex.StackTrace);
    //            sw.WriteLine("-----------------");
    //            sw.Flush();
    //            sw.Close();
    //        }
    //    }
    //}
}