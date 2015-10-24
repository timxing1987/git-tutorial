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
    ///     PredefineModel:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class PredefineModel
    {
        #region Model

        /// <summary>
        /// </summary>
        public string Innerid { get; set; }

        /// <summary>
        ///     用户id
        /// </summary>
        public string Accountid { get; set; }

        /// <summary>
        ///     关键字
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        ///     标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     图片
        /// </summary>
        public string Images { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     定义是微信机器人使用还是信息发送使用：1：微信机器人使用；2、信息发送使用；
        /// </summary>
        public int? Subtype { get; set; }

        /// <summary>
        ///     回复平台(1：微信；2：微博)
        /// </summary>
        public int? Socialsource { get; set; }

        /// <summary>
        ///     类型: 1:事件 2:回复
        /// </summary>
        public int? Type { get; set; }

        /// <summary>
        ///     原文链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     排序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        ///     父id
        /// </summary>
        public string Parentid { get; set; }

        /// <summary>
        ///     上层所有父id的列表
        /// </summary>
        public string Parentidlist { get; set; }

        /// <summary>
        ///     是否可用: 1:可用 0:不可用
        /// </summary>
        public int? Isenable { get; set; }

        /// <summary>
        ///     是否删除 1：已删除 0：未删除
        /// </summary>
        public int? Isdelete { get; set; }

        /// <summary>
        ///     摘要
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        ///     设置来源(1：市场活动 2：微信 3：其他)
        /// </summary>
        public int? Source { get; set; }

        /// <summary>
        /// </summary>
        public string Condition1 { get; set; }

        /// <summary>
        /// </summary>
        public string Condition2 { get; set; }

        /// <summary>
        /// </summary>
        public string Condition3 { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime? Createdtime { get; set; }

        #endregion Model
    }
}