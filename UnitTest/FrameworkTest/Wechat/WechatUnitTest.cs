using System;
using System.Configuration;
using System.Threading;
using Cedar.Core.IoC;
using Cedar.Foundation.WeChat.DataAccess;
using Cedar.Foundation.WeChat.Entities.WeChat;
using Cedar.Foundation.WeChat.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.CommonAPIs;

namespace FrameworkTest.Wechat
{
    [TestClass]
    public class WechatUnitTest
    {
        private const string wechatkey = "wechatkey";
        private static readonly string APPID = ConfigurationManager.AppSettings["APPID"];
        private static readonly string AppSecret = ConfigurationManager.AppSettings["AppSecret"];
        private readonly WeChatDA da;

        public WechatUnitTest()
        {
            da = ServiceLocatorFactory.GetServiceLocator().GetService<WeChatDA>();
            AccessTokenRedisContainer.Register(APPID, AppSecret);
        }

        [TestMethod]
        public void CheckRegisteredTestMethod()
        {
            var result = AccessTokenRedisContainer.CheckRegistered(APPID);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetAccessTokenTestMethod()
        {
            var result = AccessTokenRedisContainer.GetAccessToken(APPID);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetJsApiTicketResultTestMethod()
        {
            var result = AccessTokenRedisContainer.GetJsApiTicketResult(APPID);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TryGetAccessTokenTestMethod()
        {
            var originalresult = AccessTokenRedisContainer.GetAccessToken(APPID);
            var result = AccessTokenRedisContainer.TryGetAccessToken(APPID, AppSecret, true);
            Assert.AreNotEqual(originalresult, result);
        }

        [TestMethod]
        public void TryGetJsApiTicketTestMethod()
        {
            var originalresult = AccessTokenRedisContainer.GetJsApiTicket(APPID);
            var result = AccessTokenRedisContainer.TryGetJsApiTicket(APPID, AppSecret, true);
            Assert.AreEqual(originalresult, result);
        }

        [TestMethod]
        public void IsWechatFriendExistsTestMethod_DA_False()
        {
            var result = da.IsWechatFriendExists(APPID, "openid111");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreaeWechatFriendTestMethod_DA()
        {
            Thread.Sleep(new TimeSpan(0, 0, 1));
            var name = DateTime.Now.ToString("yyyyMMddHHmmss");
            var friend = new FriendModel
            {
                Innerid = Guid.NewGuid().ToString(),
                Accountid = APPID,
                Nickname = name,
                Photo = "Photo",
                OPENID = name,
                Country = "Country",
                Province = "province",
                City = "city",
                Sex = 1,
                Isdel = 0,
                SubscribeTime = DateTime.Now.Ticks,
                Subscribe = 1,
                Createdtime = DateTime.Now
            };
            var result = da.CreaeWechatFriend(friend);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsWechatFriendExistsTestMethod_DA_True()
        {
            Thread.Sleep(new TimeSpan(0, 0, 1));
            var openid = DateTime.Now.ToString("yyyyMMddHHmmss");
            var friend = new FriendModel
            {
                Innerid = Guid.NewGuid().ToString(),
                Accountid = APPID,
                Nickname = "Nickname",
                Photo = "Photo",
                OPENID = openid,
                Country = "Country",
                Province = "province",
                City = "city",
                Sex = 1,
                Isdel = 0,
                SubscribeTime = DateTime.Now.Ticks,
                Subscribe = 1,
                Createdtime = DateTime.Now
            };
            var creaeWechatFriendresult = da.CreaeWechatFriend(friend);
            var isWechatFriendExistsresult = da.IsWechatFriendExists(APPID, openid);
            Assert.IsTrue(creaeWechatFriendresult);
            Assert.IsTrue(isWechatFriendExistsresult);
        }

        [TestMethod]
        public void UpdateWechatFriendTestMethod_DA_True()
        {
            Thread.Sleep(new TimeSpan(0, 0, 1));
            var openid = DateTime.Now.ToString("yyyyMMddHHmmss");
            var friend = new FriendModel
            {
                Innerid = Guid.NewGuid().ToString(),
                Accountid = APPID,
                Nickname = "Nickname",
                Photo = "Photo",
                OPENID = openid,
                Country = "Country",
                Province = "province",
                City = "city",
                Sex = 1,
                Isdel = 0,
                SubscribeTime = DateTime.Now.Ticks,
                Subscribe = 1,
                Createdtime = DateTime.Now
            };
            var friend_updated = new FriendModel
            {
                Innerid = Guid.NewGuid().ToString(),
                Accountid = APPID,
                Nickname = "Nickname1",
                Photo = "Photo1",
                OPENID = openid,
                Country = "Country1",
                Province = "province1",
                City = "city1",
                Sex = 0,
                Isdel = 1,
                SubscribeTime = DateTime.Now.Ticks,
                Subscribe = 0,
                Createdtime = DateTime.Now
            };
            var creaeWechatFriendresult = da.CreaeWechatFriend(friend);
            var isWechatFriendExistsresult = da.IsWechatFriendExists(APPID, openid);
            var updateWechatFriendresult = da.UpdateWechatFriend(friend_updated);
            Assert.IsTrue(creaeWechatFriendresult);
            Assert.IsTrue(isWechatFriendExistsresult);
            Assert.IsTrue(updateWechatFriendresult);
        }

        [TestMethod]
        public void UpdateWechatFriendTestMethod_DA_False()
        {
            Thread.Sleep(new TimeSpan(0, 0, 1));
            var openid = DateTime.Now.ToString("yyyyMMddHHmmss");
            var friend = new FriendModel
            {
                Innerid = Guid.NewGuid().ToString(),
                Accountid = APPID,
                Nickname = "Nickname",
                Photo = "Photo",
                OPENID = openid,
                Country = "Country",
                Province = "province",
                City = "city",
                Sex = 1,
                Isdel = 0,
                SubscribeTime = DateTime.Now.Ticks,
                Subscribe = 1,
                Createdtime = DateTime.Now
            };
            var updateWechatFriendresult = da.UpdateWechatFriend(friend);
            Assert.IsFalse(updateWechatFriendresult);
        }

        [TestMethod]
        public void GenerateWechatFriendByOpenidTestMethod()
        {
            var openid = "oRpPlwRd_ftOsoXTyaHzoa3yWHvo"; //阿布天
            var service = ServiceLocatorFactory.GetServiceLocator().GetService<IWeChatManagementService>();
            var result = service.GenerateWechatFriend(APPID, openid);
            var isWechatFriendExistsresult = da.IsWechatFriendExists(APPID, openid);
            Assert.AreEqual(result.errcode, 0);
            Assert.IsTrue(isWechatFriendExistsresult);
        }
    }
}