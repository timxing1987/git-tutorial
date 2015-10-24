using Cedar.AuditTrail.Interception;

namespace FrameworkTest
{
    public class DemoInstanceService
    {
        [AuditTrailCallHandler("test")]
        public string TestMethod(string input)
        {
            return input;
        }
    }
}