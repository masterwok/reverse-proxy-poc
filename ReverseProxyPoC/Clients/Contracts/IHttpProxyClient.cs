using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ReverseProxyPoC.Clients.Contracts
{
    /// <summary>
    /// The IHttpProxyClient contract provides a method for proxying HTTP requests.
    /// </summary>
    public interface IHttpProxyClient
    {
        /// <summary>
        /// Asynchronously proxy an HTTP request.
        /// </summary>
        /// <param name="httpContext">The HttpContext of the incoming request.</param>
        /// <param name="template">The template (relative route).</param>
        /// <returns>An asynchronous Task instance.</returns>
        Task Proxy(HttpContext httpContext, string template);
    }
}