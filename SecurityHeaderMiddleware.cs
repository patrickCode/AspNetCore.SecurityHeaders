using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace AspNetCore.SecurityHeader
{
    /// <summary>
    /// Middleware for adding security headers and remove insecure headers
    /// </summary>
    public class SecurityHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SecurityHeaderPolicy _policy;

        /// <summary>
        /// Constructs an instance of <see cref="SecurityHeaderMiddleware"/>
        /// </summary>
        /// <param name="next" cref="RequestDelegate">Next middleware in the pipeline</param>
        /// <param name="policy" cref="SecurityHeaderPolicy">Policy for adding secure headers and removing insecure headers</param>
        public SecurityHeaderMiddleware(RequestDelegate next, SecurityHeaderPolicy policy)
        {
            _next = next;
            _policy = policy;
        }

        public Task Invoke(HttpContext context)
        {
            AddHeaders(context);
            RemoveHeaders(context);
            return _next.Invoke(context);
        }

        private void AddHeaders(HttpContext context)
        {
            if (_policy == null || _policy.AddedHeaders == null || !_policy.AddedHeaders.Any())
                return;

            foreach (var header in _policy.AddedHeaders)
            {
                if (!context.Response.Headers.ContainsKey(header.Key))
                {
                    context.Response.Headers.Add(header.Key, new StringValues(header.Value.ToArray()));
                }
            }
        }

        private void RemoveHeaders(HttpContext context)
        {
            if (_policy == null || _policy.RemovedHeaders == null || !_policy.RemovedHeaders.Any())
                return;

            foreach (var header in _policy.RemovedHeaders)
            {
                if (context.Response.Headers.ContainsKey(header))
                    context.Response.Headers.Remove(header);
            }
        }
    }
}
