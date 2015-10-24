using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cedar.Framework.Common.BaseClasses;

namespace CCN.Modules.Customer.BusinessEntity
{
    /// <summary>
    /// 会员标签信息model
    /// </summary>
    public class CustTagModel
    {
        /// <summary>
        /// id
        /// </summary>
        public string Innerid { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        public string Tagname { get; set; }

        /// <summary>
        /// 热度
        /// </summary>
        public int Hotcount { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public int Isenabled { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? Createdtime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? Modifiedtime { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public CustTagModel()
        {
            Createdtime = DateTime.Now;
        }
    }

    /// <summary>
    /// 会员标签信息model
    /// </summary>
    public class CustTagQueryModel : QueryModel
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        public string Tagname { get; set; }

        /// <summary>
        /// 热度
        /// </summary>
        public int? Hotcount { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public int? Isenabled { get; set; }

        /// <summary>
        /// 创建时间 1
        /// </summary>
        public DateTime? CreatedtimeS { get; set; }

        /// <summary>
        /// 创建时间 2
        /// </summary>
        public DateTime? ModifiedtimeE { get; set; }
        
    }

    /// <summary>
    /// 打标签model
    /// </summary>
    public class CustTagRelation
    {
        /// <summary>
        /// id
        /// </summary>
        public string Innerid { get; set; }

        /// <summary>
        /// 标签id
        /// </summary>
        public string Tagid { get; set; }

        /// <summary>
        /// 谁打的
        /// </summary>
        public string Fromid { get; set; }

        /// <summary>
        /// 打给谁的
        /// </summary>
        public string Toid { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? Createdtime { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public CustTagRelation()
        {
            Createdtime = DateTime.Now;
        }
    }
}
