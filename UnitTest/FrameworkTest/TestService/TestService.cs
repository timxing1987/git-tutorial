using Cedar.AuditTrail.Interception;
using Cedar.Framwork.Caching.Interception;

namespace FrameworkTest.TestService
{
    public class TestService : ITestService
    {
        [AuditTrailCallHandler("SayHello")]
        public string SayHello(dynamic words)
        {
            return "SayHelloResults";
        }

        [CachingCallHandler]
        public ReturnModel SayHelloCaching(int id, dynamic words)
        {
            return new ReturnModel {code = 7001, content = "SayHelloResults"};
        }
    }
}