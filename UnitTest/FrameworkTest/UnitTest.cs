using System.Linq;
using Cedar.Core.EntLib.Data;
using Cedar.Foundation.SMS.Common;
using Cedar.Framework.Common.BaseClasses;
using Cedar.Framework.Common.Server.BaseClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrameworkTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod()
        {
            var factoy = new DatabaseWrapperFactory().GetDatabase("mysqldb");
            var d = factoy.Query("select * from base_carbrand").ToList();
            Assert.IsNotNull(factoy);
        }

        [TestMethod]
        public void TestXxMethod()
        {
            var helper = new MySqlDbHelper("mysqldb");

            var query = new AAQueryModel();
            query.PageIndex = 1;
            query.PageSize = 5;
            query.Group = "";

            const string spName = "sp_common_pager";
            const string tableName = @"car_info";
            const string fields = " * ";
            var orderField = string.IsNullOrWhiteSpace(query.Order) ? "createdtime desc" : query.Order;
            //查询条件 
            var sqlWhere = "1=1";
            var model = new PagingModel(spName, tableName, fields, orderField, sqlWhere, query.PageSize, query.PageIndex);
            var list2 = helper.ExecutePaging<dynamic>(model, query.Echo);

            Assert.IsNotNull(list2);
        }

        [TestMethod]
        public void TestMethod_SMS()
        {
            var sms = new SMSMSG();
            var result = sms.SendSms("15862409166", "测试发送");
            Assert.IsNotNull(null);
        }
    }

    public class AAQueryModel : QueryModel
    {
    }
}