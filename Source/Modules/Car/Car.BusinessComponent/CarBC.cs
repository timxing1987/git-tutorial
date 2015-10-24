using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCN.Modules.Car.BusinessEntity;
using CCN.Modules.Car.DataAccess;
using Cedar.AuditTrail.Interception;
using Cedar.Framework.Common.BaseClasses;
using Cedar.Framework.Common.Server.BaseClasses;
using Cedar.Core.IoC;
using Newtonsoft.Json.Linq;

namespace CCN.Modules.Car.BusinessComponent
{
    /// <summary>
    /// </summary>
    public class CarBC : BusinessComponentBase<CarDataAccess>
    {
        /// <summary>
        /// </summary>
        /// <param name="da"></param>
        public CarBC(CarDataAccess da)
            : base(da)
        {
            
        }

        #region 车辆

        /// <summary>
        /// 获取车辆列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        public BasePageList<CarInfoListViewModel> GetCarPageList(CarQueryModel query)
        {
            return DataAccess.GetCarPageList(query);
        }

        /// <summary>
        /// 获取车辆详情
        /// </summary>
        /// <param name="id">车辆id</param>
        /// <returns></returns>
        public JResult GetCarInfoById(string id)
        {
            var jResult = new JResult();
            var carInfo = DataAccess.GetCarInfoById(id);
            if (carInfo == null)
            {
                jResult.errcode = 400;
                jResult.errmsg = "";
                return jResult;
            }
            
            jResult.errcode = 0;
            jResult.errmsg = carInfo;
            return jResult;
        }

        /// <summary>
        /// 获取车辆详情
        /// </summary>
        /// <param name="id">车辆id</param>
        /// <returns></returns>
        public JResult GetCarViewById(string id)
        {
            var jResult = new JResult();
            var carInfo = DataAccess.GetCarViewById(id);
            if (carInfo == null)
            {
                jResult.errcode = 400;
                jResult.errmsg = "";
                return jResult;
            }

            jResult.errcode = 0;
            jResult.errmsg = carInfo;
            return jResult;
        }

        /// <summary>
        /// 车辆估值
        /// </summary>
        /// <param name="id">车辆id</param>
        /// <returns></returns>
        public JResult GetCarEvaluateById(string id)
        {
            var jResult = new JResult();
            var carInfo = DataAccess.GetCarInfoById(id);

            if (carInfo == null)
            {
                jResult.errcode = 400;
                jResult.errmsg = "车辆信息不存在";
                return jResult;
            }
            
            //车辆评估信息已经存在
            if(!string.IsNullOrWhiteSpace(carInfo.estimateprice))
            {
                jResult.errcode = 0;
                jResult.errmsg = carInfo.estimateprice;
                return jResult;
            }

            var paramList = new Dictionary<string, string>
            {
                {"modelId", carInfo.model_id.ToString()},
                {"regDate", Convert.ToDateTime(carInfo.register_date).ToString("yyyy-MM")},
                {"mile", carInfo.mileage.ToString()},
                {"zone", carInfo.cityid.ToString()}
            };

            var juhe = new JuheUtility();
            var result = juhe.GetUsedCarPrice(paramList);

            if (string.IsNullOrWhiteSpace(result))
            {
                jResult.errcode = 400;
                jResult.errmsg = "没有车辆评估信息";
                return jResult;
            }

            try
            {
                JObject jobj = JObject.Parse(result);
                result = Math.Round(Convert.ToDouble(jobj["eval_price"]), 2).ToString();
            }
            catch (Exception)
            {
                jResult.errcode = 400;
                jResult.errmsg = "没有车辆评估信息";
                return jResult;
            }
            

            //保存评估信息
            Task.Factory.StartNew(() =>
            {
                DataAccess.SaveCarEvaluateInfo(id, result);
            });

            jResult.errcode = 0;
            jResult.errmsg = result;
            return jResult;
        }

        /// <summary>
        /// 获取本月本车型成交数量
        /// </summary>
        /// <param name="modelid">车型id</param>
        /// <returns></returns>
        public JResult GetCarSales(string modelid)
        {
            var num = DataAccess.GetCarSales(modelid);
            return new JResult
            {
                errcode = 0,
                errmsg = num
            };
        }

        /// <summary>
        /// 添加车辆
        /// </summary>
        /// <param name="model">车辆信息</param>
        /// <returns></returns>
        public JResult AddCar(CarInfoModel model)
        {
            model.Innerid = Guid.NewGuid().ToString();
            model.status = 1;
            model.createdtime = DateTime.Now;
            model.modifiedtime = null;
            var result = DataAccess.AddCar(model);
            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = ""
            };
        }

