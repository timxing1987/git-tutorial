using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cedar.Framework.Common.BaseClasses;

namespace CCN.Modules.Car.BusinessEntity
{
    /// <summary>
    /// 车辆信息
    /// </summary>
    public class CarInfoModel
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public string Innerid { get; set; }

        /// <summary>
        /// 聚合数据车辆id
        /// </summary>
        public int? Carid { get; set; }

        /// <summary>
        /// 车辆标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public string pic_url { get; set; }

        /// <summary>
        /// 省份id
        /// </summary>
        public int? provid { get; set; }

        /// <summary>
        /// 城市id
        /// </summary>
        public int? cityid { get; set; }

        /// <summary>
        /// 品牌id
        /// </summary>
        public int? brand_id { get; set; }

        /// <summary>
        /// 车系id
        /// </summary>
        public int? series_id { get; set; }

        /// <summary>
        /// 车型id
        /// </summary>
        public int? model_id { get; set; }
        
        /// <summary>
        /// 颜色
        /// </summary>
        public int? colorid { get; set; }

        /// <summary>
        /// 收购时间
        /// </summary>
        public DateTime? buytime { get; set; }

        /// <summary>
        /// 收购价格(万元)
        /// </summary>
        public decimal? buyprice { get; set; }

        /// <summary>
        /// 成交价格（万元）
        /// </summary>
        public decimal? dealprice { get; set; }

        /// <summary>
        /// 成交均价（万元）(聚合评估)
        /// </summary>
        public decimal? avgprice { get; set; }

        /// <summary>
        /// 当月成交量 (结算得出)
        /// </summary>
        public int? dealnumber { get; set; }

        /// <summary>
        /// 重大事故/水浸/火烧
        /// </summary>
        public short? isproblem { get; set; }

        /// <summary>
        /// 转让原因
        /// </summary>
        public string sellreason { get; set; }

        /// <summary>
        /// 原车主信息
        /// </summary>
        public string masterdesc { get; set; }

        /// <summary>
        /// 成交备注
        /// </summary>
        public string dealdesc { get; set; }

        /// <summary>
        /// 删除备注
        /// </summary>
        public string deletedesc { get; set; }

        /// <summary>
        /// 年检到期时间
        /// </summary>
        public DateTime? ckyear_date { get; set; }

        /// <summary>
        /// 交强险到期时间
        /// </summary>
        public DateTime? tlci_date { get; set; }

        /// <summary>
        /// 是否定期保养
        /// </summary>
        public short? istain { get; set; }

        /// <summary>
        /// 待售价格（万元）
        /// </summary>
        public decimal? price { get; set; }

        /// <summary>
        /// 行驶里程（万元）
        /// </summary>
        public decimal? mileage { get; set; }

        /// <summary>
        /// 车辆上牌日期
        /// </summary>
        public DateTime? register_date { get; set; }
        
        /// <summary>
        /// 卖家类型，1表示个人，2表示商家
        /// </summary>
        public int? seller_type { get; set; }

        /// <summary>
        /// 状态状态[1.在售，2.已售，0.已删除]
        /// </summary>
        public int? status { get; set; }

