using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cedar.Framework.Common.BaseClasses;

namespace CCN.Modules.CustRelations.BusinessEntity
{

    /// <summary>
    /// 好友关系申请model
    /// </summary>
    public class CustRelationsApplyModels
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Innerid { get; set; }

        /// <summary>
        /// 提交用户id
        /// </summary>
        public string Fromid { get; set; }

        /// <summary>
        /// 欲加好友id
        /// </summary>
        public string Toid { get; set; }

        /// <summary>
        /// [0待接收/1已接收/2拒绝]
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime? Createdtime { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? Modifiedtime { get; set; }

        /// <summary>
        /// 附件消息
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 来源[如 账号查找/扫二维码/...]
        /// </summary>
        public int Sourceid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CustRelationsApplyModels()
        {
            Createdtime = DateTime.Now;
        }
    }

    /// <summary>
    /// 好友关系申请model
    /// </summary>
    public class CustRelationsApplyQueryModels : QueryModel
    {

        /// <summary>
        /// 主键
        /// </summary>
        public string Innerid { get; set; }

        /// <summary>
        /// 提交用户id
        /// </summary>
        public string Fromid { get; set; }

        /// <summary>
        /// 欲加好友id
        /// </summary>
        public string Toid { get; set; }

        /// <summary>
        /// [0待接收/1已接收/2拒绝]
        /// </summary>
        public int? Status { get; set; }

    }

    /// <summary>
    /// 好友关系申请model
    /// </summary>
    public class CustRelationsApplyViewModels
    {

        /// <summary>
        /// 主键
        /// </summary>
        public string Innerid { get; set; }

        /// <summary>
        /// 提交用户id
        /// </summary>
        public string Fromid { get; set; }

        /// <summary>
        /// 欲加好友id
        /// </summary>
        public string Toid { get; set; }

        /// <summary>
        /// [0待接收/1已接收/2拒绝]
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 会员头像
        /// </summary>
        public string FromUserIcon { get; set; }
    }
}
