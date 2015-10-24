#region

using Cedar.Foundation.WeChat.Entities.WeChat;

#endregion

namespace Cedar.Foundation.WeChat.DataAccess
{
    /// <summary>
    /// </summary>
    public class WeChatDA : WeChatDataAccessBase
    {
        /// <summary>
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        public bool IsWechatFriendExists(string appid, string openid)
        {
            const string sql = "select 1 from wechat_friend where accountid=@appid and openid=@openid;";
            var result = Helper.ExecuteScalar<int>(sql, new {appid, openid});
            return result == 1;
        }

        /// <summary>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CreaeWechatFriend(FriendModel model)
        {
            var strSql = (@"INSERT INTO wechat_friend
                  (`innerid`,`accountid`,`nickname`,`photo`,`openid`,`remarkname`,`area`,`sex`,`isdel`,`subscribe_time`,`subscribe`,country,province,city,`createdtime`)
                  VALUES
                  (@innerid,@accountid,@nickname,@photo,@openid,@remarkname,@area,@sex,@isdel,@subscribetime,@subscribe,@country,@province,@city,@createdtime);");

            return Helper.Execute(strSql, model) > 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateWechatFriend(FriendModel model)
        {
            var strSql = @"UPDATE wechat_friend
                                        SET `nickname` = @nickname, `photo` = @photo, `remarkname` = @remarkname,`area` = @area,
                                         `sex` = @sex, `isdel` = @isdel, `subscribe_time` = @subscribetime,
                                          country=@country,province=@province,city=@city,
                                         `subscribe` = @subscribe
                                          WHERE `openid` = @openid and `accountid` = @accountid ";

            return Helper.Execute(strSql, model) > 0;
        }
    }
}