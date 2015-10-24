using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cedar.Framework.Common.BaseClasses;
using Cedar.Framework.Common.Server.BaseClasses;
using CCN.Modules.DataAnalysis.DataAccess;

namespace CCN.Modules.DataAnalysis.BusinessComponent
{
    /// <summary>
    /// 
    /// </summary>
    public class DataAnalysisBC : BusinessComponentBase<DataAnalysisDA>
    {
        /// <summary>
        /// </summary>
        /// <param name="da"></param>
        public DataAnalysisBC(DataAnalysisDA da)
            : base(da)
        {

        }

        /// <summary>
        /// 本地市场按月的持有量TOP10
        /// </summary>
        /// <returns></returns>
        public JResult GetLocalByMonthTop10()
        {
            var list = DataAccess.GetLocalByMonthTop10();
            if (list == null)
            {
                return new JResult
                {
                    errcode = 400,
                    errmsg = ""
                };
            }
            return new JResult
            {
                errcode = 0,
                errmsg = list
            };
        }

        #region 买家分布

        /// <summary>
        /// 获取不同年龄段买家分布
        /// </summary>
        /// <returns></returns>
        public JResult GetAgeArea()
        {
            var list = DataAccess.GetAgeArea();
            if (list == null)
            {
                return new JResult
                {
                    errcode = 400,
                    errmsg = ""
                };
            }
            return new JResult
            {
                errcode = 0,
                errmsg = list
            };
        }

        /// <summary>
        /// 获取买家性别比例
        /// </summary>
        /// <returns></returns>
        public JResult GetGenterPer()
        {
            var list = DataAccess.GetGenterPer();
            if (list == null)
            {
                return new JResult
                {
                    errcode = 400,
                    errmsg = ""
                };
            }
            return new JResult
            {
                errcode = 0,
                errmsg = list
            };
        }

        #endregion

        #region 2015年交易额交易量折线图

        /// <summary>
        /// 年度市场交易走势
        /// </summary>
        /// <returns></returns>
        public JResult GetTradeLineByYear()
        {
            var list = DataAccess.GetTradeLineByYear();
            if (list == null)
            {
                return new JResult
                {
                    errcode = 400,
                    errmsg = ""
                };
            }
            return new JResult
            {
                errcode = 0,
                errmsg = list
            };
        }

        #endregion

        #region 二手车属性

        /// <summary>
        /// 二手车使用年限分析
        /// </summary>
        /// <returns></returns>
        public JResult GetUsedCarYearAnalysis()
        {
            var list = DataAccess.GetUsedCarYearAnalysis();
            if (list == null)
            {
                return new JResult
                {
                    errcode = 400,
                    errmsg = ""
                };
            }
            return new JResult
            {
                errcode = 0,
                errmsg = list
            };
        }

        /// <summary>
        /// 可接受的二手车行驶里程
        /// </summary>
        /// <returns></returns>
        public JResult GetUsedCarAccept()
        {
            var list = DataAccess.GetUsedCarAccept();
            if (list == null)
            {
                return new JResult
                {
                    errcode = 400,
                    errmsg = ""
                };
            }
            return new JResult
            {
                errcode = 0,
                errmsg = list
            };
        }

        /// <summary>
        /// 3-5万公里满意度
        /// </summary>
        /// <returns></returns>
        public JResult GetSatisfaction3To5()
        {
            var list = DataAccess.GetSatisfaction3To5();
            if (list == null)
            {
                return new JResult
                {
                    errcode = 400,
                    errmsg = ""
                };
            }
            return new JResult
            {
                errcode = 0,
                errmsg = list
            };
        }

        /// <summary>
        /// 3-5万公里不满意度
        /// </summary>
        /// <returns></returns>
        public JResult GetUnSatisfaction3To5()
        {
            var list = DataAccess.GetUnSatisfaction3To5();
            if (list == null)
            {
                return new JResult
                {
                    errcode = 400,
                    errmsg = ""
                };
            }
            return new JResult
            {
                errcode = 0,
                errmsg = list
            };
        }

        /// <summary>
        /// 1-3万公里满意度
        /// </summary>
        /// <returns></returns>
        public JResult GetSatisfaction1To3()
        {
            var list = DataAccess.GetSatisfaction1To3();
            if (list == null)
            {
                return new JResult
                {
                    errcode = 400,
                    errmsg = ""
                };
            }
            return new JResult
            {
                errcode = 0,
                errmsg = list
            };
        }

        /// <summary>
        /// 1-3万公里不满意度
        /// </summary>
        /// <returns></returns>
        public JResult GetUnSatisfaction1To3()
        {
            var list = DataAccess.GetUnSatisfaction1To3();
            if (list == null)
            {
                return new JResult
                {
                    errcode = 400,
                    errmsg = ""
                };
            }
            return new JResult
            {
                errcode = 0,
                errmsg = list
            };
        }

        #endregion

        #region 品牌热度排行

        /// <summary>
        /// 月度品牌热搜前10
        /// </summary>
        /// <returns></returns>
        public JResult GetHotBrandTop10()
        {
            var list = DataAccess.GetHotBrandTop10();
            if (list == null)
            {
                return new JResult
                {
                    errcode = 400,
                    errmsg = ""
                };
            }
            return new JResult
            {
                errcode = 0,
                errmsg = list
            };
        }

        #endregion

        #region 交易量

        /// <summary>
        /// 二手车交易量全国占比排行前10省份
        /// </summary>
        /// <returns></returns>
        public JResult GetUsedCarTradeTop10()
        {
            var list = DataAccess.GetUsedCarTradeTop10();
            if (list == null)
            {
                return new JResult
                {
                    errcode = 400,
                    errmsg = ""
                };
            }
            return new JResult
            {
                errcode = 0,
                errmsg = list
            };
        }

        /// <summary>
        /// 二手车交易量全国占比排行倒数8省份
        /// </summary>
        /// <returns></returns>
        public JResult GetUsedCarTradeLaset8()
        {
            var list = DataAccess.GetUsedCarTradeLaset8();
            if (list == null)
            {
                return new JResult
                {
                    errcode = 400,
                    errmsg = ""
                };
            }
            return new JResult
            {
                errcode = 0,
                errmsg = list
            };
        }

        /// <summary>
        /// 二手车近几年交易量
        /// </summary>
        /// <returns></returns>
        public JResult GetUsedCarTradeRecentYears()
        {
            var list = DataAccess.GetUsedCarTradeRecentYears();
            if (list == null)
            {
                return new JResult
                {
                    errcode = 400,
                    errmsg = ""
                };
            }
            return new JResult
            {
                errcode = 0,
                errmsg = list
            };
        }

        #endregion

    }
}
