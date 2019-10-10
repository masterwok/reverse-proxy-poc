using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ReverseProxyPoC.Middleware
{
    public sealed class ProxyMiddleware
    {
        private readonly RequestDelegate _next;

        public ProxyMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context) => await _next(context);
    }
}