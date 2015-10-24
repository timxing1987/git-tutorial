using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCN.Modules.Base.BusinessComponent;
using CCN.Modules.Base.BusinessEntity;
using CCN.Modules.Base.Interface;
using Cedar.AuditTrail.Interception;
using Cedar.Framework.Common.BaseClasses;
using Cedar.Framework.Common.Server.BaseClasses;

namespace CCN.Modules.Base.BusinessService
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseManagementService: ServiceBase<BaseBC>, IBaseManagementService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bc"></param>
        public BaseManagementService(BaseBC bc) :base(bc)
        {

        }

        #region Code

        /// <summary>
        /// 获取代码值列表
        /// </summary>
        /// <param name="typekey">代码类型key</param>
        /// <returns></returns>
        public JResult GetCodeByTypeKey(string typekey)
        {
            return BusinessComponent.GetCodeByTypeKey(typekey);
        }

        #endregion

        #region 验证码

        /// <summary>
        /// 会员注册获取验证码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JResult SendVerification(BaseVerification model)
        {
            return BusinessComponent.SendVerification(model);
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="target"></param>
        /// <param name="vcode">验证码</param>
        /// <param name="utype">用处类型[1注册,2登录,3,其他]</param>
        /// <returns>返回结果。1.正确，0不正确</returns>
        public JResult CheckVerification(string target, string vcode,int utype)
        {
            return BusinessComponent.CheckVerification(target, vcode,utype);
        }

        /// <summary>
        /// 获取省份
        /// </summary>
        /// <param name="initial">首字母</param>
        /// <returns></returns>
        public JResult GetProvListEx(string initial)
        {
            return BusinessComponent.GetProvListEx(initial);
        }

        #endregion

        #region 区域

        /// <summary>
        /// 获取省份
        /// </summary>
        /// <param name="initial">首字母</param>
        /// <returns></returns>
        [AuditTrailCallHandler("GetProvList")]
        public IEnumerable<BaseProvince> GetProvList(string initial)
        {
            return BusinessComponent.GetProvList(initial);
        }

        /// <summary>
        /// 获取省份
        /// </summary>
        /// <param name="provId">省份id</param>
        /// <param name="initial">首字母</param>
        /// <returns></returns>
        [AuditTrailCallHandler("GetCityList")]
        public IEnumerable<BaseCity> GetCityList(int provId, string initial)
        {
            return BusinessComponent.GetCityList(provId, initial);
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
            return BusinessComponent.GetCarBrand(initial);
        }

        /// <summary>
        /// 根据品牌id获取车系
        /// </summary>
        /// <param name="brandId">品牌id</param>
        /// <returns></returns>
        public IEnumerable<BaseCarSeriesModel> GetCarSeries(int brandId)
        {
            return BusinessComponent.GetCarSeries(brandId);
        }

        /// <summary>
        /// 根据车系ID获取车型
        /// </summary>
        /// <param name="seriesId">车系id</param>
        /// <returns></returns>
        public IEnumerable<BaseCarModelModel> GetCarModel(int seriesId)
        {
            return BusinessComponent.GetCarModel(seriesId);
        }

        /// <summary>
        /// 根据ID获取车型信息
        /// </summary>
        /// <param name="innerid">id</param>
        /// <returns></returns>
        public JResult GetCarModelById(int innerid)
        {
            return BusinessComponent.GetCarModelById(innerid);
        }

        #endregion

    }
}
