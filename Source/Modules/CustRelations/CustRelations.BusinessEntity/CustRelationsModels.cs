using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cedar.Framework.Common.BaseClasses;

namespace CCN.Modules.CustRelations.BusinessEntity
{
    /// <summary>
    /// 好友关系model
    /// </summary>
    public class CustRelationsModels
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Innerid { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public string Userid { get; set; }

        /// <summary>
        /// 好友id
        /// </summary>
        public string Frientsid { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime Createdtime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CustRelationsModels()
        {
            Createdtime = DateTime.Now;
        }
    }

    /// <summary>
    /// 好友关系view model
    /// </summary>
    public class CustRelationsViewModel
    {

        /// <summary>
        /// 好友id
        /// </summary>
        public string Frientsid { get; set; }
        
        /// <summary>
        /// 会员名
        /// </summary>
        public string Custname { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 固话
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Headportrait { get; set; }

        /// <summary>
        /// 用户状态[1.正常，2.冻结]
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 认证状态[0.未提交认证，1.提交认证(待审核)，2.审核通过，3.审核不过，4.重新填写(重新提交认证)]
        /// </summary>
        public int AuthStatus { get; set; }

        /// <summary>
        /// 类型1.车商，2,个人
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string Realname { get; set; }

        /// <summary>
        /// 总积分
        /// </summary>
        public int Totalpoints { get; set; }

        /// <summary>
        /// 会员等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? Createdtime { get; set; }
    }

    /// <summary>
    /// 查找会员view model
    /// </summary>
    public class CustViewModel
    {
        /// <summary>
        /// 会员id
        /// </summary>
        public string Innerid { get; set; }

        /// <summary>
        /// 会员名
        /// </summary>
        public string Custname { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Headportrait { get; set; }

        /// <summary>
        /// 会员等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 是否好友关系 1是，2否
        /// </summary>
        public int IsFriends { get; set; }
    }

    /// <summary>
    /// 查找会员view model
    /// </summary>
    public class CustQueryModel :QueryModel
    {
        /// <summary>
        /// 自身id
        /// </summary>
        public string Oneselfid { get; set; }

        /// <summary>
        /// 会员名
        /// </summary>
        public string Custname { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
    }
}
