using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCN.Modules.Customer.BusinessEntity
{
    /// <summary>
    /// 会员操作状态类型
    /// </summary>
    public class CustResult
    {
        /// <summary>
        /// 操作状态码
        /// </summary>
        public int errcode { get; set; }

        /// <summary>
        /// 结果类型
        /// </summary>
        public dynamic errmsg { get; set; }
    }
}
