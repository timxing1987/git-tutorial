using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using Owin;
using Senparc.Weixin.MP.CommonAPIs;

namespace CCN.Midware.Wechat
{
    class Program
    {
        static void Main(string[] args)
        {
            string _appid = ConfigurationManager.AppSettings["APPID"];
            string _appSecret = ConfigurationManager.AppSettings["AppSecret"];
            string host = $"http://{ConfigurationManager.AppSettings["hostip"]}";
            using (WebApp.Start<Startup>(host))
            {
                Console.WriteLine($"Service start and linsent on {host}...");
                if (!AccessTokenRedisContainer.CheckRegistered(_appid))
                    AccessTokenRedisContainer.Register(_appid, _appSecret);
                Console.ReadLine();
            }
        }
    }
}
