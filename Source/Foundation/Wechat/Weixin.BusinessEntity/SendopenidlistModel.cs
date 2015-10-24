/**  版本信息模板在安装目录下，可自行修改。
* SendopenidlistModel.cs
*
* 功 能： N/A
* 类 名： sr_social_wechat_sendopenidlist
*
* Ver    变更日期             负责人 Messi  变更内容
* ───────────────────────────────────
* V0.01  2014/12/23 13:53:59   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*/

namespace Cedar.Foundation.WeChat.Entities.WeChat
{
    /// <summary>
    ///     sr_social_wechat_sendopenidlist:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class SendopenidlistModel
    {
        #region Model

        /// <summary>
        /// </summary>
        public string Innerid { get; set; }

        /// <summary>
        ///     公众号的微信号
        /// </summary>
        public string Accountid { get; set; }

        /// <summary>
        ///     公众号群发助手的微信号
        /// </summary>
        public string Receiveid { get; set; }

        /// <summary>
        ///     批次id
        /// </summary>
        public string Batchid { get; set; }

        #endregion Model
    }
}