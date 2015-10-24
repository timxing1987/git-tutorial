/**  版本信息模板在安装目录下，可自行修改。
* RmModel.cs
*
* 功 能： N/A
* 类 名： sr_social_wechat_requestmessage
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
    ///     RmModel:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class RmModel
    {
        #region Model

        /// <summary>
        /// </summary>
        public string Innerid { get; set; }

        /// <summary>
        ///     信息发送目标
        /// </summary>
        public string Tousername { get; set; }

        /// <summary>
        ///     信息源
        /// </summary>
        public string Fromusername { get; set; }

        /// <summary>
        ///     加密
        /// </summary>
        public string Encrypt { get; set; }

        /// <summary>
        ///     消息id
        /// </summary>
        public string Msgid { get; set; }

        /// <summary>
        ///     消息类型
        /// </summary>
        public string Msgtype { get; set; }

        /// <summary>
        ///     地理位置坐标经度
        /// </summary>
        public string Location_X { get; set; }

        /// <summary>
        ///     地理位置坐标纬度
        /// </summary>
        public string Location_Y { get; set; }

        /// <summary>
        ///     规模
        /// </summary>
        public string Scale { get; set; }

        /// <summary>
        ///     标签
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        ///     媒体id
        /// </summary>
        public string Mediaid { get; set; }

        /// <summary>
        ///     图片地址
        /// </summary>
        public string Picurl { get; set; }

        /// <summary>
        ///     语音格式：amr
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        ///     语音识别结果，UTF8编码
        /// </summary>
        public string Recognition { get; set; }

        /// <summary>
        ///     缩略图
        /// </summary>
        public string Thumbmediaid { get; set; }

        /// <summary>
        ///     标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// </summary>
        public DateTime? Createdtime { get; set; }

        #endregion Model
    }
}