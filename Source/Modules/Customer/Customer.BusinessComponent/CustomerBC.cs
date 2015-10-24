#region

using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using CCN.Modules.Customer.BusinessEntity;
using CCN.Modules.Customer.DataAccess;
using Cedar.AuditTrail.Interception;
using Cedar.Core.ApplicationContexts;
using Cedar.Core.Logging;
using Cedar.Framework.Common.BaseClasses;
using Cedar.Framework.Common.Server.BaseClasses;

#endregion

namespace CCN.Modules.Customer.BusinessComponent
{
    /// <summary>
    /// </summary>
    public class CustomerBC : BusinessComponentBase<CustomerDA>
    {
        /// <summary>
        /// </summary>
        /// <param name="da"></param>
        public CustomerBC(CustomerDA da)
            : base(da)
        {

        }

        #region 用户模块

        /// <summary>
        /// 会员注册检查Email是否被注册
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>0：未被注册，非0：Email被注册</returns>
        public int CheckEmail(string email)
        {
            return DataAccess.CheckEmail(email);
        }

        /// <summary>
        /// 会员注册检查手机号是否被注册
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <returns>0：未被注册，非0：被注册</returns>
        public int CheckMobile(string mobile)
        {
            return DataAccess.CheckMobile(mobile);
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        public JResult CustRegister(CustModel userInfo)
        {
            //LoggerFactories.CreateLogger().Write(JsonConvert.SerializeObject(userInfo, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }), TraceEventType.Information);
            var mYan = DataAccess.CheckMobile(userInfo.Mobile);
            if (mYan > 0)
            {
                return new JResult
                {
                    errcode = 402,
                    errmsg = "手机号被其他人注册"
                };
            }

            //生成会员名称
            userInfo.Custname = string.Concat("ccn_", DateTime.Now.Year, "_",
                userInfo.Mobile.Substring(userInfo.Mobile.Length - 6));

            if (!string.IsNullOrWhiteSpace(userInfo.Wechat?.Openid))
            {
                //检查openid是否被其他手机号注册
                var m = DataAccess.CustInfoByOpenid(userInfo.Wechat.Openid);
                if (m != null) //openid已绑定其他手机号（如果绑定自己手机号，CheckMobile接口就过滤掉）
                {
                    return JResult._jResult(403, "openid已绑定其他手机号");
                }

                //填充会员
                var wechat = DataAccess.CustWechatByOpenid(userInfo.Wechat.Openid);
                if (wechat != null)
                {
                    userInfo.Custname = wechat.Nickname;
                }
            }

            //密码加密
            userInfo.Password = Encryptor.EncryptAes(userInfo.Password);

            userInfo.Type = 1; //这版只有车商
            userInfo.Status = 1; //初始化状态[1.正常]
            userInfo.AuthStatus = 0; //初始化认证状态[0.未提交认证]
            userInfo.Createdtime = DateTime.Now;
            userInfo.Totalpoints = 0;
            userInfo.Level = 0;

            var innerid = Guid.NewGuid().ToString();
            userInfo.Innerid = innerid;

            var result = DataAccess.CustRegister(userInfo);

            #region 生成二维码

            Task.Factory.StartNew(() =>
            {
                try
                {
                    var filename = string.Concat("cust_qrcode_", DateTime.Now.ToString("yyyyMMddHHmmssfff"), ".jpg");
                    var filepath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "TempFile\\", filename);
                    var website = ConfigHelper.GetAppSettings("website");
                    var bitmap = BarCodeUtility.CreateBarcode(website + "?innerid=" + userInfo.Innerid, 240, 240);

                    //保存图片到临时文件夹
                    bitmap.Save(filepath);

                    //上传图片到七牛云
                    var qinniu = new QiniuUtility();
                    var qrcodeKey = qinniu.PutFile(filepath, "", filename);

                    //删除本地临时文件
                    if (File.Exists(filepath))
                    {
                        File.Delete(filepath);
                    }

                    //上传成功更新会员二维码
                    if (!string.IsNullOrWhiteSpace(qrcodeKey))
                    {
                        DataAccess.UpdateQrCode(innerid, qrcodeKey);
                    }
                }
                catch (Exception ex)
                {
                    // ignored
                    LoggerFactories.CreateLogger().Write("CustRegister接口异常", TraceEventType.Error, ex);
                }
            });
            #endregion

            #region 注册送积分

            Task.Factory.StartNew(() =>
            {
                DataAccess.ChangePoint(new CustPointModel()
                {
                    Custid = innerid,
                    Createdtime = userInfo.Createdtime,
                    Type = 1,
                    Innerid = Guid.NewGuid().ToString(),
                    Point = 10,
                    Remark = "",
                    Sourceid = 1,
                    Validtime = null
                });
            });

            #endregion

            return new JResult
            {
                errcode = result > 0 ? 0 : 404,
                errmsg = result > 0 ? "注册成功" : "注册失败"
            };
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginInfo">登录账户</param>
        /// <returns>用户信息</returns>
        public JResult CustLogin(CustLoginInfo loginInfo)
        {
            if (string.IsNullOrWhiteSpace(loginInfo.Mobile))
            {
                return JResult._jResult(403, "帐户名不能为空");
            }
            if (string.IsNullOrWhiteSpace(loginInfo.Password))
            {
                return JResult._jResult(404, "密码不能为空");
            }

            loginInfo.Password = Encryptor.EncryptAes(loginInfo.Password);

            var userInfo = DataAccess.CustLogin(loginInfo);
            if (userInfo == null)
            {
                return JResult._jResult(401, "帐户名或登录密码不正确");
            }
            if (userInfo.Status == 2)
            {
                return JResult._jResult(402, "帐户被冻结");
            }
            return JResult._jResult(0, userInfo);
        }

        /// <summary>
        /// 用户登录(openid登录)
        /// </summary>
        /// <param name="openid">openid</param>
        /// <returns>用户信息</returns>
        public JResult CustLoginByOpenid(string openid)
        {
            var userInfo = DataAccess.CustLoginByOpenid(openid);
            if (userInfo == null)
            {
                return JResult._jResult(405, "会员不存在");
            }
            if (userInfo.Status == 2)
            {
                return JResult._jResult(402, "帐户被冻结");
            }

            return JResult._jResult(0, userInfo);
        }

        /// <summary>
        /// 获取会员详情
        /// </summary>
        /// <param name="innerid">会员id</param>
        /// <returns></returns>
        public JResult GetCustById(string innerid)
        {
            var model = DataAccess.GetCustById(innerid);
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
        /// 获取会员详情（根据手机号）
        /// </summary>
        /// <param name="mobile">会员手机号</param>
        /// <returns></returns>
        public JResult GetCustByMobile(string mobile)
        {
            var model = DataAccess.GetCustByMobile(mobile);
            return JResult._jResult(model);
        }

        /// <summary>
        /// 获取会员列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        public BasePageList<CustModel> GetCustPageList(CustQueryModel query)
        {
            return DataAccess.GetCustPageList(query);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="mRetrievePassword"></param>
        /// <returns></returns>
        public JResult UpdatePassword(CustRetrievePassword mRetrievePassword)
        {

            var model = DataAccess.GetCustByMobile(mRetrievePassword.Mobile);
            if (model == null)
            {
                return JResult._jResult(403, "账户不存在");
            }

            if (model.Status == 2)
            {
                return JResult._jResult(404, "账户被冻结");
            }

            //密码加密
            mRetrievePassword.NewPassword = Encryptor.EncryptAes(mRetrievePassword.NewPassword);
            var result = DataAccess.UpdatePassword(mRetrievePassword);
            return new JResult
            {
                errcode = result > 0 ? 0 : 405,
                errmsg = result > 0 ? "修改成功" : "修改失败"
            };
        }

        /// <summary>
        /// 修改会员信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JResult UpdateCustInfo(CustModel model)
        {
            var newModel = new CustModel
            {
                Innerid = model.Innerid,
                Custname = model.Custname,
                Telephone = model.Telephone,
                Email = model.Email,
                Headportrait = model.Headportrait
            };

            var result = DataAccess.UpdateCustInfo(newModel);
            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = result > 0 ? "修改成功" : "修改失败"
            };
        }

        /// <summary>
        /// 修改会员状态(冻结和解冻)
        /// </summary>
        /// <param name="innerid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public JResult UpdateCustStatus(string innerid, int status)
        {
            var result = DataAccess.UpdateCustStatus(innerid, status);
            return JResult._jResult(result);
        }
        #endregion

        #region 用户认证

        /// <summary>
        /// 用户添加认证信息
        /// </summary>
        /// <param name="model">认证信息</param>
        /// <returns></returns>
        public JResult AddAuthentication(CustAuthenticationModel model)
        {
            if (string.IsNullOrWhiteSpace(model?.Custid))
            {
                return new JResult
                {
                    errcode = 401,
                    errmsg = "会员id不能空"
                };
            }
            model.Createdtime = DateTime.Now;
            model.Modifiedtime = null;

            int result;
            var m = DataAccess.GetCustAuthByCustid(model.Custid);
            if (m == null)
            {
                result = DataAccess.AddAuthentication(model);
            }
            else
            {
                model.Innerid = m.Innerid;
                result = DataAccess.UpdateAuthentication(model);
            }

            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = result > 0 ? "申请认证成功" : "申请认证失败"
            };
        }

        /// <summary>
        /// 用户修改认证信息
        /// </summary>
        /// <param name="model">认证信息</param>
        /// <returns></returns>
        public JResult UpdateAuthentication(CustAuthenticationModel model)
        {
            if (string.IsNullOrWhiteSpace(model?.Custid))
            {
                return new JResult
                {
                    errcode = 401,
                    errmsg = "会员id不能空"
                };
            }
            model.Modifiedtime = DateTime.Now;
            model.AuditPer = null;
            model.AuditDesc = null;
            model.AuditTime = null;
            model.AuditResult = null;
            var result = DataAccess.UpdateAuthentication(model);
            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = result > 0 ? "修改认证信息成功" : "修改认证信息失败"
            };
        }

