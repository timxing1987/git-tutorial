/**  版本信息模板在安装目录下，可自行修改。
* AttentionModel.cs
*
* 功 能： N/A
* 类 名： sr_social_wechat_attention
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
    ///     sr_social_wechat_attention:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class AttentionModel
    {
        #region Model

        /// <summary>
        /// </summary>
        public string Innerid { get; set; }

        /// <summary>
        ///     微信平台ID
        /// </summary>
        public string Userid { get; set; }

        /// <summary>
        ///     关注者的ID(即OpenID)
        /// </summary>
        public string Friend { get; set; }

        /// <summary>
        ///     动作类型(1:关注;2:取消关注)
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        ///     关注时间
        /// </summary>
        public DateTime Createdtime { get; set; }

        #endregion Model
    }
}