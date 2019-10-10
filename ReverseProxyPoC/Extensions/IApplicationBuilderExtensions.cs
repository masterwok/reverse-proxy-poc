using Microsoft.AspNetCore.Builder;
using ReverseProxyPoC.Middleware;

namespace ReverseProxyPoC.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseProxyMiddleware(
            this IApplicationBuilder builder
        ) => builder.UseMiddleware<ProxyMiddleware>();
    }
}