/**  版本信息模板在安装目录下，可自行修改。
* sr_social_wechat_sendmessagedetail.cs
*
* 功 能： N/A
* 类 名： sr_social_wechat_sendmessagedetail
*
* Ver    变更日期             负责人 Messi  变更内容
* ───────────────────────────────────
* V0.01  2014/12/23 13:53:59   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*/

using System;

namespace Cedar.Foundation.WeChat.Entities.WeChat
{
    /// <summary>
    ///     sr_social_wechat_sendmessagedetail:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class SendmessagedetailModel
    {
        #region Model

        /// <summary>
        /// </summary>
        public string Innerid { get; set; }

        /// <summary>
        /// </summary>
        public string Sendid { get; set; }

        /// <summary>
        /// </summary>
        public string Receiveid { get; set; }

        /// <summary>
        /// </summary>
        public string Sendtype { get; set; }

        /// <summary>
        /// </summary>
        public DateTime? Senddate { get; set; }

        /// <summary>
        /// </summary>
        public string Sendcontent { get; set; }

        /// <summary>
        /// </summary>
        public int Result { get; set; }

        /// <summary>
        /// </summary>
        public string Batchid { get; set; }

        /// <summary>
        /// </summary>
        public string ActivityId { get; set; }

        /// <summary>
        /// </summary>
        public string Condition2 { get; set; }

        /// <summary>
        /// </summary>
        public string Condition3 { get; set; }

        /// <summary>
        /// </summary>
        public DateTime? Createtime { get; set; }

        #endregion Model
    }
}