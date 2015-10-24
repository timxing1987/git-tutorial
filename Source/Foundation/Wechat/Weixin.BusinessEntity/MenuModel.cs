/**  版本信息模板在安装目录下，可自行修改。
* MenuModel.cs
*
* 功 能： N/A
* 类 名： sr_social_wechat_menu
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/23 14:15:05   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*/

namespace Cedar.Foundation.WeChat.Entities.WeChat
{
    /// <summary>
    ///     MenuModel:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class MenuModel
    {
        #region Model

        /// <summary>
        /// </summary>
        public string Innerid { get; set; }

        /// <summary>
        ///     账户ID
        /// </summary>
        public string Accountid { get; set; }

        /// <summary>
        ///     菜单级别 1:一级菜单 2:二级菜单
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        ///     菜单名称
        /// </summary>
        public string Menuname { get; set; }

        /// <summary>
        ///     菜单Key值(click为必须)
        /// </summary>
        public string Menukey { get; set; }

        /// <summary>
        ///     网页链接(view为必须)
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     菜单响应动作类型(目前仅为click和view)
        /// </summary>
        public string Menutype { get; set; }

        /// <summary>
        ///     是否删除 0:使用;1:删除
        /// </summary>
        public int? Isdel { get; set; }

        /// <summary>
        ///     父菜单Key值
        /// </summary>
        public string Parentid { get; set; }

        /// <summary>
        /// </summary>
        public string Condition1 { get; set; }

        /// <summary>
        /// </summary>
        public string Condition2 { get; set; }

        /// <summary>
        /// </summary>
        public string Condition3 { get; set; }

        #endregion Model
    }
}