        /// <summary>
        /// 车况备注/优势
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// createdtime
        /// </summary>
        public DateTime? createdtime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? modifiedtime { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? post_time { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? audit_time { get; set; }

        /// <summary>
        /// 销售时间
        /// </summary>
        public DateTime? sold_time { get; set; }

        /// <summary>
        /// 存档时间
        /// </summary>
        public DateTime? keep_time { get; set; }

        /// <summary>
        /// 车源估值情况
        /// </summary>
        public string estimateprice { get; set; }

        /// <summary>
        /// 车源估值
        /// </summary>
        public float? eval_price { get; set; }

        /// <summary>
        /// 明年评估价格
        /// </summary>
        public float? next_year_eval_price { get; set; }
        
        /// <summary>
        /// 商业险到期时间
        /// </summary>
        public DateTime? audit_date { get; set; }
        
        /// <summary>
        /// 会员ID
        /// </summary>
        public string custid { get; set; }

        #region code name

        /// <summary>
        /// 省份
        /// </summary>
        public string provname { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string cityname { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string brand_name { get; set; }

        /// <summary>
        /// 车系
        /// </summary>
        public string series_name { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public string model_name { get; set; }
        
        /// <summary>
        /// 变速箱类型
        /// </summary>
        public string geartype { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// 排量
        /// </summary>
        public string liter { get; set; }
        
        /// <summary>
        /// 排放标准
        /// </summary>
        public string dischargeName { get; set; }
        
        #endregion
    }

    /// <summary>
    /// 车辆列表显示字段
    /// </summary>
    public class CarInfoListViewModel
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public string Innerid { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public string pic_url { get; set; }

        /// <summary>
        /// 收购时间
        /// </summary>
        public DateTime? buytime { get; set; }

        /// <summary>
        /// 收购价格(万元)
        /// </summary>
        public decimal? buyprice { get; set; }

        /// <summary>
        /// 成交价格（万元）
        /// </summary>
        public decimal? dealprice { get; set; }
        
        /// <summary>
        /// 待售价格（万元）
        /// </summary>
        public decimal? price { get; set; }

        /// <summary>
        /// 行驶里程
        /// </summary>
        public string mileage { get; set; }
        
        /// <summary>
        /// 车辆上牌日期
        /// </summary>
        public DateTime? register_date { get; set; }

        /// <summary>
        /// 状态状态[1.在售，2.已售，0.已删除]
        /// </summary>
        public int? status { get; set; }
        
        #region code name

        /// <summary>
        /// 省份
        /// </summary>
        public string provname { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string cityname { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string brand_name { get; set; }

        /// <summary>
        /// 车系
        /// </summary>
        public string series_name { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public string model_name { get; set; }


        /// <summary>
        /// 变速箱类型
        /// </summary>
        public string geartype { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// 排量
        /// </summary>
        public string liter { get; set; }
        
        /// <summary>
        /// 排放标准
        /// </summary>
        public string dischargeName { get; set; }

        #endregion
    }

    /// <summary>
    /// 车辆查询条件
    /// </summary>
    public class CarQueryModel : QueryModel
    {
        /// <summary>
        /// 车辆标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public string pic_url { get; set; }

        /// <summary>
        /// 省份id
        /// </summary>
        public int? provid { get; set; }

        /// <summary>
        /// 城市id
        /// </summary>
        public int? cityid { get; set; }

        /// <summary>
        /// 品牌id
        /// </summary>
        public int? brand_id { get; set; }

        /// <summary>
        /// 车系id
        /// </summary>
        public int? series_id { get; set; }

        /// <summary>
        /// 车型id
        /// </summary>
        public int? model_id { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public int? colorid { get; set; }

        /// <summary>
        /// 收购时间
        /// </summary>
        public DateTime? buytime { get; set; }

        /// <summary>
        /// min收购价格(万元)
        /// </summary>
        public decimal? minbuyprice { get; set; }

        /// <summary>
        /// max收购价格(万元)
        /// </summary>
        public decimal? maxbuyprice { get; set; }

        /// <summary>
        /// 成交价格（万元）
        /// </summary>
        public decimal? dealprice { get; set; }

        /// <summary>
        /// 重大事故/水浸/火烧
        /// </summary>
        public short? isproblem { get; set; }
        
        /// <summary>
        /// 年检到期时间
        /// </summary>
        public DateTime? ckyear_date { get; set; }

        /// <summary>
        /// 交强险到期时间
        /// </summary>
        public DateTime? tlci_date { get; set; }

        /// <summary>
        /// 是否定期保养
        /// </summary>
        public short? istain { get; set; }

        /// <summary>
        /// 待售价格（万元）
        /// </summary>
        public decimal? price { get; set; }

        /// <summary>
        /// 行驶里程
        /// </summary>
        public decimal? mileage { get; set; }

        /// <summary>
        /// 车辆上牌日期
        /// </summary>
        public DateTime? register_date { get; set; }

        /// <summary>
        /// 变速箱id
        /// </summary>
        public int? gearid { get; set; }

        /// <summary>
        /// 排量[车型基础数据中有，备用]
        /// </summary>
        public int? literid { get; set; }

        /// <summary>
        /// 车身结构
        /// </summary>
        public int? carshructid { get; set; }

        /// <summary>
        /// 排放标准
        /// </summary>
        public int? dischargeid { get; set; }
        
        /// <summary>
        /// 状态状态[1.在售，2.已售，0.已删除]
        /// </summary>
        public int? status { get; set; }
        
        /// <summary>
        /// createdtime
        /// </summary>
        public DateTime? createdtime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? modifiedtime { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? post_time { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? audit_time { get; set; }

        /// <summary>
        /// 销售时间
        /// </summary>
        public DateTime? sold_time { get; set; }

        /// <summary>
        /// 存档时间
        /// </summary>
        public DateTime? keep_time { get; set; }

        /// <summary>
        /// 商业险到期时间
        /// </summary>
        public DateTime? audit_date { get; set; }
        
        /// <summary>
        /// 会员ID
        /// </summary>
        public string custid { get; set; }

        /// <summary>
        /// 全局搜索字段
        /// </summary>
        public string SearchField { get; set; }

    }
}
