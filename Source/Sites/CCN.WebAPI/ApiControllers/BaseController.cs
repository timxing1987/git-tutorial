using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using CCN.Modules.Base.BusinessEntity;
using CCN.Modules.Base.Interface;
using Cedar.Core.ApplicationContexts;
using Cedar.Core.IoC;
using Cedar.Framework.Common.BaseClasses;
using Cedar.Framework.Common.Client.DelegationHandler;

namespace CCN.WebAPI.ApiControllers
{
    /// <summary>
    /// 基础模块
    /// </summary>
    [RoutePrefix("api/Base")]
    public class BaseController : ApiController
    {
        private readonly IBaseManagementService _baseservice;

        public BaseController()
        {
            _baseservice = ServiceLocatorFactory.GetServiceLocator().GetService<IBaseManagementService>();
        }

        #region Code

        /// <summary>
        /// 获取代码值列表
        /// </summary>
        /// <param name="typekey">代码类型key</param>
        /// <returns></returns>
        [Route("GetCodeByTypeKey")]
        [HttpGet]
        public JResult GetCodeByTypeKey(string typekey)
        {
            return _baseservice.GetCodeByTypeKey(typekey);
        }

        #endregion

        #region 验证码

        /// <summary>
        /// 手机获取验证码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("SendVerification")]
        [HttpPost]
        public JResult SendVerification([FromBody] BaseVerification model)
        {
            return _baseservice.SendVerification(model);
        }

        /// <summary>
        /// 检查验证码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("CheckVerification")]
        [HttpPost]
        public JResult CheckVerification([FromBody] BaseVerification model)
        {
            return _baseservice.CheckVerification(model.Target, model.Vcode, model.UType);
        }

        #endregion

        #region 区域

        /// <summary>
        /// 获取省份
        /// </summary>
        /// <param name="initial"></param>
        /// <returns></returns>
        [Route("GetProvList")]
        [HttpGet]
        public JResult GetProvList(string initial = null)
        {
            var list = _baseservice.GetProvList(initial);
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
                errcode = 401,
                errmsg = "No Data"
            };
        }

        /// <summary>
        /// 根据省份id获取城市
        /// </summary>
        /// <param name="provid">省份id</param>
        /// <param name="initial">首字母</param>
        /// <returns></returns>
        [Route("GetCityList")]
        [HttpGet]
        public JResult GetCityList(int provid, string initial = null)
        {
            var list = _baseservice.GetCityList(provid, initial);
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
                errcode = 401,
                errmsg = "No Data"
            };
        }

        /// <summary>
        /// 获取省份
        /// </summary>
        /// <param name="initial"></param>
        /// <returns></returns>
        [Route("GetProvListEx")]
        [HttpGet]
        public JResult GetProvListEx(string initial = null)
        {
            return _baseservice.GetProvListEx(initial);
        }

        #endregion

        #region 品牌/车系/车型

        /// <summary>
        /// 获取品牌
        /// </summary>
        /// <param name="initial">首字母</param>
        /// <returns></returns>
        [Route("GetCarBrand")]
        [HttpGet]
        public JResult GetCarBrand(string initial = null)
        {
            var list = _baseservice.GetCarBrand(initial);
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
                errcode = 401,
                errmsg = "No Data"
            };
        }

        /// <summary>
        /// 根据品牌id获取车系
        /// </summary>
        /// <param name="brandId">品牌id</param>
        /// <returns></returns>
        [Route("GetCarSeries")]
        [HttpGet]
        public JResult GetCarSeries(int brandId)
        {
            var list = _baseservice.GetCarSeries(brandId);
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
                errcode = 401,
                errmsg = "No Data"
            };
        }

        /// <summary>
        /// 根据车系ID获取车型
        /// </summary>
        /// <param name="seriesId">车系id</param>
        /// <returns></returns>
        [Route("GetCarModel")]
        [HttpGet]
        public JResult GetCarModel(int seriesId)
        {
            var list = _baseservice.GetCarModel(seriesId);
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
                errcode = 401,
                errmsg = "No Data"
            };
        }

        /// <summary>
        /// 根据ID获取车型信息
        /// </summary>
        /// <param name="innerid">id</param>
        /// <returns></returns>
        [Route("GetCarModelById")]
        [HttpGet]
        public JResult GetCarModelById(int innerid)
        {
            return _baseservice.GetCarModelById(innerid);
        }

        #endregion

        /// <summary>
        ///     上传文件
        /// </summary>
        /// <returns>图片主键</returns>
        [HttpPost]
        [Route("FileUpload")]
        public string FileUpload()
        {
            var files = HttpContext.Current.Request.Files;
            if (files.Count == 0)
            {
                return "0";
            }

            var filename = string.Concat("card_logo_", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            var filepath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "TempFile\\", filename, ".jpg");

            try
            {
                files[0].SaveAs(filepath);

                //上传图片到七牛云
                var qinniu = new QiniuUtility();
                var qrcodeKey = qinniu.PutFile(filepath, "", filename);

                //删除本地临时文件
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }

                return qrcodeKey;
            }
            catch (Exception ex)
            {
                return "-2";
            }
        }
    }
}
