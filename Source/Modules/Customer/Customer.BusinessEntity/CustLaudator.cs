using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCN.Modules.Customer.BusinessEntity
{
    /// <summary>
    /// 
    /// </summary>
    public class CustLaudator
    {
        /// <summary>
        /// id
        /// </summary>
        public string Innerid { get; set; }
        
        /// <summary>
        /// 微信公众号id
        /// </summary>
        public string Accountid { get; set; }

        /// <summary>
        /// openid
        /// </summary>
        public string Openid { get; set; }

        /// <summary>
        /// 微信昵称
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 性别 1.男2女
        /// </summary>
        public short Sex { get; set; }

        /// <summary>
        /// 微信头像
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarkname { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Subscribe_time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public short Subscribe { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 点赞时间
        /// </summary>
        public DateTime? Createdtime { get; set; }

        /// <summary>
        /// 被点赞人id
        /// </summary>
        public string Tocustid { get; set; }

        /// <summary>
        /// 车辆id
        /// </summary>
        public string Carid { get; set; }

        /// <summary>
        /// 是否重复点赞 ,>0代表点过赞
        /// </summary>
        public int? IsPraise { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CustLaudatorRelation
    {
        /// <summary>
        /// id
        /// </summary>
        public string Innerid { get; set; }

        /// <summary>
        /// 点赞这id
        /// </summary>
        public string Laudatorid { get; set; }

        /// <summary>
        /// 被点赞人id
        /// </summary>
        public string Tocustid { get; set; }

        /// <summary>
        /// 车辆id
        /// </summary>
        public string Carid { get; set; }
        /// <summary>
        /// 点赞时间
        /// </summary>
        public DateTime? Createdtime { get; set; }
        
    }

}
