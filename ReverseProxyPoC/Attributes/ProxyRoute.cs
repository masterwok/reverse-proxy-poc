using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

namespace ReverseProxyPoC.Attributes
{
    /// <summary>
    /// This controller action attribute can be used in tandem with a ProxyFilter to proxy requests
    /// to an external endpoint.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ProxyRoute : RouteAttribute
    {
        /// <summary>
        /// Create a new ProxyRoute instance.
        /// </summary>
        /// <param name="template">The relative route to proxy (e.g. posts/{id}).</param>
        public ProxyRoute(string template) : base(template) => Expression.Empty();
    }
}