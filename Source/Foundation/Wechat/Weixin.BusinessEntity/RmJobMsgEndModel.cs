/**  版本信息模板在安装目录下，可自行修改。
* RmEnterModel.cs
*
* 功 能： N/A
* 类 名： sr_social_wechat_requestmsge_jobmsgend
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
    ///     RmJobMsgEndModel:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class RmJobMsgEndModel
    {
        #region Model

        /// <summary>
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
        ///     状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        ///     group_id下粉丝数；或者openid_list中的粉丝数
        /// </summary>
        public string Totalcount { get; set; }

        /// <summary>
        ///     过滤（过滤是指，有些用户在微信设置不接收该公众号的消息）后，准备发送的粉丝数，原则上，FilterCount = SentCount + ErrorCount
        /// </summary>
        public string Filtercount { get; set; }

        /// <summary>
        ///     发送成功的粉丝数
        /// </summary>
        public string Sendcount { get; set; }

        /// <summary>
        ///     发送失败的粉丝数
        /// </summary>
        public string Errorcount { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime? Createdtime { get; set; }

        #endregion Model
    }
}