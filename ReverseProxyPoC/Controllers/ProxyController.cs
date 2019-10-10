using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReverseProxyPoC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class ProxyController : ControllerBase
    {
        [HttpGet]
        [Route("Foo")]
        public ActionResult<HttpResponse> Foo()
        {
            return Ok(new
            {
                foo = "bar"
            });
        }
    }
}