/**  版本信息模板在安装目录下，可自行修改。
* sr_social_wechat_requestmsge_scan.cs
*
* 功 能： N/A
* 类 名： sr_social_wechat_requestmsge_scan
*
* Ver    变更日期             负责人  Messi 变更内容
* ───────────────────────────────────
* V0.01  2014/12/23 13:53:58   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;

namespace Cedar.Foundation.WeChat.Entities.WeChat
{
    /// <summary>
    ///     sr_social_wechat_requestmsge_scan:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class RmScanModel
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
        /// </summary>
        public string Ticket { get; set; }

        /// <summary>
        ///     事件KEY值，是一个32位无符号整数，即创建二维码时的二维码scene_id
        /// </summary>
        public string Eventkey { get; set; }

        /// <summary>
        /// </summary>
        public DateTime? Createdtime { get; set; }

        #endregion Model
    }
}