using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using ReverseProxyPoC.Clients.Contracts;

namespace ReverseProxyPoC.Clients
{
    public sealed class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper() => _httpClient = new HttpClient();

        public HttpClientWrapper(
            HttpMessageHandler httpMessageHandler
        ) => _httpClient = new HttpClient(httpMessageHandler);

        public HttpClientWrapper(
            HttpMessageHandler httpMessageHandler
            , bool disposeHandler
        ) => _httpClient = new HttpClient(httpMessageHandler, disposeHandler);

        public void Dispose() => _httpClient.Dispose();

        public void CancelPendingRequests() => _httpClient.CancelPendingRequests();

        public Task<HttpResponseMessage> DeleteAsync(string requestUri) => _httpClient.DeleteAsync(requestUri);

        public Task<HttpResponseMessage> DeleteAsync(string requestUri, CancellationToken cancellationToken) =>
            _httpClient.DeleteAsync(requestUri, cancellationToken);

        public Task<HttpResponseMessage> DeleteAsync(Uri requestUri) => _httpClient.DeleteAsync(requestUri);

        public Task<HttpResponseMessage> DeleteAsync(Uri requestUri, CancellationToken cancellationToken) =>
            _httpClient.DeleteAsync(requestUri, cancellationToken);

        public Task<HttpResponseMessage> GetAsync(string requestUri) => _httpClient.GetAsync(requestUri);

        public Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption) =>
            _httpClient.GetAsync(requestUri, completionOption);

        public Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption,
            CancellationToken cancellationToken) =>
            _httpClient.GetAsync(requestUri, completionOption, cancellationToken);

        public Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken) =>
            _httpClient.GetAsync(requestUri, cancellationToken);

        public Task<HttpResponseMessage> GetAsync(Uri requestUri) => _httpClient.GetAsync(requestUri);

        public Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption) =>
            _httpClient.GetAsync(requestUri, completionOption);

        public Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption,
            CancellationToken cancellationToken) =>
            _httpClient.GetAsync(requestUri, completionOption, cancellationToken);

        public Task<HttpResponseMessage> GetAsync(Uri requestUri, CancellationToken cancellationToken) =>
            _httpClient.GetAsync(requestUri, cancellationToken);

        public Task<byte[]> GetByteArrayAsync(string requestUri) => _httpClient.GetByteArrayAsync(requestUri);

        public Task<byte[]> GetByteArrayAsync(Uri requestUri) => _httpClient.GetByteArrayAsync(requestUri);

        public Task<Stream> GetStreamAsync(string requestUri) => _httpClient.GetStreamAsync(requestUri);

        public Task<Stream> GetStreamAsync(Uri requestUri) => _httpClient.GetStreamAsync(requestUri);

        public Task<string> GetStringAsync(string requestUri) => _httpClient.GetStringAsync(requestUri);

        public Task<string> GetStringAsync(Uri requestUri) => _httpClient.GetStringAsync(requestUri);

        public Task<HttpResponseMessage> PatchAsync(string requestUri, HttpContent content) =>
            _httpClient.PatchAsync(requestUri, content);

        public Task<HttpResponseMessage> PatchAsync(string requestUri, HttpContent content,
            CancellationToken cancellationToken) =>
            _httpClient.PatchAsync(requestUri, content, cancellationToken);

        public Task<HttpResponseMessage> PatchAsync(Uri requestUri, HttpContent content) =>
            _httpClient.PatchAsync(requestUri, content);

        public Task<HttpResponseMessage> PatchAsync(Uri requestUri, HttpContent content,
            CancellationToken cancellationToken) =>
            _httpClient.PatchAsync(requestUri, content, cancellationToken);

        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content) =>
            _httpClient.PostAsync(requestUri, content);

        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content,
            CancellationToken cancellationToken) =>
            _httpClient.PostAsync(requestUri, content, cancellationToken);

        public Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content) =>
            _httpClient.PostAsync(requestUri, content);

        public Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content,
            CancellationToken cancellationToken) =>
            _httpClient.PostAsync(requestUri, content, cancellationToken);

        public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content) =>
            _httpClient.PutAsync(requestUri, content);

        public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content,
            CancellationToken cancellationToken) =>
            _httpClient.PutAsync(requestUri, content, cancellationToken);

        public Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content) =>
            _httpClient.PutAsync(requestUri, content);

        public Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content,
            CancellationToken cancellationToken) =>
            _httpClient.PutAsync(requestUri, content, cancellationToken);

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) => _httpClient.SendAsync(request);

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption) =>
            _httpClient.SendAsync(request, completionOption);

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption,
            CancellationToken cancellationToken) =>
            _httpClient.SendAsync(request, completionOption, cancellationToken);

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) =>
            _httpClient.SendAsync(request, cancellationToken);

        public Uri BaseAddress
        {
            get => _httpClient.BaseAddress;
            set => _httpClient.BaseAddress = value;
        }

        public HttpRequestHeaders DefaultRequestHeaders => _httpClient.DefaultRequestHeaders;

        public Version DefaultRequestVersion
        {
            get => _httpClient.DefaultRequestVersion;
            set => _httpClient.DefaultRequestVersion = value;
        }

        public long MaxResponseContentBufferSize
        {
            get => _httpClient.MaxResponseContentBufferSize;
            set => _httpClient.MaxResponseContentBufferSize = value;
        }

        public TimeSpan Timeout
        {
            get => _httpClient.Timeout;
            set => _httpClient.Timeout = value;
        }
    }
}