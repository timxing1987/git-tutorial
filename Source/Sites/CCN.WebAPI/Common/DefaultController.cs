#region

using CCN.WebAPI.Properties;
using Cedar.Framework.Common.Client.MVCExtention;

#endregion

namespace CCN.WebAPI.Main.Common
{
    public class DefaultController : ExtendedController
    {
        public DefaultController()
            : base(Resources.DefaultLocalServiceName)
        {
        }
    }
}