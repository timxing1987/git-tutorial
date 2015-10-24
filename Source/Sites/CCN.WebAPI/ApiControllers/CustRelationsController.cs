using System.Web.Http;
using CCN.Modules.CustRelations.BusinessEntity;
using CCN.Modules.CustRelations.Interface;
using Cedar.Core.IoC;
using Cedar.Framework.Common.BaseClasses;

namespace CCN.WebAPI.ApiControllers
{
    /// <summary>
    /// 关系controller
    /// </summary>
    [RoutePrefix("api/CustRelations")]
    public class CustRelationsController : ApiController
    {
        private readonly ICustRelationsManagementService _service;

        public CustRelationsController()
        {
            _service = ServiceLocatorFactory.GetServiceLocator().GetService<ICustRelationsManagementService>();
        }

        #region 好友关系管理

        /// <summary>
        /// 查询会员
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        [Route("GetCustPageList")]
        [HttpPost]
        public BasePageList<CustViewModel> GetCustPageList([FromBody] CustQueryModel query)
        {
            return _service.GetCustPageList(query);
        }

        /// <summary>
        /// 获取加好友申请
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        [Route("GetCustRelationsPageList")]
        [HttpPost]
        public BasePageList<CustRelationsApplyViewModels> GetCustRelationsPageList([FromBody]CustRelationsApplyQueryModels query)
        {
            var list = _service.GetCustRelationsPageList(query);
            return list;
        }

        /// <summary>
        /// 根据请求id获取申请信息
        /// </summary>
        /// <param name="innerid"></param>
        /// <returns></returns>
        [Route("GetRelationsApplyById")]
        [HttpGet]
        public JResult GetRelationsApplyById(string innerid)
        {
            return _service.GetRelationsApplyById(innerid);
        }

        /// <summary>
        /// 添加好友申请
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("AddRelationsApply")]
        [HttpPost]
        public JResult AddRelationsApply([FromBody] CustRelationsApplyModels model)
        {
            return _service.AddRelationsApply(model);
        }

        /// <summary>
        /// 处理好友申请
        /// </summary>
        /// <returns></returns>
        [Route("HandleRelationsApply")]
        [HttpGet]
        public JResult HandleRelationsApply(string innerid, int status)
        {
            return _service.HandleRelationsApply(innerid, status);
        }

        /// <summary>
        /// 删除好友的申请
        /// </summary>
        /// <param name="innerid"></param>
        /// <returns></returns>
        [Route("DeleteApplyById")]
        [HttpDelete]
        public JResult DeleteApplyById(string innerid)
        {
            return _service.DeleteApplyById(innerid);
        }

        /// <summary>
        /// 删除好友关系
        /// </summary>
        /// <param name="fromid"></param>
        /// <param name="toid"></param>
        /// <returns></returns>
        [Route("DeleteRelations")]
        [HttpDelete]
        public JResult DeleteRelations(string fromid, string toid)
        {
            return _service.DeleteRelations(fromid, toid);
        }

        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        [Route("GetCustRelationsByUserId")]
        [HttpGet]
        public JResult GetCustRelationsByUserId(string userid)
        {
            var list = _service.GetCustRelationsByUserId(userid);
            return list;
        }

        #endregion
    }
}
