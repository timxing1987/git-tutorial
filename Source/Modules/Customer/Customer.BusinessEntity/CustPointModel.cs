using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cedar.Framework.Common.BaseClasses;

namespace CCN.Modules.Customer.BusinessEntity
{
    /// <summary>
    /// 积分model
    /// </summary>
    public class CustPointModel
    {
        /// <summary>
        /// id
        /// </summary>
        public string Innerid { get; set; }

        /// <summary>
        /// 会员id
        /// </summary>
        public string Custid { get; set; }

        /// <summary>
        /// 1.加2.减
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 来源 【code】
        /// </summary>
        public int Sourceid { get; set; }

        /// <summary>
        /// 积分数
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime? Validtime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Createdtime { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public CustPointModel()
        {
            Createdtime = DateTime.Now;
        }

    }

    /// <summary>
    /// 积分model
    /// </summary>
    public class CustPointQueryModel : QueryModel
    {
        /// <summary>
        /// 会员id
        /// </summary>
        public string Custid { get; set; }

        /// <summary>
        /// 1.加2.减
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 来源 【code】
        /// </summary>
        public int Sourceid { get; set; }

        /// <summary>
        /// 积分范围
        /// </summary>
        public int MinPoint { get; set; }

        /// <summary>
        /// 积分范围
        /// </summary>
        public int MaxPoint { get; set; }
    }

    /// <summary>
    /// 积分model
    /// </summary>
    public class CustPointViewModel
    {
        /// <summary>
        /// id
        /// </summary>
        public string Innerid { get; set; }

        /// <summary>
        /// 会员id
        /// </summary>
        public string Custid { get; set; }

        /// <summary>
        /// 1.加2.减
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 来源 【code】
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public int Point { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime? Validtime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Createdtime { get; set; }
    }

    /// <summary>
    /// 积分兑换礼券 model
    /// </summary>
    public class CustPointExChangeCouponModel
    {
        /// <summary>
        /// 礼券id
        /// </summary>
        public string Cardid { get; set; }

        /// <summary>
        /// 会员id
        /// </summary>
        public string Custid { get; set; }

        /// <summary>
        /// 来源id
        /// </summary>
        public int Sourceid { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime? Validtime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Createdtime { get; set; }

        /// <summary>
        /// 礼券code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 礼券qrcode
        /// </summary>
        public string QrCode { get; set; }
    }
}
