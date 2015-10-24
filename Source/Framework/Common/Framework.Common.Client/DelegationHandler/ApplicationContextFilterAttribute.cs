using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Cedar.Core.ApplicationContexts;

namespace Cedar.Framework.Common.Client.DelegationHandler
{
    /// <summary>
    /// </summary>
    public class ApplicationContextFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                ApplicationContext.Current.TransactionId = Guid.NewGuid().ToString();
                if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Count > 0) // 允许匿名访问
                {
                    base.OnActionExecuting(actionContext);
                    return;
                }

                var cookie = actionContext.Request.Headers.GetCookies();
                if (cookie == null || cookie.Count < 1)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    return;
                }

                //get sessionid
                var sessionid = string.Empty;
                foreach (var perCookie in cookie[0].Cookies)
                {
                    if (perCookie.Name == "sessionid")
                    {
                        sessionid = perCookie.Value;
                        break;
                    }
                }

                if (string.IsNullOrEmpty(sessionid))
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    return;
                }

                //get userid
                var userid = string.Empty;
                foreach (var perCookie in cookie[0].Cookies)
                {
                    if (perCookie.Name == "userid")
                    {
                        userid = perCookie.Value;
                        break;
                    }
                }

                ApplicationContext.Current.UserId = userid;
                ApplicationContext.Current.SessionId = sessionid;
                //base.OnActionExecuting(actionContext);
            }
            catch
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }
    }
}