using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCN.Modules.DataAnalysis.BusinessEntity;

namespace CCN.Modules.DataAnalysis.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class DataAnalysisDA : DataAnalysisDataAccessBase
    {
        /// <summary>
        /// 
        /// </summary>
        public DataAnalysisDA()
        {

        }

        /// <summary>
        /// 本地市场按月的持有量TOP10
        /// </summary>
        /// <returns></returns>
        public IEnumerable<dynamic> GetLocalByMonthTop10()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select count(*) as cnt,month(post_time) as monthflg,model_id from car_info_bak 
                            where cityid=125 AND month(post_time)=10 group by monthflg,model_id order by cnt desc
                            limit 0, 10; ");

            var list = Helper.Query<dynamic>(sql.ToString());
            return list;
        }

        #region 买家分布

        /// <summary>
        /// 获取不同年龄段买家分布
        /// </summary>
        /// <returns></returns>
        public IEnumerable<dynamic> GetAgeArea()
        {
            List<DataAnalysisModel> list = new List<DataAnalysisModel>();
            
            DataAnalysisModel da1 = new DataAnalysisModel();
            da1.key = "First";
            da1.value = "1";
            DataAnalysisModel da2 = new DataAnalysisModel();
            da2.key = "Second";
            da2.value = "2";
            DataAnalysisModel da3 = new DataAnalysisModel();
            da3.key = "Third";
            da3.value = "3";
            DataAnalysisModel da4 = new DataAnalysisModel();
            da4.key = "Fourth";
            da4.value = "2";
            DataAnalysisModel da5 = new DataAnalysisModel();
            da5.key = "Fifth";
            da5.value = "1";
            list.Add(da1);
            list.Add(da2);
            list.Add(da3);
            list.Add(da4);
            list.Add(da5);

            return list;
        }

        /// <summary>
        /// 获取买家性别比例
        /// </summary>
        /// <returns></returns>
        public IEnumerable<dynamic> GetGenterPer()
        {
            List<DataAnalysisModel> list = new List<DataAnalysisModel>();

            DataAnalysisModel da1 = new DataAnalysisModel();
            da1.key = "Male";
            da1.value = "87%";
            DataAnalysisModel da2 = new DataAnalysisModel();
            da2.key = "Female";
            da2.value = "13%";

            list.Add(da1);
            list.Add(da2);

            return list;
        }

        #endregion

        #region 2015年交易额交易量折线图

        /// <summary>
        /// 年度市场交易走势
        /// </summary>
        /// <returns></returns>
        public IEnumerable<dynamic> GetTradeLineByYear()
        {
            List<DataAnalysisModel> list = new List<DataAnalysisModel>();

            DataAnalysisModel da1 = new DataAnalysisModel();
            da1.key = "2014年12月";
            da1.value = "60.18";
            da1.value2 = "399.39";
            DataAnalysisModel da2 = new DataAnalysisModel();
            da2.key = "2015年1月";
            da2.value = "49.04";
            da2.value2 = "279.73";
            DataAnalysisModel da3 = new DataAnalysisModel();
            da3.key = "2015年2月";
            da3.value = "36.58";
            da3.value2 = "196.81";
            DataAnalysisModel da4 = new DataAnalysisModel();
            da4.key = "2015年3月";
            da4.value = "52.72";
            da4.value2 = "292.06";
            DataAnalysisModel da5 = new DataAnalysisModel();
            da5.key = "2015年4月";
            da5.value = "54.89";
            da5.value2 = "305.57";
            DataAnalysisModel da6 = new DataAnalysisModel();
            da6.key = "2015年5月";
            da6.value = "52";
            da6.value2 = "303.63";
            DataAnalysisModel da7 = new DataAnalysisModel();
            da7.key = "2015年6月";
            da7.value = "76.03";
            da7.value2 = "447.63";
            DataAnalysisModel da8 = new DataAnalysisModel();
            da8.key = "2015年7月";
            da8.value = "77.05";
            da8.value2 = "461.68";
            DataAnalysisModel da9 = new DataAnalysisModel();
            da9.key = "2015年8月";
            da9.value = "74.79";
            da9.value2 = "426.5";

            list.Add(da1);
            list.Add(da2);
            list.Add(da3);
            list.Add(da4);
            list.Add(da5);
            list.Add(da6);
            list.Add(da7);
            list.Add(da8);
            list.Add(da9);

            return list;
        }

        #endregion

        #region 二手车属性

        /// <summary>
        /// 二手车使用年限分析
        /// </summary>
        /// <returns></returns>
        public IEnumerable<dynamic> GetUsedCarYearAnalysis()
        {
            List<DataAnalysisModel> list = new List<DataAnalysisModel>();

            DataAnalysisModel da1 = new DataAnalysisModel();
            da1.key = "3年内";
            da1.value = "18.32%";
            DataAnalysisModel da2 = new DataAnalysisModel();
            da2.key = "3-6年";
            da2.value = "67.25%";
            DataAnalysisModel da3 = new DataAnalysisModel();
            da3.key = "6-10年";
            da3.value = "5.38%";
            DataAnalysisModel da4 = new DataAnalysisModel();
            da4.key = "10年以上";
            da4.value = "9.05%";

            list.Add(da1);
            list.Add(da2);
            list.Add(da3);
            list.Add(da4);

            return list;
        }

        /// <summary>
        /// 可接受的二手车行驶里程
        /// </summary>
        /// <returns></returns>
        public IEnumerable<dynamic> GetUsedCarAccept()
        {
            List<DataAnalysisModel> list = new List<DataAnalysisModel>();

            DataAnalysisModel da1 = new DataAnalysisModel();
            da1.key = "<=1万公里";
            da1.value = "17.4%";
            DataAnalysisModel da2 = new DataAnalysisModel();
            da2.key = "1-3万公里";
            da2.value = "28.5%";
            DataAnalysisModel da3 = new DataAnalysisModel();
            da3.key = "3-5万公里";
            da3.value = "31.7%";
            DataAnalysisModel da4 = new DataAnalysisModel();
            da4.key = "5-10万公里";
            da4.value = "17.4%";
            DataAnalysisModel da5 = new DataAnalysisModel();
            da5.key = "10-15万公里";
            da5.value = "3.8%";
            DataAnalysisModel da6 = new DataAnalysisModel();
            da6.key = ">15万公里";
            da6.value = "17.4%";

            list.Add(da1);
            list.Add(da2);
            list.Add(da3);
            list.Add(da4);
            list.Add(da5);
            list.Add(da6);

            return list;
        }

        /// <summary>
        /// 3-5万公里满意度
        /// </summary>
        /// <returns></returns>
        public IEnumerable<dynamic> GetSatisfaction3To5()
        {
            List<DataAnalysisModel> list = new List<DataAnalysisModel>();

            DataAnalysisModel da1 = new DataAnalysisModel();
            da1.key = "丰田";
            da1.value = "37.1%";
            DataAnalysisModel da2 = new DataAnalysisModel();
            da2.key = "日产";
            da2.value = "37%";
            DataAnalysisModel da3 = new DataAnalysisModel();
            da3.key = "现代";
            da3.value = "36.2%";
            DataAnalysisModel da4 = new DataAnalysisModel();
            da4.key = "马自达";
            da4.value = "36.1%";
            DataAnalysisModel da5 = new DataAnalysisModel();
            da5.key = "福特";
            da5.value = "36%";

            list.Add(da1);
            list.Add(da2);
            list.Add(da3);
            list.Add(da4);
            list.Add(da5);

            return list;
        }

        /// <summary>
        /// 3-5万公里不满意度
        /// </summary>
        /// <returns></returns>
        public IEnumerable<dynamic> GetUnSatisfaction3To5()
        {
            List<DataAnalysisModel> list = new List<DataAnalysisModel>();

            DataAnalysisModel da1 = new DataAnalysisModel();
            da1.key = "路虎";
            da1.value = "29.7%";
            DataAnalysisModel da2 = new DataAnalysisModel();
            da2.key = "奥迪";
            da2.value = "28.9%";
            DataAnalysisModel da3 = new DataAnalysisModel();
            da3.key = "金杯";
            da3.value = "27.9%";
            DataAnalysisModel da4 = new DataAnalysisModel();
            da4.key = "吉利";
            da4.value = "26.5%";
            DataAnalysisModel da5 = new DataAnalysisModel();
            da5.key = "保时捷";
            da5.value = "25.5%";

            list.Add(da1);
            list.Add(da2);
            list.Add(da3);
            list.Add(da4);
            list.Add(da5);

            return list;
        }

        /// <summary>
        /// 1-3万公里满意度
        /// </summary>
        /// <returns></returns>
        public IEnumerable<dynamic> GetSatisfaction1To3()
        {
            List<DataAnalysisModel> list = new List<DataAnalysisModel>();

            DataAnalysisModel da1 = new DataAnalysisModel();
            da1.key = "长城";
            da1.value = "32.8%";
            DataAnalysisModel da2 = new DataAnalysisModel();
            da2.key = "雪弗兰";
            da2.value = "32.1%";
            DataAnalysisModel da3 = new DataAnalysisModel();
            da3.key = "大众";
            da3.value = "32.0%";
            DataAnalysisModel da4 = new DataAnalysisModel();
            da4.key = "奥迪";
            da4.value = "31.5%";
            DataAnalysisModel da5 = new DataAnalysisModel();
            da5.key = "保时捷";
            da5.value = "30%";

            list.Add(da1);
            list.Add(da2);
            list.Add(da3);
            list.Add(da4);
            list.Add(da5);

            return list;
        }

        /// <summary>
        /// 1-3万公里不满意度
        /// </summary>
        /// <returns></returns>
        public IEnumerable<dynamic> GetUnSatisfaction1To3()
        {
            List<DataAnalysisModel> list = new List<DataAnalysisModel>();

            DataAnalysisModel da1 = new DataAnalysisModel();
            da1.key = "比亚迪";
            da1.value = "27.2%";
            DataAnalysisModel da2 = new DataAnalysisModel();
            da2.key = "现代";
            da2.value = "26.5%";
            DataAnalysisModel da3 = new DataAnalysisModel();
            da3.key = "本田";
            da3.value = "25.7%";
            DataAnalysisModel da4 = new DataAnalysisModel();
            da4.key = "日产";
            da4.value = "24.7%";
            DataAnalysisModel da5 = new DataAnalysisModel();
            da5.key = "丰田";
            da5.value = "24.2%";

            list.Add(da1);
            list.Add(da2);
            list.Add(da3);
            list.Add(da4);
            list.Add(da5);

            return list;
        }

        #endregion

        #region 品牌热度排行

        /// <summary>
        /// 月度品牌热搜前10
        /// </summary>
        /// <returns></returns>
        public IEnumerable<dynamic> GetHotBrandTop10()
        {
            List<DataAnalysisModel> list = new List<DataAnalysisModel>();

            DataAnalysisModel da1 = new DataAnalysisModel();
            da1.key = "大众";
            da1.value = "18%";
            DataAnalysisModel da2 = new DataAnalysisModel();
            da2.key = "丰田";
            da2.value = "14%";
            DataAnalysisModel da3 = new DataAnalysisModel();
            da3.key = "奥迪";
            da3.value = "12%";
            DataAnalysisModel da4 = new DataAnalysisModel();
            da4.key = "宝马";
            da4.value = "12%";
            DataAnalysisModel da5 = new DataAnalysisModel();
            da5.key = "本田";
            da5.value = "11%";
            DataAnalysisModel da6 = new DataAnalysisModel();
            da6.key = "奔驰";
            da6.value = "8%";
            DataAnalysisModel da7 = new DataAnalysisModel();
            da7.key = "现代";
            da7.value = "8%";
            DataAnalysisModel da8 = new DataAnalysisModel();
            da8.key = "别克";
            da8.value = "7%";
            DataAnalysisModel da9 = new DataAnalysisModel();
            da9.key = "日产";
            da9.value = "6%";
            DataAnalysisModel da10 = new DataAnalysisModel();
            da10.key = "未知";
            da10.value = "4%";

            list.Add(da1);
            list.Add(da2);
            list.Add(da3);
            list.Add(da4);
            list.Add(da5);
            list.Add(da6);
            list.Add(da7);
            list.Add(da8);
            list.Add(da9);
            list.Add(da10);

            return list;
        }

        #endregion

        #region 交易量

        /// <summary>
        /// 二手车交易量全国占比排行前10省份
        /// </summary>
        /// <returns></returns>
        public IEnumerable<dynamic> GetUsedCarTradeTop10()
        {
            List<DataAnalysisModel> list = new List<DataAnalysisModel>();

            DataAnalysisModel da1 = new DataAnalysisModel();
            da1.key = "广东";
            da1.value = "19.56";
            da1.value2 = "10.08";
            DataAnalysisModel da2 = new DataAnalysisModel();
            da2.key = "江苏";
            da2.value = "0.65";
            da2.value2 = "8.22";
            DataAnalysisModel da3 = new DataAnalysisModel();
            da3.key = "四川";
            da3.value = "13.09";
            da3.value2 = "7.79";
            DataAnalysisModel da4 = new DataAnalysisModel();
            da4.key = "浙江";
            da4.value = "5.25";
            da4.value2 = "7.41";
            DataAnalysisModel da5 = new DataAnalysisModel();
            da5.key = "北京";
            da5.value = "9.24";
            da5.value2 = "7.36";
            DataAnalysisModel da6 = new DataAnalysisModel();
            da6.key = "山东";
            da6.value = "7.07";
            da6.value2 = "6.31";
            DataAnalysisModel da7 = new DataAnalysisModel();
            da7.key = "河北";
            da7.value = "2.07";
            da7.value2 = "5.47";
            DataAnalysisModel da8 = new DataAnalysisModel();
            da8.key = "上海";
            da8.value = "6.22";
            da8.value2 = "4.36";
            DataAnalysisModel da9 = new DataAnalysisModel();
            da9.key = "辽宁";
            da9.value = "1.39";
            da9.value2 = "4.24";
            DataAnalysisModel da10 = new DataAnalysisModel();
            da10.key = "河南";
            da10.value = "7.54";
            da10.value2 = "3.88";

            list.Add(da1);
            list.Add(da2);
            list.Add(da3);
            list.Add(da4);
            list.Add(da5);
            list.Add(da6);
            list.Add(da7);
            list.Add(da8);
            list.Add(da9);
            list.Add(da10);

            return list;
        }
        
        /// <summary>
        /// 二手车交易量全国占比排行倒数8省份
        /// </summary>
        /// <returns></returns>
        public IEnumerable<dynamic> GetUsedCarTradeLaset8()
        {
            List<DataAnalysisModel> list = new List<DataAnalysisModel>();

            DataAnalysisModel da1 = new DataAnalysisModel();
            da1.key = "新疆";
            da1.value = "0.32";
            da1.value2 = "1.39";
            DataAnalysisModel da2 = new DataAnalysisModel();
            da2.key = "天津";
            da2.value = "1.26";
            da2.value2 = "1.31";
            DataAnalysisModel da3 = new DataAnalysisModel();
            da3.key = "湖南";
            da3.value = "1.22";
            da3.value2 = "1.22";
            DataAnalysisModel da4 = new DataAnalysisModel();
            da4.key = "宁夏";
            da4.value = "0.41";
            da4.value2 = "0.81";
            DataAnalysisModel da5 = new DataAnalysisModel();
            da5.key = "甘肃";
            da5.value = "0.4";
            da5.value2 = "0.81";
            DataAnalysisModel da6 = new DataAnalysisModel();
            da6.key = "海南";
            da6.value = "0.4";
            da6.value2 = "0.4";
            DataAnalysisModel da7 = new DataAnalysisModel();
            da7.key = "青海";
            da7.value = "0.59";
            da7.value2 = "0.17";
            DataAnalysisModel da8 = new DataAnalysisModel();
            da8.key = "西藏";
            da8.value = "0.12";
            da8.value2 = "0.12";

            list.Add(da1);
            list.Add(da2);
            list.Add(da3);
            list.Add(da4);
            list.Add(da5);
            list.Add(da6);
            list.Add(da7);
            list.Add(da8);

            return list;
        }

        /// <summary>
        /// 二手车近几年交易量
        /// </summary>
        /// <returns></returns>
        public IEnumerable<dynamic> GetUsedCarTradeRecentYears()
        {
            List<DataAnalysisModel> list = new List<DataAnalysisModel>();

            DataAnalysisModel da1 = new DataAnalysisModel();
            da1.key = "2000";
            da1.value = "25";
            DataAnalysisModel da2 = new DataAnalysisModel();
            da2.key = "2001";
            da2.value = "37";
            DataAnalysisModel da3 = new DataAnalysisModel();
            da3.key = "2002";
            da3.value = "71";
            DataAnalysisModel da4 = new DataAnalysisModel();
            da4.key = "2003";
            da4.value = "88";
            DataAnalysisModel da5 = new DataAnalysisModel();
            da5.key = "2004";
            da5.value = "134";
            DataAnalysisModel da6 = new DataAnalysisModel();
            da6.key = "2005";
            da6.value = "145";
            DataAnalysisModel da7 = new DataAnalysisModel();
            da7.key = "2006";
            da7.value = "191";
            DataAnalysisModel da8 = new DataAnalysisModel();
            da8.key = "2007";
            da8.value = "266";
            DataAnalysisModel da9 = new DataAnalysisModel();
            da9.key = "2008";
            da9.value = "274";
            DataAnalysisModel da10 = new DataAnalysisModel();
            da10.key = "2009";
            da10.value = "334";
            DataAnalysisModel da11 = new DataAnalysisModel();
            da11.key = "2010";
            da11.value = "385";
            DataAnalysisModel da12 = new DataAnalysisModel();
            da12.key = "2011";
            da12.value = "433";
            DataAnalysisModel da13 = new DataAnalysisModel();
            da13.key = "2012";
            da13.value = "479";
            DataAnalysisModel da14 = new DataAnalysisModel();
            da14.key = "2013";
            da14.value = "520";
            DataAnalysisModel da15 = new DataAnalysisModel();
            da15.key = "2014";
            da15.value = "605";

            list.Add(da1);
            list.Add(da2);
            list.Add(da3);
            list.Add(da4);
            list.Add(da5);
            list.Add(da6);
            list.Add(da7);
            list.Add(da8);
            list.Add(da9);
            list.Add(da10);
            list.Add(da11);
            list.Add(da12);
            list.Add(da13);
            list.Add(da14);
            list.Add(da15);

            return list;
        }

        #endregion
    }
}
