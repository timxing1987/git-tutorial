#region

using System;
using System.Collections.Generic;
using System.Linq;
using CCN.Modules.CustRelations.BusinessEntity;
using CCN.Modules.CustRelations.DataAccess;
using Cedar.AuditTrail.Interception;
using Cedar.Core.IoC;
using Cedar.Framework.Common.BaseClasses;
using Cedar.Framework.Common.Server.BaseClasses;

#endregion

namespace CCN.Modules.CustRelations.BusinessComponent
{
    /// <summary>
    /// </summary>
    public class CustRelationsBC : BusinessComponentBase<CustRelationsDA>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="daObject"></param>
        public CustRelationsBC(CustRelationsDA daObject) : base(daObject)
        {
        }

        #region 好友关系管理

        /// <summary>
        /// 查询会员
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        public BasePageList<CustViewModel> GetCustPageList(CustQueryModel query)
        {
            //如果没有输入条件 返回空
            if (string.IsNullOrWhiteSpace(query.Custname) && string.IsNullOrWhiteSpace(query.Mobile) && string.IsNullOrWhiteSpace(query.Email))
            {
                return new BasePageList<CustViewModel>
                {
                    aaData = null,
                    iTotalDisplayRecords = 0,
                    iTotalRecords = 0
                };
            }

            return DataAccess.GetCustPageList(query);
        }

        /// <summary>
        /// 获取加好友申请
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        public BasePageList<CustRelationsApplyViewModels> GetCustRelationsPageList(CustRelationsApplyQueryModels query)
        {
            var list = DataAccess.GetCustRelationsPageList(query);
            return list;
        }

        /// <summary>
        /// 根据请求id获取申请信息
        /// </summary>
        /// <param name="innerid"></param>
        /// <returns></returns>
        public JResult GetRelationsApplyById(string innerid)
        {
            var model = DataAccess.GetRelationsApplyById(innerid);
            if (model == null)
            {
                return new JResult
                {
                    errcode = 400,
                    errmsg = ""
                };
            }
            return new JResult
            {
                errcode = 0,
                errmsg = model
            };
        }
        
        /// <summary>
        /// 添加好友申请
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JResult AddRelationsApply(CustRelationsApplyModels model)
        {
            if (string.IsNullOrWhiteSpace(model.Fromid) || string.IsNullOrWhiteSpace(model.Toid))
            {
                return new JResult
                {
                    errcode = 401,
                    errmsg = "数据不完整"
                };
            }

            var cRelations = DataAccess.CheckRelations(model.Fromid, model.Toid);
            if (cRelations > 0)
            {
                return new JResult
                {
                    errcode = 201,
                    errmsg = "已经是好友"
                };
            }

            var cRelationsApply = DataAccess.CheckRelationsApply(model.Fromid, model.Toid);
            if (cRelationsApply > 0)
            {
                model.Modifiedtime = DateTime.Now;
                DataAccess.UpdateRelationsApply(model);
                return new JResult
                {
                    errcode = 200,
                    errmsg = "重复申请"
                };
                #region old
                //switch (rModel.Status)
                //{
                //    case 0:
                //        model.Modifiedtime = DateTime.Now;
                //        DataAccess.UpdateRelationsApply(model);
                //        return new JResult
                //        {
                //            errcode = 200,
                //            errmsg = "重复申请"
                //        };
                //    case 1:
                //        return new JResult
                //        {
                //            errcode = 201,
                //            errmsg = "已经是好友"
                //        };
                //        //case 2:
                //        //    return new JResult
                //        //    {
                //        //        errcode = 202,
                //        //        errmsg = "拒绝加好友"
                //        //    };
                //        //case 3:
                //        //    return new JResult
                //        //    {
                //        //        errcode = 203,
                //        //        errmsg = "忽略加好友"
                //        //    };
                //}
                #endregion
            }

            model.Status = 0;
            var result = DataAccess.AddRelationsApply(model);
            if (result > 0)
            {
                return new JResult
                {
                    errcode = 0,
                    errmsg = "申请成功"
                };
            }
            return new JResult
            {
                errcode = 400,
                errmsg = "申请失败"
            };
        }

        /// <summary>
        /// 处理好友申请
        /// </summary>
        /// <returns></returns>
        public JResult HandleRelationsApply(string innerid, int status)
        {
            var model = DataAccess.GetRelationsApplyById(innerid);
            if (model == null)
            {
                return new JResult
                {
                    errcode = 400,
                    errmsg = "申请信息不存在"
                };
            }
            var result = DataAccess.HandleRelationsApply(innerid,  status, model.Fromid, model.Toid);
            return new JResult
            {
                errcode = result > 0 ? 0 : 401,
                errmsg = result > 0 ? "处理成功" : "处理失败"
            };
        }

        /// <summary>
        /// 删除好友的申请
        /// </summary>
        /// <param name="innerid"></param>
        /// <returns></returns>
        public JResult DeleteApplyById(string innerid)
        {
            var result = DataAccess.DeleteApplyById(innerid);
            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = result > 0 ? "删除成功" : "删除失败"
            };
        }

        /// <summary>
        /// 删除好友关系
        /// </summary>
        /// <param name="fromid"></param>
        /// <param name="toid"></param>
        /// <returns></returns>
        public JResult DeleteRelations(string fromid, string toid)
        {
            var result = DataAccess.DeleteRelations(fromid, toid);
            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = result > 0 ? "删除成功" : "删除失败"
            };
        }

        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public JResult GetCustRelationsByUserId(string userid)
        {
            var list = DataAccess.GetCustRelationsByUserId(userid).ToList();
            if (!list.Any())
            {
                return new JResult
                {
                    errcode = 400,
                    errmsg = ""
                };
            }

            return new JResult
            {
                errcode = 0,
                errmsg = list
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errcode"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        private static JResult _jResult(int errcode, object errmsg)
        {
            return new JResult
            {
                errcode = errcode,
                errmsg = errmsg
            };
        }
        #endregion
    }
}