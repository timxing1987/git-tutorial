#region

using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Cedar.Foundation.WeChat.DataAccess;
using Cedar.Foundation.WeChat.Entities.WeChat;
using Cedar.Framework.Common.BaseClasses;
using Cedar.Framework.Common.Server.BaseClasses;
using Senparc.Weixin.MP.AdvancedAPIs;

#endregion

namespace Cedar.Foundation.WeChat.BusinessComponent
{
    /// <summary>
    /// </summary>
    public class WeChatBC : BusinessComponentBase<WeChatDA>
    {
        /// <summary>
        /// </summary>
        /// <param name="da"></param>
        public WeChatBC(WeChatDA da) : base(da)
        {
        }

        /// <summary>
        ///     更新或重新获取所有会员信息
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        public JResult GenerateWechatFriends(string appid)
        {
            var total = 0;
            var failed = 0;
            //获取openid
            var nextOpenId = "1";
            while (nextOpenId != string.Empty)
            {
                var result = UserApi.Get(appid, nextOpenId == "1" ? "" : nextOpenId);
                nextOpenId = result.next_openid;
                if (result.data != null)
                {
                    Parallel.ForEach(result.data.openid, openid =>
                    {
                        total++;
                        try
                        {
                            GenerateWechatFriendByOpenid(appid, openid, true);
                        }
                        catch
                        {
                            failed++;
                        }
                    });
                }
            }
            return new JResult
            {
                errcode = 0,
                errmsg = new
                {
                    total,
                    failed
                }
            };
        }

        /// <summary>
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="openid"></param>
        /// <param name="isUpdate"></param>
        /// <returns></returns>
        public bool GenerateWechatFriendByOpenid(string appid, string openid, bool isUpdate = false)
        {
            var isWechatFriendExists = DataAccess.IsWechatFriendExists(appid, openid);
            if (isWechatFriendExists && !isUpdate)
                return true;

            dynamic backmeg = UserApi.Info(appid, openid);
            var nickname = !string.IsNullOrEmpty(backmeg.nickname)
                ? Regex.Replace(backmeg.nickname, @"\p{Cs}", "")
                : "null";
            var friendModel = new FriendModel
            {
                Innerid = Guid.NewGuid().ToString(),
                Accountid = appid,
                Nickname = nickname,
                Photo = backmeg.headimgurl,
                OPENID = backmeg.openid,
                Country = backmeg.country,
                Province = backmeg.province,
                City = backmeg.city,
                Sex = backmeg.sex,
                Isdel = 0,
                SubscribeTime = backmeg.subscribe_time,
                Subscribe = backmeg.subscribe,
                Createdtime = DateTime.Now
            };
            var result = isWechatFriendExists
                ? DataAccess.UpdateWechatFriend(friendModel)
                : DataAccess.CreaeWechatFriend(friendModel);
            return result;
        }

        /// <summary>
        ///     根据openid获取微信粉丝信息
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        public JResult GetFriendInformationByOpenid(string appid, string openid)
        {
            return new JResult();
        }
    }
}