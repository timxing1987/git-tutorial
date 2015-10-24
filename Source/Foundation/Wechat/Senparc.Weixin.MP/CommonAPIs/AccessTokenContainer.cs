/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：AccessTokenContainer.cs
    文件功能描述：通用接口AccessToken容器，用于自动管理AccessToken，如果过期会重新获取
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20150702
    修改描述：添加GetFirstOrDefaultAppId()方法
    
    修改标识：Senparc - 20151004
    修改描述：v13.3.0 将JsApiTicketContainer整合到AccessTokenContainer

----------------------------------------------------------------*/

using System;
using System.Configuration;
using Cedar.Core.EntLib.Data;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.CommonAPIs
{
    /// <summary>
    ///     AccessToken及JsApiTicket包
    /// </summary>
    public class AccessTokenBag : BaseContainerBag
    {
        /// <summary>
        ///     只针对这个AppId的锁
        /// </summary>
        public object Lock = new object();

        public string AppId { get; set; }
        public string AppSecret { get; set; }

        public DateTime AccessTokenExpireTime { get; set; }
        public AccessTokenResult AccessTokenResult { get; set; }


        public JsApiTicketResult JsApiTicketResult { get; set; }
        public DateTime JsApiTicketExpireTime { get; set; }
    }

    /// <summary>
    ///     通用接口AccessToken容器，用于自动管理AccessToken，如果过期会重新获取
    /// </summary>
    /// <summary>
    ///     通用接口AccessToken容器存储在Redis，用于自动管理AccessToken，如果过期会重新获取
    /// </summary>
    public class AccessTokenRedisContainer
    {
        /// <summary>
        /// </summary>
        private static readonly RedisDatabaseWrapper Wraper =
            new RedisDatabaseWrapper(ConfigurationManager.AppSettings["wechat_tokenip"],
                Convert.ToInt16(ConfigurationManager.AppSettings["wechat_tokendb"]),
                ConfigurationManager.AppSettings["wechat_tokenpassword"]);

        /// <summary>
        /// </summary>
        protected readonly TimeSpan TimeOut = new TimeSpan(0, 0, 7000);

        /// <summary>
        /// </summary>
        /// <summary>
        ///     注册应用凭证信息，此操作只是注册，不会马上获取Token，并将清空之前的Token，
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        public static void Register(string appId, string appSecret)
        {
            if (!Wraper.KeyExists(appId))
                Wraper.StringSet(appId, appSecret);
            else
            {
                Wraper.KeyDelete(appId);
                Wraper.KeyDelete($"{appId}:token");
                Wraper.KeyDelete($"{appId}:jsapiticket");

                Wraper.StringSet(appId, appSecret);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public static bool CheckRegistered(string appId)
        {
            return Wraper.KeyExists(appId);
        }

        #region AccessToken

        /// <summary>
        ///     使用完整的应用凭证获取Token，如果不存在将自动注册
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static string TryGetAccessToken(string appId, string appSecret, bool getNewToken = false)
        {
            if (!CheckRegistered(appId) || getNewToken)
            {
                Register(appId, appSecret);
            }
            return GetAccessToken(appId);
        }

        /// <summary>
        ///     获取可用Token
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static string GetAccessToken(string appId, bool getNewToken = false)
        {
            var result = GetAccessTokenResult(appId, getNewToken).access_token;
            return result;
        }

        /// <summary>
        ///     获取可用Token
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static AccessTokenResult GetAccessTokenResult(string appId, bool getNewToken = false)
        {
            if (!CheckRegistered(appId))
            {
                throw new WeixinException("此appId尚未注册，请先使用AccessTokenContainer.Register完成注册（全局执行一次即可）！");
            }

            var appSecret = Wraper.StringGet(appId);
            var tokenkey = $"{appId}:token";
            var token = Wraper.StringGet(tokenkey);

            var accessTokenResult = new AccessTokenResult();
            if (token == null || getNewToken)
            {
                var tokendata = CommonApi.GetToken(appId, appSecret);
                Wraper.StringSet(tokenkey, tokendata.access_token);
                Wraper.KeyExpire(tokenkey, new TimeSpan(0, 0, tokendata.expires_in));
                token = tokendata.access_token;
                accessTokenResult.expires_in = tokendata.expires_in;
            }

            accessTokenResult.access_token = token;
            accessTokenResult.expires_in = 7200;
            return accessTokenResult;
        }

        #endregion

        #region JsApiTicket

        /// <summary>
        ///     使用完整的应用凭证获取Ticket，如果不存在将自动注册
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static string TryGetJsApiTicket(string appId, string appSecret, bool getNewTicket = false)
        {
            if (!CheckRegistered(appId) || getNewTicket)
            {
                Register(appId, appSecret);
            }
            return GetJsApiTicket(appId);
        }

        /// <summary>
        ///     获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static string GetJsApiTicket(string appId, bool getNewTicket = false)
        {
            return GetJsApiTicketResult(appId, getNewTicket).ticket;
        }

        /// <summary>
        ///     获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static JsApiTicketResult GetJsApiTicketResult(string appId, bool getNewTicket = false)
        {
            if (!CheckRegistered(appId))
            {
                throw new WeixinException("此appId尚未注册，请先使用JsApiTicketContainer.Register完成注册（全局执行一次即可）！");
            }

            var appSecret = Wraper.StringGet(appId);
            var jsapiticket = $"{appId}:jsapiticket";
            var ticket = Wraper.StringGet(jsapiticket);

            var jsApiTicketResult = new JsApiTicketResult();
            if (ticket == null || getNewTicket)
            {
                var ticketdata = CommonApi.GetTicket(appId, appSecret);
                Wraper.StringSet(jsapiticket, ticketdata.ticket);
                Wraper.KeyExpire(jsapiticket, new TimeSpan(0, 0, ticketdata.expires_in));
                ticket = ticketdata.ticket;
                jsApiTicketResult.expires_in = ticketdata.expires_in;
            }

            jsApiTicketResult.ticket = ticket;
            jsApiTicketResult.expires_in = 7200;
            return jsApiTicketResult;
        }

        #endregion
    }
}