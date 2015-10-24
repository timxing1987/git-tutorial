using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCN.Modules.Base.BusinessEntity;
using CCN.Modules.Base.DataAccess;
using Cedar.Framework.Common.BaseClasses;
using Cedar.Framework.Common.Server.BaseClasses;
using Cedar.Core.IoC;
using Cedar.Foundation.SMS.Common;

namespace CCN.Modules.Base.BusinessComponent
{
    /// <summary>
    /// 基础模块
    /// </summary>
    public class BaseBC : BusinessComponentBase<BaseDA>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="da"></param>
        public BaseBC(BaseDA da):base(da)
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
            var list = DataAccess.GetCodeByTypeKey(typekey);
            if (list.Any())
            {
                return new JResult
                {
                    errcode = 0,
                    errmsg = list
                };
            }
            return new JResult
            {
                errcode = 400,
                errmsg = ""
            };
        }

        #endregion

        #region 验证码

        /// <summary>
        /// 组织验证码内容
        /// </summary>
        /// <param name="utype"></param>
        /// <param name="vcode"></param>
        /// <param name="valid"></param>
        /// <returns></returns>
        private static string GetVerifiByType(int utype ,string vcode,int valid)
        {
            //有效期从秒转换成分
            valid = valid / 60;
            string content;
            switch (utype)
            {
                
                case 1:
                    content = $"{vcode}（平台注册验证码，{valid}分钟内有效）";
                    break;
                case 2:
                    content = $"{vcode}（平台登录验证码，{valid}分钟内有效）";
                    break;
                case 3:
                    content = $"{vcode}（平台找回密码的验证码，{valid}分钟内有效）";
                    break;
                case 0:
                    content = $"{vcode}（平台验证码，{valid}分钟内有效）";
                    break;
                default:
                    content = $"{vcode}（平台验证码，{valid}分钟内有效）";
                    break;
            }

            return content;
        }

        /// <summary>
        /// 会员注册获取验证码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JResult SendVerification(BaseVerification model)
        {
            var jResult = new JResult();
            model.Createdtime = DateTime.Now;
            model.Vcode = RandomUtility.GetRandom(model.Length);
            model.Content = GetVerifiByType(model.UType, model.Vcode, model.Valid);

            var saveRes = DataAccess.SaveVerification(model);
            if (saveRes == 0)
            {
                //保存失败
                jResult.errcode = 401;
                jResult.errmsg = "发送验证码失败";
                return jResult;
            }

            #region 发送验证码
            Task.Factory.StartNew(() =>
            {
                switch (model.TType)
                {
                    case 1:
                        //发送手机
                        var sms = new SMSMSG();
                        var result = sms.PostSms(model.Target, model.Content);
                        if (result.errcode != "0")
                        {
                            model.Result = 0;
                        }
                        break;
                    case 2:
                        //发送邮件
                        break;
                }
            });
            #endregion

            jResult.errcode = 0;
            jResult.errmsg = "发送验证码成功";
            return jResult;
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="target"></param>
        /// <param name="vcode">验证码</param>
        /// <param name="utype">用处类型[1注册,2登录,3,其他]</param>
        /// <returns>返回结果。1.正确，0不正确,-1.验证码过期</returns>
        public JResult CheckVerification(string target,string vcode, int utype)
        {
            var jResult = new JResult();
            var m = DataAccess.GetVerification(target,utype);
            //验证码不正确
            if (m == null || !m.Vcode.Equals(vcode))
            {
                jResult.errcode = 400;
                jResult.errmsg = "验证码错误";
                return jResult;
            }
            //
            if (m.Createdtime.AddSeconds(m.Valid) < DateTime.Now)
            {
                jResult.errcode = 401;
                jResult.errmsg = "验证码过期";
                return jResult;
            }
            jResult.errcode = 0;
            jResult.errmsg = "验证码正确";
            return jResult;
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
            return DataAccess.GetProvList(initial);
        }

        /// <summary>
        /// 获取城市
        /// </summary>
        /// <param name="provId">省份id</param>
        /// <param name="initial">首字母</param>
        /// <returns></returns>
        public IEnumerable<BaseCity> GetCityList(int provId, string initial)
        {
            return DataAccess.GetCityList(provId,initial);
        }

        /// <summary>
        /// 获取省份（扩展方法，根据首字母分类）
        /// </summary>
        /// <param name="initial">首字母</param>
        /// <returns></returns>
        public JResult GetProvListEx(string initial)
        {
            var list = DataAccess.GetProvList(initial).ToList();
            if (!list.Any())
            {
                return new JResult
                {
                    errcode = 400,
                    errmsg = ""
                };
            }
            
            var listProv = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            var listResult = new List<JsonGroupByModel>();
            foreach (var item in listProv)
            {
                var il = list.Where(x => x.Initial == item).ToList();
                if (!il.Any())
                {
                    continue;
                }
                var m = new JsonGroupByModel
                {
                    Initial = item,
                    ProvList = il
                };
                listResult.Add(m);
            }

            return new JResult
            {
                errcode = 0,
                errmsg = listResult
            };
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
            return DataAccess.GetCarBrand(initial);
        }

        /// <summary>
        /// 根据品牌id获取车系
        /// </summary>
        /// <param name="brandId">品牌id</param>
        /// <returns></returns>
        public IEnumerable<BaseCarSeriesModel> GetCarSeries(int brandId)
        {
            return DataAccess.GetCarSeries(brandId);
        }

        /// <summary>
        /// 根据车系ID获取车型
        /// </summary>
        /// <param name="seriesId">车系id</param>
        /// <returns></returns>
        public IEnumerable<BaseCarModelModel> GetCarModel(int seriesId)
        {
            return DataAccess.GetCarModel(seriesId);
        }

        /// <summary>
        /// 根据ID获取车型信息
        /// </summary>
        /// <param name="innerid">id</param>
        /// <returns></returns>
        public JResult GetCarModelById(int innerid)
        {
            var model = DataAccess.GetCarModelById(innerid);
            if (model == null)
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
                errmsg = model
            };
        }

        #endregion
    }
}
