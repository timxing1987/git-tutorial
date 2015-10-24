/**  版本信息模板在安装目录下，可自行修改。
* GroupModel.cs
*
* 功 能： N/A
* 类 名： sr_social_wechat_group
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
    ///     GroupModel:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class GroupModel
    {
        #region Model

        /// <summary>
        /// </summary>
        public string Innerid { get; set; }

        /// <summary>
        ///     微信帐号ID
        /// </summary>
        public string Accountid { get; set; }

        /// <summary>
        ///     组别ID
        /// </summary>
        public string Groupid { get; set; }

        /// <summary>
        ///     组名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     数量
        /// </summary>
        public decimal? Count { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        ///     状态(1:删除;0:正常)
        /// </summary>
        public int? Statues { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime? Createdtime { get; set; }

        #endregion Model
    }
}