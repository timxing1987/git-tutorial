
using System.Collections.Generic;

namespace CCN.Modules.Base.BusinessEntity
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseProvince
    {
        /// <summary>
        /// id
        /// </summary>
        public int Innerid { get; set; }

        /// <summary>
        /// 省份名称
        /// </summary>
        public string ProvName { get; set; }

        /// <summary>
        /// 首字母
        /// </summary>
        public string Initial { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnabled { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class JsonGroupByModel
    {
        /// <summary>
        /// 首字母
        /// </summary>
        public string Initial { get; set; }

        /// <summary>
        /// 列表
        /// </summary>
        public List<BaseProvince> ProvList { get; set; }
    }
}