        /// <summary>
        /// 修改车辆
        /// </summary>
        /// <param name="model">车辆信息</param>
        /// <returns></returns>
        public JResult UpdateCar(CarInfoModel model)
        {
            model.createdtime = null;
            model.status = null;
            model.custid = null;

            model.modifiedtime = DateTime.Now;
            var result = DataAccess.UpdateCar(model);
            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = ""
            };
        }

        /// <summary>
        /// 删除车辆
        /// </summary>
        /// <param name="model">删除成交model</param>
        /// <returns>1.操作成功</returns>
        public JResult DeleteCar(CarInfoModel model)
        {
            var result = DataAccess.DeleteCar(model);
            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = result > 0 ? "操作成功" : "操作失败"
            };
        }

        /// <summary>
        /// 车辆成交
        /// </summary>
        /// <param name="model">车辆成交model</param>
        /// <returns>1.操作成功</returns>
        public JResult DealCar(CarInfoModel model)
        {
            if (string.IsNullOrWhiteSpace(model?.Innerid))
            {
                return new JResult
                {
                    errcode = 401,
                    errmsg = "参数不正确"
                };
            }
            if (model.sold_time == null)
            {
                model.sold_time = DateTime.Now; //成交时间    
            }            
            var result = DataAccess.DealCar(model);
            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = result > 0 ? "操作成功" : "操作失败"
            };
        }


        /// <summary>
        /// 删除车辆(物理删除，暂不用)
        /// </summary>
        /// <param name="id">车辆id</param>
        /// <returns>1.删除成功</returns>
        public JResult DeleteCar(string id)
        {
            var result = DataAccess.DeleteCar(id);
            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = ""
            };
        }

        /// <summary>
        /// 车辆状态更新
        /// </summary>
        /// <param name="carid">车辆id</param>
        /// <param name="status"></param>
        /// <returns>1.操作成功</returns>
        public JResult UpdateCarStatus(string carid, int status)
        {
            var result = DataAccess.UpdateCarStatus(carid, status);
            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = ""
            };
        }

        /// <summary>
        /// 车辆分享
        /// </summary>
        /// <param name="id">车辆id</param>
        /// <returns>1.操作成功</returns>
        public JResult ShareCar(string id)
        {
            var result = DataAccess.ShareCar(id);
            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = ""
            };
        }

        /// <summary>
        /// 累计车辆查看次数
        /// </summary>
        /// <param name="id">车辆id</param>
        /// <returns>1.累计成功</returns>
        public JResult UpSeeCount(string id)
        {
            var result = DataAccess.UpSeeCount(id);
            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = ""
            };
        }

        /// <summary>
        /// 累计点赞次数
        /// </summary>
        /// <param name="id">车辆id</param>
        /// <returns>1.累计成功</returns>
        public JResult UpPraiseCount(string id)
        {
            var result = DataAccess.UpPraiseCount(id);
            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = ""
            };
        }

        /// <summary>
        /// 累计点赞次数
        /// </summary>
        /// <param name="id">车辆id</param>
        /// <param name="content">评论内容</param>
        /// <returns>1.累计成功</returns>
        public JResult CommentCar(string id, string content)
        {
            var result = DataAccess.CommentCar(id, content);
            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = ""
            };
        }

        /// <summary>
        /// 审核车辆
        /// </summary>
        /// <param name="id">车辆id</param>
        /// <param name="status">审核状态</param>
        /// <returns>1.操作成功</returns>
        public int AuditCar(string id, int status)
        {
            return DataAccess.AuditCar(id, status);
        }

        /// <summary>
        /// 核销车辆
        /// </summary>
        /// <param name="id">车辆id</param>
        /// <returns>1.操作成功</returns>
        public int CancelCar(string id)
        {
            return DataAccess.CancelCar(id);
        }

        /// <summary>
        /// 车辆归档
        /// </summary>
        /// <param name="id">车辆id</param>
        /// <returns>1.操作成功</returns>
        public int KeepCar(string id)
        {
            return DataAccess.KeepCar(id);
        }

        #endregion

        #region 车辆图片

        /// <summary>
        /// 添加车辆图片
        /// </summary>
        /// <param name="model">车辆图片信息</param>
        /// <returns></returns>
        [AuditTrailCallHandler("CarBC.AddCarPicture")]
        public JResult AddCarPicture(CarPictureModel model)
        {
            if (model == null)
            {
                return new JResult
                {
                    errcode = 401,
                    errmsg = "参数不完整"
                };
            }
            model.Innerid = Guid.NewGuid().ToString();

            //初始化排序
            var max = DataAccess.GetCarPictureMaxSort(model.Carid);

            model.Sort = max + 1;

            var result = DataAccess.AddCarPicture(model);

            if (result > 0)
            {
                DataAccess.SetCarCover(model.Carid, model.Path);
            }

            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = ""
            };
        }

        /// <summary>
        /// 删除车辆图片
        /// </summary>
        /// <param name="innerid">车辆图片id</param>
        /// <returns></returns>
        [AuditTrailCallHandler("CarBC.DeleteCarPicture")]
        public JResult DeleteCarPicture(string innerid)
        {
            var result = DataAccess.DeleteCarPicture(innerid);
            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = ""
            };
        }

        /// <summary>
        /// 获取车辆已有图片
        /// </summary>
        /// <param name="carid">车辆id</param>
        /// <returns></returns>
        [AuditTrailCallHandler("CarBC.GetCarPictureByCarid")]
        public JResult GetCarPictureByCarid(string carid)
        {
            var model = DataAccess.GetCarPictureByCarid(carid);
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

        /// <summary>
        /// 图片调换位置
        /// </summary>
        /// <param name="listPicture">车辆图片列表</param>
        /// <returns></returns>
        [AuditTrailCallHandler("CarDataAccess.ExchangePictureSort")]
        public JResult ExchangePictureSort(List<CarPictureModel> listPicture)
        {
            //如果图片数量小与2个，或要交换的排序为0，或者两个要交换的排序相同 的话就不能调整排序
            if (listPicture.Count() < 2 || listPicture[0].Sort == 0 || listPicture[1].Sort == 0 || listPicture[0].Sort == listPicture[1].Sort)
            {
                return new JResult
                {
                    errcode = 401,
                    errmsg = "参数不完整"
                };
            }

            var result = DataAccess.ExchangePictureSort(listPicture);
            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = ""
            };
        }

        #endregion
    }
}
