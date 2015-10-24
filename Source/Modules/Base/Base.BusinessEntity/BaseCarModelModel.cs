using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCN.Modules.Base.BusinessEntity
{
    /// <summary>
    /// 车型
    /// </summary>
    public class BaseCarModelModel
    {
        /// <summary>
        /// id
        /// </summary>
        public int Innerid { get; set; }

        /// <summary>
        /// 车型名称
        /// </summary>
        public string Modelname { get; set; }

        /// <summary>
        /// 指导价
        /// </summary>
        public decimal Modelprice { get; set; }

        /// <summary>
        /// 年款
        /// </summary>
        public int Modelyear { get; set; }

        /// <summary>
        /// 最早注册年份
        /// </summary>
        public int Minregyear { get; set; }

        /// <summary>
        /// 最晚注册年份
        /// </summary>
        public int Maxregyear { get; set; }

        /// <summary>
        /// 排量
        /// </summary>
        public string Liter { get; set; }

        /// <summary>
        /// 变速箱类型
        /// </summary>
        public string Geartype { get; set; }

        /// <summary>
        /// 排放标准
        /// </summary>
        public string Dischargestandard { get; set; }

        /// <summary>
        /// 车系ID
        /// </summary>
        public int Seriesid { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnabled { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
