using System.Web.Http;

namespace CCN.Midware.Wechat.Controllers
{
    [RoutePrefix("api/default")]
    public class DefaultController : ApiController
    {
        [Route("index")]
        [HttpGet]
        public string Index(string id)
        {
            return "Hello";
        }
    }
}
