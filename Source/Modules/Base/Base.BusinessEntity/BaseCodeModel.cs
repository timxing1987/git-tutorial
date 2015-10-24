using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCN.Modules.Base.BusinessEntity
{
    /// <summary>
    /// 代码值
    /// </summary>
    public class BaseCodeModel
    {
        /// <summary>
        /// id
        /// </summary>
        public string Innerid { get; set; }

        /// <summary>
        /// 代码值
        /// </summary>
        public string CodeValue { get; set; }

        /// <summary>
        /// 代码名称
        /// </summary>
        public string CodeName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 代码类型key
        /// </summary>
        public string Typekey { get; set; }

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
    /// 代码值
    /// </summary>
    public class BaseCodeSelectModel
    {
        /// <summary>
        /// 代码值
        /// </summary>
        public string CodeValue { get; set; }

        /// <summary>
        /// 代码名称
        /// </summary>
        public string CodeName { get; set; }
    }
}
