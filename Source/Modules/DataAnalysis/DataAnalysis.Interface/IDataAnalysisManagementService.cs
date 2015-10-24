using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cedar.Framework.Common.BaseClasses;

namespace CCN.Modules.DataAnalysis.Interface
{
    /// <summary>
    /// 数据接口
    /// </summary>
    public interface IDataAnalysisManagementService
    {
        /// <summary>
        /// 本地市场按月的持有量TOP10
        /// </summary>
        /// <returns></returns>
        JResult GetLocalByMonthTop10();

        #region 买家分布

        /// <summary>
        /// 获取不同年龄段买家分布
        /// </summary>
        /// <returns></returns>
        JResult GetAgeArea();

        /// <summary>
        /// 获取买家性别比例
        /// </summary>
        /// <returns></returns>
        JResult GetGenterPer();

        #endregion

        #region  2015年交易额交易量折线图

        /// <summary>
        /// 年度市场交易走势
        /// </summary>
        /// <returns></returns>
        JResult GetTradeLineByYear();

        #endregion

        #region 二手车属性

        /// <summary>
        /// 二手车使用年限分析
        /// </summary>
        /// <returns></returns>
        JResult GetUsedCarYearAnalysis();

        /// <summary>
        /// 可接受的二手车行驶里程
        /// </summary>
        /// <returns></returns>
        JResult GetUsedCarAccept();

        /// <summary>
        /// 3-5万公里满意度
        /// </summary>
        /// <returns></returns>
        JResult GetSatisfaction3To5();

        /// <summary>
        /// 3-5万公里不满意度
        /// </summary>
        /// <returns></returns>
        JResult GetUnSatisfaction3To5();

        /// <summary>
        /// 1-3万公里满意度
        /// </summary>
        /// <returns></returns>
        JResult GetSatisfaction1To3();

        /// <summary>
        /// 1-3万公里不满意度
        /// </summary>
        /// <returns></returns>
        JResult GetUnSatisfaction1To3();

        #endregion

        #region 品牌热度排行

        /// <summary>
        /// 月度品牌热搜前10
        /// </summary>
        /// <returns></returns>
        JResult GetHotBrandTop10();

        #endregion

        #region 交易量

        /// <summary>
        /// 二手车交易量全国占比排行前10省份
        /// </summary>
        /// <returns></returns>
        JResult GetUsedCarTradeTop10();

        /// <summary>
        /// 二手车交易量全国占比排行倒数8省份
        /// </summary>
        /// <returns></returns>
        JResult GetUsedCarTradeLaset8();

        /// <summary>
        /// 二手车近几年交易量
        /// </summary>
        /// <returns></returns>
        JResult GetUsedCarTradeRecentYears();

        #endregion

    }
}
