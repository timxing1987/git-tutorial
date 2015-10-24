using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CCN.Modules.Car.BusinessEntity;
using Cedar.AuditTrail.Interception;
using Cedar.Core.Data;
using Cedar.Core.EntLib.Data;
using Cedar.Framework.Common.BaseClasses;
using Cedar.Framework.Common.Server.BaseClasses;
using Dapper;

namespace CCN.Modules.Car.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class CarDataAccess : DataAccessBase
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public CarDataAccess()
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
            const string spName = "sp_common_pager";
            const string tableName = @"car_info as a 
                                    left join base_carbrand as c1 on a.brand_id=c1.innerid 
                                    left join base_carseries as c2 on a.series_id=c2.innerid 
                                    left join base_carmodel as c3 on a.model_id=c3.innerid 
                                    left join base_city as ct on a.cityid=ct.innerid ";
            const string fields = "a.innerid,a.pic_url,a.price,a.buyprice,a.dealprice,a.buytime,a.status,a.mileage,a.register_date,c1.brandname as brand_name,c2.seriesname as series_name,c3.modelname as model_name,ct.cityname";
            var orderField = string.IsNullOrWhiteSpace(query.Order) ? "a.createdtime desc" : query.Order;

            #region 查询条件
            var sqlWhere = new StringBuilder("1=1");

            sqlWhere.Append(query.status != null 
                ? $" and a.status={query.status}" 
                : " and a.status<>0");

            if (!string.IsNullOrWhiteSpace(query.custid))
            {
                sqlWhere.Append($" and a.custid='{query.custid}'");
            }

            if (!string.IsNullOrWhiteSpace(query.title))
            {
                sqlWhere.Append($" and a.title like '%{query.title}%'");
            }

            //省份
            if (query.provid != null)
            {
                sqlWhere.Append($" and a.provid={query.provid}");
            }

            //城市
            if (query.cityid != null)
            {
                sqlWhere.Append($" and a.cityid={query.cityid}");
            }

            //品牌
            if (query.brand_id != null)
            {
                sqlWhere.Append($" and a.brand_id={query.brand_id}");
            }

            //车系
            if (query.series_id != null)
            {
                sqlWhere.Append($" and a.series_id={query.series_id}");
            }

            //车型
            if (query.model_id != null)
            {
                sqlWhere.Append($" and a.model_id={query.model_id}");
            }

            //收购价大于..
            if (query.minbuyprice.HasValue)
            {
                sqlWhere.Append($" and a.buyprice>={query.minbuyprice}");
            }

            //收购价小于..
            if (query.maxbuyprice.HasValue)
            {
                sqlWhere.Append($" and a.buyprice<={query.maxbuyprice}");
            }

            if (!string.IsNullOrWhiteSpace(query.SearchField) && query.model_id == null)
            {
                //sqlWhere.Append($" and (c1.brandname like '%{query.SearchField}%' or c2.seriesname like '%{query.SearchField}%')");
                //车辆添加时会将【品牌/车系】放到该字段
                sqlWhere.Append($" and title like '%{query.SearchField}%'");
            }

            #endregion

            var model = new PagingModel(spName, tableName, fields, orderField, sqlWhere.ToString(), query.PageSize, query.PageIndex);
            var list = Helper.ExecutePaging<CarInfoListViewModel>(model, query.Echo);
            return list;
        }

        /// <summary>
        /// 获取车辆详细信息(info)
        /// </summary>
        /// <param name="id">车辆id</param>
        /// <returns></returns>
        public CarInfoModel GetCarInfoById(string id)
        {
            const string sql =
                @"select 
                a.innerid,
                a.custid,
                a.carid,
                a.title,
                a.pic_url,
                a.provid,
                a.cityid,
                a.brand_id,
                a.series_id,
                a.model_id,
                a.colorid,
                a.mileage,
                a.register_date,
                a.buytime,
                a.buyprice,
                a.price,
                a.dealprice,
                a.isproblem,
                a.remark,
                a.ckyear_date,
                a.tlci_date,
                a.audit_date,
                a.istain,
                a.sellreason,
                a.masterdesc,
                a.dealdesc,
                a.deletedesc,
                a.estimateprice,
                a.status,
                a.createdtime,
                a.modifiedtime,
                a.seller_type,
                a.post_time,
                a.audit_time,
                a.sold_time,
                a.keep_time,
                a.eval_price,
                a.next_year_eval_price,
                pr.provname,
                ct.cityname,
                cb.brandname as brand_name,
                cs.seriesname as series_name,
                cm.modelname as model_name,
                cm.liter,
                cm.geartype,
                cm.dischargestandard as dischargeName,
                bc1.codename as color
                from `car_info` as a 
                left join base_province as pr on a.provid=pr.innerid
                left join base_city as ct on a.cityid=ct.innerid
                left join base_carbrand as cb on a.brand_id=cb.innerid
                left join base_carseries as cs on a.series_id=cs.innerid
                left join base_carmodel as cm on a.model_id=cm.innerid
                left join base_code as bc1 on a.colorid=bc1.codevalue and bc1.typekey='car_color'
                where a.innerid=@innerid";
            var result = Helper.Query<CarInfoModel>(sql, new { innerid = id }).FirstOrDefault();
            return result;
        }

        /// <summary>
        /// 获取车辆详情(view)
        /// </summary>
        /// <param name="id">车辆id</param>
        /// <returns></returns>
        public CarInfoModel GetCarViewById(string id)
        {
            const string sql =
                @"select a.innerid, a.custid, a.carid, a.title, pic_url, 
                a.mileage, a.register_date, a.buytime, a.buyprice, 
                a.price, a.dealprice, a.isproblem, 
                a.remark, a.ckyear_date, a.tlci_date, a.audit_date, a.istain, 
                sellreason, a.masterdesc, a.dealdesc, a.deletedesc, a.estimateprice, 
                a.`status`, a.createdtime, a.modifiedtime, 
                a.seller_type,
                a.post_time, a.audit_time, a.sold_time, a.keep_time, 
                a.eval_price, a.next_year_eval_price, 
                pr.provname,
                ct.cityname,
                cb.brandname as brand_name,
                cs.seriesname as series_name,
                cm.modelname as model_name,
                cm.liter,
                cm.geartype,
                cm.dischargestandard as dischargeName,
                bc1.codename as color
                from `car_info` as a 
                left join base_province as pr on a.provid=pr.innerid
                left join base_city as ct on a.cityid=ct.innerid
                left join base_carbrand as cb on a.brand_id=cb.innerid
                left join base_carseries as cs on a.series_id=cs.innerid
                left join base_carmodel as cm on a.model_id=cm.innerid
                left join base_code as bc1 on a.colorid=bc1.codevalue and bc1.typekey='car_color'
                where a.innerid=@innerid";
            var result = Helper.Query<CarInfoModel>(sql, new { innerid = id }).FirstOrDefault();
            return result;
        }

        /// <summary>
        /// 添加车辆
        /// </summary>
        /// <param name="model">车辆信息</param>
        /// <returns></returns>
        public int AddCar(CarInfoModel model)
        {
            const string sql = @"INSERT INTO `car_info`
                                (`innerid`,`custid`,`carid`,`title`,`pic_url`,`provid`,`cityid`,`brand_id`,`series_id`,`model_id`,`colorid`,`mileage`,`register_date`,`buytime`,`buyprice`,`price`,`dealprice`,`isproblem`,`remark`,`ckyear_date`,`tlci_date`,`audit_date`,`istain`,`sellreason`,`masterdesc`,`dealdesc`,`deletedesc`,`estimateprice`,`status`,`createdtime`,`modifiedtime`,`seller_type`,`post_time`,`audit_time`,`sold_time`,`keep_time`,`eval_price`,`next_year_eval_price`)
                                VALUES
                                (@innerid,@custid,@carid,@title,@pic_url,@provid,@cityid,@brand_id,@series_id,@model_id,@colorid,@mileage,@register_date,@buytime,@buyprice,@price,@dealprice,@isproblem,@remark,@ckyear_date,@tlci_date,@audit_date,@istain,@sellreason,@masterdesc,@dealdesc,@deletedesc,@estimateprice,@status,@createdtime,@modifiedtime,@seller_type,@post_time,@audit_time,@sold_time,@keep_time,@eval_price,@next_year_eval_price);";
            int result;
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
        /// 添加车辆
        /// </summary>
        /// <param name="model">车辆信息</param>
        /// <returns></returns>
        public int UpdateCar(CarInfoModel model)
        {
            var sql = new StringBuilder("update `car_info` set ");
            sql.Append(Helper.CreateField(model).Trim().TrimEnd(','));
            sql.Append(" where innerid = @innerid");
            int result;
            try
            {
                result = Helper.Execute(sql.ToString(), model);
            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// 删除车辆(物理删除，暂不用)
        /// </summary>
        /// <param name="id">车辆id</param>
        /// <returns>1.删除成功</returns>
        public int DeleteCar(string id)
        {
            const string sql = @"delete from car_info where innerid`=@innerid;";
            try
            {
                Helper.Execute(sql, new { innerid = id });
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// 删除车辆
        /// </summary>
        /// <param name="model">删除成交model</param>
        /// <returns>1.操作成功</returns>
        public int DeleteCar(CarInfoModel model)
        {
            try
            {
                const string sql = "update car_info set status=0,deletedesc=@deletedesc where `innerid`=@innerid;";
                Helper.Execute(sql, new { innerid = model.Innerid, model.deletedesc });
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// 车辆成交
        /// </summary>
        /// <param name="model">车辆成交model</param>
        /// <returns>1.操作成功</returns>
        public int DealCar(CarInfoModel model)
        {
            try
            {
                const string sql = "update car_info set status=2,dealprice=@dealprice,dealdesc=@dealdesc,sold_time=@sold_time where `innerid`=@innerid;";
                Helper.Execute(sql, new { innerid = model.Innerid,model.dealprice, model.dealdesc });
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// 保存评估信息
        /// </summary>
        /// <param name="carid">车辆id</param>
        /// <param name="evaluate"></param>
        /// <returns>1.操作成功</returns>
        public int SaveCarEvaluateInfo(string carid, string evaluate)
        {
            try
            {
                const string sql = "update car_info set estimateprice=@estimateprice where `innerid`=@innerid;";
                Helper.Execute(sql, new { innerid = carid, estimateprice= evaluate });
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// 获取本月本车型成交数量
        /// </summary>
        /// <param name="modelid">车型id</param>
        /// <returns></returns>
        public int GetCarSales(string modelid)
        {
            const string sql1 = "select count(1) from car_info where model_id=@modelid and `status`=2;";
            const string sql2 = "select count(1) from car_info_bak where model_id=@modelid;";
            var num1 = Helper.ExecuteScalar<int>(sql1, new {modelid});
            var num2 = Helper.ExecuteScalar<int>(sql2, new {modelid});
            return num1 + num2;
        }

        /// <summary>
        /// 车辆状态更新
        /// </summary>
        /// <param name="carid">车辆id</param>
        /// <param name="status"></param>
        /// <returns>1.操作成功</returns>
        public int UpdateCarStatus(string carid, int status)
        {
            try
            {
                const string sql = "update car_info set status=@status where `innerid`=@innerid;";
                Helper.Execute(sql, new { innerid = carid, status });
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// 车辆分享
        /// </summary>
        /// <param name="id">车辆id</param>
        /// <returns>1.操作成功</returns>
        public int ShareCar(string id)
        {
            try
            {
                //累计分享次数
                var sql = "update car_share set sharecount=sharecount+1 where carid=@carid;";
                var resCount = Helper.Execute(sql, new {carid = id});
                if (resCount == 0)
                {
                    //表示没有子表数据
                    sql = "INSERT INTO `car_share`(`innerid`,`carid`,`sharecount`,`seecount`,`praisecount`,`commentcount`) VALUES(uuid(), @carid, 1, 0, 0, 0);";
                    Helper.Execute(sql, new { carid = id });
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 1;
        }
        
        /// <summary>
        /// 累计车辆查看次数
        /// </summary>
        /// <param name="id">车辆id</param>
        /// <returns>1.累计成功</returns>
        public int UpSeeCount(string id)
        {
            const string sql = @"update car_share set seecount=seecount+1 where carid=@carid;";
            try
            {
                Helper.Execute(sql, new { carid = id });
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// 累计点赞次数
        /// </summary>
        /// <param name="id">车辆id</param>
        /// <returns>1.累计成功</returns>
        public int UpPraiseCount(string id)
        {
            const string sql = @"update car_share set praisecount=praisecount+1 where carid=@carid;";
            try
            {
                Helper.Execute(sql, new { carid = id });
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// 累计点赞次数
        /// </summary>
        /// <param name="id">车辆id</param>
        /// <param name="content">评论内容</param>
        /// <returns>1.累计成功</returns>
        public int CommentCar(string id,string content)
        {
            const string sql = @"update car_share set commentcount=commentcount+1 where carid=@carid;";
            try
            {
                Helper.Execute(sql, new { carid = id });
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 1;
        }

        #region 车辆图片

        /// <summary>
        /// 添加车辆图片
        /// </summary>
        /// <param name="model">车辆图片信息</param>
        /// <returns></returns>
        [AuditTrailCallHandler("CarDataAccess.AddCarPicture")]
        public int AddCarPicture(CarPictureModel model)
        {
            const string sql = @"INSERT INTO car_picture
                        (innerid, carid, typeid, path, sort, createdtime)
                        VALUES
                        (@innerid, @carid, @typeid, @path, @sort, @createdtime);";
            
            try
            {
                Helper.Execute(sql, model);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 设置车辆封面图片
        /// </summary>
        /// <param name="carid">车辆id</param>
        /// <param name="imgKey">封面图片key</param>
        /// <returns></returns>
        [AuditTrailCallHandler("CarDataAccess.SetCarCover")]
        public int SetCarCover(string carid, string imgKey)
        {
            const string sql = @"update car_info set pic_url=@pic_url where innerid=@innerid;";

            try
            {
                Helper.Execute(sql, new {innerid = carid, pic_url = imgKey});
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 删除车辆图片
        /// </summary>
        /// <param name="innerid">车辆图片id</param>
        /// <returns></returns>
        [AuditTrailCallHandler("CarDataAccess.DeleteCarPicture")]
        public int DeleteCarPicture(string innerid)
        {
            const string sql = @"delete from car_picture where innerid=@innerid;";

            try
            {
                Helper.Execute(sql, new {innerid});
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 图片调换位置
        /// </summary>
        /// <param name="listPicture">车辆图片列表</param>
        /// <returns></returns>
        [AuditTrailCallHandler("CarDataAccess.ExchangePictureSort")]
        public int ExchangePictureSort(List<CarPictureModel> listPicture)
        {
            const string sql = @"update car_picture set sort=@sort where innerid=@innerid;";

            using (var conn = Helper.GetConnection())
            {
                var tran = conn.BeginTransaction();
                try
                {
                    conn.Execute(sql, new {innerid = listPicture[0].Innerid, sort = listPicture[0].Sort}, tran);
                    conn.Execute(sql, new {innerid = listPicture[1].Innerid, sort = listPicture[1].Sort}, tran);

                    tran.Commit();
                    return 1;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return 0;
                }
            }
        }

        /// <summary>
        /// 获取车辆已有图片的最大排序
        /// </summary>
        /// <param name="carid">车辆id</param>
        /// <returns></returns>
        [AuditTrailCallHandler("CarDataAccess.GetCarPictureMaxSort")]
        public int GetCarPictureMaxSort(string carid)
        {
            const string sql = @"select max(sort) from car_picture where carid=@carid;";

            try
            {
                var maxsort = Helper.ExecuteScalar<int>(sql, new { carid });
                return maxsort;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取车辆已有图片
        /// </summary>
        /// <param name="carid">车辆id</param>
        /// <returns></returns>
        [AuditTrailCallHandler("CarDataAccess.GetCarPictureByCarid")]
        public IEnumerable<CarPictureModel> GetCarPictureByCarid(string carid)
        {
            const string sql = @"select innerid, carid, typeid, path, sort, createdtime from car_picture where carid=@carid order by sort asc;";

            try
            {
                var list = Helper.Query<CarPictureModel>(sql, new { carid });
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region 赞不用

        /// <summary>
        /// 审核车辆
        /// </summary>
        /// <param name="id">车辆id</param>
        /// <param name="status">审核状态</param>
        /// <returns>1.操作成功</returns>
        public int AuditCar(string id, int status)
        {
            const string sql = @"UPDATE `car_info` SET status=@status,`audit_time`=@audit_time WHERE `innerid`=@innerid;";
            try
            {
                var result = Helper.Execute(sql, new
                {
                    status,
                    audit_time = DateTime.Now,
                    innerid = id
                });
                if (result == 0)
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// 核销车辆
        /// </summary>
        /// <param name="id">车辆id</param>
        /// <returns>1.操作成功</returns>
        public int CancelCar(string id)
        {
            const string sql = @"UPDATE `car_info` SET `sold_time`=@sold_time WHERE `innerid`=@innerid;";
            try
            {
                var result = Helper.Execute(sql, new { sold_time = DateTime.Now, innerid = id });
                if (result == 0)
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// 车辆归档
        /// </summary>
        /// <param name="id">车辆id</param>
        /// <returns>1.操作成功</returns>
        public int KeepCar(string id)
        {
            const string sql = @"UPDATE `car_info` SET `keep_time`=@keep_time WHERE `innerid`=@innerid;";
            try
            {
                var result = Helper.Execute(sql, new { keep_time = DateTime.Now, innerid = id });
                if (result == 0)
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }

        #endregion

        #endregion
    }
}
