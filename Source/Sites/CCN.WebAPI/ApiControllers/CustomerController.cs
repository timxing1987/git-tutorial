using System.Web.Http;
using CCN.Modules.Base.Interface;
using CCN.Modules.Customer.BusinessEntity;
using CCN.Modules.Customer.Interface;
using Cedar.Core.IoC;
using Cedar.Framework.Common.BaseClasses;
using Cedar.Framework.Common.Client.DelegationHandler;

namespace CCN.WebAPI.ApiControllers
{
    /// <summary>
    /// 会员模块
    /// </summary>
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
        private readonly ICustomerManagementService _custservice;

        public CustomerController()
        {
            _custservice = ServiceLocatorFactory.GetServiceLocator().GetService<ICustomerManagementService>();
        }

        #region 用户模块

        /// <summary>
        /// 会员注册检查email是否被注册
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>0：未被注册，非0：Email被注册</returns>
        [Route("CheckEmail")]
        [HttpGet]
        public JResult CheckEmail(string email)
        {
            var result = _custservice.CheckEmail(email);
            return new JResult
            {
                errcode = result,
                errmsg = ""
            };
        }

        /// <summary>
        /// 会员注册检查手机号是否被注册
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <returns>0：未被注册，非0：被注册</returns>
        [Route("CheckMobile")]
        [HttpGet]
        public JResult CheckMobile(string mobile)
        {
            var result = _custservice.CheckMobile(mobile);
            return new JResult
            {
                errcode = result,
                errmsg = ""
            };
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns>
        /// errcode,0.成功，400.验证码错误，401.异常
        /// </returns>
        [Route("CustRegister")]
        [HttpPost]
        public JResult CustRegister([FromBody] CustModel userInfo)
        {
            if (string.IsNullOrWhiteSpace(userInfo.Mobile))
            {
                return new JResult
                {
                    errcode = 400,
                    errmsg = "手机号不能空"
                };
            }
            
            var baseservice = ServiceLocatorFactory.GetServiceLocator().GetService<IBaseManagementService>();
            
            //检查验证码
            var cresult = baseservice.CheckVerification(userInfo.Mobile, userInfo.VCode, 1);
            if (cresult.errcode != 0)
            {
                //验证码错误
                return cresult;
            }

            return _custservice.CustRegister(userInfo);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginInfo">登录账户</param>
        /// <returns>用户信息</returns>
        [Route("CustLogin")]
        [HttpPost]
        public JResult CustLogin([FromBody] CustLoginInfo loginInfo)
        {
            return _custservice.CustLogin(loginInfo);
        }

        /// <summary>
        /// 用户登录(openid登录)
        /// </summary>
        /// <param name="openid">openid</param>
        /// <returns>用户信息</returns>
        [Route("CustLoginByOpenid")]
        [HttpPost]
        [NonAction]
        public JResult CustLoginByOpenid(string openid)
        {
            return _custservice.CustLoginByOpenid(openid);
        }

        /// <summary>
        /// 获取会员详情
        /// </summary>
        /// <param name="innerid">会员id</param>
        /// <returns></returns>
        [Route("GetCustById")]
        [HttpGet]
        public JResult GetCustById(string innerid)
        {
            return _custservice.GetCustById(innerid);
        }

        /// <summary>
        /// 获取会员列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        [Route("GetCustPageList")]
        [HttpPost]
        public BasePageList<CustModel> GetCustPageList([FromBody]CustQueryModel query)
        {
            return _custservice.GetCustPageList(query);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="mRetrievePassword"></param>
        /// <returns></returns>
        [Route("UpdatePassword")]
        [HttpPost]
        public JResult UpdatePassword(CustRetrievePassword mRetrievePassword)
        {
            if (string.IsNullOrWhiteSpace(mRetrievePassword.Mobile))
            {
                return JResult._jResult(402, "手机号不能空");
            }

            var baseservice = ServiceLocatorFactory.GetServiceLocator().GetService<IBaseManagementService>();

            //检查验证码
            var cresult = baseservice.CheckVerification(mRetrievePassword.Mobile, mRetrievePassword.VCode, 3);
            if (cresult.errcode != 0)
            {
                //验证码错误
                //400验证码错误
                //401验证码过期
                return cresult;
            }

            return _custservice.UpdatePassword(mRetrievePassword);
        }

        /// <summary>
        /// 修改会员信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("UpdateCustInfo")]
        [HttpPost]
        public JResult UpdateCustInfo([FromBody]CustModel model)
        {
            return _custservice.UpdateCustInfo(model);
        }

        /// <summary>
        /// 修改会员状态(冻结)
        /// </summary>
        /// <param name="innerid"></param>
        /// <returns></returns>
        [Route("FrozenCust")]
        [HttpGet]
        public JResult FrozenCust(string innerid)
        {
            return _custservice.UpdateCustStatus(innerid, 2);
        }

        /// <summary>
        /// 修改会员状态(解冻)
        /// </summary>
        /// <param name="innerid"></param>
        /// <returns></returns>
        [Route("ThawCust")]
        [HttpGet]
        public JResult ThawCust(string innerid)
        {
            return _custservice.UpdateCustStatus(innerid, 1);
        }
        #endregion

        #region 用户认证

        /// <summary>
        /// 用户添加认证信息
        /// </summary>
        /// <param name="model">认证信息</param>
        /// <returns></returns>
        [Route("AddAuthentication")]
        [HttpPost]
        public JResult AddAuthentication([FromBody] CustAuthenticationModel model)
        {
            return _custservice.AddAuthentication(model);
        }

        /// <summary>
        /// 用户修改认证信息
        /// </summary>
        /// <param name="model">认证信息</param>
        /// <returns></returns>
        [Route("UpdateAuthentication")]
        [HttpPost]
        [ApplicationContextFilter]
        public JResult UpdateAuthentication([FromBody] CustAuthenticationModel model)
        {
            return _custservice.UpdateAuthentication(model);
        }

        /// <summary>
        /// 审核认证信息
        /// </summary>
        /// <param name="model">会员相关信息</param>
        /// <returns></returns>
        [Route("AuditAuthentication")]
        [HttpPost]
        [ApplicationContextFilter]
        public JResult AuditAuthentication([FromBody] CustAuthenticationModel model)
        {
            return _custservice.AuditAuthentication(model);
        }

        /// <summary>
        /// 获取会员认证信息 by innerid
        /// </summary>
        /// <param name="innerid">id</param>
        /// <returns></returns>
        [Route("GetCustAuthById")]
        [HttpGet]
        public JResult GetCustAuthById(string innerid)
        {
            return _custservice.GetCustAuthById(innerid);
        }

        /// <summary>
        /// 获取会员认证信息 by custid
        /// </summary>
        /// <param name="custid">会员id</param>
        /// <returns></returns>
        [Route("GetCustAuthByCustid")]
        [HttpGet]
        public JResult GetCustAuthByCustid(string custid)
        {
            return _custservice.GetCustAuthByCustid(custid);
        }

        #endregion

        #region 会员点赞

        /// <summary>
        /// 给会员点赞
        /// </summary>
        /// <param name="model">粉丝信息</param>
        /// <returns></returns>
        [Route("CustPraise")]
        [HttpPost]
        public JResult CustPraise([FromBody] CustLaudator model)
        {
            return _custservice.CustPraise(model);
        }

        /// <summary>
        /// 根据会员id获取所有点赞人列表
        /// </summary>
        /// <param name="custid">会员id</param>
        /// <returns></returns>
        [Route("GetLaudatorListByCustid")]
        [HttpGet]
        public JResult GetLaudatorListByCustid(string custid)
        {
            return _custservice.GetLaudatorListByCustid(custid);
        }

        #endregion

        #region 会员标签


        /// <summary>
        /// 添加标签
        /// </summary>
        /// <param name="model">标签信息</param>
        /// <returns></returns>
        [Route("AddTag")]
        [HttpPost]
        public JResult AddTag([FromBody]CustTagModel model)
        {
            return _custservice.AddTag(model);
        }

        /// <summary>
        /// 修改标签
        /// </summary>
        /// <param name="model">标签信息</param>
        /// <returns></returns>
        [Route("UpdateTag")]
        [HttpPut]
        public JResult UpdateTag([FromBody]CustTagModel model)
        {
            return _custservice.UpdateTag(model);
        }

        /// <summary>
        /// 修改标签状态
        /// </summary>
        /// <param name="innerid">标签ID</param>
        /// <param name="status">0禁用，1启用</param>
        /// <returns></returns>
        [Route("UpdateTagStatus")]
        [HttpGet]
        public JResult UpdateTagStatus(string innerid, int status)
        {
            return _custservice.UpdateTagStatus(innerid, status);
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="innerid">标签id</param>
        /// <returns></returns>
        [Route("DeleteTag")]
        [HttpDelete]
        public JResult DeleteTag(string innerid)
        {
            return _custservice.DeleteTag(innerid);
        }

        /// <summary>
        /// 获取标签详情
        /// </summary>
        /// <param name="innerid">标签id</param>
        /// <returns></returns>
        [Route("GetTagById")]
        [HttpGet]
        public JResult GetTagById(string innerid)
        {
            return _custservice.GetTagById(innerid);
        }

        /// <summary>
        /// 获取标签列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        [Route("GetTagPageList")]
        [HttpPost]
        public BasePageList<CustTagModel> GetTagPageList([FromBody]CustTagQueryModel query)
        {
            return _custservice.GetTagPageList(query);
        }

        /// <summary>
        /// 打标签
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("DoTagRelation")]
        [HttpPost]
        public JResult DoTagRelation([FromBody]CustTagRelation model)
        {
            return _custservice.DoTagRelation(model);
        }

        /// <summary>
        /// 删除标签关系
        /// </summary>
        /// <param name="innerid"></param>
        /// <returns></returns>
        [Route("DelTagRelation")]
        [HttpDelete]
        public JResult DelTagRelation(string innerid)
        {
            return _custservice.DelTagRelation(innerid);
        }

        /// <summary>
        /// 获取会员拥有的标签
        /// </summary>
        /// <param name="custid"></param>
        /// <returns></returns>
        [Route("GetTagRelation")]
        [HttpGet]
        public JResult GetTagRelation(string custid)
        {
            return _custservice.GetTagRelation(custid);
        }

        /// <summary>
        /// 获取会员该标签的操作者
        /// </summary>
        /// <param name="custid"></param>
        /// <param name="tagid"></param>
        /// <returns></returns>
        [Route("GetTagRelationWithOper")]
        [HttpGet]
        public JResult GetTagRelationWithOper(string custid, string tagid)
        {
            return _custservice.GetTagRelationWithOper(custid, tagid);
        }

        #endregion

        #region 会员积分

        /// <summary>
        /// 会员积分变更
        /// </summary>
        /// <param name="model">变更信息</param>
        /// <returns></returns>
        [Route("ChangePoint")]
        [HttpPost]
        public JResult ChangePoint([FromBody]CustPointModel model)
        {
            return _custservice.ChangePoint(model);
        }

        /// <summary>
        /// 获取会员积分记录列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        [Route("GetCustPointLogPageList")]
        [HttpPost]
        public BasePageList<CustPointViewModel> GetCustPointLogPageList([FromBody]CustPointQueryModel query)
        {
            return _custservice.GetCustPointLogPageList(query);
        }

        /// <summary>
        /// 积分兑换礼券
        /// </summary>
        /// <param name="model">兑换相关信息</param>
        /// <returns></returns>
        [Route("PointExchangeCoupon")]
        [HttpPost]
        public JResult PointExchangeCoupon([FromBody] CustPointExChangeCouponModel model)
        {
            return _custservice.PointExchangeCoupon(model);
        }

        #endregion

        #region 会员礼券

        /// <summary>
        /// 获取获取礼券列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        [Route("GetCouponPageList")]
        [HttpPost]
        public BasePageList<CouponInfoModel> GetCouponPageList([FromBody] CouponQueryModel query)
        {
            return _custservice.GetCouponPageList(query);
        }

        /// <summary>
        /// 添加礼券
        /// </summary>
        /// <param name="model">礼券信息</param>
        /// <returns></returns>
        [Route("AddCoupon")]
        [HttpPost]
        public JResult AddCoupon([FromBody] CouponInfoModel model)
        {
            return _custservice.AddCoupon(model);
        }

        /// <summary>
        /// 修改礼券
        /// </summary>
        /// <param name="model">礼券信息</param>
        /// <returns></returns>
        [Route("UpdateCoupon")]
        [HttpPut]
        public JResult UpdateCoupon([FromBody] CouponInfoModel model)
        {
            return _custservice.UpdateCoupon(model);
        }

        /// <summary>
        /// 获取礼券信息
        /// </summary>
        /// <param name="innerid">id</param>
        /// <returns></returns>
        [Route("GetCouponById")]
        [HttpGet]
        public JResult GetCouponById(string innerid)
        {
            return _custservice.GetCouponById(innerid);
        }


        #endregion
    }
}
