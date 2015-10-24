using System;
using Cedar.Framework.Common.BaseClasses;

namespace CCN.Modules.Customer.BusinessEntity
{
    /// <summary>
    /// 礼券model
    /// </summary>
    public class CouponInfoModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Innerid { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 子标题
        /// </summary>
        public string Titlesub { get; set; }

        /// <summary>
        /// 面额
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string Logourl { get; set; }

        /// <summary>
        /// 有效期类型[1.时间,2.天数]
        /// </summary>
        public int? Vtype { get; set; }

        /// <summary>
        /// [1]开始时间
        /// </summary>
        public DateTime? Vstart { get; set; }

        /// <summary>
        /// [1]结束时间
        /// </summary>
        public DateTime? Vend { get; set; }
        
        /// <summary>
        /// [2]起效天数
        /// </summary>
        public int? Value1 { get; set; }

        /// <summary>
        /// [2]有效天使
        /// </summary>
        public int? Value2 { get; set; }

        /// <summary>
        /// 起始库存
        /// </summary>
        public int? Maxcount { get; set; }

        /// <summary>
        /// 当前库存
        /// </summary>
        public int? Count { get; set; }

        /// <summary>
        /// Code展示类型，"CODE_TYPE_TEXT"，文本；"CODE_TYPE_BARCODE"，一维码 ；"CODE_TYPE_QRCODE"，二维码；"CODE_TYPE_ONLY_QRCODE",二维码无code显示；"CODE_TYPE_ONLY_BARCODE",一维码无code显示；
        /// </summary>
        public string Codetype { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? Createdtime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? Modifiedtime { get; set; }

        /// <summary>
        /// 使用启用
        /// </summary>
        public int? IsEnabled { get; set; }
        
    }

    /// <summary>
    /// 礼券model
    /// </summary>
    public class CouponQueryModel : QueryModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 子标题
        /// </summary>
        public string Titlesub { get; set; }

        /// <summary>
        /// 面额min
        /// </summary>
        public int MinAmount { get; set; }

        /// <summary>
        /// 面额max
        /// </summary>
        public int MaxAmount { get; set; }

        /// <summary>
        /// 有效期类型[1.时间,2.天数]
        /// </summary>
        public int Vtype { get; set; }
        
        /// <summary>
        /// 起始库存
        /// </summary>
        public int Maxcount { get; set; }

        /// <summary>
        /// 当前库存
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Code展示类型，"CODE_TYPE_TEXT"，文本；"CODE_TYPE_BARCODE"，一维码 ；"CODE_TYPE_QRCODE"，二维码；"CODE_TYPE_ONLY_QRCODE",二维码无code显示；"CODE_TYPE_ONLY_BARCODE",一维码无code显示；
        /// </summary>
        public string Codetype { get; set; }

        /// <summary>
        /// 使用启用
        /// </summary>
        public int? IsEnabled { get; set; }

    }
}
