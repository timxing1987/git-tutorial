using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CCN.Modules.Base.BusinessEntity;
using Cedar.Core.Data;
using Cedar.Core.EntLib.Data;
using Cedar.Framework.Common.Server.BaseClasses;

namespace CCN.Modules.Base.DataAccess
{
    /// <summary>
    /// 基础模块
    /// </summary>
    public class BaseDA  : DataAccessBase
    {
        //private readonly Database Helper;

        /// <summary>
        /// 
        /// </summary>
        public BaseDA() 
        {
            //Helper = new DatabaseWrapperFactory().GetDatabase("mysqldb");
        }

        #region Code

        /// <summary>
        /// 获取代码值列表
        /// </summary>
        /// <param name="typekey">代码类型key</param>
        /// <returns></returns>
        public IEnumerable<BaseCodeSelectModel> GetCodeByTypeKey(string typekey)
        {
            const string sql = "select codevalue, codename from base_code where isenabled=1 and typekey=@typekey order by sort asc;";
            var list = Helper.Query<BaseCodeSelectModel>(sql, new { typekey });
            return list;
        }

        #endregion

        #region 验证码

        /// <summary>
        /// 验证码保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveVerification(BaseVerification model)
        {
            int result;
            const string sql = "insert into base_verification (innerid, target, vcode, valid, createdtime, ttype,utype,content, result) values (uuid(), @target, @vcode, @valid, @createdtime, @ttype, @utype,@content, @result);";
            try
            {
                result = Helper.Execute(sql, model);
            }
            catch (Exception ex)
            {
                result = 0;
            }
            
            return result;
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="target"></param>
        /// <param name="utype">用处类型[1注册,2登录,3,其他]</param>
        /// <returns></returns>
        public BaseVerification GetVerification(string target,int utype)
        {
            const string sql = "select innerid, target, vcode, valid, createdtime, ttype, utype,content, result from base_verification where target=@target and utype=@utype order by createdtime desc limit 1;";
            var m = Helper.Query<BaseVerification>(sql, new {target, utype }).FirstOrDefault();
            return m;
        }

        #endregion

        #region 区域

        /// <summary>
        /// 获取省份
        /// </summary>
        /// <param name="initial">首字母</param>
        /// <returns></returns>
        public IEnumerable<BaseProvince> GetProvList(string initial)
        {
            string where = " isenabled=1";
            if (!string.IsNullOrWhiteSpace(initial))
            {
                where += $" and initial='{initial.ToUpper()}'";
            }
            var sql = $"select innerid, provname, initial, isenabled, remark from base_province where {where} order by initial asc,provname asc";
            var provList = Helper.Query<BaseProvince>(sql);
            return provList;
        }

        /// <summary>
        /// 根据省份获取城市
        /// </summary>
        /// <param name="provId">省份ID</param>
        /// <param name="initial">首字母</param>
        /// <returns></returns>
        public IEnumerable<BaseCity> GetCityList(int provId,string initial)
        {
            string where = " isenabled=1 and provid=" + provId;
            if (!string.IsNullOrWhiteSpace(initial))
            {
                where += $" and initial='{initial.ToUpper()}'";
            }
            var sql = $"select innerid, cityname, initial, provid, isenabled, remark from base_city where {where} order by initial asc,cityname asc";
            var cityList = Helper.Query<BaseCity>(sql);
            return cityList;
        }

        #endregion

        #region 品牌/车系/车型

        /// <summary>
        /// 获取品牌
        /// </summary>
        /// <param name="initial">首字母</param>
        /// <returns></returns>
        public IEnumerable<BaseCarBrandModel> GetCarBrand(string initial)
        {
            var where = " isenabled=1";
            if (!string.IsNullOrWhiteSpace(initial))
            {
                where += $" and initial='{initial.ToUpper()}'";
            }
            var sql = $"select innerid, brandname, initial, isenabled, remark, logurl from base_carbrand where {where} order by initial asc,brandname asc";
            var brandList = Helper.Query<BaseCarBrandModel>(sql);
            return brandList;
        }

        /// <summary>
        /// 根据品牌id获取车系
        /// </summary>
        /// <param name="brandId">品牌id</param>
        /// <returns></returns>
        public IEnumerable<BaseCarSeriesModel> GetCarSeries(int brandId)
        {
            var sql = $"select innerid, seriesname, seriesgroupname, brandid, isenabled, remark from base_carseries where isenabled=1 and brandid={brandId}";
            var seriesList = Helper.Query<BaseCarSeriesModel>(sql);
            return seriesList;
        }

        /// <summary>
        /// 根据车系ID获取车型
        /// </summary>
        /// <param name="seriesId">车系id</param>
        /// <returns></returns>
        public IEnumerable<BaseCarModelModel> GetCarModel(int seriesId)
        {
            var sql = $"select innerid, modelname, modelprice, modelyear, minregyear, maxregyear, liter, geartype, dischargestandard, seriesid, isenabled, remark from base_carmodel where isenabled=1 and seriesid={seriesId}";
            var modelList = Helper.Query<BaseCarModelModel>(sql);
            return modelList;
        }

        /// <summary>
        /// 根据ID获取车型信息
        /// </summary>
        /// <param name="innerid">id</param>
        /// <returns></returns>
        public BaseCarModelModel GetCarModelById(int innerid)
        {
            var sql = $"select innerid, modelname, modelprice, modelyear, minregyear, maxregyear, liter, geartype, dischargestandard, seriesid, isenabled, remark from base_carmodel where innerid={innerid}";
            var model = Helper.Query<BaseCarModelModel>(sql).FirstOrDefault();
            return model;
        }

        #endregion
    }
}
