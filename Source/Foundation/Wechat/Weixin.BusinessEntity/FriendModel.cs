/**  版本信息模板在安装目录下，可自行修改。
* FriendModel.cs
*
* 功 能： N/A
* 类 名： sr_social_wechat_friend
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/23 14:15:05   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*/

using System;

namespace Cedar.Foundation.WeChat.Entities.WeChat
{
    /// <summary>
    ///     FriendModel:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class FriendModel
    {
        #region Model

        /// <summary>
        /// </summary>
        public string Innerid { get; set; }

        /// <summary>
        ///     微信平台ID
        /// </summary>
        public string Accountid { get; set; }

        /// <summary>
        ///     昵称
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        ///     昵称
        /// </summary>
        public byte[] Nicknamebyte { get; set; }

        /// <summary>
        /// </summary>
        public string OPENID { get; set; }

        /// <summary>
        ///     头像路径
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        ///     备注名
        /// </summary>
        public string Remarkname { get; set; }

        /// <summary>
        ///     地区
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        ///     用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息。
        /// </summary>
        public int Subscribe { get; set; }

        /// <summary>
        ///     用户关注时间，为时间戳。如果用户曾多次关注，则取最后关注时间
        /// </summary>
        public long SubscribeTime { get; set; }

        /// <summary>
        ///     性别
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        ///     群组ID
        /// </summary>
        public string Groupid { get; set; }

        /// <summary>
        ///     是不删除(1:删除;0:未删除)
        /// </summary>
        public int? Isdel { get; set; }

        /// <summary>
        ///     国家
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        ///     省份
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// </summary>
        public string City { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime? Createdtime { get; set; }

        #endregion
    }
}