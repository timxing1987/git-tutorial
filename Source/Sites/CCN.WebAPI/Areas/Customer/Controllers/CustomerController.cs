#region

using System.Web.Mvc;
using CCN.Modules.Customer.Interface;
using Cedar.Core.IoC;

#endregion

namespace CCN.WebAPI.Areas.Customer.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerManagementService _service;

        public CustomerController()
        {
            _service = ServiceLocatorFactory.GetServiceLocator().GetService<ICustomerManagementService>();
        }
        
        public ActionResult CouponList()
        {
            return View();
        }

        public ActionResult CouponEdit(string innerid)
        {
            ViewBag.innerid = string.IsNullOrWhiteSpace(innerid) ? "" : innerid;
            return View();
        }

        public ActionResult CouponView(string innerid)
        {
            ViewBag.innerid = string.IsNullOrWhiteSpace(innerid) ? "" : innerid;
            return View();
        }

        #region 用户认证

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CustomerList()
        {
            return View();
        }

        /// <summary>
        /// 用户详情
        /// </summary>
        /// <returns></returns>
        public ActionResult CustomerView(string innerid)
        {
            ViewBag.innerid = string.IsNullOrWhiteSpace(innerid) ? "" : innerid;
            return View();
        }

        #endregion
    }
}