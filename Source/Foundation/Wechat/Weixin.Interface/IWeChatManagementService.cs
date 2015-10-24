#region

using Cedar.Framework.Common.BaseClasses;

#endregion

namespace Cedar.Foundation.WeChat.Interface
{
    /// <summary>
    /// </summary>
    public interface IWeChatManagementService
    {
        /// <summary>
        ///     更新或重新获取所有会员信息
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        JResult GenerateWechatFriends(string appid);

        /// <summary>
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="openid"></param>
        /// <param name="isUpdate"></param>
        /// <returns></returns>
        JResult GenerateWechatFriend(string appid, string openid, bool isUpdate = false);

        /// <summary>
        ///     根据openid获取微信粉丝信息
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        JResult GetFriendInformationByOpenid(string appid, string openid);
    }
}