#region

using Cedar.Foundation.WeChat.BusinessComponent;
using Cedar.Foundation.WeChat.Interface;
using Cedar.Framework.Common.BaseClasses;
using Cedar.Framework.Common.Server.BaseClasses;

#endregion

namespace Cedar.Foundation.WeChat.BusinessService
{
    /// <summary>
    /// </summary>
    public class WeChatManagementService : ServiceBase<WeChatBC>, IWeChatManagementService
    {
        /// <summary>
        /// </summary>
        public WeChatManagementService(WeChatBC bc)
            : base(bc)
        {
        }

        /// <summary>
        ///     更新或重新获取所有会员信息
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        public JResult GenerateWechatFriends(string appid)
        {
            return BusinessComponent.GenerateWechatFriends(appid);
        }

        /// <summary>
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="openid"></param>
        /// <param name="isUpdate"></param>
        /// <returns></returns>
        public JResult GenerateWechatFriend(string appid, string openid, bool isUpdate = false)
        {
            var result = BusinessComponent.GenerateWechatFriendByOpenid(appid, openid, isUpdate);
            return new JResult
            {
                errcode = 0,
                errmsg = result
            };
        }

        /// <summary>
        ///     根据openid获取微信粉丝信息
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        public JResult GetFriendInformationByOpenid(string appid, string openid)
        {
            return BusinessComponent.GetFriendInformationByOpenid(appid, openid);
        }
    }
}