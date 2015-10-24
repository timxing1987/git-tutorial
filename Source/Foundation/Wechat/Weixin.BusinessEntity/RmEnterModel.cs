/**  版本信息模板在安装目录下，可自行修改。
* RmEnterModel.cs
*
* 功 能： N/A
* 类 名： sr_social_wechat_requestmsge_enter
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
    ///     RmEnterModel:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class RmEnterModel
    {
        #region Model

        /// <summary>
        ///     auto_increment
        /// </summary>
        public string Innerid { get; set; }

        /// <summary>
        ///     发送者id
        /// </summary>
        public string Fromusername { get; set; }

        /// <summary>
        ///     接收者id
        /// </summary>
        public string Tousername { get; set; }

        /// <summary>
        ///     消息id
        /// </summary>
        public string Msgid { get; set; }

        /// <summary>
        ///     消息类型
        /// </summary>
        public string Msgtype { get; set; }

        /// <summary>
        ///     加密
        /// </summary>
        public string Encrypt { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime Createtime { get; set; }

        #endregion Model
    }
}