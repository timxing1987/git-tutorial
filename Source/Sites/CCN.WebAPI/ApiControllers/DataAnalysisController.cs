using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CCN.Modules.DataAnalysis.Interface;
using Cedar.Core.IoC;
using Cedar.Framework.Common.BaseClasses;

using System.Threading.Tasks;
using Cedar.Core.ApplicationContexts;

namespace CCN.WebAPI.ApiControllers
{
    /// <summary>
    /// 数据接口
    /// </summary>
    [RoutePrefix("api/DataAnalysis")]
    public class DataAnalysisController : ApiController
    {
        private readonly IDataAnalysisManagementService _dataanalysisservice;

        public DataAnalysisController()
        {
            _dataanalysisservice = ServiceLocatorFactory.GetServiceLocator().GetService<IDataAnalysisManagementService>();
        }

        /// <summary>
        /// 本地市场按月的持有量TOP10
        /// </summary>
        /// <returns></returns>
        [Route("GetLocalByMonthTop10")]
        [HttpGet]
        public JResult GetLocalByMonthTop10()
        {
            var result = _dataanalysisservice.GetLocalByMonthTop10();
            return result;
        }

        #region 买家分布

        /// <summary>
        /// 获取不同年龄段买家分布
        /// </summary>
        /// <returns></returns>
        [Route("GetAgeArea")]
        [HttpGet]
        public JResult GetAgeArea()
        {
            var result = _dataanalysisservice.GetAgeArea();
            return result;
        }

        /// <summary>
        /// 获取买家性别比例
        /// </summary>
        /// <returns></returns>
        [Route("GetGenterPer")]
        [HttpGet]
        public JResult GetGenterPer()
        {
            var result = _dataanalysisservice.GetGenterPer();
            return result;
        }

        #endregion

        #region 2015年交易额交易量折线图

        /// <summary>
        /// 年度市场交易走势
        /// </summary>
        /// <returns></returns>
        [Route("GetTradeLineByYear")]
        [HttpGet]
        public JResult GetTradeLineByYear()
        {
            var result = _dataanalysisservice.GetTradeLineByYear();
            return result;
        }

        #endregion

        #region 二手车属性

        /// <summary>
        /// 二手车使用年限分析
        /// </summary>
        /// <returns></returns>
        [Route("GetUsedCarYearAnalysis")]
        [HttpGet]
        public JResult GetUsedCarYearAnalysis()
        {
            var result = _dataanalysisservice.GetUsedCarYearAnalysis();
            return result;
        }

        /// <summary>
        /// 可接受的二手车行驶里程
        /// </summary>
        /// <returns></returns>
        [Route("GetUsedCarAccept")]
        [HttpGet]
        public JResult GetUsedCarAccept()
        {
            var result = _dataanalysisservice.GetUsedCarAccept();
            return result;
        }

        /// <summary>
        /// 3-5万公里满意度
        /// </summary>
        /// <returns></returns>
        [Route("GetSatisfaction3To5")]
        [HttpGet]
        public JResult GetSatisfaction3To5()
        {
            var result = _dataanalysisservice.GetSatisfaction3To5();
            return result;
        }

        /// <summary>
        /// 3-5万公里不满意度
        /// </summary>
        /// <returns></returns>
        [Route("GetUnSatisfaction3To5")]
        [HttpGet]
        public JResult GetUnSatisfaction3To5()
        {
            var result = _dataanalysisservice.GetUnSatisfaction3To5();
            return result;
        }

        /// <summary>
        /// 1-3万公里满意度
        /// </summary>
        /// <returns></returns>
        [Route("GetSatisfaction1To3")]
        [HttpGet]
        public JResult GetSatisfaction1To3()
        {
            var result = _dataanalysisservice.GetSatisfaction1To3();
            return result;
        }

        /// <summary>
        /// 1-3万公里不满意度
        /// </summary>
        /// <returns></returns>
        [Route("GetUnSatisfaction1To3")]
        [HttpGet]
        public JResult GetUnSatisfaction1To3()
        {
            var result = _dataanalysisservice.GetUnSatisfaction1To3();
            return result;
        }

        #endregion

        #region 品牌热度排行

        /// <summary>
        /// 月度品牌热搜前10
        /// </summary>
        /// <returns></returns>
        [Route("GetHotBrandTop10")]
        [HttpGet]
        public JResult GetHotBrandTop10()
        {
            var result = _dataanalysisservice.GetHotBrandTop10();
            return result;
        }

        #endregion

        #region 交易量

        /// <summary>
        /// 月度品牌热搜前10
        /// </summary>
        /// <returns></returns>
        [Route("GetUsedCarTradeTop10")]
        [HttpGet]
        public JResult GetUsedCarTradeTop10()
        {
            var result = _dataanalysisservice.GetUsedCarTradeTop10();
            return result;
        }

        /// <summary>
        /// 二手车交易量全国占比排行倒数8省份
        /// </summary>
        /// <returns></returns>
        [Route("GetUsedCarTradeLaset8")]
        [HttpGet]
        public JResult GetUsedCarTradeLaset8()
        {
            var result = _dataanalysisservice.GetUsedCarTradeLaset8();
            return result;
        }

        /// <summary>
        /// 二手车近几年交易量
        /// </summary>
        /// <returns></returns>
        [Route("GetUsedCarTradeRecentYears")]
        [HttpGet]
        public JResult GetUsedCarTradeRecentYears()
        {
            var result = _dataanalysisservice.GetUsedCarTradeRecentYears();
            return result;
        }

        #endregion
    }
}
