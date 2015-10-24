/**  版本信息模板在安装目录下，可自行修改。
* sr_social_wechat_requestmsge_subscribe.cs
*
* 功 能： N/A
* 类 名： sr_social_wechat_requestmsge_subscribe
*
* Ver    变更日期             负责人 Messi 变更内容
* ───────────────────────────────────
* V0.01  2014/12/23 13:53:58   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*/

using System;

namespace Cedar.Foundation.WeChat.Entities.WeChat
{
    /// <summary>
    ///     sr_social_wechat_requestmsge_subscribe:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class RmSubscribeModel
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
        ///     事件KEY值，qrscene_为前缀，后面为二维码的参数值（如果不是扫描场景二维码，此参数为空）
        /// </summary>
        public string Eventkey { get; set; }

        /// <summary>
        ///     二维码的ticket，可用来换取二维码图片（如果不是扫描场景二维码，此参数为空）
        /// </summary>
        public string Ticket { get; set; }

        /// <summary>
        /// </summary>
        public DateTime? Createdtime { get; set; }

        #endregion Model
    }
}