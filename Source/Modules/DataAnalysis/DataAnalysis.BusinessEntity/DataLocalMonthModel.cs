using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCN.Modules.DataAnalysis.BusinessEntity
{
    /// <summary>
    /// 本地月持有量Top10
    /// </summary>
    public class DataLocalMonthModel
    {
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 车型Id
        /// </summary>
        public int ModelId { get; set; }
    }
}