        /// <summary>
        /// 审核认证信息
        /// </summary>
        /// <param name="model">会员相关信息</param>
        /// <returns></returns>
        [AuditTrailCallHandler("CustomerBC.AuditAuthentication")]
        public JResult AuditAuthentication(CustAuthenticationModel model)
        {
            var operid = ApplicationContext.Current.UserId;
            if (string.IsNullOrWhiteSpace(operid))
            {
                return new JResult
                {
                    errcode = 401,
                    errmsg = "操作人信息不存在"
                };
            }

            model.AuditPer = operid;

            //设置认证状态
            model.AuditResult = model.AuditResult == 1 ? 2 : 3;
            model.AuditTime = DateTime.Now;

            var result = DataAccess.AuditAuthentication(model);
            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = result > 0 ? "审核成功" : "审核失败"
            };
        }


        /// <summary>
        /// 获取会员认证信息 by innerid
        /// </summary>
        /// <param name="innerid">id</param>
        /// <returns></returns>
        public JResult GetCustAuthById(string innerid)
        {
            var list = DataAccess.GetCustAuthById(innerid);
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
        /// 获取会员认证信息 by custid
        /// </summary>
        /// <param name="custid">会员id</param>
        /// <returns></returns>
        public JResult GetCustAuthByCustid(string custid)
        {
            var model = DataAccess.GetCustAuthByCustid(custid);
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

        #region 会员点赞

        /// <summary>
        /// 给会员点赞
        /// </summary>
        /// <param name="model">粉丝信息</param>
        /// <returns></returns>
        public JResult CustPraise(CustLaudator model)
        {
            if (string.IsNullOrWhiteSpace(model?.Openid) || string.IsNullOrWhiteSpace(model.Tocustid))
            {
                return JResult._jResult(401, "参数不完整");
            }

            var m = DataAccess.GetCustLaudatorByOpenid(model.Openid, model.Tocustid);
            if (m != null) //说明点赞人信息已经存在
            {
                model.Innerid = m.Innerid; //
                //说明之前的信息是匿名的,现在非匿名，需要更新信息 或者 修改了信息后赞人，库中数据也更新
                //if (string.IsNullOrWhiteSpace(m.Nickname) && !m.Nickname.Equals(model.Nickname)) 
                if (!m.Nickname.Equals(model.Nickname) && !string.IsNullOrWhiteSpace(m.Nickname))
                {
                    var uModel = new CustLaudator
                    {
                        Accountid = model.Accountid,
                        Openid = model.Openid,
                        Nickname = model.Nickname,
                        Sex = model.Sex,
                        Photo = model.Photo,
                        Remarkname = model.Remarkname,
                        Area = model.Area,
                        Subscribe_time = model.Subscribe_time,
                        Subscribe = model.Subscribe,
                        Country = model.Country,
                        Province = model.Province,
                        City = model.City
                    };
                    DataAccess.UpdateLaudator(uModel);
                }
                if (m.IsPraise > 0)
                {
                    return JResult._jResult(402, "重复点赞");
                }
            }
            else
            {
                model.Innerid = Guid.NewGuid().ToString();
                model.Createdtime = DateTime.Now;
                var addresult = DataAccess.AddLaudator(model);
                if (addresult == 0)
                {
                    return JResult._jResult(403, "点赞人信息保存失败");
                }
            }

            var rel = new CustLaudatorRelation
            {
                Laudatorid = model.Innerid,
                Tocustid = model.Tocustid,
                Carid = model.Carid,
                Createdtime = DateTime.Now
            };
            var res = DataAccess.SaveLaudatorRelation(rel);
            return JResult._jResult(
                res > 0 ? 0 : 400,
                res > 0 ? "赞成功" : "赞失败");
        }

        /// <summary>
        /// 根据会员id获取所有点赞人列表
        /// </summary>
        /// <param name="custid">会员id</param>
        /// <returns></returns>
        public JResult GetLaudatorListByCustid(string custid)
        {
            var list = DataAccess.GetLaudatorListByCustid(custid);
            return JResult._jResult(0, list);
        }

        #endregion

        #region 会员标签


        /// <summary>
        /// 添加标签
        /// </summary>
        /// <param name="model">标签信息</param>
        /// <returns></returns>
        public JResult AddTag(CustTagModel model)
        {
            model.Isenabled = 1;
            var result = DataAccess.AddTag(model);
            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = result > 0 ? "添加成功" : "添加失败"
            };
        }

        /// <summary>
        /// 修改标签
        /// </summary>
        /// <param name="model">标签信息</param>
        /// <returns></returns>
        public JResult UpdateTag(CustTagModel model)
        {
            model.Modifiedtime = DateTime.Now;
            var result = DataAccess.UpdateTag(model);
            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = result > 0 ? "修改成功" : "修改失败"
            };
        }
        /// <summary>
        /// 修改标签状态
        /// </summary>
        /// <param name="innerid">标签ID</param>
        /// <param name="status"></param>
        /// <returns></returns>
        public JResult UpdateTagStatus(string innerid, int status)
        {
            var result = DataAccess.UpdateTagStatus(innerid, status);
            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = result > 0 ? "修改成功" : "修改失败"
            };
        }
        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="innerid">标签id</param>
        /// <returns></returns>
        public JResult DeleteTag(string innerid)
        {
            var result = DataAccess.DeleteTag(innerid);
            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = result > 0 ? "删除成功" : "删除失败"
            };
        }

        /// <summary>
        /// 获取标签详情
        /// </summary>
        /// <param name="innerid">标签id</param>
        /// <returns></returns>
        public JResult GetTagById(string innerid)
        {
            var model = DataAccess.GetTagById(innerid);
            if (model == null)
            {
                return new JResult
                {
                    errcode = 400,
                    errmsg = "暂无数据"
                };
            }
            return new JResult
            {
                errcode = 0,
                errmsg = model
            };
        }

        /// <summary>
        /// 获取标签列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        public BasePageList<CustTagModel> GetTagPageList(CustTagQueryModel query)
        {
            return DataAccess.GetTagPageList(query);
        }

        /// <summary>
        /// 打标签
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JResult DoTagRelation(CustTagRelation model)
        {
            if (model == null)
            {
                return new JResult
                {
                    errcode = 401,
                    errmsg = "参数不正确"
                };
            }

            model.Innerid = Guid.NewGuid().ToString();
            model.Createdtime = DateTime.Now;
            var result = DataAccess.DoTagRelation(model);
            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = result > 0 ? "打标签成功" : "打标签失败"
            };
        }

        /// <summary>
        /// 删除标签关系
        /// </summary>
        /// <param name="innerid"></param>
        /// <returns></returns>
        public JResult DelTagRelation(string innerid)
        {
            var result = DataAccess.DelTagRelation(innerid);
            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = result > 0 ? "删除标签成功" : "删除标签失败"
            };
        }

        /// <summary>
        /// 获取会员拥有的标签
        /// </summary>
        /// <param name="custid"></param>
        /// <returns></returns>
        public JResult GetTagRelation(string custid)
        {
            var list = DataAccess.GetTagRelation(custid);
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
        /// 获取会员该标签的操作者
        /// </summary>
        /// <param name="custid"></param>
        /// <param name="tagid"></param>
        /// <returns></returns>
        public JResult GetTagRelationWithOper(string custid, string tagid)
        {
            var list = DataAccess.GetTagRelationWithOper(custid, tagid);
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

        #region 会员积分

        /// <summary>
        /// 会员积分变更
        /// </summary>
        /// <param name="model">变更信息</param>
        /// <returns></returns>
        public JResult ChangePoint(CustPointModel model)
        {
            if (model.Point == 0)
            {
                return new JResult
                {
                    errcode = 401,
                    errmsg = "变更的积分不能为0"
                };
            }
            model.Innerid = Guid.NewGuid().ToString();

            if (model.Type == 0)
            {
                model.Type = 1;
            }

            var result = DataAccess.ChangePoint(model);
            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = result > 0 ? "添加成功" : "添加失败"
            };
        }

        /// <summary>
        /// 获取会员积分记录列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        public BasePageList<CustPointViewModel> GetCustPointLogPageList(CustPointQueryModel query)
        {
            var list = DataAccess.GetCustPointLogPageList(query);
            return list;
        }

        /// <summary>
        /// 积分兑换礼券
        /// </summary>
        /// <param name="model">兑换相关信息</param>
        /// <returns></returns>
        public JResult PointExchangeCoupon(CustPointExChangeCouponModel model)
        {
            if (model == null)
            {
                return JResult._jResult(401, "参数不正确");
            }
            if (string.IsNullOrWhiteSpace(model.Custid))
            {
                return JResult._jResult(402, "会员不存在");
            }
            if (model.Point == 0)
            {
                return JResult._jResult(403, "积分不够");
            }
            if (string.IsNullOrWhiteSpace(model.Cardid))
            {
                return JResult._jResult(404, "礼券不存在");
            }

            //生成随机数
            model.Code = RandomUtility.GetRandomCode();
            //生成二维码位图
            var bitmap = BarCodeUtility.CreateBarcode(model.Code, 240, 240);

            //保存二维码图片到临时文件夹
            var filename = string.Concat("card_qrcode_", DateTime.Now.ToString("yyyyMMddHHmmssfff"), ".jpg");
            var filepath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "TempFile\\", filename);
            bitmap.Save(filepath);

            //上传图片到七牛云
            var qinniu = new QiniuUtility();
            model.QrCode = qinniu.PutFile(filepath, "", filename);

            //删除本地临时文件
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }

            //开始兑换
            model.Createdtime = DateTime.Now;
            var result = DataAccess.PointExchangeCoupon(model);
            return JResult._jResult(
                result > 0 ? 0 : 400,
                result > 0 ? "兑换成功" : "兑换失败");
        }

        #endregion

        #region 会员礼券

        /// <summary>
        /// 获取获取礼券列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        public BasePageList<CouponInfoModel> GetCouponPageList(CouponQueryModel query)
        {
            return DataAccess.GetCouponPageList(query);
        }

        /// <summary>
        /// 添加礼券
        /// </summary>
        /// <param name="model">礼券信息</param>
        /// <returns></returns>
        public JResult AddCoupon(CouponInfoModel model)
        {
            model.Innerid = Guid.NewGuid().ToString();
            model.Count = model.Maxcount;
            model.Createdtime = DateTime.Now;
            model.IsEnabled = 1;
            var result = DataAccess.AddCoupon(model);
            return JResult._jResult(result);
        }

        /// <summary>
        /// 修改礼券
        /// </summary>
        /// <param name="model">礼券信息</param>
        /// <returns></returns>
        public JResult UpdateCoupon(CouponInfoModel model)
        {
            model.Count = null;
            model.Maxcount = null;
            model.Createdtime = null;
            model.Vtype = null;
            model.Vstart = null;
            model.Vend = null;
            model.Value1 = null;
            model.Value2 = null;
            model.IsEnabled = null;
            model.Modifiedtime = DateTime.Now;

            var result = DataAccess.UpdateCoupon(model);
            return JResult._jResult(result);
        }

        /// <summary>
        /// 获取礼券信息
        /// </summary>
        /// <param name="innerid">id</param>
        /// <returns></returns>
        public JResult GetCouponById(string innerid)
        {
            var model = DataAccess.GetCouponById(innerid);
            return JResult._jResult(model);
        }

        #endregion


    }
}