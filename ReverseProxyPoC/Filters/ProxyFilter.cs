using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using ReverseProxyPoC.Attributes;
using ReverseProxyPoC.Clients.Contracts;

namespace ReverseProxyPoC.Filters
{
    /// <inheritdoc />
    /// <summary>
    /// This action filter is responsible for proxying HTTP requests received by controller actions decorated with the
    /// ProxyAttribute.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class ProxyFilter : IAsyncActionFilter
    {
        private readonly IHttpProxyClient _httpProxyClient;

        /// <summary>
        /// Create a new ProxyFilter instance.
        /// </summary>
        /// <param name="httpProxyClient">An IHttpProxyClient instance used to proxy requests.</param>
        public ProxyFilter(IHttpProxyClient httpProxyClient) => _httpProxyClient = httpProxyClient;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            // Get the ProxyAttribute from the controller action.
            var proxyRouteAttribute = controllerActionDescriptor
                ?.MethodInfo
                ?.GetCustomAttributes(typeof(ProxyRoute), true)
                // ReSharper disable once UseNegatedPatternMatching
                .FirstOrDefault() as ProxyRoute;

            // No ProxyAttribute defined on this method, ignore.
            if (proxyRouteAttribute == null)
            {
                await next();

                return;
            }

            var httpContext = context.HttpContext;

            // ProxyAttribute found, proxy the request.
            await _httpProxyClient.Proxy(
                httpContext
                , AddValuesToTemplate(
                    httpContext
                    , proxyRouteAttribute.Template
                )
            );
        }

        /// <summary>
        /// Replace the parameters within a raw template string with those provided in the HttpContext route values.
        /// </summary>
        /// <param name="httpContext">The HttpContext of the request.</param>
        /// <param name="rawTemplate">The raw parameterized template.</param>
        /// <returns>The template with parameter values.</returns>
        private static string AddValuesToTemplate(HttpContext httpContext, string rawTemplate)
        {
            var parsedTemplate = TemplateParser.Parse(rawTemplate);

            foreach (var parameter in parsedTemplate.Parameters)
            {
                var value = httpContext.GetRouteValue(parameter.Name);

                rawTemplate = rawTemplate.Replace(
                    $"{{{parameter.Name}}}"
                    , value.ToString()
                );
            }

            return rawTemplate;
        }
    }
}