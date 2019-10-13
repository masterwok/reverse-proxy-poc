using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ReverseProxyPoC.Clients.Contracts;

namespace ReverseProxyPoC.Clients
{
    /// <inheritdoc />
    /// A significant portion of this code came from: https://auth0.com/blog/building-a-reverse-proxy-in-dot-net-core/
    public sealed class RestfulApiProxyClient : IHttpProxyClient
    {
        private readonly IHttpClientWrapper _httpClient;
        private readonly string _baseUrl;

        /// <summary>
        /// Create a new HttpProxyClient instance.
        /// </summary>
        /// <param name="httpClient">An IHttpClientWrapper instance.</param>
        /// <param name="baseUrl">The base URL of the host to proxy requests to.</param>
        public RestfulApiProxyClient(
            IHttpClientWrapper httpClient
            , string baseUrl
        )
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }

        /// <summary>
        /// Proxy the incoming request to the remote host. This method will mutate the HttpContext response property by
        /// copying the content body, headers, and status code from the proxied HTTP request.
        /// </summary>
        /// <param name="httpContext">The HttpContext of the incoming request.</param>
        /// <param name="template">The template (relative route).</param>
        /// <returns>An asynchronous Task instance.</returns>
        public async Task Proxy(HttpContext httpContext, string template)
        {
            var targetUri = new Uri($"{_baseUrl}{template}");
            var targetRequestMessage = CreateTargetMessage(httpContext, targetUri);

            var proxiedResponse = await _httpClient.SendAsync(
                targetRequestMessage
                , HttpCompletionOption.ResponseHeadersRead
                , httpContext.RequestAborted
            );

            httpContext.Response.StatusCode = (int) proxiedResponse.StatusCode;

            CopyFromTargetResponseHeaders(httpContext, proxiedResponse);

            await proxiedResponse
                .Content
                .CopyToAsync(httpContext.Response.Body);
        }

        private static HttpRequestMessage CreateTargetMessage(HttpContext httpContext, Uri targetUri)
        {
            var requestMessage = new HttpRequestMessage();

            CopyFromOriginalRequestContentAndHeaders(httpContext, requestMessage);

            requestMessage.RequestUri = targetUri;
            requestMessage.Headers.Host = targetUri.Host;
            requestMessage.Method = GetHttpMethod(httpContext.Request.Method);

            return requestMessage;
        }

        private static void CopyFromOriginalRequestContentAndHeaders(
            HttpContext context
            , HttpRequestMessage requestMessage
        )
        {
            var requestMethod = context.Request.Method;

            if (!HttpMethods.IsGet(requestMethod) &&
                !HttpMethods.IsHead(requestMethod) &&
                !HttpMethods.IsDelete(requestMethod) &&
                !HttpMethods.IsTrace(requestMethod))
            {
                var streamContent = new StreamContent(context.Request.Body);
                requestMessage.Content = streamContent;
            }

            foreach (var (key, value) in context.Request.Headers)
            {
                requestMessage.Content?.Headers.TryAddWithoutValidation(key, value.ToArray());
            }
        }

        private static HttpMethod GetHttpMethod(string method)
        {
            if (HttpMethods.IsDelete(method)) return HttpMethod.Delete;
            if (HttpMethods.IsGet(method)) return HttpMethod.Get;
            if (HttpMethods.IsHead(method)) return HttpMethod.Head;
            if (HttpMethods.IsOptions(method)) return HttpMethod.Options;
            if (HttpMethods.IsPost(method)) return HttpMethod.Post;
            if (HttpMethods.IsPut(method)) return HttpMethod.Put;
            if (HttpMethods.IsTrace(method)) return HttpMethod.Trace;

            return new HttpMethod(method);
        }

        private static void CopyFromTargetResponseHeaders(HttpContext context, HttpResponseMessage responseMessage)
        {
            foreach (var (key, value) in responseMessage.Headers)
            {
                context.Response.Headers[key] = value.ToArray();
            }

            foreach (var (key, value) in responseMessage.Content.Headers)
            {
                context.Response.Headers[key] = value.ToArray();
            }

            context.Response.Headers.Remove("transfer-encoding");
        }
    }
}