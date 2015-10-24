#region

using System.Collections.Generic;
using CCN.Modules.CustRelations.BusinessEntity;
using Cedar.Framework.Common.BaseClasses;

#endregion

namespace CCN.Modules.CustRelations.Interface
{
    /// <summary>
    /// </summary>
    public interface ICustRelationsManagementService
    {
        #region 好友关系管理

        /// <summary>
        /// 查询会员
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        BasePageList<CustViewModel> GetCustPageList(CustQueryModel query);

        /// <summary>
        /// 获取加好友申请
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        BasePageList<CustRelationsApplyViewModels> GetCustRelationsPageList(CustRelationsApplyQueryModels query);    

        /// <summary>
        /// 根据请求id获取申请信息
        /// </summary>
        /// <param name="innerid"></param>
        /// <returns></returns>
        JResult GetRelationsApplyById(string innerid);

        /// <summary>
        /// 添加好友申请
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        JResult AddRelationsApply(CustRelationsApplyModels model);

        /// <summary>
        /// 处理好友申请
        /// </summary>
        /// <returns></returns>
        JResult HandleRelationsApply(string innerid, int status);

        /// <summary>
        /// 删除好友的申请
        /// </summary>
        /// <param name="innerid"></param>
        /// <returns></returns>
        JResult DeleteApplyById(string innerid);

        /// <summary>
        /// 删除好友关系
        /// </summary>
        /// <param name="fromid"></param>
        /// <param name="toid"></param>
        /// <returns></returns>
        JResult DeleteRelations(string fromid, string toid);

        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        JResult GetCustRelationsByUserId(string userid);

        #endregion
    }
}