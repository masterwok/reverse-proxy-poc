using System.Linq.Expressions;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using ReverseProxyPoC.Attributes;

namespace ReverseProxyPoC.Controllers
{
    /// <summary>
    /// This controller is a demo of the PoC proxy. Note the ProxyRoute attribute which derives from the standard Route
    /// attribute. It has two responsibilities: the first is to define the route of the action relative to the
    /// controller route. The second, is to define the relative route of the proxied API. This attribute is picked up by
    /// the ProxyFilter before each endpoint is invoked. Because of this, proxied methods can have no return value as
    /// the ProxyFilter intercepts the request and returns the proxied result on behalf of the action.
    ///
    /// To migrate a route to an implementation local to this API (i.e. don't proxy the route), we can simply replace
    /// the ProxyRoute attribute with the standard Route attribute and implement the action.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class DemoController : ControllerBase
    {
        [HttpGet]
        [ProxyRoute("posts")]
        public void GetPosts() => Expression.Empty();

        [HttpGet]
        [ProxyRoute("posts/{id}")]
        public void GetPost(int id) => Expression.Empty();

        [HttpPost]
        [ProxyRoute("posts")]
        public void CreatePost() => Expression.Empty();

        [HttpPut]
        [ProxyRoute("posts/{id}")]
        public void UpdatePost() => Expression.Empty();

        [HttpDelete]
        [ProxyRoute("posts/{id}")]
        public void DeletePost() => Expression.Empty();
    }
}