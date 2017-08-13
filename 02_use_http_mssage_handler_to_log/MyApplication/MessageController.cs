using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyApplication
{
    public class MessageController : ApiController
    {
        readonly MyService myService;

        public MessageController(MyService myService)
        {
            this.myService = myService;
        }

        [HttpGet]
        public HttpResponseMessage GetMessageInfo()
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(myService.GetMessage())
            };
        }

        [HttpGet]
        public HttpResponseMessage GetId()
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(myService.GetId())
            };
        }
    }
}